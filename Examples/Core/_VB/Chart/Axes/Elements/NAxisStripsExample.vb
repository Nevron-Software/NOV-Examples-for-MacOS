Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis stripes example
	''' </summary>
	Public Class NAxisStripsExample
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
            Nevron.Nov.Examples.Chart.NAxisStripsExample.NAxisStripsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisStripsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            chartView.Surface.Titles(CInt((0))).Text = "Axis Strips"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

			' add interlaced stripe
			Me.m_Strip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkGray), Nothing, True, 0, 0, 1, 1)
            Me.m_Strip.Interlaced = True
            scaleY.Strips.Add(Me.m_Strip)

			' enable the major y grid lines
			scaleY.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)

			' enable the major x grid lines
			scaleX.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()

			' create dummy data
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.Name = "Bars"
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100)))
            Next

            chart.Series.Add(bar)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Y Axis Grid"))
            Dim beginUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler beginUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnBeginUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin:", beginUpDown))
            beginUpDown.Value = 0
            Dim endUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler endUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEndUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End:", endUpDown))
            endUpDown.Value = 0
            Dim infiniteCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Infinite")
            AddHandler infiniteCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInfiniteCheckBoxCheckedChanged)
            stack.Add(infiniteCheckBox)
            infiniteCheckBox.Checked = True
            Dim lengthUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler lengthUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnLengthUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length:", lengthUpDown))
            lengthUpDown.Value = 1
            Dim intervalUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler intervalUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnIntervalUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Interval:", intervalUpDown))
            intervalUpDown.Value = 1
            Dim colorBox As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler colorBox.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnColorBoxSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color:", colorBox))
            colorBox.SelectedColor = Nevron.Nov.Graphics.NColor.DarkGray
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to configure axis strips.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnIntervalUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.Interval = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnLengthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.Length = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnColorBoxSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.Fill = New Nevron.Nov.Graphics.NColorFill(CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor)
        End Sub

        Private Sub OnInfiniteCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.Infinite = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnEndUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.[End] = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnBeginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Strip.Begin = CInt(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Strip As Nevron.Nov.Chart.NScaleStrip

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisStripsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
