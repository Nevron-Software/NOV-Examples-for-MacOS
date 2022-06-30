Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NProgressBarExample
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
            Nevron.Nov.Examples.UI.NProgressBarExample.NProgressBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NProgressBarExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

			' Horizontal progress bar
			Me.m_HorizontalProgressBar = New Nevron.Nov.UI.NProgressBar()
            Me.m_HorizontalProgressBar.Style = Nevron.Nov.UI.ENProgressBarStyle.Horizontal
            Me.m_HorizontalProgressBar.Mode = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultMode
            Me.m_HorizontalProgressBar.Value = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultValue
            Me.m_HorizontalProgressBar.BufferedValue = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultBufferedValue
            Me.m_HorizontalProgressBar.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 30)
            Me.m_HorizontalProgressBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.Add(New Nevron.Nov.UI.NGroupBox("Horizontal", Me.m_HorizontalProgressBar))

			' Vertical progress bar
			Me.m_VerticalProgressBar = New Nevron.Nov.UI.NProgressBar()
            Me.m_VerticalProgressBar.Style = Nevron.Nov.UI.ENProgressBarStyle.Vertical
            Me.m_VerticalProgressBar.Mode = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultMode
            Me.m_VerticalProgressBar.Value = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultValue
            Me.m_VerticalProgressBar.BufferedValue = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultBufferedValue
            Me.m_VerticalProgressBar.PreferredSize = New Nevron.Nov.Graphics.NSize(30, 300)
            Me.m_VerticalProgressBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(New Nevron.Nov.UI.NGroupBox("Vertical", Me.m_VerticalProgressBar))

			' Circular progress bar - 50% rim
			Me.m_CircularProgressBar1 = New Nevron.Nov.UI.NProgressBar()
            Me.m_CircularProgressBar1.Style = Nevron.Nov.UI.ENProgressBarStyle.Circular
            Me.m_CircularProgressBar1.Mode = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultMode
            Me.m_CircularProgressBar1.Value = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultValue
            Me.m_CircularProgressBar1.BufferedValue = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultBufferedValue
            Me.m_CircularProgressBar1.PreferredSize = New Nevron.Nov.Graphics.NSize(150, 150)

			' Circular progress bar - 100% rim
			Me.m_CircularProgressBar2 = New Nevron.Nov.UI.NProgressBar()
            Me.m_CircularProgressBar2.Style = Nevron.Nov.UI.ENProgressBarStyle.Circular
            Me.m_CircularProgressBar2.Mode = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultMode
            Me.m_CircularProgressBar2.Value = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultValue
            Me.m_CircularProgressBar2.BufferedValue = Nevron.Nov.Examples.UI.NProgressBarExample.DefaultBufferedValue
            Me.m_CircularProgressBar2.RimWidthPercent = 100
            Me.m_CircularProgressBar2.PreferredSize = New Nevron.Nov.Graphics.NSize(150, 150)
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(Me.m_CircularProgressBar1, Me.m_CircularProgressBar2)
            pairBox.Spacing = 30
            stack.Add(New Nevron.Nov.UI.NGroupBox("Circular", pairBox))

			' Create the Progress bars array
			Me.m_ProgressBars = New Nevron.Nov.UI.NProgressBar(3) {}
            Me.m_ProgressBars(0) = Me.m_HorizontalProgressBar
            Me.m_ProgressBars(1) = Me.m_VerticalProgressBar
            Me.m_ProgressBars(2) = Me.m_CircularProgressBar1
            Me.m_ProgressBars(3) = Me.m_CircularProgressBar2
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom

			' The Mode combo box
			Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            comboBox.FillFromEnum(Of Nevron.Nov.UI.ENProgressBarMode)()
            comboBox.SelectedIndex = CInt(Me.m_HorizontalProgressBar.Mode)
            AddHandler comboBox.SelectedIndexChanged, AddressOf Me.OnModeSelected
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Mode:", comboBox))

			' The Label style combo box
			comboBox = New Nevron.Nov.UI.NComboBox()
            comboBox.FillFromEnum(Of Nevron.Nov.UI.ENProgressBarLabelStyle)()
            comboBox.SelectedIndex = CInt(Me.m_HorizontalProgressBar.LabelStyle)
            AddHandler comboBox.SelectedIndexChanged, AddressOf Me.OnLabelStyleSelected
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Label Style:", comboBox))

			' The Value numeric up down
			Dim valueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown(0, 100, Nevron.Nov.Examples.UI.NProgressBarExample.DefaultValue)
            AddHandler valueUpDown.ValueChanged, AddressOf Me.OnValueChanged
            Me.m_ValuePairBox = Nevron.Nov.UI.NPairBox.Create("Value:", valueUpDown)
            stack.Add(Me.m_ValuePairBox)

			' The Buffered value numeric up down
			Dim bufferedValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown(0, 100, Nevron.Nov.Examples.UI.NProgressBarExample.DefaultBufferedValue)
            AddHandler bufferedValueUpDown.ValueChanged, AddressOf Me.OnBufferedValueChanged
            Me.m_BufferedValuePairBox = Nevron.Nov.UI.NPairBox.Create("Buffered Value:", bufferedValueUpDown)
            stack.Add(Me.m_BufferedValuePairBox)

			' The Indeterminate part size numeric up down
			Dim indeterminateSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown(1, 100, 25)
            AddHandler indeterminateSizeUpDown.ValueChanged, AddressOf Me.OnIndeterminateSizeUpDownValueChanged
            Me.m_IndeterminatePartSizePairBox = Nevron.Nov.UI.NPairBox.Create("Indeterminate Size (%):", indeterminateSizeUpDown)
            stack.Add(Me.m_IndeterminatePartSizePairBox)

			' The Animation speed numeric up down
			Dim animationSpeedUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown(0.1, 99, 2)
            animationSpeedUpDown.DecimalPlaces = 1
            animationSpeedUpDown.[Step] = 0.1
            AddHandler animationSpeedUpDown.ValueChanged, AddressOf Me.OnAnimationSpeedUpDownValueChanged
            Me.m_AnimationSpeedPairBox = Nevron.Nov.UI.NPairBox.Create("Animation Speed (%):", animationSpeedUpDown)
            stack.Add(Me.m_AnimationSpeedPairBox)

			' Update controls visibility
			Me.UpdateControlsVisibility(Nevron.Nov.Examples.UI.NProgressBarExample.DefaultMode)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use progress bars. The progress bar is a widget that
	fills to indicate the progress of an operation. The <b>Style</b> property determines whether
	it is horizontally, vertically oriented or circular. The <b>Minimum</b> and <b>Maximum</b> properties
	determine the start and the end of the operation and the <b>Value</b> property indicates its current progress.
	All progress bars can have a label and its style is controlled through the <b>LabelStyle</b> property.
	Circular progress bars let you specify the width of their rim in percent relative to the size of the
	progress bar as this example demonstrates.
