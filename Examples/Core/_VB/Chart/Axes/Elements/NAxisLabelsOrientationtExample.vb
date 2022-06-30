Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis labels orientation example
	''' </summary>
	Public Class NAxisLabelsOrientationtExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NAxisLabelsOrientationtExample.NAxisLabelsOrientationtExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisLabelsOrientationtExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Axis Labels Orientation"

			' configure chart
			Me.m_Chart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.PrimaryAndSecondaryLinear)

            For i As Integer = 0 To Me.m_Chart.Axes.Count - 1
				' configure the axes
				Dim axis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(i)

				' set the range to [0, 100]
				axis.ViewRangeMode = Nevron.Nov.Chart.ENAxisViewRangeMode.FixedRange
                axis.MinViewRangeValue = 0
                axis.MaxViewRangeValue = 100
                Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)
                Dim title As String = String.Empty

                Select Case i
                    Case 0
                        title = "Primary Y"
                    Case 1
                        title = "Primary X"
                    Case 2
                        title = "Secondary Y"
                    Case 3
                        title = "Secondary X"
                End Select

                linearScale.Title.Text = title
                linearScale.MinTickDistance = 30
                linearScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0, False)
                linearScale.Labels.Style.AlwaysInsideScale = True
                linearScale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)(New Nevron.Nov.Chart.ENLevelLabelsLayout() {Nevron.Nov.Chart.ENLevelLabelsLayout.Stagger2})
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim orientationComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            orientationComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCartesianChartOrientation)()
            orientationComboBox.SelectedIndex = CInt(Me.m_Chart.Orientation)
            AddHandler orientationComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOrientationComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Orientation:", orientationComboBox))
            Me.m_AngleModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_AngleModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleLabelAngleMode)()
            Me.m_AngleModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale)
            AddHandler Me.m_AngleModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAxisLabelChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Angle Mode:", Me.m_AngleModeComboBox))
            Me.m_CustomAngleNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_CustomAngleNumericUpDown.Minimum = 0
            Me.m_CustomAngleNumericUpDown.Maximum = 360
            Me.m_CustomAngleNumericUpDown.Value = 0
            AddHandler Me.m_CustomAngleNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAxisLabelChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Custom Angle:", Me.m_CustomAngleNumericUpDown))
            Me.m_AllowLabelsToFlipCheckBox = New Nevron.Nov.UI.NCheckBox("Allow Label To Flip")
            Me.m_AllowLabelsToFlipCheckBox.Checked = False
            AddHandler Me.m_AllowLabelsToFlipCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAxisLabelChanged)
            stack.Add(Me.m_AllowLabelsToFlipCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to change the orientation of axis labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOrientationComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Orientation = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENCartesianChartOrientation)
        End Sub

        Private Sub OnAxisLabelChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

            For i As Integer = 0 To chart.Axes.Count - 1
				' configure the axes
				Dim scale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CInt((i))).Scale, Nevron.Nov.Chart.NLinearScale)
                scale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(CType(Me.m_AngleModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENScaleLabelAngleMode), CSng(Me.m_CustomAngleNumericUpDown.Value), Me.m_AllowLabelsToFlipCheckBox.Checked)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_AngleModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_CustomAngleNumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_AllowLabelsToFlipCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisLabelsOrientationtExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
