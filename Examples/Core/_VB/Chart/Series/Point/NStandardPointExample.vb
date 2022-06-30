Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Point Example
	''' </summary>
	Public Class NStandardPointExample
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
            Nevron.Nov.Examples.Chart.NStandardPointExample.NStandardPointExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardPointExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Point"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True

			' setup point series
			Me.m_Point = New Nevron.Nov.Chart.NPointSeries()

			'm_Point.DataLabelStyle.ArrowLength = 20;

			Me.m_Point.Name = "Point Series"
            Me.m_Point.InflateMargins = True
            Me.m_Point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(23, "Item1"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(67, "Item2"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(78, "Item3"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(12, "Item4"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(56, "Item5"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(43, "Item6"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(37, "Item7"))
            Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(51, "Item8"))
            Me.m_Point.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
            Me.m_Chart.Series.Add(Me.m_Point)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim inflateMarginsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler inflateMarginsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInflateMarginsCheckBoxCheckedChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Inflate Margins: ", inflateMarginsCheckBox))
            Dim verticalAxisRoundToTick As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler verticalAxisRoundToTick.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnverticalAxisRoundToTickCheckedChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Left Axis Round To Tick: ", verticalAxisRoundToTick))
            Dim pointSizeNumericUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler pointSizeNumericUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPointSizeNumericUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Point Size: ", pointSizeNumericUpDown))
            Dim pointShapeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            pointShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENPointShape)()
            AddHandler pointShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPointShapeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Point Shape: ", pointShapeComboBox))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard point chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPointShapeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.Shape = CType(TryCast(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENPointShape)
        End Sub

        Private Sub OnPointSizeNumericUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.Size = TryCast(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnverticalAxisRoundToTickCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

            If linearScale IsNot Nothing Then
                If TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                    linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
                    linearScale.InflateViewRangeBegin = True
                    linearScale.InflateViewRangeEnd = True
                Else
                    linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.Logical
                End If
            End If
        End Sub

        Private Sub OnInflateMarginsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.InflateMargins = TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardPointExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
