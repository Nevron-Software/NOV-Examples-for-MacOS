Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Line Labels Example
	''' </summary>
	Public Class NLineLabelsExample
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
            Nevron.Nov.Examples.Chart.NLineLabelsExample.NLineLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLineLabelsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Line Labels"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure Y axis
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot

			' add interlaced stripe for Y axis
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            scaleY.Strips.Add(strip)

			' line series
			Me.m_Line = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Chart.Series.Add(Me.m_Line)
            Me.m_Line.InflateMargins = True
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(2, 2)
            Me.m_Line.MarkerStyle = markerStyle
            Me.m_Line.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.000")
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Top
            dataLabelStyle.ArrowLength = 10
            dataLabelStyle.ArrowStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkOrange)
            dataLabelStyle.TextStyle.Background.Border = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkOrange)
            dataLabelStyle.Format = "<value>"
            Me.m_Chart.LabelLayout.EnableInitialPositioning = True
            Me.m_Chart.LabelLayout.EnableLabelAdjustment = True
            Me.m_Line.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Line.LabelLayout.DataPointSafeguardSize = New Nevron.Nov.Graphics.NSize(2, 2)
            Me.m_Line.LabelLayout.UseLabelLocations = True
            Me.m_Line.LabelLayout.OutOfBoundsLocationMode = Nevron.Nov.Chart.ENOutOfBoundsLocationMode.PushWithinBounds
            Me.m_Line.LabelLayout.InvertLocationsIfIgnored = True

			' fill with random data
			Me.OnGenerateDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim enableInitialPositioningCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Initial Positioning")
            AddHandler enableInitialPositioningCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableInitialPositioningCheckBoxCheckedChanged)
            stack.Add(enableInitialPositioningCheckBox)
            Dim enableLabelAdjustmentCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Label Adjustment")
            AddHandler enableLabelAdjustmentCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableLabelAdjustmentCheckBoxCheckedChanged)
            stack.Add(enableLabelAdjustmentCheckBox)
            Dim generateDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Data")
            AddHandler generateDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateDataButtonClick)
            stack.Add(generateDataButton)
            enableInitialPositioningCheckBox.Checked = True
            enableLabelAdjustmentCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how automatic data label layout works with line data labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableLabelAdjustmentCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableLabelAdjustment = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnEnableInitialPositioningCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableInitialPositioning = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnGenerateDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Line.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 30 - 1
                Dim value As Double = System.Math.Sin(i * 0.2) * 10 + random.NextDouble() * 2
                Me.m_Line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(value))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Line As Nevron.Nov.Chart.NLineSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLineLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
