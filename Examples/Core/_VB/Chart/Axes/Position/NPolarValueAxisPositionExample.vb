Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Demonstrates how to position polar value axes
	''' </summary>
	Public Class NPolarValueAxisPositionExample
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
            Nevron.Nov.Examples.Chart.NPolarValueAxisPositionExample.NPolarValueAxisPositionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPolarValueAxisPositionExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NPolarValueAxisPositionExample.CreatePolarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Polar Value Axis Position"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NPolarChart)
            Me.m_Chart.SetPredefinedPolarAxes(Nevron.Nov.Chart.ENPredefinedPolarAxes.AngleValue)

			' setup chart
			Me.m_Chart.InnerRadius = 20

			' setup polar axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            linearScale.InflateViewRangeBegin = True
            linearScale.InflateViewRangeEnd = True
            linearScale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)(New Nevron.Nov.Chart.ENLevelLabelsLayout() {Nevron.Nov.Chart.ENLevelLabelsLayout.AutoScale})
            linearScale.MajorGridLines.Visible = True
            linearScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dash

			' setup polar angle axis
			Dim angularScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            angularScale.MajorGridLines.Visible = True
            angularScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 0)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(192, 192, 192, 125))
            strip.Interlaced = True
            angularScale.Strips.Add(strip)

			' add a const line
			Dim referenceLine As Nevron.Nov.Chart.NAxisReferenceLine = New Nevron.Nov.Chart.NAxisReferenceLine()
            referenceLine.Value = 1.0
            referenceLine.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.SlateBlue)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryValue), Nevron.Nov.Chart.ENPolarAxis)).ReferenceLines.Add(referenceLine)

			' create a polar line series
			Dim series1 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series1)
            series1.Name = "Series 1"
            series1.CloseContour = True
            series1.UseXValues = True
            series1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = False
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(2, 2)
            series1.MarkerStyle = markerStyle
            Call Nevron.Nov.Examples.Chart.NPolarValueAxisPositionExample.Curve1(series1, 50)

			' create a polar line series
			Dim series2 As Nevron.Nov.Chart.NPolarLineSeries = New Nevron.Nov.Chart.NPolarLineSeries()
            Me.m_Chart.Series.Add(series2)
            series2.Name = "Series 2"
            series2.CloseContour = True
            series2.UseXValues = True
            series2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            markerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = False
            series2.MarkerStyle = markerStyle
            Call Nevron.Nov.Examples.Chart.NPolarValueAxisPositionExample.Curve2(series2, 100)

			' add a second value axes
			Me.m_RedAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryValue)
            Me.m_GreenAxis = Me.m_Chart.AddCustomAxis(Nevron.Nov.Chart.ENPolarAxisOrientation.Value)
            Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(0.0, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxisOrientation.Value, Nevron.Nov.Chart.ENScaleOrientation.Auto)
            Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(90, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxisOrientation.Value, Nevron.Nov.Chart.ENScaleOrientation.Auto)

			' color code the axes and series after the stylesheet is applied
			Me.m_RedAxis.Scale.SetColor(Nevron.Nov.Graphics.NColor.Red)
            Me.m_GreenAxis.Scale.SetColor(Nevron.Nov.Graphics.NColor.Green)
            series1.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.DarkRed)
            series2.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.DarkGreen)
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

			' radian angle step
			Dim radianAngleStepComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            radianAngleStepComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("15"))
            radianAngleStepComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("30"))
            radianAngleStepComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("45"))
            radianAngleStepComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("90"))
            AddHandler radianAngleStepComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRadianAngleStepComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Radian Angle Step", radianAngleStepComboBox))

			' red axis position
			stack.Add(New Nevron.Nov.UI.NLabel("Red Axis:"))

            If True Then
                Dim dockRedAxisToBottomCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Dock Bottom")
                AddHandler dockRedAxisToBottomCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDockRedAxisToBottomCheckBoxCheckedChanged)
                stack.Add(dockRedAxisToBottomCheckBox)
                Me.m_RedAxisAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
                Me.m_RedAxisAngleUpDown.Value = 0
                AddHandler Me.m_RedAxisAngleUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRedAxisAngleUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Angle:", Me.m_RedAxisAngleUpDown))
                Dim redAxisPaintRefelectionCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint Reflection")
                redAxisPaintRefelectionCheckBox.Checked = True
                AddHandler redAxisPaintRefelectionCheckBox.CheckedChanged, AddressOf Me.OnRedAxisPaintRefelectionCheckBoxCheckedChanged
                stack.Add(redAxisPaintRefelectionCheckBox)
                Me.m_RedAxisScaleLabelAngleMode = New Nevron.Nov.UI.NComboBox()
                Me.m_RedAxisScaleLabelAngleMode.FillFromEnum(Of Nevron.Nov.Chart.ENScaleLabelAngleMode)()
                Me.m_RedAxisScaleLabelAngleMode.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleLabelAngleMode.View)
                AddHandler Me.m_RedAxisScaleLabelAngleMode.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateRedAxisSaleLabelAngle)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Scale Label Angle Mode:", Me.m_RedAxisScaleLabelAngleMode))
                Me.m_RedAxisSaleLabelAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
                AddHandler Me.m_RedAxisSaleLabelAngleUpDown.ValueChanged, AddressOf Me.OnUpdateRedAxisSaleLabelAngle
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Scale Label Angle:", Me.m_RedAxisSaleLabelAngleUpDown))
                Dim redAxisBeginPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
                redAxisBeginPercentUpDown.Value = Me.m_RedAxis.Anchor.BeginPercent
                AddHandler redAxisBeginPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRedAxisBeginPercentUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin percent:", redAxisBeginPercentUpDown))
                Dim redAxisEndPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
                redAxisEndPercentUpDown.Value = Me.m_RedAxis.Anchor.EndPercent
                AddHandler redAxisEndPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRedAxisEndPercentUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("End percent:", redAxisEndPercentUpDown))
            End If

			' green axis position
			stack.Add(New Nevron.Nov.UI.NLabel("Green Axis:"))

            If True Then
                Dim dockGreenAxisToLeftCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Dock Left")
                AddHandler dockGreenAxisToLeftCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDockGreenAxisToLeftCheckBoxCheckedChanged)
                stack.Add(dockGreenAxisToLeftCheckBox)
                Me.m_GreenAxisAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
                Me.m_GreenAxisAngleUpDown.Value = 90
                AddHandler Me.m_GreenAxisAngleUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnGreenAxisAngleUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Angle:", Me.m_GreenAxisAngleUpDown))
                Dim greenAxisPaintRefelectionCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint Reflection")
                greenAxisPaintRefelectionCheckBox.Checked = True
                AddHandler greenAxisPaintRefelectionCheckBox.CheckedChanged, AddressOf Me.OnGreenAxisPaintRefelectionCheckBoxCheckedChanged
                stack.Add(greenAxisPaintRefelectionCheckBox)
                Me.m_GreenAxisScaleLabelAngleMode = New Nevron.Nov.UI.NComboBox()
                Me.m_GreenAxisScaleLabelAngleMode.FillFromEnum(Of Nevron.Nov.Chart.ENScaleLabelAngleMode)()
                Me.m_GreenAxisScaleLabelAngleMode.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleLabelAngleMode.View)
                AddHandler Me.m_GreenAxisScaleLabelAngleMode.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateGreenAxisSaleLabelAngle)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Scale Label Angle Mode:", Me.m_GreenAxisScaleLabelAngleMode))
                Me.m_GreenAxisSaleLabelAngleUpDown = New Nevron.Nov.UI.NNumericUpDown()
                AddHandler Me.m_GreenAxisSaleLabelAngleUpDown.ValueChanged, AddressOf Me.OnUpdateGreenAxisSaleLabelAngle
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Scale Label Angle:", Me.m_GreenAxisSaleLabelAngleUpDown))
                Dim greenAxisBeginPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
                greenAxisBeginPercentUpDown.Value = Me.m_GreenAxis.Anchor.BeginPercent
                AddHandler greenAxisBeginPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnGreenAxisBeginPercentUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin percent:", greenAxisBeginPercentUpDown))
                Dim greenAxisEndPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
                greenAxisEndPercentUpDown.Value = Me.m_GreenAxis.Anchor.EndPercent
                AddHandler greenAxisEndPercentUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnGreenAxisEndPercentUpDownValueChanged)
                stack.Add(Nevron.Nov.UI.NPairBox.Create("End percent:", greenAxisEndPercentUpDown))
            End If

            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to control the polar value axis position.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnUpdateGreenAxisSaleLabelAngle(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            TryCast(Me.m_GreenAxis.Scale, Nevron.Nov.Chart.NLinearScale).Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(CType(Me.m_GreenAxisScaleLabelAngleMode.SelectedIndex, Nevron.Nov.Chart.ENScaleLabelAngleMode), Me.m_GreenAxisSaleLabelAngleUpDown.Value)
        End Sub

        Private Sub OnGreenAxisPaintRefelectionCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_GreenAxis.PaintReflection = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnGreenAxisEndPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_GreenAxis.Anchor.EndPercent = CSng(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnGreenAxisBeginPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_GreenAxis.Anchor.BeginPercent = CSng(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnGreenAxisAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim valueCrossPolarAnchor As Nevron.Nov.Chart.NValueCrossPolarAxisAnchor = TryCast(Me.m_GreenAxis.Anchor, Nevron.Nov.Chart.NValueCrossPolarAxisAnchor)

            If valueCrossPolarAnchor IsNot Nothing Then
                valueCrossPolarAnchor.Value = Me.m_GreenAxisAngleUpDown.Value
            End If
        End Sub

        Private Sub OnDockGreenAxisToLeftCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NDockPolarAxisAnchor(Nevron.Nov.Chart.ENPolarAxisDockZone.Left)
                Me.m_GreenAxisAngleUpDown.Enabled = False
            Else
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_GreenAxisAngleUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxisOrientation.Value, Nevron.Nov.Chart.ENScaleOrientation.Auto)
                Me.m_GreenAxisAngleUpDown.Enabled = True
            End If
        End Sub

        Private Sub OnUpdateRedAxisSaleLabelAngle(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            TryCast(Me.m_RedAxis.Scale, Nevron.Nov.Chart.NLinearScale).Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(CType(Me.m_RedAxisScaleLabelAngleMode.SelectedIndex, Nevron.Nov.Chart.ENScaleLabelAngleMode), Me.m_RedAxisSaleLabelAngleUpDown.Value)
        End Sub

        Private Sub OnRedAxisPaintRefelectionCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RedAxis.PaintReflection = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnRedAxisEndPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RedAxis.Anchor.EndPercent = CSng(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnRedAxisBeginPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RedAxis.Anchor.BeginPercent = CSng(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnRedAxisAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim valueCrossPolarAnchor As Nevron.Nov.Chart.NValueCrossPolarAxisAnchor = TryCast(Me.m_RedAxis.Anchor, Nevron.Nov.Chart.NValueCrossPolarAxisAnchor)

            If valueCrossPolarAnchor IsNot Nothing Then
                valueCrossPolarAnchor.Value = Me.m_RedAxisAngleUpDown.Value
            End If
        End Sub

        Private Sub OnDockRedAxisToBottomCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NDockPolarAxisAnchor(Nevron.Nov.Chart.ENPolarAxisDockZone.Bottom)
                Me.m_RedAxisAngleUpDown.Enabled = False
            Else
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NValueCrossPolarAxisAnchor(Me.m_RedAxisAngleUpDown.Value, Me.m_Chart.Axes(Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxisOrientation.Value, Nevron.Nov.Chart.ENScaleOrientation.Auto)
                Me.m_RedAxisAngleUpDown.Enabled = True
            End If
        End Sub

        Private Sub OnRadianAngleStepComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim angleScale As Nevron.Nov.Chart.NAngularScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENPolarAxis.PrimaryAngle), Nevron.Nov.Chart.ENPolarAxis)).Scale, Nevron.Nov.Chart.NAngularScale)
            angleScale.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep

            Select Case CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex
                Case 0
                    angleScale.CustomStep = New Nevron.Nov.NAngle(15, Nevron.Nov.NUnit.Degree)
                Case 1
                    angleScale.CustomStep = New Nevron.Nov.NAngle(30, Nevron.Nov.NUnit.Degree)
                Case 2
                    angleScale.CustomStep = New Nevron.Nov.NAngle(45, Nevron.Nov.NUnit.Degree)
                Case 3
                    angleScale.CustomStep = New Nevron.Nov.NAngle(90, Nevron.Nov.NUnit.Degree)
                Case Else
                    Call Nevron.Nov.NDebug.Assert(False)
            End Select
        End Sub

        Private Sub OnBeginAngleUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.BeginAngle = New Nevron.Nov.NAngle(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Nevron.Nov.NUnit.Degree)
        End Sub

		#EndRegion

		#Region"Implementation"

		Friend Shared Sub Curve1(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 1 + System.Math.Cos(angle)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(CDbl((angle * 180.0 / System.Math.PI)), radius))
            Next
        End Sub

        Friend Shared Sub Curve2(ByVal series As Nevron.Nov.Chart.NPolarLineSeries, ByVal count As Integer)
            series.DataPoints.Clear()
            Dim angleStep As Double = 2 * System.Math.PI / count

            For i As Integer = 0 To count - 1
                Dim angle As Double = i * angleStep
                Dim radius As Double = 0.2 + 1.7 * System.Math.Sin(2 * angle) + 1.7 * System.Math.Cos(2 * angle)
                radius = System.Math.Abs(radius)
                series.DataPoints.Add(New Nevron.Nov.Chart.NPolarLineDataPoint(CDbl(angle) * 180.0 / System.Math.PI, radius))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NPolarChart
        Private m_RedAxis As Nevron.Nov.Chart.NPolarAxis
        Private m_GreenAxis As Nevron.Nov.Chart.NPolarAxis
        Private m_RedAxisAngleUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_GreenAxisAngleUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RedAxisSaleLabelAngleUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RedAxisScaleLabelAngleMode As Nevron.Nov.UI.NComboBox
        Private m_GreenAxisSaleLabelAngleUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_GreenAxisScaleLabelAngleMode As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPolarValueAxisPositionExampleSchema As Nevron.Nov.Dom.NSchema

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
