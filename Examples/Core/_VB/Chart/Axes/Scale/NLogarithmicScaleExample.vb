Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Logarithmic Scale Example
	''' </summary>
	Public Class NLogarithmicScaleExample
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
            Nevron.Nov.Examples.Chart.NLogarithmicScaleExample.NLogarithmicScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLogarithmicScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Logarithmic Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim logarithmicScale As Nevron.Nov.Chart.NLogarithmicScale = New Nevron.Nov.Chart.NLogarithmicScale()
            logarithmicScale.MinorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            logarithmicScale.MinorTickCount = 3
            logarithmicScale.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep

			' add interlaced stripe 
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            strip.Interlaced = True
            logarithmicScale.Strips.Add(strip)
            logarithmicScale.CustomStep = 1
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = logarithmicScale
            Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Chart.Series.Add(line)
            line.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
            line.InflateMargins = False
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            line.MarkerStyle = markerStyle
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(15, 15)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.Format = "<value>"
            line.DataLabelStyle = dataLabelStyle
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(12))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(100))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(250))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(500))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(1500))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(5500))
            line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(9090))
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim logarithmBaseUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            logarithmBaseUpDown.Minimum = 1
            AddHandler logarithmBaseUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLogarithmBaseUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Logarithm Base:", logarithmBaseUpDown))
            Dim invertedCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Inverted")
            AddHandler invertedCheckBox.CheckedChanged, AddressOf Me.OnInvertedCheckBoxCheckedChanged
            invertedCheckBox.Checked = False
            stack.Add(invertedCheckBox)
            logarithmBaseUpDown.Value = 10
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a logarithmic scale.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLogarithmBaseUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim logScale As Nevron.Nov.Chart.NLogarithmicScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLogarithmicScale)
            logScale.LogarithmBase = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnInvertedCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLogarithmicScale).Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLogarithmicScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
