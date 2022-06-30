Imports System
Imports System.IO
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Xml

Namespace Nevron.Nov.Examples.UI
    Public Class NComboBoxDataBindingExample
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
            Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NComboBoxDataBindingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NComboBoxDataBindingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.VerticalSpacing = 10

			' Load the contry data
			Dim countries As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry) = Me.LoadCountryData()

			' Create the simple combo box
			Me.m_ComboBox = New Nevron.Nov.UI.NComboBox()
            Dim pairBox As Nevron.Nov.UI.NPairBox = Me.CreatePairBox("Select country:", Me.m_ComboBox)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Data source items -> Labels", pairBox))

			' Create the simple data binding
			Dim countryNameDataBinding As Nevron.Nov.Dom.NNodeCollectionDataBinding(Of Nevron.Nov.UI.NComboBoxItemCollection, Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry) = New Nevron.Nov.Dom.NNodeCollectionDataBinding(Of Nevron.Nov.UI.NComboBoxItemCollection, Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry)()
            Call Nevron.Nov.Dom.NDataBinding.SetDataBinding(Me.m_ComboBox.Items, countryNameDataBinding)
            countryNameDataBinding.DataSource = countries
            AddHandler countryNameDataBinding.CreateItemNode, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NCreateItemNodeEventArgs(Of Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry))(AddressOf Me.OnCountryNameDataBindingCreateItemNode)
            countryNameDataBinding.RebuildTarget()

			' Create the advanced combo box
			Me.m_AdvancedComboBox = New Nevron.Nov.UI.NComboBox()
            pairBox = Me.CreatePairBox("Select country:", Me.m_AdvancedComboBox)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Data source items -> Custom widgets", pairBox))

			' Create the advanced data binding
			Dim countryDataBinding As Nevron.Nov.Dom.NNodeCollectionDataBinding(Of Nevron.Nov.UI.NComboBoxItemCollection, Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry) = New Nevron.Nov.Dom.NNodeCollectionDataBinding(Of Nevron.Nov.UI.NComboBoxItemCollection, Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry)()
            Call Nevron.Nov.Dom.NDataBinding.SetDataBinding(Me.m_AdvancedComboBox.Items, countryDataBinding)
            countryDataBinding.DataSource = countries
            AddHandler countryDataBinding.CreateItemNode, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NCreateItemNodeEventArgs(Of Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry))(AddressOf Me.OnCountryDataBindingCreateItemNode)
            countryDataBinding.RebuildTarget()

			' Select the first country
			Me.m_ComboBox.SelectedIndex = 0
            Me.m_AdvancedComboBox.SelectedIndex = 0

			' Subscribe to the selected index changed events
			AddHandler Me.m_ComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnComboBoxSelectedIndexChanged)
            AddHandler Me.m_AdvancedComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnComboBoxSelectedIndexChanged)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last

			' Create the property editors
			Dim enabledCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enabled", True)
            AddHandler enabledCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnabledCheckBoxCheckedChanged)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", enabledCheckBox))

			' Add an event log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a combo box and bind data to it. This is done by creating a <b>NNodeCollectionDataBinding</b>
	and then attaching it to the combo box's collection of items. When the data binding needs a new combo box item to be created, it
	calls the <b>CreateItemNode event</b> where you should create the combo box item using the <b>Item</b> property of the event argument
	and then you should assign the newly created combo box item to the event argument's <b>Node</b> property.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePairBox(ByVal labelText As String, ByVal comboBox As Nevron.Nov.UI.NComboBox) As Nevron.Nov.UI.NPairBox
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(labelText)
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(label, comboBox, Nevron.Nov.UI.ENPairBoxRelation.Box1AboveBox2, False)
            pairBox.Spacing = 3
            Return pairBox
        End Function

        Private Function LoadCountryData() As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry)
			' Get the country list XML stream
			Dim stream As System.IO.Stream = NResources.Instance.GetResourceStream("RSTR_CountryList_xml")

			' Load an xml document from the stream
			Dim xmlDocument As Nevron.Nov.Xml.NXmlDocument = Nevron.Nov.Xml.NXmlDocument.LoadFromStream(stream)

			' Process it
			Dim rows As Nevron.Nov.Xml.NXmlNode = xmlDocument.GetChildAt(CInt((0))).GetChildAt(1)
            Dim countries As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry)()
            Dim i As Integer = 0, countryCount As Integer = rows.ChildrenCount

            While i < countryCount
                Dim row As Nevron.Nov.Xml.NXmlNode = rows.GetChildAt(i)

				' Get the country name
				Dim country As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry = New Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry(Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.GetValue(row.GetChildAt(1)))
                If System.[String].IsNullOrEmpty(country.Name) Then Continue While

				' Get the country's capital
				country.Capital = Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.GetValue(row.GetChildAt(6))
                If System.[String].IsNullOrEmpty(country.Capital) Then Continue While

				' Get the country's currency
				country.CurrencyCode = Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.GetValue(row.GetChildAt(7))
                country.CurrencyName = Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.GetValue(row.GetChildAt(8))
                If System.[String].IsNullOrEmpty(country.CurrencyCode) OrElse System.[String].IsNullOrEmpty(country.CurrencyName) Then Continue While

				' Get the country code (ISO 3166-1 2 Letter Code)
				country.Code = Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.GetValue(row.GetChildAt(10))
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

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnabledCheckBoxCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim enabled As Boolean = CBool(args.NewValue)
            Me.m_ComboBox.Enabled = enabled
            Me.m_AdvancedComboBox.Enabled = enabled
        End Sub

        Private Sub OnComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_bSelectionUpdating Then
                Me.m_bSelectionUpdating = False
                Return
            End If

			' Get the list box and the other list box
			Dim selectedIndex As Integer = CInt(args.NewValue)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim otherComboBox As Nevron.Nov.UI.NComboBox = If(comboBox Is Me.m_ComboBox, Me.m_AdvancedComboBox, Me.m_ComboBox)

			' Log the selection
			Dim selectedItem As Nevron.Nov.UI.NComboBoxItem = comboBox.SelectedItem
            Dim country As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry = Nevron.Nov.Dom.NNodeCollectionDataBinding(Of Nevron.Nov.UI.NComboBoxItemCollection, Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry).GetDataBoundItem(selectedItem)
            Me.m_EventsLog.LogEvent("'" & country.Name & "' selected")

			' Synchronize the selection between the two list boxes
			Me.m_bSelectionUpdating = True
            otherComboBox.SelectedIndex = selectedIndex
        End Sub

        Private Sub OnCountryNameDataBindingCreateItemNode(ByVal args As Nevron.Nov.Dom.NCreateItemNodeEventArgs(Of Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry))
			' Create a combo box item for the current country
			Dim country As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry = args.Item
            args.Node = New Nevron.Nov.UI.NComboBoxItem(country.Name)
        End Sub

        Private Sub OnCountryDataBindingCreateItemNode(ByVal args As Nevron.Nov.Dom.NCreateItemNodeEventArgs(Of Nevron.Nov.UI.NComboBoxItem, Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry))
            Dim country As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry = args.Item

			' Create a stack panel
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Padding = New Nevron.Nov.Graphics.NMargins(3)
            stack.Tag = country

			' Create the flag image box and the country name label
			Dim countryLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(country.Name)
            countryLabel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            countryLabel.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Bold)
            Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(country.Flag)
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
			
			' Create a combo box item to host the created widget
			Dim comboBoxItem As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(stack)
            comboBoxItem.Text = country.Name
            args.Node = comboBoxItem
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_bSelectionUpdating As Boolean = False
        Private m_ComboBox As Nevron.Nov.UI.NComboBox
        Private m_AdvancedComboBox As Nevron.Nov.UI.NComboBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NComboBoxDataBindingExample.
		''' </summary>
		Public Shared ReadOnly NComboBoxDataBindingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetValue(ByVal node As Nevron.Nov.Xml.NXmlNode) As String
            If node.ChildrenCount <> 1 Then Return Nothing
            Dim textNode As Nevron.Nov.Xml.NXmlTextNode = TryCast(node.GetChildAt(0), Nevron.Nov.Xml.NXmlTextNode)
            Return If(textNode IsNot Nothing, textNode.Text, Nothing)
        End Function

		#EndRegion

		#Region"Nested Types"

		Protected Class NCountry
            Implements System.IComparable(Of Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry), Nevron.Nov.INDeeplyCloneable

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

            Public Function CompareTo(ByVal other As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry) As Integer Implements Global.System.IComparable(Of Global.Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry).CompareTo
                Return Me.Name.CompareTo(other.Name)
            End Function
			''' <summary>
			''' Creates an identical copy of this object.
			''' </summary>
			''' <returns>A copy of this instance.</returns>
			Public Function DeepClone() As Object Implements Global.Nevron.Nov.INDeeplyCloneable.DeepClone
                Dim country As Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry = New Nevron.Nov.Examples.UI.NComboBoxDataBindingExample.NCountry(Me.Name)
                Return country
            End Function

            Public Name As String
            Public Code As String
            Public CurrencyCode As String
            Public CurrencyName As String
            Public Capital As String
            Public Flag As Nevron.Nov.Graphics.NImage
        End Class

		#EndRegion
	End Class
End Namespace
