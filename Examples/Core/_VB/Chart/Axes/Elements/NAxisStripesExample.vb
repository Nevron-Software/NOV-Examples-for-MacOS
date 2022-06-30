Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis stripes example
	''' </summary>
	Public Class NAxisStripesExample
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
            Nevron.Nov.Examples.Chart.NAxisStripesExample.NAxisStripesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisStripesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Stripes"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure the y scale
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            yScale.Strips.Add(stripStyle)
            yScale.MajorGridLines.Visible = True

			' Create a point series
			Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
            point.InflateMargins = True
            point.UseXValues = True
            point.Name = "Series 1"
            chart.Series.Add(point)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = False
            point.DataLabelStyle = dataLabelStyle

			' Add some data
			Dim yValues As Double() = New Double() {31, 67, 12, 84, 90}
            Dim xValues As Double() = New Double() {5, 36, 51, 76, 93}

            For i As Integer = 0 To yValues.Length - 1
                point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(xValues(i), yValues(i)))
            Next

			' Add a constline for the left axis
			Me.m_XStripe = New Nevron.Nov.Chart.NAxisStripe()
            Me.m_XStripe.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.SteelBlue, 0.5F))
            Me.m_XStripe.Range = New Nevron.Nov.Graphics.NRange(40, 60)
            Me.m_XStripe.Text = "X Axis Stripe"
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Stripes.Add(Me.m_XStripe)

			' Add a constline for the bottom axis
			Me.m_YStripe = New Nevron.Nov.Chart.NAxisStripe()
            Me.m_YStripe.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.SteelBlue, 0.5F))
            Me.m_YStripe.Range = New Nevron.Nov.Graphics.NRange(40, 60)
            Me.m_YStripe.Text = "Y Axis Stripe"
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Stripes.Add(Me.m_YStripe)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Y Axis Stripe"))
            Dim yStripeBeginValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler yStripeBeginValueUpDown.ValueChanged, AddressOf Me.OnYStripeBeginValueUpDownValueChanged
            yStripeBeginValueUpDown.Value = Me.m_YStripe.Range.Begin
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Value:", yStripeBeginValueUpDown))
            Dim yStripeEndValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler yStripeEndValueUpDown.ValueChanged, AddressOf Me.OnYStripeEndValueUpDownValueChanged
            yStripeEndValueUpDown.Value = Me.m_YStripe.Range.[End]
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End Value:", yStripeEndValueUpDown))
            Dim yStripeIncludeInAxisRangeCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Include in Axis Range")
            yStripeIncludeInAxisRangeCheckBox.Checked = False
            AddHandler yStripeIncludeInAxisRangeCheckBox.CheckedChanged, AddressOf Me.OnStripeIncludeInAxisRangeCheckBoxCheckedChanged
            stack.Add(yStripeIncludeInAxisRangeCheckBox)
            Dim yStripePaintAfterSeriesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint After Series")
            yStripePaintAfterSeriesCheckBox.Checked = Me.m_YStripe.PaintAfterChartContent
            AddHandler yStripePaintAfterSeriesCheckBox.CheckedChanged, AddressOf Me.OnYStripePaintAfterSeriesCheckBoxCheckedChanged
            stack.Add(yStripePaintAfterSeriesCheckBox)
            Dim yTextAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            yTextAlignmentComboBox.FillFromEnum(Of Nevron.Nov.ENContentAlignment)()
            yTextAlignmentComboBox.SelectedIndex = CInt(Me.m_YStripe.TextAlignment)
            AddHandler yTextAlignmentComboBox.SelectedIndexChanged, AddressOf Me.OnYTextAlignmentComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text Alignment:", yTextAlignmentComboBox))
            stack.Add(New Nevron.Nov.UI.NLabel("X Axis Stripe"))
            Dim xStripeBeginValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            xStripeBeginValueUpDown.Value = Me.m_XStripe.Range.Begin
            AddHandler xStripeBeginValueUpDown.ValueChanged, AddressOf Me.OnXStripeBeginValueUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin Value:", xStripeBeginValueUpDown))
            Dim xStripeEndValueUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            xStripeEndValueUpDown.Value = Me.m_XStripe.Range.[End]
            AddHandler xStripeEndValueUpDown.ValueChanged, AddressOf Me.OnXStripeEndValueUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End Value:", xStripeEndValueUpDown))
            Dim xStripeIncludeInAxisRangeCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Include in Axis Range")
            xStripeIncludeInAxisRangeCheckBox.Checked = False
            AddHandler xStripeIncludeInAxisRangeCheckBox.CheckedChanged, AddressOf Me.OnXStripeIncludeInAxisRangeCheckBoxCheckedChanged
            stack.Add(xStripeIncludeInAxisRangeCheckBox)
            Dim xStripePaintAfterSeriesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Paint After Series")
            xStripePaintAfterSeriesCheckBox.Checked = Me.m_XStripe.PaintAfterChartContent
            AddHandler xStripePaintAfterSeriesCheckBox.CheckedChanged, AddressOf Me.OnXStripePaintAfterSeriesCheckBoxCheckedChanged
            stack.Add(xStripePaintAfterSeriesCheckBox)
            Dim xTextAlignmentComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            xTextAlignmentComboBox.FillFromEnum(Of Nevron.Nov.ENContentAlignment)()
            xTextAlignmentComboBox.SelectedIndex = CInt(Me.m_XStripe.TextAlignment)
            AddHandler xTextAlignmentComboBox.SelectedIndexChanged, AddressOf Me.OnXTextAlignmentComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text Alignment:", xTextAlignmentComboBox))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to configure axis stripes.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnXStripeBeginValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XStripe.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_XStripe.Range.[End])
        End Sub

        Private Sub OnXStripeEndValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XStripe.Range = New Nevron.Nov.Graphics.NRange(Me.m_XStripe.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnXStripeIncludeInAxisRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_XStripe.IncludeRangeBeginInAxisRange = isChecked
            Me.m_XStripe.IncludeRangeEndInAxisRange = isChecked
        End Sub

        Private Sub OnXStripePaintAfterSeriesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XStripe.PaintAfterChartContent = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnXTextAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_XStripe.TextAlignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENContentAlignment)
        End Sub

        Private Sub OnYStripeBeginValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YStripe.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_YStripe.Range.[End])
        End Sub

        Private Sub OnYStripeEndValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YStripe.Range = New Nevron.Nov.Graphics.NRange(Me.m_YStripe.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnStripeIncludeInAxisRangeCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim isChecked As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_YStripe.IncludeRangeBeginInAxisRange = isChecked
            Me.m_YStripe.IncludeRangeEndInAxisRange = isChecked
        End Sub

        Private Sub OnYStripePaintAfterSeriesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YStripe.PaintAfterChartContent = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYTextAlignmentComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_YStripe.TextAlignment = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENContentAlignment)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_XStripe As Nevron.Nov.Chart.NAxisStripe
        Private m_YStripe As Nevron.Nov.Chart.NAxisStripe


		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisStripesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
