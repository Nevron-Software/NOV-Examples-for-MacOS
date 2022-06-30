Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Cluster Stack Bar Labels Example
	''' </summary>
	Public Class NClusterStackBarLabelsExample
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
            Nevron.Nov.Examples.Chart.NClusterStackBarLabelsExample.NClusterStackBarLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NClusterStackBarLabelsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Cluster Stack Bar Labels"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure Y axis
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleY.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dash
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scaleY

			' add interlaced stripe for Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            scaleY.Strips.Add(stripStyle)
            Dim dataPointSafeguardSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(2, 2)

			' series 1
			Me.m_Bar1 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(Me.m_Bar1)
            Me.m_Bar1.Name = "Bar 1"
            Me.m_Bar1.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_Bar1.DataLabelStyle = Me.CreateDataLabelStyle(Nevron.Nov.ENVerticalAlignment.Center)

			' series 2
			Me.m_Bar2 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(Me.m_Bar2)
            Me.m_Bar2.Name = "Bar 2"
            Me.m_Bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            Me.m_Bar2.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.OrangeRed)
            Me.m_Bar2.DataLabelStyle = Me.CreateDataLabelStyle(Nevron.Nov.ENVerticalAlignment.Center)

			' series 3
			Me.m_Bar3 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(Me.m_Bar3)
            Me.m_Bar3.Name = "Bar 3"
            Me.m_Bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            Me.m_Bar3.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightGreen)
            Me.m_Bar3.DataLabelStyle = Me.CreateDataLabelStyle(Nevron.Nov.ENVerticalAlignment.Top)

			' enable initial labels positioning
			Me.m_Chart.LabelLayout.EnableInitialPositioning = True

			' enable label adjustment
			Me.m_Chart.LabelLayout.EnableLabelAdjustment = True

			' series 1 data points must not be overlapped
			Me.m_Bar1.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Bar1.LabelLayout.DataPointSafeguardSize = dataPointSafeguardSize

			' do not use label location proposals for series 1
			Me.m_Bar1.LabelLayout.UseLabelLocations = False

			' series 2 data points must not be overlapped
			Me.m_Bar2.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Bar2.LabelLayout.DataPointSafeguardSize = dataPointSafeguardSize

			' do not use label location proposals for series 2
			Me.m_Bar2.LabelLayout.UseLabelLocations = False

			' series 3 data points must not be overlapped
			Me.m_Bar3.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Bar3.LabelLayout.DataPointSafeguardSize = dataPointSafeguardSize

			' series 3 data labels can be placed above and below the origin point
			Me.m_Bar3.LabelLayout.UseLabelLocations = True
            Me.m_Bar3.LabelLayout.LabelLocations = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLabelLocation)(New Nevron.Nov.Chart.ENLabelLocation() {Nevron.Nov.Chart.ENLabelLocation.Top, Nevron.Nov.Chart.ENLabelLocation.Bottom})
            Me.m_Bar3.LabelLayout.InvertLocationsIfIgnored = False
            Me.m_Bar3.LabelLayout.OutOfBoundsLocationMode = Nevron.Nov.Chart.ENOutOfBoundsLocationMode.PushWithinBounds

			' fill with random data
			Me.OnGenerateDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_EnableInitialPositioningCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Initial Positioning")
            AddHandler Me.m_EnableInitialPositioningCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableInitialPositioningCheckBoxCheckedChanged)
            stack.Add(Me.m_EnableInitialPositioningCheckBox)
            Me.m_EnableLabelAdjustmentCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Label Adjustment")
            AddHandler Me.m_EnableLabelAdjustmentCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableLabelAdjustmentCheckBoxCheckedChanged)
            stack.Add(Me.m_EnableLabelAdjustmentCheckBox)
            Dim generateDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Data")
            AddHandler generateDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateDataButtonClick)
            stack.Add(generateDataButton)
            Me.m_EnableInitialPositioningCheckBox.Checked = True
            Me.m_EnableLabelAdjustmentCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how automatic data label layout works with cluster stack bar labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableLabelAdjustmentCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableLabelAdjustment = Me.m_EnableLabelAdjustmentCheckBox.Checked
        End Sub

        Private Sub OnEnableInitialPositioningCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableInitialPositioning = Me.m_EnableInitialPositioningCheckBox.Checked
        End Sub

        Private Sub OnGenerateDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar1.DataPoints.Clear()
            Me.m_Bar2.DataPoints.Clear()
            Me.m_Bar3.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](5, 20)))
                Me.m_Bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](5, 20)))
                Me.m_Bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](5, 20)))
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateDataLabelStyle(ByVal vertAlign As Nevron.Nov.ENVerticalAlignment) As Nevron.Nov.Chart.NDataLabelStyle
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Top
            dataLabelStyle.ArrowLength = 20
            dataLabelStyle.Format = "<value>"
            Return dataLabelStyle
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar1 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar2 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar3 As Nevron.Nov.Chart.NBarSeries
        Private m_EnableInitialPositioningCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EnableLabelAdjustmentCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NClusterStackBarLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
