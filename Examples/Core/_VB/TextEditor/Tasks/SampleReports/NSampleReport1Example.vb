Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create a sample report.
	''' </summary>
	Public Class NSampleReport1Example
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
            Nevron.Nov.Examples.Text.NSampleReport1Example.NSampleReport1ExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NSampleReport1Example), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Protected Overrides"

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
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to programmatically create a sample report.</p>"
        End Function

        Private Sub PopulateRichText()
            If True Then
                Dim headerSection As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                Me.m_RichText.Content.Sections.Add(headerSection)
                headerSection.Blocks.Add(Me.CreateTitleParagraph("Welcome to our annual report. Further information on Sample Group can be found at: www.samplegroup.com"))
                headerSection.Blocks.Add(Me.CreateContentParagraph("Sample Group is a diversified international market infrastructure and capital markets business sitting at the heart of the world’s financial community."))
                headerSection.Blocks.Add(Me.CreateContentParagraph("The Group operates a broad range of international equity, bond and derivatives markets, including Stock Exchange; Europe’s leading fixed income market; and a pan-European equities MTF. Through its platforms, the Group offers international business and investors unrivalled access to Europe’s capital markets."))
                headerSection.Blocks.Add(Me.CreateContentParagraph("Post trade and risk management services are a significant part of the Group’s business operations. In addition to majority ownership of multi-asset global CCP operator, Sunset Group, the Group operates G&B, a clearing house; Monte Span, the European settlement business; and AutoSettle, the Group’s newly established central securities depository based in Luxembourg. The Group is a global leader in indexing and analytic solutions. The Group also provides customers with an extensive range of real time and reference data products. The Group is a leading developer of high performance trading platforms and capital markets software for customers around the world, through MillenniumIT. Since December 2014, the Group has owned Bonita Investments, an investment management business."))
                headerSection.Blocks.Add(Me.CreateContentParagraph("Headquartered in London, with significant operations in North America, China and Russia, the Group employs approximately 6000 people"))
            End If

            If True Then
                Dim financialHighlightsSection As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                financialHighlightsSection.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                Me.m_RichText.Content.Sections.Add(financialHighlightsSection)
                financialHighlightsSection.Blocks.Add(Me.CreateTitleParagraph("Financial highlights"))
                financialHighlightsSection.Blocks.Add(Me.CreateContentParagraph("The following charts provide insight to the group's total income, operating profit, and earnings per share for the years since 2008."))
                Dim chartSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 200)

                If True Then
                    Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable()
                    table.AllowSpacingBetweenCells = False
                    table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
                    table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
                    financialHighlightsSection.Blocks.Add(table)

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)

                        If True Then
                            Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                            tableCell.Blocks.Add(Me.CreateSampleBarChart(chartSize, "Adjusted total income", New Double() {674.9, 814.8, 852.9, 1, 213.1, 1, 043.9, 1, 096.4, 1, 381.1}, New String() {"2008", "2009", "2010", "2011", "2012", "2013", "2014"}))
                            tableRow.Cells.Add(tableCell)
                        End If

                        If True Then
                            Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                            tableCell.Blocks.Add(Me.CreateSampleBarChart(chartSize, "Adjusted operating profit", New Double() {341.1, 441.9, 430.2, 514.7, 417.5, 479.9, 558.0}, New String() {"2008", "2009", "2010", "2011", "2012", "2013", "2014"}))
                            tableRow.Cells.Add(tableCell)
                        End If
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)

                        If True Then
                            Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                            tableCell.Blocks.Add(Me.CreateSampleBarChart(chartSize, "Operating profit", New Double() {283.0, 358.5, 348.4, 353.1, 242.1, 329.4, 346.0}, New String() {"2008", "2009", "2010", "2011", "2012", "2013", "2014"}))
                            tableRow.Cells.Add(tableCell)
                        End If

                        If True Then
                            Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
                            tableCell.Blocks.Add(Me.CreateSampleBarChart(chartSize, "Adjusted earnings per share", New Double() {67.9, 92.6, 97.0, 98.6, 75.6, 96.5, 103.3}, New String() {"2008", "2009", "2010", "2011", "2012", "2013", "2014"}))
                            tableRow.Cells.Add(tableCell)
                        End If
                    End If
                End If
            End If

            If True Then
                Dim operationalHighlights As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
                operationalHighlights.ColumnCount = 2
                operationalHighlights.BreakType = Nevron.Nov.Text.ENSectionBreakType.NextPage
                operationalHighlights.Blocks.Add(Me.CreateTitleParagraph("Operational highlights"))
                Me.m_RichText.Content.Sections.Add(operationalHighlights)
                operationalHighlights.Blocks.Add(Me.CreateContentParagraph("The Group is delivering on its strategy, leveraging its range of products and services and further diversifying its offering through new product development and strategic investments. A few examples of the progress being made are highlighted below: "))
                operationalHighlights.Blocks.Add(Me.CreateContentParagraph("Capital Markets"))

                If True Then
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.Bullet)
                    Me.m_RichText.Content.BulletLists.Add(bulletList)

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Revenues for calendar year 2014 increased by 12 per cent to £333.2 million (2013: £296.8 million). Primary Markets saw a seven year high in new issue activity with 219 new companies admitted, including AA, the largest UK capital raising IPO of the year")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("UK cash equity average daily value traded increased 15 per cent and average daily number of trades in Italy increased 16 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Average daily value traded on Turquoise, our European cash equities MTF, increased 42 per cent to €3.7 billion per day and share of European trading increased to over 9 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("In Fixed Income, MTS cash and BondVision value traded increased by 32 per cent, while MTS Repo value traded increased by 3 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If
                End If

                operationalHighlights.Blocks.Add(Me.CreateContentParagraph("Post Trade Services"))

                If True Then
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.Bullet)
                    Me.m_RichText.Content.BulletLists.Add(bulletList)

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Revenues for calendar year 2014 increased by 3 per cent in constant currency terms. In sterling terms revenues declined by 2 per cent to £96.5 million")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Our Group  cleared 69.7 million equity trades, up 16 per cent and 39.0 million derivative contracts up 20 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Our Group is the largest CSD entering the first wave of TARGET2-Securities from June 2015. Successful testing with the European Central Bank finished in December 2014. In addition, Our Group moved settlement of contracts executed on the Italian market from T+3 to T+2 in October 2014")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If
                End If

                operationalHighlights.Blocks.Add(Me.CreateContentParagraph("Post Trade Services 2"))

                If True Then
                    Dim bulletList As Nevron.Nov.Text.NBulletList = New Nevron.Nov.Text.NBulletList(Nevron.Nov.Text.ENBulletListTemplateType.Bullet)
                    Me.m_RichText.Content.BulletLists.Add(bulletList)

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Adjusted income for the calendar year 2014 was £389.4 million, up 24 per cent on a pro forma constant currency basis. LCH.Clearnet received EMIR reauthorisation for the UK and France businesses — SwapClear, the world’s leading interest rate swap clearing service, cleared $642 trillion notional, up 26 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Compression services at SwapClear reduced level of notional outstanding, from $426 trillion to $362 trillion")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Our Group was granted clearing house recognition in Canada and Australia")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("Clearing of commodities for the London Metal Exchange ceased in September 2014 as expected")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    If True Then
                        Dim par As Nevron.Nov.Text.NParagraph = Me.CreateContentParagraph("RepoClear, one of Europe’s largest fixed income clearers, cleared €73.4 trillion in nominal value, up 1 per cent")
                        par.SetBulletList(bulletList, 0)
                        operationalHighlights.Blocks.Add(par)
                    End If

                    operationalHighlights.Blocks.Add(Me.CreateContentParagraph("Group Adjusted Total Income by segment"))
                    Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable()
                    table.Margins = New Nevron.Nov.Graphics.NMargins(10)
                    table.AllowSpacingBetweenCells = False
                    operationalHighlights.Blocks.Add(table)
                    table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
                    table.Columns.Add(New Nevron.Nov.Text.NTableColumn())
                    table.Columns.Add(New Nevron.Nov.Text.NTableColumn())

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("2013"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("2014"))
                        tableRow.Cells.Add(tc3)
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc1.Blocks.Add(Me.CreateContentParagraph("Capital Markets"))
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("249.1"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("333.2"))
                        tableRow.Cells.Add(tc3)
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc1.Blocks.Add(Me.CreateContentParagraph("Post Trade Service"))
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("94.7"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("129.1"))
                        tableRow.Cells.Add(tc3)
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc1.Blocks.Add(Me.CreateContentParagraph("Information Services "))
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("281.0"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("373.0"))
                        tableRow.Cells.Add(tc3)
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc1.Blocks.Add(Me.CreateContentParagraph("Technology Services"))
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("47.3"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("66.0"))
                        tableRow.Cells.Add(tc3)
                    End If

                    If True Then
                        Dim tableRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
                        table.Rows.Add(tableRow)
                        Dim tc1 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc1.Blocks.Add(Me.CreateContentParagraph("Other"))
                        tableRow.Cells.Add(tc1)
                        Dim tc2 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc2.Blocks.Add(Me.CreateContentParagraph("87.2"))
                        tableRow.Cells.Add(tc2)
                        Dim tc3 As Nevron.Nov.Text.NTableCell = Me.CreateTableCellWithBorder()
                        tc3.Blocks.Add(Me.CreateContentParagraph("90.4"))
                        tableRow.Cells.Add(tc3)
                    End If
                End If
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"


		#EndRegion

		#Region"Implementation"


		''' <summary>
		''' Creates a table cell with border
		''' </summary>
		''' <returns></returns>
		Private Function CreateTableCellWithBorder() As Nevron.Nov.Text.NTableCell
            Dim tableCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            tableCell.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            tableCell.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Return tableCell
        End Function
		''' <summary>
		''' Creates a section title paragraph
		''' </summary>
		''' <paramname="text"></param>
		''' <returns></returns>
		Private Function CreateTitleParagraph(ByVal text As String) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim textInline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline()
            textInline.Text = text
            textInline.FontSize = 14
            textInline.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGB(89, 76, 46))
            paragraph.Inlines.Add(textInline)
            Return paragraph
        End Function
		''' <summary>
		''' Creates a section title paragraph
		''' </summary>
		''' <paramname="text"></param>
		''' <returns></returns>
		Private Function CreateSecondaryTitleParagraph(ByVal text As String) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim textInline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline()
            textInline.FontSize = 12
            textInline.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromRGB(89, 76, 46))
            paragraph.Inlines.Add(textInline)
            Return paragraph
        End Function
		''' <summary>
		''' Creates a content paragraph
		''' </summary>
		''' <returns></returns>
		Private Function CreateContentParagraph(ByVal text As String) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(text)
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Justify
            Return paragraph
        End Function
		''' <summary>
		''' Creates a sample bar chart given title, values and labels
		''' </summary>
		''' <paramname="size"></param>
		''' <paramname="title"></param>
		''' <paramname="values"></param>
		''' <paramname="labels"></param>
		''' <returns></returns>
		Private Function CreateSampleBarChart(ByVal size As Nevron.Nov.Graphics.NSize, ByVal title As String, ByVal values As Double(), ByVal labels As String()) As Nevron.Nov.Text.NParagraph
            Dim chartView As Nevron.Nov.Chart.NChartView = Me.CreateCartesianChartView()
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
            Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.LegendView.Mode = Nevron.Nov.Chart.ENSeriesLegendMode.None
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            chart.Series.Add(bar)

            For i As Integer = 0 To values.Length - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(values(i)))
            Next

            Dim scaleX As Nevron.Nov.Chart.NOrdinalScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            scaleX.Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(labels)
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim chartInline As Nevron.Nov.Text.NWidgetInline = New Nevron.Nov.Text.NWidgetInline()
            chartInline.Content = chartView
            paragraph.Inlines.Add(chartInline)
            Return paragraph
        End Function

        Protected Function CreateCartesianChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)
            Return chartView
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSampleReport1ExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
