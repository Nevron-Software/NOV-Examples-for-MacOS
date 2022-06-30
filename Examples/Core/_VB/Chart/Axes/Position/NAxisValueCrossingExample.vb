Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis value crossing example
	''' </summary>
	Public Class NAxisValueCrossingExample
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
            Nevron.Nov.Examples.Chart.NAxisValueCrossingExample.NAxisValueCrossingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisValueCrossingExample), NExampleBaseSchema)
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
            chartView.Surface.Titles(CInt((0))).Text = "Axis Value Crossing"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim primaryX As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)
            Dim primaryY As Nevron.Nov.Chart.NCartesianAxis = Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)

			' configure axes
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(primaryY.Scale, Nevron.Nov.Chart.NLinearScale)

			' configure scales
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            yScale.MajorGridLines = Me.CreateDottedGrid()
            Dim yStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 40)), Nothing, True, 0, 0, 1, 1)
            yStrip.Interlaced = True
            yScale.Strips.Add(yStrip)
            Dim xScale As Nevron.Nov.Chart.NLinearScale = CType(primaryX.Scale, Nevron.Nov.Chart.NLinearScale)
            xScale.MajorGridLines = Me.CreateDottedGrid()
            Dim xStrip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightGray, 40)), Nothing, True, 0, 0, 1, 1)
            xStrip.Interlaced = True
            xScale.Strips.Add(xStrip)

			' cross X and Y axes at their 0 values
			primaryX.Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(0, primaryY, Nevron.Nov.Chart.ENCartesianAxisOrientation.Horizontal, Nevron.Nov.Chart.ENScaleOrientation.Right, 0.0F, 100.0F)
            primaryY.Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(0, primaryX, Nevron.Nov.Chart.ENCartesianAxisOrientation.Vertical, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 100.0F)

			' setup bubble series
			Dim bubble As Nevron.Nov.Chart.NBubbleSeries = New Nevron.Nov.Chart.NBubbleSeries()
            bubble.Name = "Bubble Series"
            bubble.InflateMargins = True
            bubble.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bubble.UseXValues = True

			' fill with random data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                bubble.DataPoints.Add(New Nevron.Nov.Chart.NBubbleDataPoint(random.[Next](-20, 20), random.[Next](-20, 20), random.[Next](1, 6)))
            Next

            Me.m_Chart.Series.Add(bubble)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Vertical Axis"))
            Dim verticalAxisUsePositionCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Use Position")
            AddHandler verticalAxisUsePositionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAxisUsePositionCheckBoxCheckedChanged)
            stack.Add(verticalAxisUsePositionCheckBox)
            Me.m_VerticalAxisPositionValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_VerticalAxisPositionValueUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVerticalAxisPositionValueUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Position Value:", Me.m_VerticalAxisPositionValueUpDown))
            stack.Add(New Nevron.Nov.UI.NLabel("Horizontal Axis"))
            Dim horizontalAxisUsePositionCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Use Position")
            AddHandler horizontalAxisUsePositionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAxisUsePositionCheckBoxCheckedChanged)
            stack.Add(horizontalAxisUsePositionCheckBox)
            Me.m_HorizontalAxisPositionValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler Me.m_HorizontalAxisPositionValueUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnHorizontalAxisPositionValueUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Position Value:", Me.m_HorizontalAxisPositionValueUpDown))
            verticalAxisUsePositionCheckBox.Checked = True
            horizontalAxisUsePositionCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to cross two axes at a specified value.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateDottedGrid() As Nevron.Nov.Chart.NScaleGridLines
            Dim scaleGrid As Nevron.Nov.Chart.NScaleGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleGrid.Visible = True
            scaleGrid.Stroke.Width = 1
            scaleGrid.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            scaleGrid.Stroke.Color = Nevron.Nov.Graphics.NColor.Gray
            Return scaleGrid
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHorizontalAxisUsePositionCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim usePosition As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_HorizontalAxisPositionValueUpDown.Enabled = usePosition

            If usePosition Then
                Dim posValue As Double = Me.m_HorizontalAxisPositionValueUpDown.Value
                Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(posValue, Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxisOrientation.Horizontal, Nevron.Nov.Chart.ENScaleOrientation.Right, 0.0F, 100.0F)
            Else
                Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Bottom, True)
            End If
        End Sub

        Private Sub OnHorizontalAxisPositionValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim crossAnchor As Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor)

            If crossAnchor IsNot Nothing Then
                crossAnchor.Value = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
            End If
        End Sub

        Private Sub OnVerticalAxisPositionValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim crossAnchor As Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Anchor, Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor)

            If crossAnchor IsNot Nothing Then
                crossAnchor.Value = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
            End If
        End Sub

        Private Sub OnVerticalAxisUsePositionCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim usePosition As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_VerticalAxisPositionValueUpDown.Enabled = usePosition

            If usePosition Then
                Dim posValue As Double = Me.m_VerticalAxisPositionValueUpDown.Value
                Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Anchor = New Nevron.Nov.Chart.NValueCrossCartesianAxisAnchor(posValue, Me.m_Chart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxisOrientation.Vertical, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 100.0F)
            Else
                Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, True)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_HorizontalAxisPositionValueUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_VerticalAxisPositionValueUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisValueCrossingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
