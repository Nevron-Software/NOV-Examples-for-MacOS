Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Tools
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' The example shows how to align different chart areas
	''' </summary>
	Public Class NAligningChartAreasExample
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
            Nevron.Nov.Examples.Chart.NAligningChartAreasExample.NAligningChartAreasExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAligningChartAreasExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Dim dockPanel As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            Me.m_ChartView.Surface.Content = dockPanel
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel()
            label.Margins = New Nevron.Nov.Graphics.NMargins(10)
            label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 12)
            label.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            label.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            label.Text = "Aligning Chart Areas"
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(label, Nevron.Nov.Layout.ENDockArea.Top)
            dockPanel.AddChild(label)

			' configure title
			Dim stackPanel As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stackPanel.UniformHeights = Nevron.Nov.Layout.ENUniformSize.Max
            stackPanel.FillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            stackPanel.FitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(stackPanel, Nevron.Nov.Layout.ENDockArea.Center)
            dockPanel.AddChild(stackPanel)
            Dim stockPriceChart As Nevron.Nov.Chart.NCartesianChart = New Nevron.Nov.Chart.NCartesianChart()
            stockPriceChart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XValueTimelineYLinear)
            stockPriceChart.FitMode = Nevron.Nov.Chart.ENCartesianChartFitMode.Stretch
            stockPriceChart.Margins = New Nevron.Nov.Graphics.NMargins(10, 0, 10, 10)
            Me.ConfigureInteractivity(stockPriceChart)
            stackPanel.AddChild(stockPriceChart)

			' setup the stock series
			Me.m_StockPrice = New Nevron.Nov.Chart.NStockSeries()
            stockPriceChart.Series.Add(Me.m_StockPrice)
            Me.m_StockPrice.Name = "Price"
            Me.m_StockPrice.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
            Me.m_StockPrice.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_StockPrice.CandleShape = Nevron.Nov.Chart.ENCandleShape.Stick
            Me.m_StockPrice.UpStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.RoyalBlue)
            Me.m_StockPrice.CandleWidth = 10
            Me.m_StockPrice.UseXValues = True
            Me.m_StockPrice.InflateMargins = False

			' setup the volume chart
			Dim stockVolumeChart As Nevron.Nov.Chart.NCartesianChart = New Nevron.Nov.Chart.NCartesianChart()
            stockVolumeChart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XValueTimelineYLinear)
            stockVolumeChart.FitMode = Nevron.Nov.Chart.ENCartesianChartFitMode.Stretch
            stockVolumeChart.Margins = New Nevron.Nov.Graphics.NMargins(10, 0, 10, 10)
            Me.ConfigureInteractivity(stockVolumeChart)
            stackPanel.AddChild(stockVolumeChart)

			' setup the stock volume series
			' setup the volume series
			Me.m_StockVolume = New Nevron.Nov.Chart.NAreaSeries()
            stockVolumeChart.Series.Add(Me.m_StockVolume)
            Me.m_StockVolume.Name = "Volume"
            Me.m_StockVolume.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_StockVolume.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
            Me.m_StockVolume.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.YellowGreen)
            Me.m_StockVolume.UseXValues = True
			
			' make sure all axes are synchronized
			stockPriceChart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).SynchronizedAxes = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Dom.NNodeRef)(New Nevron.Nov.Dom.NNodeRef(stockVolumeChart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)))
            stockVolumeChart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).SynchronizedAxes = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Dom.NNodeRef)(New Nevron.Nov.Dom.NNodeRef(stockPriceChart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryX)))
            Me.GenerateData()

			' align the left parts of those charts
			Dim guideLines As Nevron.Nov.Chart.NAlignmentGuidelineCollection = New Nevron.Nov.Chart.NAlignmentGuidelineCollection()
            Dim guideLine As Nevron.Nov.Chart.NAlignmentGuideline = New Nevron.Nov.Chart.NAlignmentGuideline()
            guideLine.ContentSide = Nevron.Nov.Chart.ENContentSide.Left
            guideLine.Targets = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Dom.NNodeRef)(New Nevron.Nov.Dom.NNodeRef() {New Nevron.Nov.Dom.NNodeRef(stockPriceChart), New Nevron.Nov.Dom.NNodeRef(stockVolumeChart)})
            guideLines.Add(guideLine)
            Me.m_ChartView.Surface.AlignmentGuidelines = guideLines
            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to align different chart areas.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub ConfigureInteractivity(ByVal chart As Nevron.Nov.Chart.NChart)
            Dim interactor As Nevron.Nov.UI.NInteractor = New Nevron.Nov.UI.NInteractor()
            Dim rectangleZoomTool As Nevron.Nov.Chart.Tools.NRectangleZoomTool = New Nevron.Nov.Chart.Tools.NRectangleZoomTool()
            rectangleZoomTool.Enabled = True
            rectangleZoomTool.VerticalValueSnapper = New Nevron.Nov.Chart.NAxisRulerMinMaxSnapper()
            interactor.Add(rectangleZoomTool)
            Dim dataPanTool As Nevron.Nov.Chart.Tools.NDataPanTool = New Nevron.Nov.Chart.Tools.NDataPanTool()
            dataPanTool.StartMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonDown
            dataPanTool.EndMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonUp
            dataPanTool.Enabled = True
            interactor.Add(dataPanTool)
            chart.Interactor = interactor
        End Sub

        Private Sub GenerateData()
            Dim open, high, low, close As Double
            Me.m_StockPrice.DataPoints.Clear()
            Me.m_StockVolume.DataPoints.Clear()
            Dim dt As System.DateTime = System.DateTime.Now - New System.TimeSpan(120, 0, 0, 0)
            Dim dPrevClose As Double = 100
            Dim dVolume As Double = 15
            Dim random As System.Random = New System.Random()

            For nIndex As Integer = 0 To 100 - 1
                open = dPrevClose

                If dPrevClose < 25 OrElse random.NextDouble() > 0.5 Then
					' upward price change
					close = open + (2 + (random.NextDouble() * 20))
                    high = close + (random.NextDouble() * 10)
                    low = open - (random.NextDouble() * 10)
                Else
					' downward price change
					close = open - (2 + (random.NextDouble() * 20))
                    high = open + (random.NextDouble() * 10)
                    low = close - (random.NextDouble() * 10)
                End If

                If low < 1 Then
                    low = 1
                End If

                While dt.DayOfWeek = System.DayOfWeek.Saturday OrElse dt.DayOfWeek = System.DayOfWeek.Sunday
                    dt = dt.AddDays(1)
                End While

				' add stock / volume data
				Me.m_StockPrice.DataPoints.Add(New Nevron.Nov.Chart.NStockDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), open, close, high, low))
                Me.m_StockVolume.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(dt), dVolume))

				' move forward
				dVolume += 10 * (0.5 - random.NextDouble())
                If dVolume <= 0 Then dVolume += 15
                dt = dt.AddDays(1)
            Next
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_StockPrice As Nevron.Nov.Chart.NStockSeries
        Private m_StockVolume As Nevron.Nov.Chart.NAreaSeries
			
		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAligningChartAreasExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
