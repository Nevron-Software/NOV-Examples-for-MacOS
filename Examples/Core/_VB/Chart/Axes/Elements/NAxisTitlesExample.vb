Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis titles example.
	''' </summary>
	Public Class NAxisTitlesExample
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
            Nevron.Nov.Examples.Chart.NAxisTitlesExample.NAxisTitlesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisTitlesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            chartView.Surface.Titles(CInt((0))).Text = "Axis Titles"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

			' add interlaced stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            scaleY.Strips.Add(strip)
            Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)

			' create dummy data
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.Name = "Bars"
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100)))
            Next

            Me.m_Chart.Series.Add(bar)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("X Axis Title"))
            Dim xAxisTitleTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            AddHandler xAxisTitleTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXAxisTitleTextBoxTextChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text:", xAxisTitleTextBox))
            xAxisTitleTextBox.Text = "X Axis Title"
            Dim xOffsetUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler xOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXOffsetUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", xOffsetUpDown))
            xOffsetUpDown.Value = 10
            Dim xAlignmentCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            xAlignmentCombo.FillFromEnum(Of Nevron.Nov.ENHorizontalAlignment)()
            AddHandler xAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnXAlignmentComboSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", xAlignmentCombo))
            xAlignmentCombo.SelectedIndex = CInt(Nevron.Nov.ENHorizontalAlignment.Center)
            stack.Add(New Nevron.Nov.UI.NLabel("Y Axis Title"))
            Dim yAxisTitleTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            AddHandler yAxisTitleTextBox.TextChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnYAxisTitleTextBoxTextChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Text:", yAxisTitleTextBox))
            yAxisTitleTextBox.Text = "Y Axis Title"
            Dim yOffsetUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler yOffsetUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnYOffsetUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", yOffsetUpDown))
            yOffsetUpDown.Value = 10
            Dim yAlignmentCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            yAlignmentCombo.FillFromEnum(Of Nevron.Nov.ENHorizontalAlignment)()
            AddHandler yAlignmentCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnYAlignmentComboSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Offset:", yAlignmentCombo))
            yAlignmentCombo.SelectedIndex = CInt(Nevron.Nov.ENHorizontalAlignment.Center)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the most important features of the axis titles.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Shared Sub SetAlignment(ByVal title As Nevron.Nov.Chart.NScaleTitle, ByVal alignment As Nevron.Nov.ENHorizontalAlignment)
            title.RulerAlignment = alignment

            Select Case alignment
                Case Nevron.Nov.ENHorizontalAlignment.Left
                    title.ContentAlignment = Nevron.Nov.ENContentAlignment.MiddleLeft
                Case Nevron.Nov.ENHorizontalAlignment.Center
                    title.ContentAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
                Case Nevron.Nov.ENHorizontalAlignment.Right
                    title.ContentAlignment = Nevron.Nov.ENContentAlignment.MiddleRight
            End Select
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnXAxisTitleTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title.Text = CType(arg.TargetNode, Nevron.Nov.UI.NTextBox).Text
        End Sub

        Private Sub OnXOffsetUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title.Offset = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnXAlignmentComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Call Nevron.Nov.Examples.Chart.NAxisTitlesExample.SetAlignment(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title, CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENHorizontalAlignment))
        End Sub

        Private Sub OnYAxisTitleTextBoxTextChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title.Text = CType(arg.TargetNode, Nevron.Nov.UI.NTextBox).Text
        End Sub

        Private Sub OnYOffsetUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title.Offset = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnYAlignmentComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Call Nevron.Nov.Examples.Chart.NAxisTitlesExample.SetAlignment(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title, CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.ENHorizontalAlignment))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisTitlesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
