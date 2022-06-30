Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis grid lines example
	''' </summary>
	Public Class NAxisGridLinesExample
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
            Nevron.Nov.Examples.Chart.NAxisGridLinesExample.NAxisGridLinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisGridLinesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Grid"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

			' add interlaced stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            scaleY.Strips.Add(strip)

			' enable the major y grid lines
			scaleY.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)

			' enable the major x grid lines
			scaleX.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()

			' create dummy data
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.Name = "Bars"
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100)))
            Next

            Me.m_Chart.Series.Add(bar)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Y Axis Grid"))
            Dim yAxisGridColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler yAxisGridColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnYAxisGridColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", yAxisGridColor))
            yAxisGridColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim yAxisGridStyle As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            yAxisGridStyle.FillFromEnum(Of Nevron.Nov.Graphics.ENDashStyle)()
            AddHandler yAxisGridStyle.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnYAxisGridStyleSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Style:", yAxisGridStyle))
            yAxisGridStyle.SelectedIndex = CInt(Nevron.Nov.Graphics.ENDashStyle.Solid)
            stack.Add(New Nevron.Nov.UI.NLabel("X Axis Grid"))
            Dim xAxisGridColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler xAxisGridColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXAxisGridColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", xAxisGridColor))
            xAxisGridColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim xAxisGridStyle As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            xAxisGridStyle.FillFromEnum(Of Nevron.Nov.Graphics.ENDashStyle)()
            AddHandler xAxisGridStyle.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXAxisGridStyleSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Style:", xAxisGridStyle))
            xAxisGridStyle.SelectedIndex = CInt(Nevron.Nov.Graphics.ENDashStyle.Solid)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to configure the axis grid.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnXAxisGridStyleSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.MajorGridLines.Stroke.DashStyle = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Graphics.ENDashStyle)
        End Sub

        Private Sub OnXAxisGridColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.MajorGridLines.Stroke.Color = CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor
        End Sub

        Private Sub OnYAxisGridStyleSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.MajorGridLines.Stroke.DashStyle = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Graphics.ENDashStyle)
        End Sub

        Private Sub OnYAxisGridColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.MajorGridLines.Stroke.Color = CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisGridLinesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
