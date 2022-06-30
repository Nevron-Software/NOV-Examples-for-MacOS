Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' XY Scatter Line Example
	''' </summary>
	Public Class NXYScatterLineExample
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
            Nevron.Nov.Examples.Chart.NXYScatterLineExample.NXYScatterLineExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NXYScatterLineExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "XY Scatter Line"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlaced stripe to the Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(stripStyle)
            Me.m_Line = New Nevron.Nov.Chart.NLineSeries()
            chart.Series.Add(Me.m_Line)
            Me.m_Line.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Line.InflateMargins = True
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            Me.m_Line.MarkerStyle = markerStyle
            markerStyle.Visible = True
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(10, 10)
            markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Red)
            Me.m_Line.Name = "Line Series"
            Me.m_Line.UseXValues = True

			' add xy values
			Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(15, 10))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(25, 23))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(45, 12))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(55, 21))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(61, 16))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(67, 19))
            Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(72, 11))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim changeXValuesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change X Values")
            AddHandler changeXValuesButton.Click, AddressOf Me.OnChangeXValuesButtonClick
            stack.Add(changeXValuesButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a xy scatter line chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChangeXValuesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim random As System.Random = New System.Random()
            Me.m_Line.DataPoints(CInt((0))).X = random.[Next](10)

            For i As Integer = 1 To Me.m_Line.DataPoints.Count - 1
                Me.m_Line.DataPoints(CInt((i))).X = Me.m_Line.DataPoints(CInt((i - 1))).X + random.[Next](1, 10)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Line As Nevron.Nov.Chart.NLineSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NXYScatterLineExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
