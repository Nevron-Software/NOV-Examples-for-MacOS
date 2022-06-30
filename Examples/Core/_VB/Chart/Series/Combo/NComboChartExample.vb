Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Combo Chart Example
	''' </summary>
	Public Class NComboChartExample
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
            Nevron.Nov.Examples.Chart.NComboChartExample.NComboChartExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NComboChartExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Combo Chart"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' Setup the primary Y axis
			Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Title.Text = "Number of Occurences"

			' add interlace stripe
			Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale.Strips.Add(strip)

			' Setup the secondary Y axis
			Dim scaleY2 As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleY2.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter("0%"))
            scaleY2.Title.Text = "Cumulative Percent"
            Dim axisY2 As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            Me.m_Chart.Axes.Add(axisY2)
            axisY2.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Right)
            axisY2.Visible = True
            axisY2.Scale = scaleY2
            axisY2.ViewRangeMode = Nevron.Nov.Chart.ENAxisViewRangeMode.FixedRange
            axisY2.MinViewRangeValue = 0
            axisY2.MaxViewRangeValue = 1

			' add the bar series
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(bar)
            bar.Name = "Bar Series"
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' add the line series
			Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Chart.Series.Add(line)
            line.Name = "Cumulative %"
            line.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(10, 10)
            markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            line.MarkerStyle = markerStyle
            line.VerticalAxis = axisY2

			' fill with random data and sort in descending order
			Dim count As Integer = 10
            Dim randomValues As Nevron.Nov.DataStructures.NList(Of Double) = New Nevron.Nov.DataStructures.NList(Of Double)()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To count - 1
                randomValues.Add(random.[Next](100, 700))
            Next

            randomValues.Sort()

            For i As Integer = 0 To randomValues.Count - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(randomValues(i)))
            Next

			' calculate cumulative sum of the bar values
			Dim cs As Double = 0
            Dim arrCumulative As Double() = New Double(count - 1) {}

            For i As Integer = 0 To count - 1
                cs += randomValues(i)
                arrCumulative(i) = cs
            Next

            If cs > 0 Then
                For i As Integer = 0 To count - 1
                    arrCumulative(i) /= cs
                    line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(arrCumulative(i)))
                Next
            End If

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a combo chart consisting of two series (NOV Chart supports an unlimited number of series).</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateLegendFormatCombo() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Dim item As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Value and Label")
            item.Tag = "<value> <label>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Value")
            item.Tag = "<value>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Label")
            item.Tag = "<label>"
            comboBox.Items.Add(item)
            item = New Nevron.Nov.UI.NComboBoxItem("Size")
            item.Tag = "<size>"
            comboBox.Items.Add(item)
            Return comboBox
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NComboChartExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
