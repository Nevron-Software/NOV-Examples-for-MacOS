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
    Public Class NCalendarExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NCalendarExample.NCalendarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCalendarExample), NExampleBase.NExampleBaseSchema)

			' Fill the list of cultures
			Dim cultureNames As String() = New String() {"en-US", "en-GB", "fr-FR", "de-DE", "es-ES", "ru-RU", "zh-CN", "ja-JP", "it-IT", "hi-IN", "ar-AE", "he-IL", "id-ID", "ko-KR", "pt-BR", "sv-SE", "tr-TR", "pt-BR", "bg-BG", "ro-RO", "pl-PL", "nl-NL", "cs-CZ"}
            Nevron.Nov.Examples.UI.NCalendarExample.Cultures = New Nevron.Nov.DataStructures.NList(Of System.Globalization.CultureInfo)()
            Dim i As Integer = 0, count As Integer = cultureNames.Length

            While i < count
                Dim cultureInfo As System.Globalization.CultureInfo

                Try
                    cultureInfo = New System.Globalization.CultureInfo(cultureNames(i))
                Catch
                    cultureInfo = Nothing
                End Try

                If cultureInfo IsNot Nothing AndAlso Nevron.Nov.Examples.UI.NCalendarExample.Cultures.Contains(cultureInfo) = False Then
                    Call Nevron.Nov.Examples.UI.NCalendarExample.Cultures.Add(cultureInfo)
                End If

                i += 1
            End While

			' Sort the cultures by their English name
			Call Nevron.Nov.Examples.UI.NCalendarExample.Cultures.Sort(New Nevron.Nov.Examples.UI.NCalendarExample.NCultureNameComparer())
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_Calendar = New Nevron.Nov.UI.NCalendar()
            Me.m_Calendar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Calendar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler Me.m_Calendar.SelectedDateChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCalendarSelectedDateChanged)
            Return Me.m_Calendar
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
            Dim i As Integer = 0, count As Integer = Nevron.Nov.Examples.UI.NCalendarExample.Cultures.Count

            While i < count
                Dim culture As System.Globalization.CultureInfo = Nevron.Nov.Examples.UI.NCalendarExample.Cultures(i)
                Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem(culture.EnglishName)
                item.Tag = culture.Name
                combo.Items.Add(item)

                If Equals(culture.Name, Me.m_Calendar.CultureName) Then
                    selectedIndex = i
                End If

                i += 1
            End While

            groupBox.Content = combo
            AddHandler combo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCultureComboSelectedIndexChanged)

			' add the property editors
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Calendar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Calendar, Nevron.Nov.UI.NCalendar.EnabledProperty, Nevron.Nov.UI.NCalendar.HighlightTodayProperty, Nevron.Nov.UI.NCalendar.MonthFormatModeProperty, Nevron.Nov.UI.NCalendar.DayOfWeekFormatModeProperty)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

			' add the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and configure a calendar widget. Using the controls to the right you can
	modify various aspect of the calendar's appearance and behavior. NOV calendar is fully localizable, you can test
	how it looks for different cultures by selecting one from the cultures combo box on the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCalendarSelectedDateChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim [date] As System.DateTime = CDate(args.NewValue)
            Me.m_EventsLog.LogEvent("Selected Date Changed: " & [date].ToString("d", Me.m_Calendar.CultureInfo))
        End Sub

        Private Sub OnCultureComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim combo As Nevron.Nov.UI.NComboBox = TryCast(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim selectedItem As Nevron.Nov.UI.NComboBoxItem = combo.SelectedItem
            If selectedItem Is Nothing Then Return
            Me.m_Calendar.CultureName = CStr(selectedItem.Tag)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Calendar As Nevron.Nov.UI.NCalendar
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCalendarExampleSchema As Nevron.Nov.Dom.NSchema

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
