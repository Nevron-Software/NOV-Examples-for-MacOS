Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Legend Appearance Example
	''' </summary>
	Public Class NLegendAppearanceExample
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
            Nevron.Nov.Examples.Chart.NLegendAppearanceExample.NLegendAppearanceExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NLegendAppearanceExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

            ' configure title
            chartView.Surface.Titles(CInt((0))).Text = "Legend Appearance"
            Me.m_Legend = chartView.Surface.Legends(0)
            Me.m_Legend.ExpandMode = Nevron.Nov.Chart.ENLegendExpandMode.ColsFixed
            Me.m_Legend.ColCount = 3
            Me.m_Legend.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            Me.m_Legend.BorderThickness = New Nevron.Nov.Graphics.NMargins(2)
            Me.m_Legend.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.LightGray)
            Me.m_Legend.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top

            ' configure chart
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

            ' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            'linearScale.Strips.Add(strip);

			' setup a bar series
			Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.Name = "Bar Series"
            bar.InflateMargins = True
            bar.UseXValues = False
            bar.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints

			' add some data to the bar series
			bar.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.DataPoints
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(18, "C++"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(15, "Ruby"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(21, "Python"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(23, "Java"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(27, "Javascript"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(29, "C#"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(26, "PHP"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(17, "Objective C"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(24, "SQL"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(13, "Object Pascal"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(19, "Visual Basic"))
            bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(16, "Open Edge ABL"))
            chart.Series.Add(bar)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim legendHeaderTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            AddHandler legendHeaderTextBox.TextChanged, AddressOf Me.OnLegendHeaderTextBoxChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Header: ", legendHeaderTextBox))
            Dim legendFooterTextBox As Nevron.Nov.UI.NTextBox = New Nevron.Nov.UI.NTextBox()
            AddHandler legendFooterTextBox.TextChanged, AddressOf Me.OnLegendFooterTextBoxChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Footer: ", legendFooterTextBox))
            Me.m_HorizontalInterlaceStripesCheckBox = New Nevron.Nov.UI.NCheckBox("Horizontal Interlace Stripes")
            AddHandler Me.m_HorizontalInterlaceStripesCheckBox.CheckedChanged, AddressOf Me.OnVerticalInterlaceStripesCheckBoxCheckedChanged
            stack.Add(Me.m_HorizontalInterlaceStripesCheckBox)
            Me.m_VerticalInterlaceStripesCheckBox = New Nevron.Nov.UI.NCheckBox("Vertical Interlace Stripes")
            AddHandler Me.m_VerticalInterlaceStripesCheckBox.CheckedChanged, AddressOf Me.OnHorizontalInterlaceStripesCheckBoxCheckedChanged
            stack.Add(Me.m_VerticalInterlaceStripesCheckBox)
            Dim showHorizontalGridLinesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Horizontal Gridlines")
            showHorizontalGridLinesCheckBox.Checked = True
            AddHandler showHorizontalGridLinesCheckBox.CheckedChanged, AddressOf Me.OnShowHorizontalGridLinesCheckBoxCheckedChanged
            stack.Add(showHorizontalGridLinesCheckBox)
            Dim showVerticalGridLinesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Vertical Gridlines")
            showVerticalGridLinesCheckBox.Checked = True
            AddHandler showVerticalGridLinesCheckBox.CheckedChanged, AddressOf Me.OnShowVerticalGridLinesCheckBoxCheckedChanged
            stack.Add(showVerticalGridLinesCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to modify the legend appearance.</p>"
        End Function

		#EndRegion

        #Region"Implementation"

        Private Sub ApplyInterlaceStyles()
            Dim interlaceStyles As Nevron.Nov.Chart.NLegendInterlaceStylesCollection = New Nevron.Nov.Chart.NLegendInterlaceStylesCollection()

            If Me.m_HorizontalInterlaceStripesCheckBox.Checked Then
                Dim horzInterlaceStyle As Nevron.Nov.Chart.NLegendInterlaceStyle = New Nevron.Nov.Chart.NLegendInterlaceStyle()
                horzInterlaceStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.LightBlue, 0.5F))
                horzInterlaceStyle.Type = Nevron.Nov.Chart.ENLegendInterlaceStyleType.Row
                horzInterlaceStyle.Length = 1
                horzInterlaceStyle.Interval = 1
                interlaceStyles.Add(horzInterlaceStyle)
            End If

            If Me.m_VerticalInterlaceStripesCheckBox.Checked Then
                Dim vertInterlaceStyle As Nevron.Nov.Chart.NLegendInterlaceStyle = New Nevron.Nov.Chart.NLegendInterlaceStyle()
                vertInterlaceStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.DarkGray, 0.5F))
                vertInterlaceStyle.Type = Nevron.Nov.Chart.ENLegendInterlaceStyleType.Col
                vertInterlaceStyle.Length = 1
                vertInterlaceStyle.Interval = 1
                interlaceStyles.Add(vertInterlaceStyle)
            End If

            Me.m_Legend.InterlaceStyles = interlaceStyles
        End Sub

        #EndRegion

		#Region"Event Handlers"

        Private Sub OnShowVerticalGridLinesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_Legend.ClearLocalValue(Nevron.Nov.Chart.NLegend.VerticalGridStrokeProperty)
            Else
                Me.m_Legend.VerticalGridStroke = Nothing
            End If
        End Sub

        Private Sub OnShowHorizontalGridLinesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_Legend.ClearLocalValue(Nevron.Nov.Chart.NLegend.HorizontalGridStrokeProperty)
            Else
                Me.m_Legend.HorizontalGridStroke = Nothing
            End If
        End Sub

        Private Sub OnLegendHeaderTextBoxChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim header As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(CType(arg.TargetNode, Nevron.Nov.UI.NTextBox).Text)
            header.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            header.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            header.Font = New Nevron.Nov.Graphics.NFont("Arimo", 14, Nevron.Nov.Graphics.ENFontStyle.Bold)
            Me.m_Legend.Header = header
        End Sub

        Private Sub OnLegendFooterTextBoxChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim footer As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(CType(arg.TargetNode, Nevron.Nov.UI.NTextBox).Text)
            footer.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            footer.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            footer.Font = New Nevron.Nov.Graphics.NFont("Arimo", 14, Nevron.Nov.Graphics.ENFontStyle.Bold)
            Me.m_Legend.Footer = footer
        End Sub

        Private Sub OnVerticalInterlaceStripesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.ApplyInterlaceStyles()
        End Sub

        Private Sub OnHorizontalInterlaceStripesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.ApplyInterlaceStyles()
        End Sub

		#EndRegion

        #Region"Fields"

        Private m_Legend As Nevron.Nov.Chart.NLegend
        Private m_HorizontalInterlaceStripesCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_VerticalInterlaceStripesCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLegendAppearanceExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
