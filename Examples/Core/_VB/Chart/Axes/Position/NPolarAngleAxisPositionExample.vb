Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Demonstrates how to position polar angle axes
	''' </summary>
	Public Class NPolarAngleAxisPositionExample
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
            Nevron.Nov.Examples.Chart.NPolarAngleAxisPositionExample.NPolarAngleAxisPositionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarAngleAxisPositionExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarAngleAxisPositionExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Angle Axis Position"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPolarChart)
            Me.m_Chart.SetPredefinedPolarAxes(Nevron.Nov.Chart.ENPredefinedPolarAxes.AngleSecondaryAngleValue)

			' setup chart
			Me.m_Chart.InnerRadius = 40

			' create a polar line series
			Dim series1 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series1)
            series1.Name = "Series 1"
            series1.CloseContour = True
            series1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            series1.MarkerStyle = New Nevron.Nov.Chart.NMarkerStyle(False)
            series1.UseXValues = True
            Call Nevron.Nov.Examples.Chart.NPolarAngleAxisPositionExample.Curve1(series1, 50)

			' create a polar line series
			Dim series2 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series2)
            series2.Name = "Series 2"
            series2.CloseContour = True
            series2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            series2.MarkerStyle = New Nevron.Nov.Chart.NMarkerStyle(False)
            series2.UseXValues = True
            Call Nevron.Nov.Examples.Chart.NPolarAngleAxisPositionExample.Curve2(series2, 100)

			' setup polar axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            linearScale.InflateViewRangeBegin = True
            linearScale.InflateViewRangeEnd = True
            linearScale.MajorGridLines.Visible = True
            linearScale.MajorGridLines.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Gray, Nevron.Nov.Graphics.ENDashStyle.Solid)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' setup polar angle axis
			Dim degreeScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            degreeScale.MajorGridLines.Visible = True
            degreeScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)

			' add a second value axes
			Dim valueAxis As Nevron.Nov.Chart.NPolarAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue)
            Me.m_RedAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle)
            Me.m_GreenAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.SecondaryAngle)
            Dim gradScale As Nevron.Nov.Chart.NAngularScale = New Nevron.Nov.Chart.NAngularScale()
            gradScale.AngleUnit = Nevron.Nov.NUnit.Grad
            gradScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            Me.m_GreenAxis.Scale = gradScale
            Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(70, Me.m_RedAxis, Nevron.Nov.Chart.ENPolarAxisOrientation.Angle, Nevron.Nov.Chart.ENScaleOrientation.Right)
            Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NDockPolarAxisAnchor(Nevron.Nov.Chart.ENPolarAxisDockZone.OuterRim)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(90, Me.m_RedAxis, Nevron.Nov.Chart.ENPolarAxisOrientation.Value, Nevron.Nov.Chart.ENScaleOrientation.Auto)

			' apply style sheet
			' color code the axes and series after the stylesheet is applied
			Me.m_RedAxis.Scale.SetColor(Nevron.Nov.Graphics.NColor.Red)
            Me.m_GreenAxis.Scale.SetColor(Nevron.Nov.Graphics.NColor.Green)
            series2.ValueAxis = Me.m_GreenAxis
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)

			' begin angle
			Dim beginAngleUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler beginAngleUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBeginAngleUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Angle:", beginAngleUpDown))

			' red axis controls
			stack.Add(New Nevron.Nov.UI.NLabel("Degree Axis (Red):"))
            Dim dockRedAxisCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Dock")
            stack.Add(dockRedAxisCheckBox)
            Me.m_RedAxisCrossValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Cross Value:", Me.m_RedAxisCrossValueUpDown))

			' green axis controls
			stack.Add(New Nevron.Nov.UI.NLabel("Grad Axis (Green):"))
            Dim dockGreenAxisCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Dock")
            stack.Add(dockGreenAxisCheckBox)
            Me.m_GreenAxisCrossValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Cross Value:", Me.m_GreenAxisCrossValueUpDown))

			' wire events
			AddHandler dockRedAxisCheckBox.CheckedChanged, AddressOf Me.OnDockRedAxisCheckBoxCheckedChanged
            AddHandler Me.m_RedAxisCrossValueUpDown.ValueChanged, AddressOf Me.OnRedAxisCrossValueUpDownValueChanged
            AddHandler dockGreenAxisCheckBox.CheckedChanged, AddressOf Me.OnDockGreenAxisCheckBoxCheckedChanged
            AddHandler Me.m_GreenAxisCrossValueUpDown.ValueChanged, AddressOf Me.OnGreenAxisCrossValueUpDownValueChanged

			' init values
			Me.m_RedAxisCrossValueUpDown.Value = 50
            dockRedAxisCheckBox.Checked = True
            dockGreenAxisCheckBox.Checked = False
            Me.m_GreenAxisCrossValueUpDown.Value = 70
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to control the polar angle axis position.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.BeginAngle = New Nevron.Nov.NAngle(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Nevron.Nov.NUnit.Degree)
        End Sub

        Private Sub OnDockGreenAxisCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NDockPolarAxisAnchor(Nevron.Nov.Chart.ENPolarAxisDockZone.OuterRim)
                Me.m_GreenAxisCrossValueUpDown.Enabled = False
            Else
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_GreenAxisCrossValueUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxisOrientation.Angle, Nevron.Nov.Chart.ENScaleOrientation.Auto)
                Me.m_GreenAxisCrossValueUpDown.Enabled = True
            End If
        End Sub

        Private Sub OnGreenAxisCrossValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_GreenAxisCrossValueUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxisOrientation.Angle, Nevron.Nov.Chart.ENScaleOrientation.Auto)
        End Sub

        Private Sub OnDockRedAxisCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NDockPolarAxisAnchor(Nevron.Nov.Chart.ENPolarAxisDockZone.OuterRim)
                Me.m_RedAxisCrossValueUpDown.Enabled = False
            Else
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_RedAxisCrossValueUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxisOrientation.Angle, Nevron.Nov.Chart.ENScaleOrientation.Auto)
                Me.m_RedAxisCrossValueUpDown.Enabled = True
            End If
        End Sub

        Private Sub OnRedAxisCrossValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_RedAxisCrossValueUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxisOrientation.Angle, Nevron.Nov.Chart.ENScaleOrientation.Auto)
        End Sub

		#EndRegion

		#Region"Implementation"

		Friend Shared Sub Curve1(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 100 * System.Math.Cos(angle)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(angle * 180 / System.Math.PI, radius))
            Next
        End Sub

        Friend Shared Sub Curve2(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 33 + 100 * System.Math.Sin(2 * angle) + 1.7 * System.Math.Cos(2 * angle)
                radius = System.Math.Abs(radius)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(angle * 180 / System.Math.PI, radius))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart
        Private m_RedAxis As Nevron.Nov.Chart.NPolarAxis
        Private m_GreenAxis As Nevron.Nov.Chart.NPolarAxis
        Private m_GreenAxisCrossValueUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RedAxisCrossValueUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarAngleAxisPositionExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreatePolarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Polar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
