Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Legend Position Example
	''' </summary>
	Public Class NLegendPositionExample
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
            Nevron.Nov.Examples.Chart.NLegendPositionExample.NLegendPositionExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLegendPositionExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

            ' configure title
            Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Legend Layout"

            ' configure chart
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

            ' add a bar series
            Dim bar1 As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar1.Name = "Bar1"
            bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            bar1.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            chart.Series.Add(bar1)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1
                bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim legendDockAreaComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            legendDockAreaComboBox.FillFromEnum(Of Nevron.Nov.Layout.ENDockArea)()
            AddHandler legendDockAreaComboBox.SelectedIndexChanged, AddressOf Me.OnLegendDockAreaComboBoxSelectedIndexChanged
            legendDockAreaComboBox.SelectedIndex = CInt(Nevron.Nov.Layout.ENDockArea.Right)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Dock Area: ", legendDockAreaComboBox))
            Dim dockInsideChartPlotCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Dock in Chart Plot Area")
            AddHandler dockInsideChartPlotCheckBox.CheckedChanged, AddressOf Me.OnDockInsideChartPlotCheckBoxCheckedChanged
            stack.Add(dockInsideChartPlotCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to position the legend.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnDockInsideChartPlotCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim legend As Nevron.Nov.Chart.NLegend = Me.m_ChartView.Surface.Legends(0)
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            legend.ParentNode.RemoveChild(legend)

            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                ' dock the legend inside the chart
                Dim dockPanel As Nevron.Nov.UI.NDockPanel = New Nevron.Nov.UI.NDockPanel()
                chart.Content = dockPanel
                dockPanel.Add(legend)
            Else
                ' dock the legend inside the chart
                Dim content As Nevron.Nov.UI.NDockPanel = TryCast(Me.m_ChartView.Surface.Content, Nevron.Nov.UI.NDockPanel)
                content.Add(legend)
            End If
        End Sub

        Private Sub OnLegendDockAreaComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim dockArea As Nevron.Nov.Layout.ENDockArea = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Layout.ENDockArea)
            Dim legend As Nevron.Nov.Chart.NLegend = Me.m_ChartView.Surface.Legends(0)

            ' adjust the legend layout / position accordingly to the dock area
            Select Case dockArea
                Case Nevron.Nov.Layout.ENDockArea.Left
                    legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.RowsOnly
                    legend.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                Case Nevron.Nov.Layout.ENDockArea.Top
                    legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.ColsOnly
                    legend.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                Case Nevron.Nov.Layout.ENDockArea.Right
                    legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.RowsOnly
                    legend.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                Case Nevron.Nov.Layout.ENDockArea.Bottom
                    legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.ColsOnly
                    legend.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                Case Nevron.Nov.Layout.ENDockArea.Center
                    legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.RowsOnly
                    legend.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                    legend.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            End Select

            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(legend, dockArea)
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_ChartView As Nevron.Nov.Chart.NChartView

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NLegendPositionExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
