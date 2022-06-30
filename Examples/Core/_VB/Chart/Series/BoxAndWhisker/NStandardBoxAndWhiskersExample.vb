Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Box and Whiskers Example
	''' </summary>
	Public Class NStandardBoxAndWhiskersExample
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
            Nevron.Nov.Examples.Chart.NStandardBoxAndWhiskersExample.NStandardBoxAndWhiskersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardBoxAndWhiskersExample), NExampleBaseSchema)
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

            ' configure title
            chartView.Surface.Titles(CInt((0))).Text = "Standard Box and Whiskers"

            ' configure chart
            Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            ' add interlace stripe
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)
            Me.m_BoxAndWhiskerSeries = New Nevron.Nov.Chart.NBoxAndWhiskerSeries()
            Me.m_BoxAndWhiskerSeries.WidthMode = Nevron.Nov.Chart.ENBarWidthMode.FixedWidth
            Me.m_Chart.Series.Add(Me.m_BoxAndWhiskerSeries)
            Me.m_BoxAndWhiskerSeries.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Vertical, Nevron.Nov.Graphics.ENGradientVariant.Variant4, Nevron.Nov.Graphics.NColor.LightYellow, Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_BoxAndWhiskerSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_BoxAndWhiskerSeries.MedianStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Indigo)
            Me.m_BoxAndWhiskerSeries.AverageStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.DarkRed, Nevron.Nov.Graphics.ENDashStyle.Dot)
            Me.m_BoxAndWhiskerSeries.OutlierStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkCyan)
            Me.m_BoxAndWhiskerSeries.OutlierFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
            Me.GenerateData(Me.m_BoxAndWhiskerSeries, 7)
            Return chartView
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim boxWidthUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            boxWidthUpDown.Value = Me.m_BoxAndWhiskerSeries.Width
            AddHandler boxWidthUpDown.ValueChanged, AddressOf Me.OnBoxWidthUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Box Width:", boxWidthUpDown))
            Dim whiskerWidthPercentUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            whiskerWidthPercentUpDown.Value = Me.m_BoxAndWhiskerSeries.WhiskerWidthPercent
            AddHandler whiskerWidthPercentUpDown.ValueChanged, AddressOf Me.OnWhiskerWidthPercentUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Whisker Width %:", whiskerWidthPercentUpDown))
            Dim inflateMarginsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Inflate Margins")
            AddHandler inflateMarginsCheckBox.CheckedChanged, AddressOf Me.OnInflateMarginsCheckBoxCheckedChanged
            inflateMarginsCheckBox.Checked = True
            stack.Add(inflateMarginsCheckBox)
            Dim yAxisRoundToTickCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Y Axis Round To Tick")
            AddHandler yAxisRoundToTickCheckBox.CheckedChanged, AddressOf Me.OnYAxisRoundToTickCheckBoxCheckedChanged
            yAxisRoundToTickCheckBox.Checked = True
            stack.Add(yAxisRoundToTickCheckBox)
            Dim generateDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Data")
            AddHandler generateDataButton.Click, AddressOf Me.OnGenerateDataButtonClick
            stack.Add(generateDataButton)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard box and whisker chart.</p>"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnBoxWidthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_BoxAndWhiskerSeries.Width = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnGenerateDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.GenerateData(Me.m_BoxAndWhiskerSeries, 7)
        End Sub

        Private Sub OnWhiskerWidthPercentUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_BoxAndWhiskerSeries.WhiskerWidthPercent = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnInflateMarginsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_BoxAndWhiskerSeries.InflateMargins = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnYAxisRoundToTickCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                yScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
                yScale.InflateViewRangeBegin = True
                yScale.InflateViewRangeEnd = True
            Else
                yScale.InflateViewRangeBegin = False
                yScale.InflateViewRangeEnd = False
            End If
        End Sub

        #EndRegion

        #Region"Implementation"

        Private Sub GenerateData(ByVal series As Nevron.Nov.Chart.NBoxAndWhiskerSeries, ByVal nCount As Integer)
            series.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To nCount - 1
                Dim boxLower As Double = 1000 + random.NextDouble() * 200
                Dim boxUpper As Double = boxLower + 200 + random.NextDouble() * 200
                Dim whiskersLower As Double = boxLower - (20 + random.NextDouble() * 300)
                Dim whiskersUpper As Double = boxUpper + (20 + random.NextDouble() * 300)
                Dim IQR As Double = (boxUpper - boxLower)
                Dim median As Double = boxLower + IQR * 0.25 + random.NextDouble() * IQR * 0.5
                Dim average As Double = boxLower + IQR * 0.25 + random.NextDouble() * IQR * 0.5
                Dim outliersCount As Integer = random.[Next](5)
                Dim outliers As Double() = New Double(outliersCount - 1) {}

                For k As Integer = 0 To outliersCount - 1
                    Dim outlier As Double = 0

                    If random.NextDouble() > 0.5 Then
                        outlier = boxUpper + IQR * 1.5 + random.NextDouble() * 100
                    Else
                        outlier = boxLower - IQR * 1.5 - random.NextDouble() * 100
                    End If

                    outliers(k) = outlier
                Next

                series.DataPoints.Add(New Nevron.Nov.Chart.NBoxAndWhiskerDataPoint(i, boxUpper, boxLower, median, average, whiskersUpper, whiskersLower, outliers, String.Empty))
            Next
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_BoxAndWhiskerSeries As Nevron.Nov.Chart.NBoxAndWhiskerSeries

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NStandardBoxAndWhiskersExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
