Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis ruler caps example
	''' </summary>
	Public Class NAxisRulerCapsExample
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
            Nevron.Nov.Examples.Chart.NAxisRulerCapsExample.NAxisRulerCapsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisRulerCapsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Ruler Caps"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.Padding = New Nevron.Nov.Graphics.NMargins(20)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' feed some random data 
			Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
            point.UseXValues = True
            point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' fill in some random data
			Dim random As System.Random = New System.Random()

            For j As Integer = 0 To 30 - 1
                point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(5 + random.[Next](90), 5 + random.[Next](90)))
            Next

            Me.m_Chart.Series.Add(point)

			' X Axis
			Dim xScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            xScale.MajorGridLines = Me.CreateScaleGrid()
            Dim xScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak = New Nevron.Nov.Chart.NCustomScaleBreak()
            xScaleBreak.Style = Nevron.Nov.Chart.ENScaleBreakStyle.Line
            xScaleBreak.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Orange, 124))
            xScaleBreak.Length = 20
            xScaleBreak.Range = New Nevron.Nov.Graphics.NRange(29, 41)
            xScale.ScaleBreaks.Add(xScaleBreak)

			' add an interlaced strip to the X axis
			Dim xInterlacedStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            xInterlacedStrip.Interlaced = True
            xInterlacedStrip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 125))
            xScale.Strips.Add(xInterlacedStrip)
            Dim xAxis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)
            xAxis.Scale = xScale

			'			xAxis.ViewRangeMode = ENAxisViewRangeMode.FixedRange;
			'			xAxis.MinViewRangeValue = 0;
			'			xAxis.MaxViewRangeValue = 100;

			Dim xAxisAnchor As Nevron.Nov.Chart.NDockCartesianAxisAnchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Bottom)
            xAxisAnchor.BeforeSpace = 10
            xAxis.Anchor = xAxisAnchor

			' Y Axis
			Dim yScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            yScale.MajorGridLines = Me.CreateScaleGrid()
            Dim yScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak = New Nevron.Nov.Chart.NCustomScaleBreak()
            yScaleBreak.Style = Nevron.Nov.Chart.ENScaleBreakStyle.Line
            yScaleBreak.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Orange, 124))
            yScaleBreak.Length = 20
            yScaleBreak.Range = New Nevron.Nov.Graphics.NRange(29, 41)
            yScale.ScaleBreaks.Add(yScaleBreak)

			' add an interlaced strip to the Y axis
			Dim yInterlacedStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            yInterlacedStrip.Interlaced = True
            yInterlacedStrip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 125))
            yInterlacedStrip.Interval = 1
            yInterlacedStrip.Length = 1
            yScale.Strips.Add(yInterlacedStrip)
            Dim yAxis As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)
            yAxis.Scale = yScale

			'			yAxis.ViewRangeMode = ENAxisViewRangeMode.FixedRange;
			'			yAxis.MinViewRangeValue = 0;
			'			yAxis.MaxViewRangeValue = 100;

			Dim yAxisAnchor As Nevron.Nov.Chart.NDockCartesianAxisAnchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left)
            yAxisAnchor.BeforeSpace = 10
            yAxis.Anchor = yAxisAnchor
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_BeginCapShapeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_BeginCapShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCapShape)()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Cap Shape:", Me.m_BeginCapShapeComboBox))
            Me.m_BeginCapShapeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENCapShape.Ellipse)
            Me.m_ScaleBreakCapShapeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ScaleBreakCapShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCapShape)()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Scale Break Cap Shape:", Me.m_ScaleBreakCapShapeComboBox))
            Me.m_ScaleBreakCapShapeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENCapShape.VerticalLine)
            Me.m_EndCapShapeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_EndCapShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCapShape)()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End Cap Shape:", Me.m_EndCapShapeComboBox))
            Me.m_EndCapShapeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENCapShape.Arrow)
            Me.m_PaintOnScaleBreaksCheckBox = New Nevron.Nov.UI.NCheckBox("Paint on Scale Breaks")
            stack.Add(Me.m_PaintOnScaleBreaksCheckBox)
            Me.m_PaintOnScaleBreaksCheckBox.Checked = False
            Me.m_SizeUpDown = New Nevron.Nov.UI.NNumericUpDown()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Cap Size:", Me.m_SizeUpDown))
            Me.m_SizeUpDown.Value = 5

			' wire for events
			AddHandler Me.m_BeginCapShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRulerStyleChanged)
            AddHandler Me.m_ScaleBreakCapShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRulerStyleChanged)
            AddHandler Me.m_EndCapShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRulerStyleChanged)
            AddHandler Me.m_PaintOnScaleBreaksCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRulerStyleChanged)
            AddHandler Me.m_SizeUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRulerStyleChanged)
            Me.OnRulerStyleChanged(Nothing)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to alter the appearance of axis ruler caps.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub UpdateRulerStyleForAxis(ByVal axis As Nevron.Nov.Chart.NCartesianAxis)
            Dim scale As Nevron.Nov.Chart.NStandardScale = CType(axis.Scale, Nevron.Nov.Chart.NStandardScale)
            Dim capSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(Me.m_SizeUpDown.Value, Me.m_SizeUpDown.Value)

			' apply style to begin and end caps
			scale.Ruler.BeginCap = New Nevron.Nov.Chart.NRulerCapStyle(CType(Me.m_BeginCapShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENCapShape), capSize, 0, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black))
            scale.Ruler.EndCap = New Nevron.Nov.Chart.NRulerCapStyle(CType(Me.m_EndCapShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENCapShape), capSize, 3, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black))
            scale.Ruler.ScaleBreakCap = New Nevron.Nov.Chart.NRulerCapStyle(CType(Me.m_ScaleBreakCapShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENCapShape), capSize, 0, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black))
            scale.Ruler.PaintOnScaleBreaks = Me.m_PaintOnScaleBreaksCheckBox.Checked
        End Sub

        Private Function CreateScaleGrid() As Nevron.Nov.Chart.NScaleGridLines
            Dim scaleGrid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleGrid.Visible = True
            scaleGrid.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Gray, Nevron.Nov.Graphics.ENDashStyle.Dash)
            Return scaleGrid
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRulerStyleChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateRulerStyleForAxis(Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX))
            Me.UpdateRulerStyleForAxis(Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_BeginCapShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_ScaleBreakCapShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_EndCapShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_PaintOnScaleBreaksCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_SizeUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisRulerCapsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
