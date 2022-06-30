Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Cluster Bar Example
	''' </summary>
	Public Class NClusterBarExample
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
            Nevron.Nov.Examples.Chart.NClusterBarExample.NClusterBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NClusterBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Cluster Bar Labels"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' add a bar series
			Me.m_Bar1 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar1.Name = "Bar1"
            Me.m_Bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            Me.m_Bar1.DataLabelStyle = Me.CreateDataLabelStyle()
            Me.m_Bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(Me.m_Bar1)

			' add another bar series
			Me.m_Bar2 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar2.Name = "Bar2"
            Me.m_Bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            Me.m_Bar2.DataLabelStyle = Me.CreateDataLabelStyle()
            Me.m_Bar2.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(Me.m_Bar2)
            Me.FillRandomData()
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack)
            Dim gapPercentNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            propertyStack.Add(Nevron.Nov.UI.NPairBox.Create("Gap Percent: ", gapPercentNumericUpDown))
            gapPercentNumericUpDown.Value = Me.m_Bar1.GapFactor * 100.0
            AddHandler gapPercentNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.gapPercentNumericUpDown_ValueChanged)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a cluster bar chart.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Creates a new data label style object
		''' </summary>
		''' <returns></returns>
		Private Function CreateDataLabelStyle() As Nevron.Nov.Chart.NDataLabelStyle
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Format = "<value>"
            Return dataLabelStyle
        End Function

        Private Sub FillRandomData()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                Me.m_Bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub gapPercentNumericUpDown_ValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Bar1.GapFactor = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value / 100.0
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Bar1 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar2 As Nevron.Nov.Chart.NBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NClusterBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
