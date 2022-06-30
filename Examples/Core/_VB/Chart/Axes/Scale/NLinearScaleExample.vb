Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Linear Scale Example
	''' </summary>
	Public Class NLinearScaleExample
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
            Nevron.Nov.Examples.Chart.NLinearScaleExample.NLinearScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLinearScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Linear Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' configure the y axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim majorGrid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            majorGrid.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.DarkGray, Nevron.Nov.Graphics.ENDashStyle.Dot)
            linearScale.MajorGridLines = majorGrid

			' add a strip line style
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            strip.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)
            Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Chart.Series.Add(line)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 7 - 1
                line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](-100, 100)))
            Next

            line.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
            line.InflateMargins = True

			' assign marker
			Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(6, 6)
            line.MarkerStyle = markerStyle

			' assign data label style
			Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Format = "<value>"
            dataLabelStyle.ArrowStroke.Color = Nevron.Nov.Graphics.NColor.CornflowerBlue
            line.DataLabelStyle = dataLabelStyle
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_MajorTickModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_MajorTickModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENMajorTickMode)()
            AddHandler Me.m_MajorTickModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMajorTickModeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Major Tick Mode:", Me.m_MajorTickModeComboBox))
            Me.m_MinDistanceUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_MinDistanceUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinDistanceUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Min Distance:", Me.m_MinDistanceUpDown))
            Me.m_MinDistanceUpDown.Value = 25
            Me.m_MaxCountNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_MaxCountNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMaxCountNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Max Count:", Me.m_MaxCountNumericUpDown))
            Me.m_MaxCountNumericUpDown.Value = 10
            Me.m_CustomStepUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_CustomStepUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCustomStepUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Custom Step:", Me.m_CustomStepUpDown))
            Me.m_CustomStepUpDown.Value = 1
            Dim invertedCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Inverted")
            AddHandler invertedCheckBox.CheckedChanged, AddressOf Me.OnInvertedCheckBoxCheckedChanged
            invertedCheckBox.Checked = False
            stack.Add(invertedCheckBox)
            Dim generateRandomDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Random Data")
            AddHandler generateRandomDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateRandomDataButtonClick)
            stack.Add(generateRandomDataButton)
            Me.m_MajorTickModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENMajorTickMode.AutoMinDistance)
            Me.OnMajorTickModeComboBoxSelectedIndexChanged(Nothing)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a linear (numeric) scale.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnInvertedCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnGenerateRandomDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim line As Nevron.Nov.Chart.NLineSeries = CType(Me.m_Chart.Series(0), Nevron.Nov.Chart.NLineSeries)
            line.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(random.[Next](100)))
            Next
        End Sub

        Private Sub OnCustomStepUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).CustomStep = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMinDistanceUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).MinTickDistance = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnMaxCountNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).MaxTickCount = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnMajorTickModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim majorTickMode As Nevron.Nov.Chart.ENMajorTickMode = CType(Me.m_MajorTickModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENMajorTickMode)
            Me.m_MaxCountNumericUpDown.Enabled = majorTickMode = Nevron.Nov.Chart.ENMajorTickMode.AutoMaxCount
            Me.m_MinDistanceUpDown.Enabled = majorTickMode = Nevron.Nov.Chart.ENMajorTickMode.AutoMinDistance
            Me.m_CustomStepUpDown.Enabled = majorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).MajorTickMode = majorTickMode
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_MajorTickModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_MaxCountNumericUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_MinDistanceUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_CustomStepUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLinearScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
