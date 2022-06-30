Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Chart orientation example
	''' </summary>
	Public Class NChartOrientationExample
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
            Nevron.Nov.Examples.Chart.NChartOrientationExample.NChartOrientationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NChartOrientationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Chart Orientation"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.Orientation = Nevron.Nov.Chart.ENCartesianChartOrientation.LeftToRight
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' add a bar series
			Me.m_Bar1 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            Me.m_Bar1.Name = "Bar 1"
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Format = "<value>"
            dataLabelStyle.Visible = True
            Me.m_Bar1.DataLabelStyle = dataLabelStyle
            Me.m_Bar1.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            Me.m_Chart.Series.Add(Me.m_Bar1)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Me.OnPositiveDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim orientationComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            orientationComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCartesianChartOrientation)()
            orientationComboBox.SelectedIndex = CInt(Me.m_Chart.Orientation)
            AddHandler orientationComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOrientationComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Orientation:", orientationComboBox))
            Dim positiveDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive Values")
            AddHandler positiveDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveDataButtonClick)
            stack.Add(positiveDataButton)
            Dim positiveAndNegativeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive and Negative Values")
            AddHandler positiveAndNegativeDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveAndNegativeDataButtonClick)
            stack.Add(positiveAndNegativeDataButton)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how change the chart orientation. This feature allows you to display left to right and right to left charts</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOrientationComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Orientation = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENCartesianChartOrientation)
        End Sub

        Private Sub OnPositiveAndNegativeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar1.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100) - 50))
            Next
        End Sub

        Private Sub OnPositiveDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar1.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar1 As Nevron.Nov.Chart.NBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NChartOrientationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
