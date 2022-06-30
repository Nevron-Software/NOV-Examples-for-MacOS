Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Bar Labels Example
	''' </summary>
	Public Class NBarLabelsExample
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
            Nevron.Nov.Examples.Chart.NBarLabelsExample.NBarLabelsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NBarLabelsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Bar Labels"

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

			' bar series
			Me.m_Bar = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.000")
            Me.m_Chart.Series.Add(Me.m_Bar)
            Me.m_Bar.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Top
            dataLabelStyle.ArrowLength = 20
            dataLabelStyle.Format = "<value>"
            Me.m_Bar.DataLabelStyle = dataLabelStyle

			' enable initial labels positioning
			Me.m_Chart.LabelLayout.EnableInitialPositioning = True

			' enable label adjustment
			Me.m_Chart.LabelLayout.EnableLabelAdjustment = True

			' use only "top" location for the labels
			Me.m_Bar.LabelLayout.UseLabelLocations = True
            Me.m_Bar.LabelLayout.LabelLocations = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLabelLocation)(New Nevron.Nov.Chart.ENLabelLocation() {Nevron.Nov.Chart.ENLabelLocation.Top})
            Me.m_Bar.LabelLayout.OutOfBoundsLocationMode = Nevron.Nov.Chart.ENOutOfBoundsLocationMode.PushWithinBounds
            Me.m_Bar.LabelLayout.InvertLocationsIfIgnored = True

			' protect data points from being overlapped
			Me.m_Bar.LabelLayout.EnableDataPointSafeguard = True
            Me.m_Bar.LabelLayout.DataPointSafeguardSize = New Nevron.Nov.Graphics.NSize(2, 2)

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
            Me.m_RemoveOverlappedLabelsCheckBox = New Nevron.Nov.UI.NCheckBox("Remove Overlapped Labels")
            AddHandler Me.m_RemoveOverlappedLabelsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRemoveOverlappedLabelsCheckBoxCheckedChanged)
            stack.Add(Me.m_RemoveOverlappedLabelsCheckBox)
            Me.m_EnableLabelAdjustmentCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Label Adjustment")
            AddHandler Me.m_EnableLabelAdjustmentCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableLabelAdjustmentCheckBoxCheckedChanged)
            stack.Add(Me.m_EnableLabelAdjustmentCheckBox)
            Me.m_LocationsComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_LocationsComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Top"))
            Me.m_LocationsComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Top - Bottom"))
            AddHandler Me.m_LocationsComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLocationsComboBoxSelectedIndexChanged)
            stack.Add(Me.m_LocationsComboBox)
            Dim generateDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Data")
            AddHandler generateDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateDataButtonClick)
            stack.Add(generateDataButton)
            Me.m_EnableInitialPositioningCheckBox.Checked = True
            Me.m_RemoveOverlappedLabelsCheckBox.Checked = True
            Me.m_EnableLabelAdjustmentCheckBox.Checked = True
            Me.m_LocationsComboBox.SelectedIndex = 0
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how automatic data label layout works with bar data labels.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableLabelAdjustmentCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.EnableLabelAdjustment = Me.m_EnableLabelAdjustmentCheckBox.Checked
        End Sub

        Private Sub OnRemoveOverlappedLabelsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.LabelLayout.RemoveOverlappedLabels = Me.m_RemoveOverlappedLabelsCheckBox.Checked
        End Sub

        Private Sub OnEnableInitialPositioningCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RemoveOverlappedLabelsCheckBox.Enabled = Me.m_EnableInitialPositioningCheckBox.Checked
            Me.m_LocationsComboBox.Enabled = Me.m_EnableInitialPositioningCheckBox.Checked
            Me.m_Chart.LabelLayout.EnableInitialPositioning = Me.m_EnableInitialPositioningCheckBox.Checked
        End Sub

        Private Sub OnLocationsComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim locations As Nevron.Nov.Chart.ENLabelLocation()

            Select Case Me.m_LocationsComboBox.SelectedIndex
                Case 0
                    locations = New Nevron.Nov.Chart.ENLabelLocation() {Nevron.Nov.Chart.ENLabelLocation.Top}
                Case 1
                    locations = New Nevron.Nov.Chart.ENLabelLocation() {Nevron.Nov.Chart.ENLabelLocation.Top, Nevron.Nov.Chart.ENLabelLocation.Bottom}
                Case Else
                    Call Nevron.Nov.NDebug.Assert(False)
                    locations = Nothing
            End Select

            Me.m_Bar.LabelLayout.LabelLocations = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLabelLocation)(locations)
        End Sub

        Private Sub OnGenerateDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 30 - 1
                Dim value As Double = System.Math.Sin(i * 0.2) * 10 + random.NextDouble() * 4
                Me.m_Bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(value))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar As Nevron.Nov.Chart.NBarSeries
        Private m_EnableInitialPositioningCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_RemoveOverlappedLabelsCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EnableLabelAdjustmentCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_LocationsComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBarLabelsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
