Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis ticks example.
	''' </summary>
	Public Class NAxisTicksExample
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
            Nevron.Nov.Examples.Chart.NAxisTicksExample.NAxisTicksExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisTicksExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            chartView.Surface.Titles(CInt((0))).Text = "Axis Ticks"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.MinorTickCount = 1
            scaleY.InnerMinorTicks.Visible = True
            scaleY.InnerMinorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black)
            scaleY.InnerMinorTicks.Length = 5
            scaleY.OuterMinorTicks.Visible = True
            scaleY.OuterMinorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black)
            scaleY.OuterMinorTicks.Length = 5
            scaleY.InnerMajorTicks.Visible = True
            scaleY.InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black)

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

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Major Outer Ticks"))
            Dim majorOuterTickColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler majorOuterTickColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMajorOuterTickColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", majorOuterTickColor))
            majorOuterTickColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim majorOuterTicksLengthNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler majorOuterTicksLengthNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMajorOuterTicksLengthNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length", majorOuterTicksLengthNumericUpDown))
            majorOuterTicksLengthNumericUpDown.Value = 10
            stack.Add(New Nevron.Nov.UI.NLabel("Major Inner Ticks"))
            Dim majorInnerTickColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler majorInnerTickColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMajorInnerTickColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", majorInnerTickColor))
            majorInnerTickColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim majorInnerTicksLengthNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler majorInnerTicksLengthNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMajorInnerTicksLengthNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length", majorInnerTicksLengthNumericUpDown))
            majorInnerTicksLengthNumericUpDown.Value = 10
            stack.Add(New Nevron.Nov.UI.NLabel("Minor Inner Ticks"))
            Dim minorInnerTickColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler minorInnerTickColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinorInnerTickColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", minorInnerTickColor))
            minorInnerTickColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim minorInnerTicksLengthNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler minorInnerTicksLengthNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinorInnerTicksLengthNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length", minorInnerTicksLengthNumericUpDown))
            minorInnerTicksLengthNumericUpDown.Value = 10
            stack.Add(New Nevron.Nov.UI.NLabel("Minor Outer Ticks"))
            Dim minorOuterTickColor As Nevron.Nov.UI.NColorBox = New Nevron.Nov.UI.NColorBox()
            AddHandler minorOuterTickColor.SelectedColorChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinorOuterTickColorSelectedColorChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color", minorOuterTickColor))
            minorOuterTickColor.SelectedColor = Nevron.Nov.Graphics.NColor.Black
            Dim minorOuterTicksLengthNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler minorOuterTicksLengthNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnMinorOuterTicksLengthNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Length", minorOuterTicksLengthNumericUpDown))
            minorOuterTicksLengthNumericUpDown.Value = 10
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to configure axis ticks.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnMinorOuterTicksLengthNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).OuterMinorTicks.Length = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMinorInnerTicksLengthNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).InnerMinorTicks.Length = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMajorOuterTicksLengthNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).OuterMajorTicks.Length = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMajorInnerTicksLengthNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).InnerMajorTicks.Length = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnMajorInnerTickColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor)
        End Sub

        Private Sub OnMinorInnerTickColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).InnerMinorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor)
        End Sub

        Private Sub OnMajorOuterTickColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).OuterMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor)
        End Sub

        Private Sub OnMinorOuterTickColorSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale).OuterMinorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(1, CType(arg.TargetNode, Nevron.Nov.UI.NColorBox).SelectedColor)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisTicksExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
