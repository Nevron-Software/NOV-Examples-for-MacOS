Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates the functionality of the NNumericDisplayPanel.
	''' </summary>
	Public Class NNumericLedDisplayExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' 
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Gauge.NNumericLedDisplayExample.NNumericLedDisplayExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NNumericLedDisplayExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            AddHandler stack.Unregistered, AddressOf Me.OnStackUnregistered
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_NumericDisplay1 = Me.CreateNumericLedDisplay()
            stack.Add(Me.m_NumericDisplay1)
            Me.m_NumericDisplay2 = Me.CreateNumericLedDisplay()
            stack.Add(Me.m_NumericDisplay2)
            Me.m_NumericDisplay3 = Me.CreateNumericLedDisplay()
            stack.Add(Me.m_NumericDisplay3)
            Me.m_DataFeedTimer = New Nevron.Nov.NTimer()
            AddHandler Me.m_DataFeedTimer.Tick, New Nevron.Nov.[Function](AddressOf Me.OnDataFeedTimerTick)
            Me.m_DataFeedTimer.Start()
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))

			' init form controls
			Me.m_CellSizeComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Cell Size:", Me.m_CellSizeComboBox, True))
            Me.m_CellSizeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Small"))
            Me.m_CellSizeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Normal"))
            Me.m_CellSizeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Large"))
            Me.m_CellSizeComboBox.SelectedIndex = 1
            AddHandler Me.m_CellSizeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCellSizeComboBoxSelectedIndexChanged)
            Me.m_DisplayStyleComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Display Style:", Me.m_DisplayStyleComboBox, True))
            Me.m_DisplayStyleComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENDisplayStyle)()
            Me.m_DisplayStyleComboBox.SelectedIndex = CInt(Me.m_NumericDisplay1.DisplayStyle)
            AddHandler Me.m_DisplayStyleComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDisplayStyleComboBoxSelectedIndexChanged)
            Me.m_ContentAlignmentComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Content Alignment:", Me.m_ContentAlignmentComboBox, True))
            Me.m_ContentAlignmentComboBox.FillFromEnum(Of Nevron.Nov.ENContentAlignment)()
            Me.m_ContentAlignmentComboBox.SelectedIndex = CInt(Me.m_NumericDisplay1.ContentAlignment)
            AddHandler Me.m_ContentAlignmentComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnContentAlignmentComboBoxSelectedIndexChanged)
            Me.m_SignModeComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Sign Mode", Me.m_SignModeComboBox, True))
            Me.m_SignModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENDisplaySignMode)()
            Me.m_SignModeComboBox.SelectedIndex = CInt(Me.m_NumericDisplay1.SignMode)
            AddHandler Me.m_SignModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSignModeComboBoxSelectedIndexChanged)
            Me.m_ShowLeadingZerosCheckBox = New Nevron.Nov.UI.NCheckBox("Show Leading Zeroes")
            propertyStack.Add(Me.m_ShowLeadingZerosCheckBox)
            AddHandler Me.m_ShowLeadingZerosCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowLeadingZerosCheckBoxCheckedChanged)
            Me.m_AttachSignToNumberCheckBox = New Nevron.Nov.UI.NCheckBox("Attach Sign to Number")
            propertyStack.Add(Me.m_AttachSignToNumberCheckBox)
            AddHandler Me.m_AttachSignToNumberCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAttachSignToNumberCheckBoxCheckedChanged)
            Dim litFillButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Lit Fill")
            AddHandler litFillButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnLitFillButtonClick)
            propertyStack.Add(litFillButton)
            Dim dimFillButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Dim Fill")
            AddHandler dimFillButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnDimFillButtonClick)
            propertyStack.Add(dimFillButton)
            Me.m_StopStartTimerButton = New Nevron.Nov.UI.NButton("Stop Timer")
            AddHandler Me.m_StopStartTimerButton.Click, AddressOf Me.OnStopStartTimerButtonClick
            propertyStack.Add(Me.m_StopStartTimerButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates the properties of the numeric led display.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateNumericLedDisplay() As Nevron.Nov.Chart.NNumericLedDisplay
            Dim numericLedDisplay As Nevron.Nov.Chart.NNumericLedDisplay = New Nevron.Nov.Chart.NNumericLedDisplay()
            numericLedDisplay.Value = 0.0
            numericLedDisplay.CellCountMode = Nevron.Nov.Chart.ENDisplayCellCountMode.Fixed
            numericLedDisplay.CellCount = 7
            numericLedDisplay.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            numericLedDisplay.Border = Nevron.Nov.UI.NBorder.CreateSunken3DBorder(New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic))
            numericLedDisplay.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            numericLedDisplay.Margins = New Nevron.Nov.Graphics.NMargins(5)
            numericLedDisplay.Padding = New Nevron.Nov.Graphics.NMargins(5)
            numericLedDisplay.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Return numericLedDisplay
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnStopStartTimerButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim label As Nevron.Nov.UI.NLabel = CType(CType(arg.TargetNode, Nevron.Nov.UI.NButton).Content, Nevron.Nov.UI.NLabel)

            If label.Text.StartsWith("Stop") Then
                label.Text = "Start Timer"
                Me.m_DataFeedTimer.[Stop]()
            Else
                label.Text = "Stop Timer"
                Me.m_DataFeedTimer.Start()
            End If
        End Sub

        Private Sub OnDataFeedTimerTick()
            Dim value1 As Double = -50 + Me.m_Random.[Next](10000) / 100.0
            Me.m_NumericDisplay1.Value = value1
            Dim value2 As Double

            If Me.m_Counter Mod 4 = 0 Then
                value2 = -50 + Me.m_Random.[Next](10000) / 100.0
                Me.m_NumericDisplay2.Value = value2
            End If

            Dim value3 As Double

            If Me.m_Counter Mod 8 = 0 Then
                value3 = 200 + Me.m_Random.[Next](10000) / 100.0
                Me.m_NumericDisplay3.Value = value3
            End If

            Me.m_Counter += 1
        End Sub

        Private Sub OnLitFillButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Call Nevron.Nov.Editors.NEditorWindow.CreateForType(Of Nevron.Nov.Graphics.NFill)(CType(CType(Me.m_NumericDisplay1.LitFill.DeepClone(), Nevron.Nov.Graphics.NFill), Nevron.Nov.Graphics.NFill), CType((Nothing), Nevron.Nov.Editors.NEditorContext), CType((Me.m_NumericDisplay1.DisplayWindow), Nevron.Nov.UI.NWindow), CBool((False)), CType((AddressOf Me.OnLitFillEdited), Nevron.Nov.[Function](Of Nevron.Nov.Graphics.NFill))).Open()
        End Sub

        Private Sub OnLitFillEdited(ByVal fill As Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay1.LitFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay2.LitFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay3.LitFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
        End Sub

        Private Sub OnDimFillButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Call Nevron.Nov.Editors.NEditorWindow.CreateForType(Of Nevron.Nov.Graphics.NFill)(CType(CType(Me.m_NumericDisplay1.DimFill.DeepClone(), Nevron.Nov.Graphics.NFill), Nevron.Nov.Graphics.NFill), CType((Nothing), Nevron.Nov.Editors.NEditorContext), CType((Me.m_NumericDisplay1.DisplayWindow), Nevron.Nov.UI.NWindow), CBool((False)), CType((AddressOf Me.OnDimFillEdited), Nevron.Nov.[Function](Of Nevron.Nov.Graphics.NFill))).Open()
        End Sub

        Private Sub OnDimFillEdited(ByVal fill As Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay1.DimFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay2.DimFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
            Me.m_NumericDisplay3.DimFill = CType((fill.DeepClone()), Nevron.Nov.Graphics.NFill)
        End Sub

        Private Sub OnCellSizeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim segmentWidth As Double = 0.0
            Dim segmentGap As Double = 0.0
            Dim cellSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(0.0, 0.0)

            Select Case Me.m_CellSizeComboBox.SelectedIndex
                Case 0 ' small
                    segmentWidth = 2.0
                    segmentGap = 1.0
                    cellSize = New Nevron.Nov.Graphics.NSize(15, 30)
                Case 1 ' normal
                    segmentWidth = 3
                    segmentGap = 1
                    cellSize = New Nevron.Nov.Graphics.NSize(20, 40)
                Case 2 ' large
                    segmentWidth = 4
                    segmentGap = 2
                    cellSize = New Nevron.Nov.Graphics.NSize(26, 52)
            End Select

            Dim displays As Nevron.Nov.Chart.NNumericLedDisplay() = New Nevron.Nov.Chart.NNumericLedDisplay() {Me.m_NumericDisplay1, Me.m_NumericDisplay2, Me.m_NumericDisplay3}

            For i As Integer = 0 To displays.Length - 1
                Dim display As Nevron.Nov.Chart.NNumericLedDisplay = displays(i)
                display.CellSize = cellSize
                display.SegmentGap = segmentGap
                display.SegmentWidth = segmentWidth
                display.DecimalCellSize = cellSize
                display.DecimalSegmentGap = segmentGap
                display.DecimalSegmentWidth = segmentWidth
            Next
        End Sub

        Private Sub OnDisplayStyleComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NumericDisplay1.DisplayStyle = CType(Me.m_DisplayStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplayStyle)
            Me.m_NumericDisplay2.DisplayStyle = CType(Me.m_DisplayStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplayStyle)
            Me.m_NumericDisplay3.DisplayStyle = CType(Me.m_DisplayStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplayStyle)
        End Sub

        Private Sub OnContentAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NumericDisplay1.ContentAlignment = CType(Me.m_ContentAlignmentComboBox.SelectedIndex, Nevron.Nov.ENContentAlignment)
            Me.m_NumericDisplay2.ContentAlignment = CType(Me.m_ContentAlignmentComboBox.SelectedIndex, Nevron.Nov.ENContentAlignment)
            Me.m_NumericDisplay3.ContentAlignment = CType(Me.m_ContentAlignmentComboBox.SelectedIndex, Nevron.Nov.ENContentAlignment)
        End Sub

        Private Sub OnStopStartTimerButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)
            If Me.m_DataFeedTimer.IsStarted Then
                Me.m_DataFeedTimer.[Stop]()
                Me.m_StopStartTimerButton.Content = New Nevron.Nov.UI.NLabel("Start Timer")
            Else
                Me.m_DataFeedTimer.Start()
                Me.m_StopStartTimerButton.Content = New Nevron.Nov.UI.NLabel("Stop Timer")
            End If
        End Sub

        Private Sub OnSignModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NumericDisplay1.SignMode = CType(Me.m_SignModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplaySignMode)
            Me.m_NumericDisplay2.SignMode = CType(Me.m_SignModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplaySignMode)
            Me.m_NumericDisplay3.SignMode = CType(Me.m_SignModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENDisplaySignMode)
        End Sub

        Private Sub OnShowLeadingZerosCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NumericDisplay1.ShowLeadingZeros = Me.m_ShowLeadingZerosCheckBox.Checked
            Me.m_NumericDisplay2.ShowLeadingZeros = Me.m_ShowLeadingZerosCheckBox.Checked
            Me.m_NumericDisplay3.ShowLeadingZeros = Me.m_ShowLeadingZerosCheckBox.Checked
        End Sub

        Private Sub OnAttachSignToNumberCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_NumericDisplay1.AttachSignToNumber = Me.m_AttachSignToNumberCheckBox.Checked
            Me.m_NumericDisplay2.AttachSignToNumber = Me.m_AttachSignToNumberCheckBox.Checked
            Me.m_NumericDisplay3.AttachSignToNumber = Me.m_AttachSignToNumberCheckBox.Checked
        End Sub

        Private Sub OnStackUnregistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_DataFeedTimer.[Stop]()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_StopStartTimerButton As Nevron.Nov.UI.NButton
        Private m_AttachSignToNumberCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ShowLeadingZerosCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_SignModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_ContentAlignmentComboBox As Nevron.Nov.UI.NComboBox
        Private m_DisplayStyleComboBox As Nevron.Nov.UI.NComboBox
        Private m_NumericDisplay1 As Nevron.Nov.Chart.NNumericLedDisplay
        Private m_NumericDisplay2 As Nevron.Nov.Chart.NNumericLedDisplay
        Private m_NumericDisplay3 As Nevron.Nov.Chart.NNumericLedDisplay
        Private m_DataFeedTimer As Nevron.Nov.NTimer
        Private m_CellSizeComboBox As Nevron.Nov.UI.NComboBox
        Private m_Counter As Integer = 0
        Private m_Random As System.Random = New System.Random()

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NNumericLedDisplayExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
	End Class
End Namespace
