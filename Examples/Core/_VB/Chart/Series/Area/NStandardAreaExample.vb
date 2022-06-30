Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Area Example
	''' </summary>
	Public Class NStandardAreaExample
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
            Nevron.Nov.Examples.Chart.NStandardAreaExample.NStandardAreaExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardAreaExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Area"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' setup X axis
			Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            scaleX.InflateContentRange = False
            scaleX.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.AutoMaxCount
            scaleX.DisplayDataPointsBetweenTicks = False
            scaleX.Labels.Visible = False

            For i As Integer = 0 To Nevron.Nov.Examples.Chart.NStandardAreaExample.monthLetters.Length - 1
                scaleX.CustomLabels.Add(New Nevron.Nov.Chart.NCustomValueLabel(i, Nevron.Nov.Examples.Chart.NStandardAreaExample.monthLetters(i)))
            Next

			' add interlaced stripe for Y axis
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            Dim scaleY As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            scaleY.Strips.Add(stripStyle)

			' setup area series
			Me.m_Area = New Nevron.Nov.Chart.NAreaSeries()
            Me.m_Area.Name = "Area Series"
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Visible = True
            dataLabelStyle.Format = "<value>"
            Me.m_Area.DataLabelStyle = dataLabelStyle

            For i As Integer = 0 To Nevron.Nov.Examples.Chart.NStandardAreaExample.monthValues.Length - 1
                Me.m_Area.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(Nevron.Nov.Examples.Chart.NStandardAreaExample.monthValues(i)))
            Next

            chart.Series.Add(Me.m_Area)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim originModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            originModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENSeriesOriginMode)()
            AddHandler originModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOriginModeComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin Mode: ", originModeComboBox))
            Dim customOriginUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler customOriginUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCustomOriginUpDownValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Custom Origin: ", customOriginUpDown))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard area chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCustomOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area.CustomOrigin = CDbl(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnOriginModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Area.OriginMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENSeriesOriginMode)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Area As Nevron.Nov.Chart.NAreaSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardAreaExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Friend Shared ReadOnly monthValues As Double() = New Double() {16, 19, 16, 15, 18, 19, 24, 21, 22, 17, 19, 15}
        Friend Shared ReadOnly monthLetters As String() = New String() {"J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D"}

		#EndRegion
	End Class
End Namespace
