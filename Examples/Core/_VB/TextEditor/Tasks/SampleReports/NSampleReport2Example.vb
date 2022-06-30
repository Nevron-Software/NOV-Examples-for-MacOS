Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically manipulate text documents, find content etc.
	''' </summary>
	Public Class NSampleReport2Example
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' 
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' 
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Text.NSampleReport2Example.NSampleReport2ExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSampleReport2Example), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Dim richTextWithRibbon As Nevron.Nov.Text.NRichTextViewWithRibbon = New Nevron.Nov.Text.NRichTextViewWithRibbon()
            Me.m_RichText = richTextWithRibbon.View
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Populate the rich text
			Me.PopulateRichText()
            Return richTextWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim highlightAllChartsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Highlight All Charts")
            AddHandler highlightAllChartsButton.Click, AddressOf Me.OnHighlightAllChartsButtonClick
            stack.Add(highlightAllChartsButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to programmatically create reports as well as how to collect text elements of different type.</p>
"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Center
            paragraph.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateHeaderText("ACME Corporation"))
            paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
            paragraph.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateNormalText("Monthly Health Report"))
            section.Blocks.Add(paragraph)

			' generate sample data
			Dim sales As Double() = New Double(11) {}
            Dim hours As Double() = New Double(11) {}
            Dim profitloss As Double() = New Double(11) {}
            Dim months As String() = New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                sales(i) = 50 + random.[Next](50)
                hours(i) = 50 + random.[Next](50)
                profitloss(i) = 25 - random.[Next](50)
            Next

            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable()
            section.Blocks.Add(table)
            table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
            table.Columns.Add(New Nevron.Nov.Text.NTableColumn())

            If True Then
                Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                Dim tableCell1 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                Dim par1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateHeaderText("Sales New Projects"))
                tableCell1.Blocks.Add(par1)
                tableRow.Cells.Add(tableCell1)
                Dim tableCell2 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                tableCell2.Blocks.Add(New Nevron.Nov.Text.NParagraph())
                tableRow.Cells.Add(tableCell2)
                table.Rows.Add(tableRow)
            End If

            If True Then
                Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                Dim tableCell1 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                Dim par1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateHeaderText("Total Sales: " & Nevron.Nov.Examples.Text.NSampleReport2Example.GetTotal(CType((sales), System.[Double]())).ToString()))
                par1.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateNormalText("Last Month: " & sales(CInt((11))).ToString()))
                tableCell1.Blocks.Add(par1)
                tableRow.Cells.Add(tableCell1)
                Dim tableCell2 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                tableCell2.Blocks.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateBarChart(True, New Nevron.Nov.Graphics.NSize(400, 200), "Sales", sales, months))
                tableRow.Cells.Add(tableCell2)
                table.Rows.Add(tableRow)
            End If

            If True Then
                Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                Dim tableCell1 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                Dim par1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateHeaderText("Billable Hours: " & Nevron.Nov.Examples.Text.NSampleReport2Example.GetTotal(CType((hours), System.[Double]())).ToString()))
                par1.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateNormalText("Last Month: " & hours(CInt((11))).ToString()))
                tableCell1.Blocks.Add(par1)
                tableRow.Cells.Add(tableCell1)
                Dim tableCell2 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                tableCell2.Blocks.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateBarChart(False, New Nevron.Nov.Graphics.NSize(400, 200), "Hours", hours, months))
                tableRow.Cells.Add(tableCell2)
                table.Rows.Add(tableRow)
            End If

            If True Then
                Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                Dim tableCell1 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                Dim par1 As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateHeaderText("Profit / Loss: " & Nevron.Nov.Examples.Text.NSampleReport2Example.GetTotal(CType((profitloss), System.[Double]())).ToString()))
                par1.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                par1.Inlines.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateNormalText("Last Month: " & profitloss(CInt((11))).ToString()))
                tableCell1.Blocks.Add(par1)
                tableRow.Cells.Add(tableCell1)
                Dim tableCell2 As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                tableCell2.Blocks.Add(Nevron.Nov.Examples.Text.NSampleReport2Example.CreateBarChart(False, New Nevron.Nov.Graphics.NSize(400, 200), "Profit / Loss", hours, months))
                tableRow.Cells.Add(tableCell2)
                table.Rows.Add(tableRow)
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHighlightAllChartsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim charts As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = Me.m_RichText.Content.GetDescendants(Nevron.Nov.Chart.NChartView.NChartViewSchema)

            For i As Integer = 0 To charts.Count - 1
                Dim chartView As Nevron.Nov.Chart.NChartView = CType(charts(i), Nevron.Nov.Chart.NChartView)
                CType(chartView.Surface.Charts(CInt((0))), Nevron.Nov.Chart.NCartesianChart).PlotFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightBlue)
            Next
        End Sub


		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSampleReport2ExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetTotal(ByVal values As Double()) As Double
            Dim total As Double = 0.0

            For i As Integer = 0 To values.Length - 1
                total += values(i)
            Next

            Return total
        End Function

        Private Shared Function CreateHeaderText(ByVal text As String) As Nevron.Nov.Text.NTextInline
            Dim inline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(text)
            inline.FontSize = 14
            inline.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            Return inline
        End Function

        Private Shared Function CreateNormalText(ByVal text As String) As Nevron.Nov.Text.NTextInline
            Dim inline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(text)
            inline.FontSize = 9
            Return inline
        End Function

		''' <summary>
		''' Creates a sample bar chart given title, values and labels
		''' </summary>
		''' <paramname="area"></param>
		''' <paramname="size"></param>
		''' <paramname="title"></param>
		''' <paramname="values"></param>
		''' <paramname="labels"></param>
		''' <returns></returns>
		Private Shared Function CreateBarChart(ByVal area As Boolean, ByVal size As Nevron.Nov.Graphics.NSize, ByVal title As String, ByVal values As Double(), ByVal labels As String()) As Nevron.Nov.Text.NParagraph
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            chartView.PreferredSize = size

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = title
            chartView.Surface.Titles(CInt((0))).Margins = Nevron.Nov.Graphics.NMargins.Zero
            chartView.Surface.Legends(CInt((0))).Visibility = Nevron.Nov.UI.ENVisibility.Hidden
            chartView.BorderThickness = Nevron.Nov.Graphics.NMargins.Zero

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.Padding = New Nevron.Nov.Graphics.NMargins(20)

			' configure axes
			chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
            chart.Margins = Nevron.Nov.Graphics.NMargins.Zero

            If area Then
                Dim areaSeries As Nevron.Nov.Chart.NAreaSeries = New Nevron.Nov.Chart.NAreaSeries()
                areaSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
                areaSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                chart.Series.Add(areaSeries)

                For i As Integer = 0 To values.Length - 1
                    areaSeries.DataPoints.Add(New Nevron.Nov.Chart.NAreaDataPoint(values(i)))
                Next
            Else
                Dim barSeries As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
                barSeries.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
                barSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                chart.Series.Add(barSeries)

                For i As Integer = 0 To values.Length - 1
                    barSeries.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(values(i)))
                Next
            End If

            Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            scaleX.Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(labels)
            scaleX.MajorTickMode = Nevron.Nov.Chart.ENMajorTickMode.CustomStep
            scaleX.CustomStep = 1
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim chartInline As Nevron.Nov.Text.NWidgetInline = New Nevron.Nov.Text.NWidgetInline()
            chartInline.Content = chartView
            paragraph.Inlines.Add(chartInline)
            Return paragraph
        End Function

		#EndRegion
	End Class
End Namespace
