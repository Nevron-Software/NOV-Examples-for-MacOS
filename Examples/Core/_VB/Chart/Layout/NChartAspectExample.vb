Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Chart Aspect example
	''' </summary>
	Public Class NChartAspectExample
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
            Nevron.Nov.Examples.Chart.NChartAspectExample.NChartAspectExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NChartAspectExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Chart Aspect"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.FitMode = Nevron.Nov.Chart.ENCartesianChartFitMode.Aspect

			' Add a x linear axis
			Dim primaryXAxis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            primaryXAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Bottom)
            Dim primaryXScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            primaryXScale.Title.Text = "X Scale Title"
            primaryXScale.Labels.Style.AlwaysInsideScale = True
            primaryXScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.View, 90, False)
            primaryXAxis.Scale = primaryXScale
            chart.Axes.Add(primaryXAxis)

			' Add a y linear axis
			Dim primaryYAxis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            primaryYAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left)
            Dim primaryYScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            primaryYScale.Title.Text = "Y Scale Title"
            primaryYScale.Title.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            primaryYScale.Labels.Style.AlwaysInsideScale = True
            primaryYAxis.Scale = primaryYScale
            chart.Axes.Add(primaryYAxis)

			' Create the x / y crossed axes
			Dim secondaryXAxis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            Dim secondaryXScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            secondaryXScale.Labels.Visible = False
            secondaryXAxis.Scale = secondaryXScale
            chart.Axes.Add(secondaryXAxis)
            Dim secondaryYAxis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            Dim secondaryYScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            secondaryYScale.Labels.Visible = False
            secondaryYAxis.Scale = secondaryYScale
            chart.Axes.Add(secondaryYAxis)

			' cross them
			secondaryXAxis.Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(0, secondaryYAxis, Nevron.Nov.Chart.ENCartesianAxisOrientation.Horizontal, Nevron.Nov.Chart.ENScaleOrientation.Right, 0, 100)
            secondaryYAxis.Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(0, secondaryXAxis, Nevron.Nov.Chart.ENCartesianAxisOrientation.Vertical, Nevron.Nov.Chart.ENScaleOrientation.Right, 0, 100)

			' add some dummy data
			Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
            chart.Series.Add(point)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = False
            point.DataLabelStyle = dataLabelStyle
            point.UseXValues = True
            point.Size = 2

			' add some random data in the range [-100, 100]
			Dim rand As System.Random = New System.Random()

            For i As Integer = 0 To 3000 - 1
                Dim x As Double = rand.[Next](190) - 95
                Dim y As Double = rand.[Next](190) - 95
                point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(x, y))
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Dim chartFitMode As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            chartFitMode.FillFromEnum(Of Nevron.Nov.Chart.ENCartesianChartFitMode)()
            chartFitMode.SelectedIndex = CInt(chart.FitMode)
            AddHandler chartFitMode.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnChartFitModeSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Fit Mode:", chartFitMode))
            Me.m_ProportionXComboBox = Me.CreateProportionComboBox()
            AddHandler Me.m_ProportionXComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnProportionComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("X:", Me.m_ProportionXComboBox))
            Me.m_ProportionYComboBox = Me.CreateProportionComboBox()
            AddHandler Me.m_ProportionYComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnProportionComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Y:", Me.m_ProportionYComboBox))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how change the chart aspect ratio.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateProportionComboBox() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 5 - 1
                comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem((i + 1).ToString()))
            Next

            comboBox.SelectedIndex = 0
            Return comboBox
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChartFitModeSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.FitMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENCartesianChartFitMode)

            Select Case chart.FitMode
                Case Nevron.Nov.Chart.ENCartesianChartFitMode.Aspect, Nevron.Nov.Chart.ENCartesianChartFitMode.AspectWithAxes
                    Me.m_ProportionXComboBox.Enabled = True
                    Me.m_ProportionYComboBox.Enabled = True
                Case Nevron.Nov.Chart.ENCartesianChartFitMode.Stretch
                    Me.m_ProportionXComboBox.Enabled = False
                    Me.m_ProportionYComboBox.Enabled = False
            End Select
        End Sub

        Private Sub OnProportionComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.Aspect = CDbl((Me.m_ProportionXComboBox.SelectedIndex + 1)) / CDbl((Me.m_ProportionYComboBox.SelectedIndex + 1))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_ProportionXComboBox As Nevron.Nov.UI.NComboBox
        Private m_ProportionYComboBox As Nevron.Nov.UI.NComboBox
			
		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NChartAspectExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
