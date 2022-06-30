Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically create inline elements with different formatting
	''' </summary>
	Public Class NWidgetInlinesExample
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
            Nevron.Nov.Examples.Text.NWidgetInlinesExample.NWidgetInlinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NWidgetInlinesExample), NExampleBaseSchema)
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
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to use widgets in order to create ""HTML like"" interfaces. The example also demonstrates how to use style sheets.</p>
<p>Press the ""Show Prev Book"" and ""Show Next Buttons"" buttons to browse through the available books.</p>
<p>Press the ""Add to Cart"" button to add the currently selected book to the shopping cart.</p>
<p>Press the ""Delete"" button to remove a book from the shopping cart.</p>
<p>Use the combo box to select the quantity of books to purchase.</p>
"
        End Function

        Private Sub PopulateRichText()
            Me.m_Books = New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfoList()
            Me.m_CurrentBookIndex = 0
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.Document.StyleSheets.Add(Me.CreateShoppingCartStyleSheet())

            If True Then
                Dim navigationTable As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(1, 2)
                Dim cell0 As Nevron.Nov.Text.NTableCell = navigationTable.Rows(CInt((0))).Cells(0)
                Dim showPrevBookButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show Prev Book")
                AddHandler showPrevBookButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnShowPrevBookButtonClick)
                cell0.Blocks.Clear()
                cell0.Blocks.Add(Me.CreateWidgetParagraph(showPrevBookButton))
                Dim cell1 As Nevron.Nov.Text.NTableCell = navigationTable.Rows(CInt((0))).Cells(1)
                Dim showNextBookButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Show Next Book")
                AddHandler showNextBookButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnShowNextBookButtonClick)
                cell1.Blocks.Clear()
                cell1.Blocks.Add(Me.CreateWidgetParagraph(showNextBookButton))
                section.Blocks.Add(navigationTable)
            End If

            Me.m_BookInfoPlaceHolder = New Nevron.Nov.Text.NGroupBlock()
            section.Blocks.Add(Me.m_BookInfoPlaceHolder)

            If True Then
                Me.m_BookInfoPlaceHolder.Blocks.Add(Me.CreateBookContent(Me.m_Books(0)))
            End If

            If True Then
                Me.m_ShoppingCartPlaceHolder = New Nevron.Nov.Text.NGroupBlock()
                Me.AddEmptyShoppingCartText()
                section.Blocks.Add(Me.m_ShoppingCartPlaceHolder)
            End If
        End Sub

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' 
		''' </summary>
		Private Sub AddEmptyShoppingCartText()
            Me.m_ShoppingCartPlaceHolder.Blocks.Clear()
            Me.m_ShoppingCartPlaceHolder.Blocks.Add(New Nevron.Nov.Text.NParagraph("Shopping Cart Is Empty"))
        End Sub
		''' <summary>
		''' Creates a style sheet for the shopping cart table
		''' </summary>
		''' <returns></returns>
		Private Function CreateShoppingCartStyleSheet() As Nevron.Nov.Dom.NStyleSheet
            Dim styleSheet As Nevron.Nov.Dom.NStyleSheet = New Nevron.Nov.Dom.NStyleSheet()
            Dim rule As Nevron.Nov.Dom.NRule = New Nevron.Nov.Dom.NRule()

            For i As Integer = 0 To 3 - 1
                Dim sb As Nevron.Nov.Dom.NSelectorBuilder = rule.GetSelectorBuilder()
                sb.Start()
                sb.Type(Nevron.Nov.Text.NTableCell.NTableCellSchema)

				' in case of the first or last row selector -> must not be last cell
				If i = 0 OrElse i = 2 Then
                    sb.StartInvertedConditions()
                    sb.LastChild()
                    sb.EndInvertedConditions()
                End If

				' descendant of table row
				sb.DescendantOf()
                sb.Type(Nevron.Nov.Text.NTableRow.NTableRowSchema)

                Select Case i
                    Case 0
						' descendant of first row
						sb.FirstChild()
                    Case 1
						' middle cells
						sb.StartInvertedConditions()
                        sb.FirstChild()
                        sb.LastChild()
                        sb.EndInvertedConditions()
                    Case 2
						' descendant of last row
						sb.LastChild()
                End Select

				' descendant of table
				sb.DescendantOf()
                sb.Type(Nevron.Nov.Text.NTable.NTableSchema)
                sb.ValueEquals(Nevron.Nov.Text.NTable.TagProperty, "ShoppingCart")
                sb.[End]()
            Next

            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.Graphics.NMargins)(Nevron.Nov.Text.NTableCell.BorderThicknessProperty, New Nevron.Nov.Graphics.NMargins(1)))
            rule.Declarations.Add(New Nevron.Nov.Dom.NValueDeclaration(Of Nevron.Nov.UI.NBorder)(Nevron.Nov.Text.NTableCell.BorderProperty, Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)))
            styleSheet.Add(rule)
            Return styleSheet
        End Function
		''' <summary>
		''' Creates a table based on the book info
		''' </summary>
		''' <paramname="bookInfo"></param>
		''' <returns></returns>
		Private Function CreateBookContent(ByVal bookInfo As Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo) As Nevron.Nov.Text.NBlock
            Dim table As Nevron.Nov.Text.NTable = New Nevron.Nov.Text.NTable(4, 2)

			' Create the image
			Dim imageTableCell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((0))).Cells(0)
            imageTableCell.RowSpan = Integer.MaxValue
            imageTableCell.Blocks.Clear()
            imageTableCell.Blocks.Add(Me.CreateImageParagraph(bookInfo.Image))
            Dim titleTableCell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((0))).Cells(1)
            titleTableCell.Blocks.Clear()
            titleTableCell.Blocks.Add(Me.CreateTitleParagraph(bookInfo.Name))
            Dim descriptionTableCell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((1))).Cells(1)
            descriptionTableCell.Blocks.Clear()
            descriptionTableCell.Blocks.Add(Me.CreateDescriptionParagraph(bookInfo.Description))
            Dim authorTableCell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((2))).Cells(1)
            authorTableCell.Blocks.Clear()
            authorTableCell.Blocks.Add(Me.CreateAuthorParagraph(bookInfo.Author))
            Dim addToCartTableCell As Nevron.Nov.Text.NTableCell = table.Rows(CInt((3))).Cells(1)
            addToCartTableCell.RowSpan = Integer.MaxValue
            addToCartTableCell.Blocks.Clear()
            Dim addToCartButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add To Cart")
            AddHandler addToCartButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddTableRow)
            addToCartTableCell.VerticalAlignment = Nevron.Nov.Text.ENVAlign.Bottom
            addToCartTableCell.Blocks.Add(Me.CreateWidgetParagraph(addToCartButton))
            Return table
        End Function
		''' <summary>
		''' Loads the current book info in the book place holder group
		''' </summary>
		Private Sub LoadBookInfo()
            Me.m_BookInfoPlaceHolder.Blocks.Clear()
            Me.m_BookInfoPlaceHolder.Blocks.Add(Me.CreateBookContent(Me.m_Books(Me.m_CurrentBookIndex)))
        End Sub
		''' <summary>
		''' Creates the author paragraph
		''' </summary>
		''' <paramname="author"></param>
		''' <returns></returns>
		Private Function CreateAuthorParagraph(ByVal author As String) As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph("Author: " & author)
        End Function
		''' <summary>
		''' Creates teh description paragraph
		''' </summary>
		''' <paramname="title"></param>
		''' <returns></returns>
		Private Function CreateDescriptionParagraph(ByVal title As String) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(title)
            paragraph.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.WhiteSmoke)
            Return paragraph
        End Function
		''' <summary>
		''' Creates the title paragraph
		''' </summary>
		''' <paramname="title"></param>
		''' <returns></returns>
		Private Function CreateTitleParagraph(ByVal title As String) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph(title)
            paragraph.FontSize = 18
            paragraph.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.RoyalBlue)
            Return paragraph
        End Function
		''' <summary>
		''' Creates the book image paragraph
		''' </summary>
		''' <paramname="image"></param>
		''' <returns></returns>
		Private Function CreateImageParagraph(ByVal image As Nevron.Nov.Graphics.NImage) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim inline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
            inline.Image = CType(image.DeepClone(), Nevron.Nov.Graphics.NImage)
            inline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
            inline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 280)
            paragraph.Inlines.Add(inline)
            Return paragraph
        End Function
		''' <summary>
		''' Creates a paragraph that contains a widget
		''' </summary>
		''' <paramname="widget"></param>
		''' <returns></returns>
		Private Function CreateWidgetParagraph(ByVal widget As Nevron.Nov.UI.NWidget) As Nevron.Nov.Text.NParagraph
            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            Dim inline As Nevron.Nov.Text.NWidgetInline = New Nevron.Nov.Text.NWidgetInline()
            inline.Content = widget
            paragraph.Inlines.Add(inline)
            Return paragraph
        End Function
		''' <summary>
		''' Creates a combo that shows the 
		''' </summary>
		''' <returns></returns>
		Private Function CreateQuantityCombo() As Nevron.Nov.UI.NComboBox
            Dim combo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 9 - 1
                combo.Items.Add(New Nevron.Nov.UI.NComboBoxItem((i + 1).ToString()))
            Next

            combo.SelectedIndex = 0
            AddHandler combo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnQuantityComboSelectedIndexChanged)
            Return combo
        End Function
		''' <summary>
		''' Adds a book row to the shopping cart
		''' </summary>
		Private Sub AddBookRow()
            Dim bookInfo As Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo = Me.m_Books(Me.m_CurrentBookIndex)
            Dim bookRow As Nevron.Nov.Text.NTableRow = New Nevron.Nov.Text.NTableRow()
            bookRow.Tag = bookInfo
            Dim nameCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            nameCell.Blocks.Add(New Nevron.Nov.Text.NParagraph(bookInfo.Name))
            bookRow.Cells.Add(nameCell)
            Dim quantityCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            quantityCell.Blocks.Add(Me.CreateWidgetParagraph(Me.CreateQuantityCombo()))
            bookRow.Cells.Add(quantityCell)
            Dim priceCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            priceCell.Blocks.Add(New Nevron.Nov.Text.NParagraph(bookInfo.Price.ToString()))
            bookRow.Cells.Add(priceCell)
            Dim totalCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            totalCell.Blocks.Add(New Nevron.Nov.Text.NParagraph())
            bookRow.Cells.Add(totalCell)
            Dim deleteCell As Nevron.Nov.Text.NTableCell = New Nevron.Nov.Text.NTableCell()
            Dim deleteRowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Delete")
            AddHandler deleteRowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnDeleteRowButtonClick)
            deleteCell.Blocks.Add(Me.CreateWidgetParagraph(deleteRowButton))
            bookRow.Cells.Add(deleteCell)
            Me.m_CartTable.Rows.Insert(Me.m_CartTable.Rows.Count - 1, bookRow)
        End Sub
		''' <summary>
		''' Adds a total row to the shopping cart
		''' </summary>
		Private Sub AddTotalRow()
            Dim totalRow As Nevron.Nov.Text.NTableRow = Me.m_CartTable.Rows.CreateNewRow()
            Dim totalCell As Nevron.Nov.Text.NTableCell = totalRow.Cells(0)
            totalCell.Blocks.Clear()
            totalCell.ColSpan = 3
            totalCell.Blocks.Add(New Nevron.Nov.Text.NParagraph("Grand Total:"))
            Me.m_CartTable.Rows.Add(totalRow)
        End Sub
		''' <summary>
		''' Updates the total values in the shopping cart
		''' </summary>
		Private Sub UpdateTotals()
            If Me.m_CartTable Is Nothing OrElse Me.m_CartTable.Columns.Count <> 5 Then Return
            Dim grandTotal As Double = 0

			' sum all book info price * quantity
			For i As Integer = 0 To Me.m_CartTable.Rows.Count - 1
                Dim row As Nevron.Nov.Text.NTableRow = Me.m_CartTable.Rows(i)
                Dim bookInfo As Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo = TryCast(row.Tag, Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo)

                If bookInfo IsNot Nothing Then
                    Dim blocks As Nevron.Nov.Text.NVFlowBlockCollection(Of Nevron.Nov.Text.NBlock) = row.Cells(CInt((1))).Blocks
                    Dim combo As Nevron.Nov.UI.NComboBox = CType(blocks.GetFirstDescendant(New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NComboBox.NComboBoxSchema)), Nevron.Nov.UI.NComboBox)

                    If combo IsNot Nothing Then
                        Dim total As Double = (combo.SelectedIndex + 1) * bookInfo.Price
                        row.Cells(CInt((3))).Blocks.Clear()
                        row.Cells(CInt((3))).Blocks.Add(New Nevron.Nov.Text.NParagraph(total.ToString()))
                        grandTotal += total
                    End If
                End If
            Next

            Dim grandTotalCell As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((Me.m_CartTable.Rows.Count - 1))).Cells(3)
            grandTotalCell.Blocks.Clear()
            grandTotalCell.Blocks.Add(New Nevron.Nov.Text.NParagraph(grandTotal.ToString()))
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when a new row must be added to the shopping cart
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnAddTableRow(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            If Me.m_CartTable Is Nothing Then
                Me.m_CartTable = New Nevron.Nov.Text.NTable(1, 5)
                Me.m_CartTable.Tag = "ShoppingCart"
                Me.m_CartTable.AllowSpacingBetweenCells = False
                Me.m_ShoppingCartPlaceHolder.Blocks.Clear()
                Me.m_ShoppingCartPlaceHolder.Blocks.Add(Me.m_CartTable)
                Dim nameCell As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((0))).Cells(0)
                nameCell.Blocks.Clear()
                nameCell.Blocks.Add(New Nevron.Nov.Text.NParagraph("Name"))
                Dim quantity As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((0))).Cells(1)
                quantity.Blocks.Clear()
                quantity.Blocks.Add(New Nevron.Nov.Text.NParagraph("Quantity"))
                Dim priceCell As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((0))).Cells(2)
                priceCell.Blocks.Clear()
                priceCell.Blocks.Add(New Nevron.Nov.Text.NParagraph("Price"))
                Dim totalCell As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((0))).Cells(3)
                totalCell.Blocks.Clear()
                totalCell.Blocks.Add(New Nevron.Nov.Text.NParagraph("Total"))
                Dim deleteCell As Nevron.Nov.Text.NTableCell = Me.m_CartTable.Rows(CInt((0))).Cells(4)
                deleteCell.Blocks.Clear()
                Me.AddTotalRow()
            End If

            Me.AddBookRow()
            Me.UpdateTotals()
        End Sub
		''' <summary>
		''' Called when a row must be deleted from the shopping cart
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnDeleteRowButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim tableRow As Nevron.Nov.Text.NTableRow = CType(arg.TargetNode.GetFirstAncestor(Nevron.Nov.Text.NTableRow.NTableRowSchema), Nevron.Nov.Text.NTableRow)
            Me.m_CartTable.Rows.Remove(tableRow)

            If Me.m_CartTable.Rows.Count = 2 Then
                Me.m_CartTable.Rows.RemoveAt(Me.m_CartTable.Rows.Count - 1)
                Me.m_CartTable = Nothing
                Me.AddEmptyShoppingCartText()
            End If

            Me.UpdateTotals()
        End Sub
		''' <summary>
		''' Called when the quantity combo for a row has changed
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnQuantityComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.UpdateTotals()
        End Sub
		''' <summary>
		''' Called to load the next book in the list
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnShowNextBookButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            If Me.m_CurrentBookIndex >= Me.m_Books.Count - 1 Then
                Return
            End If

            Me.m_CurrentBookIndex += 1
            Me.LoadBookInfo()
        End Sub
		''' <summary>
		''' Called to load the prev book in the list
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnShowPrevBookButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            If Me.m_CurrentBookIndex <= 0 Then
                Return
            End If

            Me.m_CurrentBookIndex -= 1
            Me.LoadBookInfo()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_Books As Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfoList
        Private m_BookInfoPlaceHolder As Nevron.Nov.Text.NGroupBlock
        Private m_ShoppingCartPlaceHolder As Nevron.Nov.Text.NGroupBlock
        Private m_CartTable As Nevron.Nov.Text.NTable
        Private m_CurrentBookIndex As Integer

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NWidgetInlinesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		Private Class NBookInfo
            Implements Nevron.Nov.INDeeplyCloneable
			#Region"Constructors"

			''' <summary>
			''' Initializer contructor
			''' </summary>
			''' <paramname="name"></param>
			''' <paramname="author"></param>
			''' <paramname="description"></param>
			''' <paramname="image"></param>
			''' <paramname="price"></param>
			Public Sub New(ByVal name As String, ByVal author As String, ByVal description As String, ByVal image As Nevron.Nov.Graphics.NImage, ByVal price As Double)
                Me.Name = name
                Me.Author = author
                Me.Description = description
                Me.Image = image
                Me.Price = price
            End Sub
			''' <summary>
			''' Copy constructor
			''' </summary>
			''' <paramname="bookInfo"></param>
			Public Sub New(ByVal bookInfo As Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo)
                Me.Name = bookInfo.Name
                Me.Author = bookInfo.Author
                Me.Description = bookInfo.Description
                Me.Image = CType(bookInfo.Image.DeepClone(), Nevron.Nov.Graphics.NImage)
                Me.Price = bookInfo.Price
            End Sub

			#EndRegion

			#Region"Fields"

			Public ReadOnly Name As String
            Public ReadOnly Author As String
            Public ReadOnly Description As String
            Public ReadOnly Image As Nevron.Nov.Graphics.NImage
            Public ReadOnly Price As Double

			#EndRegion

			#Region"INDeeplyCloneable"

			Public Function DeepClone() As Object Implements Global.Nevron.Nov.INDeeplyCloneable.DeepClone
                Return New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo(Me)
            End Function

			#EndRegion
		End Class

        Private Class NBookInfoList
            Inherits Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo)

            Public Sub New()
                MyBase.Add(New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo("The Name Of The Wind", "Patrick Rothfuss", "This is the riveting first-person narrative of Kvothe, a young man who grows to be one of the most notorious magicians his world has ever seen. From his childhood in a troupe of traveling players, to years spent as a near-feral orphan in a crime-riddled city, to his daringly brazen yet successful bid to enter a legendary school of magic, The Name of the Wind is a masterpiece that transports readers into the body and mind of a wizard.", Nevron.Nov.Text.NResources.Image_Books_NameOfTheWind_jpg, 12.90))
                MyBase.Add(New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo("Lord of Ohe Rings", "J.R.R. Tolkien", "In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring, filling it with his own power so that he could rule all others. But the One Ring was taken from him, and though he sought it throughout Middle-earth, it remained lost to him. After many ages it fell by chance into the hands of the hobbit Bilbo Baggins.", Nevron.Nov.Text.NResources.Image_Books_LordOfTheRings_jpg, 13.99))
                MyBase.Add(New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo("A Game Of Thrones", "George R.R. Martin", "Long ago, in a time forgotten, a preternatural event threw the seasons out of balance. In a land where summers can last decades and winters a lifetime, trouble is brewing. The cold is returning, and in the frozen wastes to the north of Winterfell, sinister and supernatural forces are massing beyond the kingdom’s protective Wall. At the center of the conflict lie the Starks of Winterfell, a family as harsh and unyielding as the land they were born to.", Nevron.Nov.Text.NResources.Image_Books_AGameOfThrones_jpg, 12.79))
                MyBase.Add(New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo("The Way Of Kings", "Brandon Sanderson", "Roshar is a world of stone and storms. Uncanny tempests of incredible power sweep across the rocky terrain so frequently that they have shaped ecology and civilization alike. Animals hide in shells, trees pull in branches, and grass retracts into the soilless ground. Cities are built only where the topography offers shelter.", Nevron.Nov.Text.NResources.Image_Books_TheWayOfKings_jpg, 7.38))
                MyBase.Add(New Nevron.Nov.Examples.Text.NWidgetInlinesExample.NBookInfo("Mistborn", "Brandon Sanderson", "For a thousand years the ash fell and no flowers bloomed. For a thousand years the Skaa slaved in misery and lived in fear. For a thousand years the Lord Ruler, the 'Sliver of Infinity' reigned with absolute power and ultimate terror, divinely invincible. Then, when hope was so long lost that not even its memory remained, a terribly scarred, heart-broken half-Skaa rediscovered it in the depths of the Lord Ruler’s most hellish prison. Kelsier 'snapped' and found in himself the powers of a Mistborn. A brilliant thief and natural leader, he turned his talents to the ultimate caper, with the Lord Ruler himself as the mark. ", Nevron.Nov.Text.NResources.Image_Books_Mistborn_jpg, 6.38))
            End Sub
        End Class

		#EndRegion
	End Class
End Namespace
