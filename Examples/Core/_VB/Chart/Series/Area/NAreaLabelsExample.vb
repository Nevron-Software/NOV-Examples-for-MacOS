Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Area Example
	''' </summary>
	Public Class NAreaLabelsExample
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
            Nevron.Nov.Examples.Chart.NAreaLabelsExample.NAreaLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAreaLabelsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Area Labels"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlaced stripe for Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.Strips.Add(stripStyle)
            scaleY.MajorGridLines.Visible = True
            Me.m_Area = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Chart.Series.Add(Me.m_Area)

			' setup area series
			Me.m_Area.InflateMargins = True
            Me.m_Area.UseXValues = True
            Me.m_Area.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.000")
            Me.m_Area.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_Area.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Top
            dataLabelStyle.ArrowLength = 20
            dataLabelStyle.ArrowStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black)
            dataLabelStyle.Format = "<value>"
            dataLabelStyle.TextStyle.Background.Visible = True
            dataLabelStyle.TextStyle.Background.Padding = New Nevron.Nov.Graphics.NMargins(0)
            Me.m_Area.DataLabelStyle = dataLabelStyle

			' disable initial label positioning
			Me.m_Chart.LabelLayout.EnableInitialPositioning = False

			' enable label adjustment
			Me.m_Chart.LabelLayout.EnableLabelAdjustment = True

			' enable the data point safesuard and set its size
			Me.m_Area.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Area.LabelLayout.DataPointSafeguardSize = New Nevron.Nov.Graphics.NSize(2, 2)

			' fill with random data
			Me.OnGenerateDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim enableLabelAdjustmentCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Label Adjustment")
            AddHandler enableLabelAdjustmentCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableLabelAdjustmentCheckBoxCheckedChanged)
            stack.Add(enableLabelAdjustmentCheckBox)
            enableLabelAdjustmentCheckBox.Checked = True
            Dim enableDataPointSafeGuardCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Data Point Safeguard")
            AddHandler enableDataPointSafeGuardCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableDataPointSafeGuardCheckBoxCheckedChanged)
            stack.Add(enableDataPointSafeGuardCheckBox)
            enableDataPointSafeGuardCheckBox.Checked = True
            Dim generateDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Data")
            AddHandler generateDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateDataButtonClick)
            stack.Add(generateDataButton)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how the automatic data label layout works with area data labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableLabelAdjustmentCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableLabelAdjustment = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnEnableDataPointSafeGuardCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area.LabelLayout.EnableDataPointSafeguard = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnGenerateDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Area.DataPoints.Clear()
            Dim xvalue As Double = 10
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 24 - 1
                Dim value As Double = System.Math.Sin(i * 0.4) * 5 + random.NextDouble() * 3
                xvalue += 1 + random.NextDouble() * 20
                Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(xvalue, value))
            Next
        End Sub


		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Area As Nevron.Nov.Chart.NAreaSeries

		#EndRegion

		#Region"Static Fields"

		Friend Shared monthValues As Double() = New Double() {16, 19, 16, 15, 18, 19, 24, 21, 22, 17, 19, 15}
        Friend Shared monthLetters As String() = New String() {"J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D"}

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAreaLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
