Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Error Bar Example
	''' </summary>
	Public Class NStandardErrorBarExample
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
            Nevron.Nov.Examples.Chart.NStandardErrorBarExample.NStandardErrorBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardErrorBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Error Bar"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlace stripe
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(stripStyle)
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.MajorGridLines.Visible = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.InflateViewRangeBegin = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.InflateViewRangeEnd = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.InflateViewRangeBegin = True
            chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.InflateViewRangeEnd = True

			' add an error bar series
			Me.m_ErrorBar = New Nevron.Nov.Chart.NErrorBarSeries()
            chart.Series.Add(Me.m_ErrorBar)
            Me.m_ErrorBar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_ErrorBar.Stroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Black)
            Me.m_ErrorBar.UseXValues = True
            Me.GenerateData()
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim inflateMarginsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Inflate Margins")
            AddHandler inflateMarginsCheckBox.CheckedChanged, AddressOf Me.OnInflateMarginsCheckBoxCheckedChanged
            inflateMarginsCheckBox.Checked = True
            stack.Add(inflateMarginsCheckBox)
            Dim showUpperXErrorCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Upper X Error")
            AddHandler showUpperXErrorCheckBox.CheckedChanged, AddressOf Me.OnShowUpperXErrorCheckBoxCheckedChanged
            showUpperXErrorCheckBox.Checked = True
            stack.Add(showUpperXErrorCheckBox)
            Dim showLowerXErrorCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Lower X Error")
            AddHandler showLowerXErrorCheckBox.CheckedChanged, AddressOf Me.OnShowLowerXErrorCheckBoxCheckedChanged
            showLowerXErrorCheckBox.Checked = True
            stack.Add(showLowerXErrorCheckBox)
            Dim xErrorSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            xErrorSizeUpDown.Value = Me.m_ErrorBar.XErrorSize
            AddHandler xErrorSizeUpDown.ValueChanged, AddressOf Me.OnXErrorSizeUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("X Error Size:", xErrorSizeUpDown))
            Dim showUpperYErrorCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Upper Y Error")
            AddHandler showUpperYErrorCheckBox.CheckedChanged, AddressOf Me.OnShowUpperYErrorCheckBoxCheckedChanged
            showUpperYErrorCheckBox.Checked = True
            stack.Add(showUpperYErrorCheckBox)
            Dim showLowerYErrorCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Lower Y Error")
            AddHandler showLowerYErrorCheckBox.CheckedChanged, AddressOf Me.OnShowLowerYErrorCheckBoxCheckedChanged
            showLowerYErrorCheckBox.Checked = True
            stack.Add(showLowerYErrorCheckBox)
            Dim yErrorSizeUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            yErrorSizeUpDown.Value = Me.m_ErrorBar.YErrorSize
            AddHandler yErrorSizeUpDown.ValueChanged, AddressOf Me.OnYErrorSizeUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("X Error Size:", yErrorSizeUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the functionality of the error bar series.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData()
            Me.m_ErrorBar.DataPoints.Clear()
            Dim y As Double
            Dim x As Double = 50.0
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 15 - 1
                y = 20 + random.NextDouble() * 30
                x += 2.0 + random.NextDouble() * 2
                Dim lowerYError As Double = 1 + random.NextDouble()
                Dim upperYError As Double = 1 + random.NextDouble()
                Dim lowerXError As Double = 1 + random.NextDouble()
                Dim upperXError As Double = 1 + random.NextDouble()
                Me.m_ErrorBar.DataPoints.Add(New Nevron.Nov.Chart.NErrorBarDataPoint(x, y, upperXError, lowerXError, upperYError, lowerYError))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnInflateMarginsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.InflateMargins = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYErrorSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.YErrorSize = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnXErrorSizeUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.XErrorSize = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnShowLowerYErrorCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.ShowLowerYError = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnShowUpperYErrorCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.ShowUpperYError = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnShowLowerXErrorCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.ShowLowerXError = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnShowUpperXErrorCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ErrorBar.ShowUpperXError = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ErrorBar As Nevron.Nov.Chart.NErrorBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardErrorBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