</p>
<p>
	The <b>Mode</b> property determines the progress bar mode and can be:
</p>
<ul>
	<li><b>Determinate</b> - the progress bar shows the progress of an operation from 0 to 100%. This is the default mode.</li>
	<li><b>Indeterminate</b> - the progress bar shows an animation indicating that a long-running operation is executing. No specific
		progress is shown. This mode should be used for operations whose progress cannot be estimated, for example a long-running database query.</li>
	<li><b>Buffered</b> - the progress bar shows the progress of an operation from 0 to 100%. It also additionally shows the buffered
		value of the operation with a lighter color. The buffered value is specified through the <b>BufferedValue</b> property.</li>
</ul>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub UpdateControlsVisibility(ByVal mode As Nevron.Nov.UI.ENProgressBarMode)
            Select Case mode
                Case Nevron.Nov.UI.ENProgressBarMode.Determinate
                    Me.m_ValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                    Me.m_BufferedValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                    Me.m_IndeterminatePartSizePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                    Me.m_AnimationSpeedPairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                Case Nevron.Nov.UI.ENProgressBarMode.Indeterminate
                    Me.m_ValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                    Me.m_BufferedValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                    Me.m_IndeterminatePartSizePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                    Me.m_AnimationSpeedPairBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                Case Nevron.Nov.UI.ENProgressBarMode.Buffered
                    Me.m_ValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                    Me.m_BufferedValuePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Visible
                    Me.m_IndeterminatePartSizePairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
                    Me.m_AnimationSpeedPairBox.Visibility = Nevron.Nov.UI.ENVisibility.Collapsed
            End Select
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnModeSelected(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim mode As Nevron.Nov.UI.ENProgressBarMode = CType(CInt(arg.NewValue), Nevron.Nov.UI.ENProgressBarMode)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).Mode = mode
            Next

            Me.UpdateControlsVisibility(mode)
        End Sub

        Private Sub OnLabelStyleSelected(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim labelStyle As Nevron.Nov.UI.ENProgressBarLabelStyle = CType(CInt(arg.NewValue), Nevron.Nov.UI.ENProgressBarLabelStyle)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).LabelStyle = labelStyle
            Next
        End Sub

        Private Sub OnValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim value As Double = CDbl(args.NewValue)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).Value = value
            Next
        End Sub

        Private Sub OnBufferedValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim bufferedValue As Double = CDbl(args.NewValue)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).BufferedValue = bufferedValue
            Next
        End Sub

        Private Sub OnIndeterminateSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim indeterminateSize As Double = CDbl(arg.NewValue)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).IndeterminatePartSizePercent = indeterminateSize
            Next
        End Sub

        Private Sub OnAnimationSpeedUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim animationSpeed As Double = CDbl(arg.NewValue)

            For i As Integer = 0 To Me.m_ProgressBars.Length - 1
                Me.m_ProgressBars(CInt((i))).AnimationSpeed = animationSpeed
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HorizontalProgressBar As Nevron.Nov.UI.NProgressBar
        Private m_VerticalProgressBar As Nevron.Nov.UI.NProgressBar
        Private m_CircularProgressBar1 As Nevron.Nov.UI.NProgressBar
        Private m_CircularProgressBar2 As Nevron.Nov.UI.NProgressBar
        Private m_ProgressBars As Nevron.Nov.UI.NProgressBar()
        Private m_ValuePairBox As Nevron.Nov.UI.NPairBox
        Private m_BufferedValuePairBox As Nevron.Nov.UI.NPairBox
        Private m_IndeterminatePartSizePairBox As Nevron.Nov.UI.NPairBox
        Private m_AnimationSpeedPairBox As Nevron.Nov.UI.NPairBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NProgressBarExample.
		''' </summary>
		Public Shared ReadOnly NProgressBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const DefaultMode As Nevron.Nov.UI.ENProgressBarMode = Nevron.Nov.UI.ENProgressBarMode.Buffered
        Private Const DefaultValue As Double = 40
        Private Const DefaultBufferedValue As Double = 60

		#EndRegion
	End Class
End Namespace
