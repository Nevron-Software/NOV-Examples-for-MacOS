Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Radar line example
	''' </summary>
	Public Class NRadarLineExample
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
            Nevron.Nov.Examples.Chart.NRadarLineExample.NRadarLineExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NRadarLineExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NRadarLineExample.CreateRadarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Radar Line"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NRadarChart)

			' set some axis labels
			Me.AddAxis("Vitamin A")
            Me.AddAxis("Vitamin B1")
            Me.AddAxis("Vitamin B2")
            Me.AddAxis("Vitamin B6")
            Me.AddAxis("Vitamin B12")
            Me.AddAxis("Vitamin C")
            Me.AddAxis("Vitamin D")
            Me.AddAxis("Vitamin E")
            Dim radarScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CInt((0))).Scale, Nevron.Nov.Chart.NLinearScale)
            radarScale.MajorGridLines.Visible = True
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            radarScale.Strips.Add(strip)
            Me.m_RadarLine1 = New Nevron.Nov.Chart.NRadarLineSeries()
            Me.m_Chart.Series.Add(Me.m_RadarLine1)
            Me.m_RadarLine1.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Chart.NChartTheme.BrightPalette(0))
            Me.m_RadarLine1.Name = "Series 1"
            Me.m_RadarLine1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim markerStyle1 As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle1.Visible = True
            markerStyle1.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle1.Size = New Nevron.Nov.Graphics.NSize(10, 10)
            Me.m_RadarLine1.MarkerStyle = markerStyle1
            Me.m_RadarLine2 = New Nevron.Nov.Chart.NRadarLineSeries()
            Me.m_Chart.Series.Add(Me.m_RadarLine2)
            Me.m_RadarLine2.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Chart.NChartTheme.BrightPalette(1))
            Me.m_RadarLine2.Name = "Series 2"
            Me.m_RadarLine2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim markerStyle2 As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle2.Visible = True
            markerStyle2.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle2.Size = New Nevron.Nov.Graphics.NSize(10, 10)
            Me.m_RadarLine2.MarkerStyle = markerStyle2

			' fill random data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 8 - 1
                Me.m_RadarLine1.DataPoints.Add(New Nevron.Nov.Chart.NRadarLineDataPoint(random.[Next](50, 90)))
                Me.m_RadarLine2.DataPoints.Add(New Nevron.Nov.Chart.NRadarLineDataPoint(random.[Next](0, 100)))
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim beginAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler beginAngleUpDown.ValueChanged, AddressOf Me.OnBeginAngleUpDownValueChanged
            beginAngleUpDown.Value = 90
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Angle:", beginAngleUpDown))
            Dim titleAngleModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            titleAngleModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleLabelAngleMode)()
            AddHandler titleAngleModeComboBox.SelectedIndexChanged, AddressOf Me.OnTitleAngleModeComboBoxSelectedIndexChanged
            titleAngleModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Title Angle Mode:", titleAngleModeComboBox))
            Dim titleAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler titleAngleUpDown.ValueChanged, AddressOf Me.OnTitleAngleUpDownValueChanged
            titleAngleUpDown.Value = 0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Title Angle:", titleAngleUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a radar line chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.BeginAngle = New Nevron.Nov.NAngle(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnTitleAngleModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            For i As Integer = 0 To Me.m_Chart.Axes.Count - 1
                Dim oldTitleAngle As Nevron.Nov.Chart.NScaleLabelAngle = Me.m_Chart.Axes(CInt((i))).TitleAngle
                Me.m_Chart.Axes(CInt((i))).TitleAngle = New Nevron.Nov.Chart.NScaleLabelAngle(CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENScaleLabelAngleMode), oldTitleAngle.CustomAngle)
            Next
        End Sub

        Private Sub OnTitleAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            For i As Integer = 0 To Me.m_Chart.Axes.Count - 1
                Dim oldTitleAngle As Nevron.Nov.Chart.NScaleLabelAngle = Me.m_Chart.Axes(CInt((i))).TitleAngle
                Me.m_Chart.Axes(CInt((i))).TitleAngle = New Nevron.Nov.Chart.NScaleLabelAngle(oldTitleAngle.LabelAngleMode, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub AddAxis(ByVal title As String)
            Dim axis As Nevron.Nov.Chart.NRadarAxis = New Nevron.Nov.Chart.NRadarAxis()

			' set title
			axis.Title = title
            axis.TitleAngle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
'			axis.TitleTextStyle.TextFormat = TextFormat.XML;

			' setup scale
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(axis.Scale, Nevron.Nov.Chart.NLinearScale)

            If Me.m_Chart.Axes.Count = 0 Then
				' if the first axis then configure grid style and stripes
				linearScale.MajorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Gainsboro, Nevron.Nov.Graphics.ENDashStyle.Dot)

				' add interlaced stripe
				Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
                strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGBA(200, 200, 200, 64))
                strip.Interlaced = True
                linearScale.Strips.Add(strip)
            Else
				' hide labels
				linearScale.Labels.Visible = False
            End If

            Me.m_Chart.Axes.Add(axis)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NRadarChart
        Private m_RadarLine1 As Nevron.Nov.Chart.NRadarLineSeries
        Private m_RadarLine2 As Nevron.Nov.Chart.NRadarLineSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NRadarLineExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateRadarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Radar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
