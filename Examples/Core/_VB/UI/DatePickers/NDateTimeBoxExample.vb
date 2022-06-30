Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NDateTimeBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NDateTimeBoxExample.NDateTimeBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NDateTimeBoxExample), NExampleBase.NExampleBaseSchema)

			' Fill the list of cultures
			Dim cultureNames As String() = New String() {"en-US", "en-GB", "fr-FR", "de-DE", "es-ES", "ru-RU", "zh-CN", "ja-JP", "it-IT", "hi-IN", "ar-AE", "he-IL", "id-ID", "ko-KR", "pt-BR", "sv-SE", "tr-TR", "pt-BR", "bg-BG", "ro-RO", "pl-PL", "nl-NL", "cs-CZ"}
            Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures = New Nevron.Nov.DataStructures.NList(Of System.Globalization.CultureInfo)()
            Dim i As Integer = 0, count As Integer = cultureNames.Length

            While i < count
                Dim cultureInfo As System.Globalization.CultureInfo

                Try
                    cultureInfo = New System.Globalization.CultureInfo(cultureNames(i))
                Catch
                    cultureInfo = Nothing
                End Try

                If cultureInfo IsNot Nothing AndAlso Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures.Contains(cultureInfo) = False Then
                    Call Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures.Add(cultureInfo)
                End If

                i += 1
            End While

			' Sort the cultures by their English name
			Call Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures.Sort(New Nevron.Nov.Examples.UI.NDateTimeBoxExample.NCultureNameComparer())
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_DateTimeBox = New Nevron.Nov.UI.NDateTimeBox()
            Me.m_DateTimeBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_DateTimeBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_DateTimeBox.SelectedDateChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDateTimeBoxSelectedColorChanged)
            Return Me.m_DateTimeBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Culture:")
            stack.Add(groupBox)
            groupBox.Margins = New Nevron.Nov.Graphics.NMargins(0, 0, 0, 10)

			' add the cultures combo box
			Dim selectedIndex As Integer = -1
            Dim combo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Dim i As Integer = 0, count As Integer = Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures.Count

            While i < count
                Dim culture As System.Globalization.CultureInfo = Nevron.Nov.Examples.UI.NDateTimeBoxExample.Cultures(i)
                Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(culture.EnglishName)
                item.Tag = culture.Name
                combo.Items.Add(item)

                If Equals(culture.Name, Me.m_DateTimeBox.CultureName) Then
                    selectedIndex = i
                End If

                i += 1
            End While

            groupBox.Content = combo
            AddHandler combo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCultureComboSelectedIndexChanged)
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_DateTimeBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_DateTimeBox, Nevron.Nov.UI.NDateTimeBox.EnabledProperty, Nevron.Nov.UI.NDateTimeBox.HighlightTodayProperty, Nevron.Nov.UI.NDateTimeBox.HasTodayButtonProperty, Nevron.Nov.UI.NDateTimeBox.ModeProperty, Nevron.Nov.UI.NDateTimeBox.FormatProperty, Nevron.Nov.UI.NDateTimeBox.SelectedDateProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure a date time box. Using the controls on the right you can
	modify various aspects of the date time box and its drop down calendar's appearance and behavior. NOV date time
	box is fully localizable, you can test how it looks for different cultures by selecting one from the cultures
	combo box on the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnDateTimeBoxSelectedColorChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim newDate As System.DateTime = CDate(args.NewValue)
            Me.m_EventsLog.LogEvent(newDate.ToString())
        End Sub

        Private Sub OnCultureComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim combo As Nevron.Nov.UI.NComboBox = TryCast(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim selectedItem As Nevron.Nov.UI.NComboBoxItem = combo.SelectedItem
            If selectedItem Is Nothing Then Return
            Me.m_DateTimeBox.CultureName = CStr(selectedItem.Tag)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_EventsLog As NExampleEventsLog
        Private m_DateTimeBox As Nevron.Nov.UI.NDateTimeBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDateTimeBoxExample.
		''' </summary>
		Public Shared ReadOnly NDateTimeBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly Cultures As Nevron.Nov.DataStructures.NList(Of System.Globalization.CultureInfo)

		#EndRegion

		#Region"Nested Types"

		Private Class NCultureNameComparer
            Implements System.Collections.Generic.IComparer(Of System.Globalization.CultureInfo)

            Public Function Compare(ByVal culture1 As System.Globalization.CultureInfo, ByVal culture2 As System.Globalization.CultureInfo) As Integer Implements Global.System.Collections.Generic.IComparer(Of Global.System.Globalization.CultureInfo).Compare
                Return culture1.EnglishName.CompareTo(culture2.EnglishName)
            End Function
        End Class

		#EndRegion
	End Class
End Namespace
