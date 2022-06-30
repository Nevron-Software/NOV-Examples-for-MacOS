Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Legend Layout Example
	''' </summary>
	Public Class NMultipleLegendsExample
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
            Nevron.Nov.Examples.Chart.NMultipleLegendsExample.NMultipleLegendsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NMultipleLegendsExample), NExampleBaseSchema)
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
            label.Text = "Multiple Legends"
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(label, Nevron.Nov.Layout.ENDockArea.Top)
            dockPanel.AddChild(label)

            ' stack panel holding content
            Dim stackPanel As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stackPanel.UniformHeights = Nevron.Nov.Layout.ENUniformSize.Max
            stackPanel.FillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            stackPanel.FitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(stackPanel, Nevron.Nov.Layout.ENDockArea.Center)
            dockPanel.AddChild(stackPanel)

            ' first group of pie + legend
            Dim firstGroupPanel As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            stackPanel.AddChild(firstGroupPanel)
            Me.m_PieChart1 = Me.CreatePieChart()
            Me.m_Legend1 = Me.CreateLegend()
            Me.m_PieChart1.Legend = Me.m_Legend1
            firstGroupPanel.AddChild(Me.m_Legend1)
            firstGroupPanel.AddChild(Me.m_PieChart1)

            ' second group of pie + legend
            Dim secondGroupPanel As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
            stackPanel.AddChild(secondGroupPanel)

            ' setup the volume chart
            Me.m_PieChart2 = Me.CreatePieChart()
            Me.m_Legend2 = Me.CreateLegend()
            Me.m_PieChart2.Legend = Me.m_Legend2
            secondGroupPanel.AddChild(Me.m_Legend2)
            secondGroupPanel.AddChild(Me.m_PieChart2)
            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim useTwoLegendsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Use Two Legends")
            useTwoLegendsCheckBox.Checked = True
            AddHandler useTwoLegendsCheckBox.CheckedChanged, AddressOf Me.OnUseTwoLegendsCheckBoxCheckedChanged
            stack.Add(useTwoLegendsCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to display series data on different legends.</p>"
        End Function

		#EndRegion

        #Region"Implementation"

        Private Function CreateLegend() As Nevron.Nov.Chart.NLegend
            Dim legend As Nevron.Nov.Chart.NLegend = New Nevron.Nov.Chart.NLegend()
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(legend, Nevron.Nov.Layout.ENDockArea.Center)
            legend.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Right
            legend.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return legend
        End Function

        Private Function CreatePieChart() As Nevron.Nov.Chart.NPieChart
            Dim pieChart As Nevron.Nov.Chart.NPieChart = New Nevron.Nov.Chart.NPieChart()
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(pieChart, Nevron.Nov.Layout.ENDockArea.Center)
            pieChart.Margins = New Nevron.Nov.Graphics.NMargins(10, 0, 10, 10)
            Dim pieSeries As Nevron.Nov.Chart.NPieSeries = New Nevron.Nov.Chart.NPieSeries()
            pieChart.Series.Add(pieSeries)
            pieChart.DockSpiderLabelsToSides = False
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.ArrowLength = 15
            dataLabelStyle.ArrowPointerLength = 0
            pieSeries.DataLabelStyle = dataLabelStyle
            pieSeries.LabelMode = Nevron.Nov.Chart.ENPieLabelMode.Spider
            pieSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            pieSeries.LegendView.Format = "<label> <percent>"
            pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(24, "Cars"))
            pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(18, "Airplanes"))
            pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(32, "Trains"))
            pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(23, "Ships"))
            pieSeries.DataPoints.Add(New Nevron.Nov.Chart.NPieDataPoint(19, "Buses"))
            Return pieChart
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnUseTwoLegendsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_PieChart1.Legend = Me.m_Legend1
                Me.m_PieChart2.Legend = Me.m_Legend2
            Else
                Me.m_PieChart1.Legend = Me.m_Legend1
                Me.m_PieChart2.Legend = Me.m_Legend1
            End If
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_PieChart1 As Nevron.Nov.Chart.NPieChart
        Private m_Legend1 As Nevron.Nov.Chart.NLegend
        Private m_PieChart2 As Nevron.Nov.Chart.NPieChart
        Private m_Legend2 As Nevron.Nov.Chart.NLegend

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NMultipleLegendsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
