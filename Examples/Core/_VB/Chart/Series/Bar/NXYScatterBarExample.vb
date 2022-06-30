Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' XY Scatter Bar Example
	''' </summary>
	Public Class NXYScatterBarExample
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
            Nevron.Nov.Examples.Chart.NXYScatterBarExample.NXYScatterBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NXYScatterBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "XY Scatter Bar"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlaced stripe to the Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(stripStyle)
            Me.m_Bar = New Nevron.Nov.Chart.NBarSeries()
            chart.Series.Add(Me.m_Bar)
            Me.m_Bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Bar.InflateMargins = True
            Me.m_Bar.WidthMode = Nevron.Nov.Chart.ENBarWidthMode.FixedWidth
            Me.m_Bar.Width = 20
            Me.m_Bar.Name = "Bar Series"
            Me.m_Bar.UseXValues = True

			' add xy values
			Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(15, 10))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(25, 23))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(45, 12))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(55, 21))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(61, 16))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(67, 19))
            Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(72, 11))
            Return chartView
        End Function
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim changeXValuesButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change X Values")
            AddHandler changeXValuesButton.Click, AddressOf Me.OnChangeXValuesButtonClick
            stack.Add(changeXValuesButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a xy scatter bar chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChangeXValuesButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim random As System.Random = New System.Random()
            Me.m_Bar.DataPoints(CInt((0))).X = random.[Next](10)

            For i As Integer = 1 To Me.m_Bar.DataPoints.Count - 1
                Me.m_Bar.DataPoints(CInt((i))).X = Me.m_Bar.DataPoints(CInt((i - 1))).X + random.[Next](1, 10)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Bar As Nevron.Nov.Chart.NBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NXYScatterBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
