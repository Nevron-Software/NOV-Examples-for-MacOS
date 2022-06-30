Imports System
Imports System.IO
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Xml

Namespace Nevron.Nov.Examples.UI
    Public Class NAutoCompleteBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NAutoCompleteBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NAutoCompleteBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.VerticalSpacing = 10

			' Load the contry data
			Me.m_Countries = Me.LoadCountryData()

			' Create the simple auto complete text box
			Me.m_TextBox = New Nevron.Nov.UI.NAutoCompleteBox()
            Me.m_TextBox.PreferredWidth = 300
            Me.m_TextBox.InitAutoComplete(Me.m_Countries)
            AddHandler Me.m_TextBox.TextChanged, AddressOf Me.OnTextBoxTextChanged
            Dim pairBox As Nevron.Nov.UI.NPairBox = Me.CreatePairBox("Enter country name:", Me.m_TextBox)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Auto complete items -> Labels", pairBox))

			' Create the advanced auto complete text box
			Me.m_AdvancedTextBox = New Nevron.Nov.UI.NAutoCompleteBox()
            Me.m_AdvancedTextBox.PreferredWidth = 300
            Me.m_AdvancedTextBox.Image = NResources.Image_ExamplesUI_Icons_Search_png
            Me.m_AdvancedTextBox.InitAutoComplete(Me.m_Countries, New Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountryFactory())
            AddHandler Me.m_AdvancedTextBox.TextChanged, AddressOf Me.OnTextBoxTextChanged
            pairBox = Me.CreatePairBox("Enter country name:", Me.m_AdvancedTextBox)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Auto complete items -> Custom widgets", pairBox))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create the property editors
			Dim enabledCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enabled", True)
            AddHandler enabledCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnabledCheckBoxCheckedChanged)
            stack.Add(enabledCheckBox)
            Me.m_CaseSensitiveCheckBox = New Nevron.Nov.UI.NCheckBox("Case Sensitive", False)
            AddHandler Me.m_CaseSensitiveCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCaseSensitiveCheckBoxCheckedChanged)
            stack.Add(Me.m_CaseSensitiveCheckBox)
            Dim stringMacthModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            stringMacthModeComboBox.FillFromEnum(Of Nevron.Nov.DataStructures.ENStringMatchMode)()
            AddHandler stringMacthModeComboBox.SelectedIndexChanged, AddressOf Me.OnStringMacthModeComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("String Match Mode:", stringMacthModeComboBox))

			' Add the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use auto complete text boxes. The auto complete text box
	is an UI element that hosts a text box and also provides an auto complete functionality. Using the 
	controls on the right you can specify whether the auto complete should be case sensitive (default)
	or not and the string match mode.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePairBox(ByVal labelText As String, ByVal textBox As Nevron.Nov.UI.NAutoCompleteBox) As Nevron.Nov.UI.NPairBox
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(labelText)
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(label, textBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2, False)
            pairBox.Spacing = 3
            Return pairBox
        End Function

        Private Function LoadCountryData() As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry)
			' Get the country list XML stream
			Dim stream As System.IO.Stream = NResources.Instance.GetResourceStream("RSTR_CountryList_xml")

			' Load an xml document from the stream
			Dim xmlDocument As Nevron.Nov.Xml.NXmlDocument = Nevron.Nov.Xml.NXmlDocument.LoadFromStream(stream)

			' Process it
			Dim rows As Nevron.Nov.Xml.NXmlNode = xmlDocument.GetChildAt(CInt((0))).GetChildAt(1)
            Dim countries As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry)()
            Dim i As Integer = 0, countryCount As Integer = rows.ChildrenCount

            While i < countryCount
                Dim row As Nevron.Nov.Xml.NXmlNode = rows.GetChildAt(i)

				' Get the country name
				Dim country As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry = New Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry(Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.GetValue(row.GetChildAt(1)))
                If System.[String].IsNullOrEmpty(country.Name) Then Continue While

				' Get the country's capital
				country.Capital = Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.GetValue(row.GetChildAt(6))
                If System.[String].IsNullOrEmpty(country.Capital) Then Continue While

				' Get the country's currency
				country.CurrencyCode = Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.GetValue(row.GetChildAt(7))
                country.CurrencyName = Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.GetValue(row.GetChildAt(8))
                If System.[String].IsNullOrEmpty(country.CurrencyCode) OrElse System.[String].IsNullOrEmpty(country.CurrencyName) Then Continue While

				' Get the country code (ISO 3166-1 2 Letter Code)
				country.Code = Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.GetValue(row.GetChildAt(10))
                If System.[String].IsNullOrEmpty(country.Code) Then Continue While

				' Get the country flag
				Dim flagResourceName As String = "RIMG_CountryFlags_" & country.Code.ToLower() & "_png"
                Dim flagResource As Nevron.Nov.NEmbeddedResource = NResources.Instance.GetResource(flagResourceName)
                If flagResource Is Nothing Then Continue While
                country.Flag = New Nevron.Nov.Graphics.NImage(New Nevron.Nov.NEmbeddedResourceRef(flagResource))

				' Add the country to the list
				countries.Add(country)
                i += 1
            End While

			' Sort the countries by name and return them
			countries.Sort()
            Return countries
        End Function

        Private Function GetCountryByName(ByVal str As String, ByVal caseSensitive As Boolean) As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry
            Dim comparison As System.StringComparison = If(caseSensitive, System.StringComparison.Ordinal, System.StringComparison.OrdinalIgnoreCase)
            Dim i As Integer = 0, count As Integer = Me.m_Countries.Count

            While i < count
                Dim country As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry = Me.m_Countries(i)
                If System.[String].Equals(str, country.Name, comparison) Then Return country
                i += 1
            End While

            Return Nothing
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnabledCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim enabled As Boolean = CBool(arg.NewValue)
            Me.m_TextBox.Enabled = enabled
            Me.m_AdvancedTextBox.Enabled = enabled
        End Sub

        Private Sub OnCaseSensitiveCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim autocompleteCaseSensitive As Boolean = CBool(arg.NewValue)
            Me.m_TextBox.CaseSensitive = autocompleteCaseSensitive
            Me.m_AdvancedTextBox.CaseSensitive = autocompleteCaseSensitive
        End Sub

        Private Sub OnStringMacthModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(arg.CurrentTargetNode, Nevron.Nov.UI.NComboBox)
            Dim stringMatchMode As Nevron.Nov.DataStructures.ENStringMatchMode = CType(comboBox.SelectedItem.Tag, Nevron.Nov.DataStructures.ENStringMatchMode)
            Me.m_TextBox.StringMatchMode = stringMatchMode
            Me.m_AdvancedTextBox.StringMatchMode = stringMatchMode
        End Sub

        Private Sub OnTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim text As String = CStr(arg.NewValue)
            Dim country As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry = Me.GetCountryByName(text, Me.m_CaseSensitiveCheckBox.Checked)

            If country IsNot Nothing Then
                Me.m_EventsLog.LogEvent("Selected country: " & country.Name)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextBox As Nevron.Nov.UI.NAutoCompleteBox
        Private m_AdvancedTextBox As Nevron.Nov.UI.NAutoCompleteBox
        Private m_CaseSensitiveCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EventsLog As NExampleEventsLog
        Private m_Countries As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry)

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAutoCompleteBoxExample.
		''' </summary>
		Public Shared ReadOnly NAutoCompleteBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetValue(ByVal node As Nevron.Nov.Xml.NXmlNode) As String
            If node.ChildrenCount <> 1 Then Return Nothing
            Dim textNode As Nevron.Nov.Xml.NXmlTextNode = TryCast(node.GetChildAt(0), Nevron.Nov.Xml.NXmlTextNode)
            Return If(textNode IsNot Nothing, textNode.Text, Nothing)
        End Function

		#EndRegion

		#Region"Nested Types"

		Private Class NCountry
            Implements System.IComparable(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry), Nevron.Nov.INDeeplyCloneable

            Public Sub New(ByVal name As String)
                Me.Name = name
                Me.Code = Nothing
                Me.CurrencyCode = Nothing
                Me.CurrencyName = Nothing
                Me.Capital = Nothing
                Me.Flag = Nothing
            End Sub

            Public Overrides Function ToString() As String
                Return Me.Name
            End Function

            Public Function CompareTo(ByVal other As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry) As Integer Implements Global.System.IComparable(Of Global.Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry).CompareTo
                Return Me.Name.CompareTo(other.Name)
            End Function
			''' <summary>
			''' Creates an identical copy of this object.
			''' </summary>
			''' <returns>A copy of this instance.</returns>
			Public Function DeepClone() As Object Implements Global.Nevron.Nov.INDeeplyCloneable.DeepClone
                Dim country As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry = New Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry(Me.Name)
                Return country
            End Function

            Public Name As String
            Public Code As String
            Public CurrencyCode As String
            Public CurrencyName As String
            Public Capital As String
            Public Flag As Nevron.Nov.Graphics.NImage
        End Class

        Private Class NCountryFactory
            Inherits Nevron.Nov.UI.NWidgetFactory(Of Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry)

            Public Overrides Function CreateWidget(ByVal country As Nevron.Nov.Examples.UI.NAutoCompleteBoxExample.NCountry) As Nevron.Nov.UI.NWidget
				' Create a dock panel
				Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                stack.Padding = New Nevron.Nov.Graphics.NMargins(3)
                stack.Tag = country

				' Create the flag image box and the country name label
				Dim countryLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(country.Name)
                countryLabel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                countryLabel.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Bold)
                Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(CType(Nevron.Nov.NSystem.SafeDeepClone(country.Flag), Nevron.Nov.Graphics.NImage))
                imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
                Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(imageBox, countryLabel)
                pairBox.Spacing = 3
                stack.Add(pairBox)

				' Create the capital label
				Dim capitalLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Capital: " & country.Capital)
                stack.Add(capitalLabel)

				' Create the currency label
				Dim currencyLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Currency: " & country.CurrencyName & ", " & country.CurrencyCode)
                stack.Add(currencyLabel)
                Return stack
            End Function
        End Class

		#EndRegion
	End Class
End Namespace
