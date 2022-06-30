Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis docking example
	''' </summary>
	Public Class NAxisDockingPercentagestExample
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
            Nevron.Nov.Examples.Chart.NAxisDockingPercentagestExample.NAxisDockingPercentagestExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisDockingPercentagestExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Docking Percentages"
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_RedAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, Nevron.Nov.Graphics.NColor.Red)
            chart.Axes.Add(Me.m_RedAxis)
            Me.m_GreenAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Right, Nevron.Nov.Graphics.NColor.Green)
            chart.Axes.Add(Me.m_GreenAxis)

			' Add a custom vertical axis
			Me.m_BlueAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, Nevron.Nov.Graphics.NColor.Blue)
            chart.Axes.Add(Me.m_BlueAxis)
            chart.Axes.Add(Nevron.Nov.Chart.NCartesianChart.CreateDockedAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Bottom, Nevron.Nov.Chart.ENScaleType.Orindal))

			' create three line series and dispay them on three vertical axes (red, green and blue axis)
			Dim line1 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.DarkRed, 10, 20)
            chart.Series.Add(line1)
            Dim line2 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.DarkGreen, 50, 100)
            chart.Series.Add(line2)
            Dim line3 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.DarkBlue, 100, 200)
            chart.Series.Add(line3)
            line1.VerticalAxis = Me.m_RedAxis
            line2.VerticalAxis = Me.m_GreenAxis
            line3.VerticalAxis = Me.m_BlueAxis
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_RedAxisEndPercentUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RedAxisEndPercentUpDown.Minimum = 10
            Me.m_RedAxisEndPercentUpDown.Maximum = 60
            AddHandler Me.m_RedAxisEndPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRedAxisEndPercentUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Red Axis End Percent:", Me.m_RedAxisEndPercentUpDown))
            Me.m_BlueAxisBeginPercentUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_BlueAxisBeginPercentUpDown.Minimum = 20
            Me.m_BlueAxisBeginPercentUpDown.Maximum = 90
            AddHandler Me.m_BlueAxisBeginPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBlueAxisEndPercentUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Blue Axis Begin Percent:", Me.m_BlueAxisBeginPercentUpDown))
            Me.m_RedAxisEndPercentUpDown.Value = 30
            Me.m_BlueAxisBeginPercentUpDown.Value = 70
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to change the area occupied by an axis when docked in an axis dock zone.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateLinearAxis(ByVal dockZone As Nevron.Nov.Chart.ENCartesianAxisDockZone, ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.Chart.NCartesianAxis
            Dim axis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            axis.Scale = Me.CreateLinearScale(color)
            axis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(dockZone, False)
            Return axis
        End Function

        Private Function CreateLinearScale(ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.Chart.NLinearScale
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            linearScale.Ruler.Stroke = New Nevron.Nov.Graphics.NStroke(1, color)
            linearScale.InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(color)
            linearScale.OuterMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(color)
            linearScale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Dim grid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            grid.Stroke.Color = color
            grid.Visible = True
            linearScale.MajorGridLines = grid
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Length = 1
            strip.Interval = 1
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(color, 50))
            linearScale.Strips.Add(strip)
            Return linearScale
        End Function

        Private Function CreateLineSeries(ByVal lightColor As Nevron.Nov.Graphics.NColor, ByVal color As Nevron.Nov.Graphics.NColor, ByVal begin As Integer, ByVal [end] As Integer) As Nevron.Nov.Chart.NLineSeries
			' Add a line series
			Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](begin, [end])))
            Next

            line.Stroke = New Nevron.Nov.Graphics.NStroke(2, color)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Format = "<value>"
            dataLabelStyle.TextStyle.Background.Visible = False
            dataLabelStyle.ArrowStroke.Width = 0
            dataLabelStyle.ArrowLength = 10
            dataLabelStyle.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arial", 8)
            dataLabelStyle.TextStyle.Background.Visible = True
            line.DataLabelStyle = dataLabelStyle
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            markerStyle.Border = New Nevron.Nov.Graphics.NStroke(color)
            markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(lightColor)
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(5, 5)
            line.MarkerStyle = markerStyle
            Return line
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRedAxisEndPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_BlueAxisBeginPercentUpDown.Minimum = Me.m_RedAxisEndPercentUpDown.Value + 10
            Me.RecalcAxes()
        End Sub

        Private Sub OnBlueAxisEndPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RedAxisEndPercentUpDown.Maximum = Me.m_BlueAxisBeginPercentUpDown.Value - 10
            Me.RecalcAxes()
        End Sub

        Private Sub RecalcAxes()
            Dim middleAxisBegin As Integer = CInt(Me.m_RedAxisEndPercentUpDown.Value)
            Dim middleAxisEnd As Integer = CInt(Me.m_BlueAxisBeginPercentUpDown.Value)

			' red axis
			Me.m_RedAxis.Anchor.EndPercent = middleAxisBegin

			' green axis
			Me.m_GreenAxis.Anchor.BeginPercent = middleAxisBegin
            Me.m_GreenAxis.Anchor.EndPercent = middleAxisEnd

			' blue axis
			Me.m_BlueAxis.Anchor.BeginPercent = middleAxisEnd
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RedAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_GreenAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_BlueAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_RedAxisEndPercentUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_BlueAxisBeginPercentUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisDockingPercentagestExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
