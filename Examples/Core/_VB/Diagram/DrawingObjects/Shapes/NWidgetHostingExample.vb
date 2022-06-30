Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Shapes
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NWidgetHostingExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Static constructor.
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NWidgetHostingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NWidgetHostingExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' Create a simple drawing
            Dim drawingViewWithRibbon As Nevron.Nov.Diagram.NDrawingViewWithRibbon = New Nevron.Nov.Diagram.NDrawingViewWithRibbon()
            Me.m_DrawingView = drawingViewWithRibbon.View
            Me.m_DrawingView.Document.HistoryService.Pause()

            Try
                Me.InitDiagram(Me.m_DrawingView.Document)
            Finally
                Me.m_DrawingView.Document.HistoryService.[Resume]()
            End Try

            Return drawingViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates the ability to embed any NOV UI Widget in shapes. This allows the creation of interactive
    diagrams that are incredibly rich on interaction features.
</p>
"
        End Function

        Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
            ' create the books list
            Me.CreateBooksList()

            ' create the shopping cart
            Me.m_ShoppingCart = New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCart()
            Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim activePage As Nevron.Nov.Diagram.NPage = drawing.ActivePage

            ' hide the grid
            drawing.ScreenVisibility.ShowGrid = False

            ' create a shape which hosts a widget
            Me.CreateBookStore(activePage)
        End Sub

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates the list of books through which the user can browse
        ''' </summary>
        Private Sub CreateBooksList()
            Me.m_Books = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook)()
            Me.m_Books.Add(New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook("The Name Of The Wind", "Patrick Rothfuss", "This is the riveting first-person narrative of Kvothe, a young man who grows to be one of the most notorious magicians his world has ever seen. From his childhood in a troupe of traveling players, to years spent as a near-feral orphan in a crime-riddled city, to his daringly brazen yet successful bid to enter a legendary school of magic, The Name of the Wind is a masterpiece that transports readers into the body and mind of a wizard.", Nevron.Nov.Diagram.NResources.Image_Books_NameOfTheWind_jpg, 12.90))
            Me.m_Books.Add(New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook("Lord of Ohe Rings", "J.R.R. Tolkien", "In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring, filling it with his own power so that he could rule all others. But the One Ring was taken from him, and though he sought it throughout Middle-earth, it remained lost to him. After many ages it fell by chance into the hands of the hobbit Bilbo Baggins.", Nevron.Nov.Diagram.NResources.Image_Books_LordOfTheRings_jpg, 13.99))
            Me.m_Books.Add(New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook("A Game Of Thrones", "George R.R. Martin", "Long ago, in a time forgotten, a preternatural event threw the seasons out of balance. In a land where summers can last decades and winters a lifetime, trouble is brewing. The cold is returning, and in the frozen wastes to the north of Winterfell, sinister and supernatural forces are massing beyond the kingdom’s protective Wall. At the center of the conflict lie the Starks of Winterfell, a family as harsh and unyielding as the land they were born to.", Nevron.Nov.Diagram.NResources.Image_Books_AGameOfThrones_jpg, 12.79))
            Me.m_Books.Add(New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook("The Way Of Kings", "Brandon Sanderson", "Roshar is a world of stone and storms. Uncanny tempests of incredible power sweep across the rocky terrain so frequently that they have shaped ecology and civilization alike. Animals hide in shells, trees pull in branches, and grass retracts into the soilless ground. Cities are built only where the topography offers shelter.", Nevron.Nov.Diagram.NResources.Image_Books_TheWayOfKings_jpg, 7.38))
            Me.m_Books.Add(New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook("Mistborn", "Brandon Sanderson", "For a thousand years the ash fell and no flowers bloomed. For a thousand years the Skaa slaved in misery and lived in fear. For a thousand years the Lord Ruler, the 'Sliver of Infinity' reigned with absolute power and ultimate terror, divinely invincible. Then, when hope was so long lost that not even its memory remained, a terribly scarred, heart-broken half-Skaa rediscovered it in the depths of the Lord Ruler’s most hellish prison. Kelsier 'snapped' and found in himself the powers of a Mistborn. A brilliant thief and natural leader, he turned his talents to the ultimate caper, with the Lord Ruler himself as the mark. ", Nevron.Nov.Diagram.NResources.Image_Books_Mistborn_jpg, 6.38))
        End Sub
        ''' <summary>
        ''' Gets the min Width and Height of all book images
        ''' </summary>
        ''' <returns></returns>
        Private Function GetMinBookImageSize() As Nevron.Nov.Graphics.NSize
            Dim size As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(Double.MaxValue, Double.MaxValue)

            For i As Integer = 0 To Me.m_Books.Count - 1
                Dim book As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook = Me.m_Books(i)

                If book.Image.Width < size.Width Then
                    size.Width = book.Image.Width
                End If

                If book.Image.Height < size.Height Then
                    size.Height = book.Image.Height
                End If
            Next

            Return size
        End Function
        ''' <summary>
        ''' Creates the book store interface
        ''' </summary>
        ''' <paramname="activePage"></param>
        Private Sub CreateBookStore(ByVal activePage As Nevron.Nov.Diagram.NPage)
            Const x1 As Double = 50
            Const x2 As Double = x1 + 200
            Const x3 As Double = x2 + 50
            Const x4 As Double = x3 + 400
            Const y1 As Double = 50
            Const y2 As Double = y1 + 50
            Const y3 As Double = y2 + 50
            Const y4 As Double = y3 + 20
            Const y5 As Double = y4 + 200
            Const y6 As Double = y5 + 20
            Const y7 As Double = y6 + 50

            ' prev button
            Dim prevButtonShape As Nevron.Nov.Diagram.NShape = Me.CreateButtonShape("Show Prev Book")
            Me.SetLeftTop(prevButtonShape, New Nevron.Nov.Graphics.NPoint(x1, y1))
            AddHandler CType(prevButtonShape.Widget, Nevron.Nov.UI.NButton).Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.LoadBook(Me.m_nSelectedBook - 1)
            activePage.Items.Add(prevButtonShape)

            ' next button
            Dim nextButtonShape As Nevron.Nov.Diagram.NShape = Me.CreateButtonShape("Show Next Book")
            Me.SetRightTop(nextButtonShape, New Nevron.Nov.Graphics.NPoint(x2, y1))
            AddHandler CType(nextButtonShape.Widget, Nevron.Nov.UI.NButton).Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.LoadBook(Me.m_nSelectedBook + 1)
            activePage.Items.Add(nextButtonShape)

            ' add to cart
            Dim addToCartButton As Nevron.Nov.Diagram.NShape = Me.CreateButtonShape("Add to Cart")
            Me.SetRightTop(addToCartButton, New Nevron.Nov.Graphics.NPoint(x2, y6))
            AddHandler CType(addToCartButton.Widget, Nevron.Nov.UI.NButton).Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.m_ShoppingCart.AddItem(Me.m_Books(Me.m_nSelectedBook), Me)
            activePage.Items.Add(addToCartButton)

            ' create selected book shapes
            Dim basicShapes As Nevron.Nov.Diagram.Shapes.NBasicShapeFactory = New Nevron.Nov.Diagram.Shapes.NBasicShapeFactory()

            ' selected image
            Me.m_SelectedBookImage = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Rectangle)
            Me.SetLeftTop(Me.m_SelectedBookImage, New Nevron.Nov.Graphics.NPoint(x1, y2))
            Dim minBookSize As Nevron.Nov.Graphics.NSize = Me.GetMinBookImageSize()
            Me.m_SelectedBookImage.Width = x2 - x1
            Me.m_SelectedBookImage.Height = y5 - y2
            activePage.Items.Add(Me.m_SelectedBookImage)

            ' selected title
            Me.m_SelectedBookTitle = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Text)
            Me.m_SelectedBookTitle.TextBlock.InitXForm(Nevron.Nov.Diagram.ENTextBlockXForm.ShapeBox)
            Me.m_SelectedBookTitle.TextBlock.FontSize = 25
            Me.m_SelectedBookTitle.TextBlock.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkBlue)
            Me.SetLeftTop(Me.m_SelectedBookTitle, New Nevron.Nov.Graphics.NPoint(x3, y2))
            Me.m_SelectedBookTitle.Width = x4 - x3
            Me.m_SelectedBookTitle.Height = y3 - y2
            activePage.Items.Add(Me.m_SelectedBookTitle)

            ' selected description
            Me.m_SelectedBookDescription = basicShapes.CreateShape(Nevron.Nov.Diagram.Shapes.ENBasicShape.Text)
            Me.m_SelectedBookDescription.TextBlock.InitXForm(Nevron.Nov.Diagram.ENTextBlockXForm.ShapeBox)
            Me.SetLeftTop(Me.m_SelectedBookDescription, New Nevron.Nov.Graphics.NPoint(x3, y4))
            Me.m_SelectedBookDescription.Width = x4 - x3
            Me.m_SelectedBookDescription.Height = y5 - y4
            activePage.Items.Add(Me.m_SelectedBookDescription)

            ' load the first book
            Me.LoadBook(0)

            ' create the shape that hosts the shopping cart widget
            Dim shoppingCartShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            shoppingCartShape.Init2DShape()
            Me.m_ShoppingCartWidget = New Nevron.Nov.UI.NContentHolder()
            Me.m_ShoppingCartWidget.Content = Me.m_ShoppingCart.CreateWidget(Me)
            shoppingCartShape.Widget = Me.m_ShoppingCartWidget
            Me.SetLeftTop(shoppingCartShape, New Nevron.Nov.Graphics.NPoint(x1, y7))
            Me.BindSizeToDesiredSize(shoppingCartShape)
            activePage.Items.Add(shoppingCartShape)
        End Sub
        ''' <summary>
        ''' Loads a book with the specified index
        ''' </summary>
        ''' <paramname="index"></param>
        Private Sub LoadBook(ByVal index As Integer)
            Me.m_nSelectedBook = Nevron.Nov.NMath.Clamp(0, Me.m_Books.Count - 1, index)
            Dim book As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook = Me.m_Books(Me.m_nSelectedBook)
            Me.m_SelectedBookImage.Geometry.Fill = New Nevron.Nov.Graphics.NImageFill(CType(book.Image.DeepClone(), Nevron.Nov.Graphics.NImage))
            Me.m_SelectedBookTitle.Text = book.Name
            Me.m_SelectedBookDescription.Text = book.Description
        End Sub
        ''' <summary>
        ''' Creates a shape
        ''' </summary>
        ''' <paramname="text"></param>
        ''' <returns></returns>
        Private Function CreateButtonShape(ByVal text As String) As Nevron.Nov.Diagram.NShape
            Dim buttonShape As Nevron.Nov.Diagram.NShape = New Nevron.Nov.Diagram.NShape()
            buttonShape.Init2DShape()

            ' make a button and place it in the shape
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(text)
            buttonShape.Widget = button

            ' bind size to button desired size 
            Me.BindSizeToDesiredSize(buttonShape)
            Return buttonShape
        End Function
        ''' <summary>
        ''' Sets the left top corner of a shape
        ''' </summary>
        ''' <paramname="shape"></param>
        ''' <paramname="location"></param>
        Private Sub SetLeftTop(ByVal shape As Nevron.Nov.Diagram.NShape, ByVal location As Nevron.Nov.Graphics.NPoint)
            ' align local pin to left/top corner so pin point can change the location 
            shape.LocPinX = 0
            shape.LocPinY = 0
            shape.LocPinRelative = True

            ' set the pin point
            shape.SetPinPoint(location)
        End Sub
        ''' <summary>
        ''' Sets the right top corner of a shape
        ''' </summary>
        ''' <paramname="shape"></param>
        ''' <paramname="location"></param>
        Private Sub SetRightTop(ByVal shape As Nevron.Nov.Diagram.NShape, ByVal location As Nevron.Nov.Graphics.NPoint)
            ' align local pin to left/top corner so pin point can change the location 
            shape.LocPinX = 1
            shape.LocPinY = 0
            shape.LocPinRelative = True

            ' set the pin point
            shape.SetPinPoint(location)
        End Sub
        ''' <summary>
        ''' Binds the size of the shape to the embedded widget desired size
        ''' </summary>
        ''' <paramname="shape"></param>
        Private Sub BindSizeToDesiredSize(ByVal shape As Nevron.Nov.Diagram.NShape)
            Dim widget As Nevron.Nov.UI.NWidget = shape.Widget

            ' bind shape width to button desired width
            Dim bx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(widget, Nevron.Nov.UI.NButton.DesiredWidthProperty)
            bx.Guard = True
            shape.SetFx(Nevron.Nov.Diagram.NShape.WidthProperty, bx)

            ' bind shape height to button desired height
            Dim by As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(widget, Nevron.Nov.UI.NButton.DesiredHeightProperty)
            by.Guard = True
            shape.SetFx(Nevron.Nov.Diagram.NShape.HeightProperty, by)
            shape.AllowResizeX = False
            shape.AllowRotate = False
            shape.AllowResizeY = False
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView
        Private m_nSelectedBook As Integer = 0
        Private m_SelectedBookImage As Nevron.Nov.Diagram.NShape
        Private m_SelectedBookTitle As Nevron.Nov.Diagram.NShape
        Private m_SelectedBookDescription As Nevron.Nov.Diagram.NShape
        Private m_Books As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook)
        Private m_ShoppingCart As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCart
        Private m_ShoppingCartWidget As Nevron.Nov.UI.NContentHolder

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NWidgetHostingExample.
        ''' </summary>
        Public Shared ReadOnly NWidgetHostingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Nested Types"

        Friend Class NBook
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
            Public Sub New(ByVal bookInfo As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook)
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
                Return New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook(Me)
            End Function

            #EndRegion
        End Class

        Friend Class NShoppingCartItem
            Public Sub New()
            End Sub

            Public Name As String
            Public Quantity As Integer
            Public Price As Double

            Public Function GetTotal() As Double
                Return Me.Quantity * Me.Price
            End Function
        End Class

        Friend Class NShoppingCart
            Private Items As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem)()

            ''' <summary>
            ''' Adds a book to the shopping cart.
            ''' </summary>
            ''' <paramname="book"></param>
            ''' <paramname="example"></param>
            Public Sub AddItem(ByVal book As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NBook, ByVal example As Nevron.Nov.Examples.Diagram.NWidgetHostingExample)
                ' try find an item with the same name. if such exists -> incerase quantity and rebuild shopping cart widget
                For i As Integer = 0 To Me.Items.Count - 1

                    If Equals(Me.Items(CInt((i))).Name, book.Name) Then
                        Me.Items(CInt((i))).Quantity += 1
                        example.m_ShoppingCartWidget.Content = Me.CreateWidget(example)
                        Return
                    End If
                Next

                ' add new item and rebuild shopping cart widget
                Dim item As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem = New Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem()
                item.Name = book.Name
                item.Price = book.Price
                item.Quantity = 1
                Me.Items.Add(item)
                example.m_ShoppingCartWidget.Content = Me.CreateWidget(example)
            End Sub

            Public Sub DeleteItem(ByVal item As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem, ByVal example As Nevron.Nov.Examples.Diagram.NWidgetHostingExample)
                ' remove the item and rebuild the shopping cart
                Me.Items.Remove(item)
                example.m_ShoppingCartWidget.Content = Me.CreateWidget(example)
            End Sub

            ''' <summary>
            ''' Gets the grand total of items
            ''' </summary>
            ''' <returns></returns>
            Public Function GetGrandTotal() As Double
                Dim grandTotal As Double = 0

                For i As Integer = 0 To Me.Items.Count - 1
                    grandTotal += Me.Items(CInt((i))).GetTotal()
                Next

                Return grandTotal
            End Function

            ''' <summary>
            ''' Creates the widget that represents the current state of the shopping cart.
            ''' </summary>
            ''' <paramname="example"></param>
            ''' <returns></returns>
            Public Function CreateWidget(ByVal example As Nevron.Nov.Examples.Diagram.NWidgetHostingExample) As Nevron.Nov.UI.NWidget
                If Me.Items.Count = 0 Then
                    Return New Nevron.Nov.UI.NLabel("The Shopping Cart is Empty")
                End If

                Const spacing As Double = 10
                
                ' create the grand total label
                Dim grandTotalLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(Me.GetGrandTotal().ToString("0.00"))

                ' items table
                Dim tableFlowPanel As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
                tableFlowPanel.HorizontalSpacing = spacing
                tableFlowPanel.VerticalSpacing = spacing
                tableFlowPanel.MaxOrdinal = 5

                ' add headers
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel("Name"))
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel("Quantity"))
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel("Price"))
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel("Total"))
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel())

                For i As Integer = 0 To Me.Items.Count - 1
                    Dim item As Nevron.Nov.Examples.Diagram.NWidgetHostingExample.NShoppingCartItem = Me.Items(i)

                    ' name
                    Dim nameLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(item.Name)
                    Dim priceLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(item.Price.ToString("0.00"))
                    Dim totalLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(item.GetTotal().ToString("0.00"))

                    ' quantity
                    Dim quantityNud As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
                    quantityNud.Value = item.Quantity
                    quantityNud.DecimalPlaces = 0
                    quantityNud.Minimum = 0
                    AddHandler quantityNud.ValueChanged, Sub(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
                                                             item.Quantity = CInt(CType(args.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
                                                             totalLabel.Text = item.GetTotal().ToString("0.00")
                                                             grandTotalLabel.Text = Me.GetGrandTotal().ToString("0.00")
                                                         End Sub

                    Dim deleteButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Delete")
                    AddHandler deleteButton.Click, Sub(ByVal args As Nevron.Nov.Dom.NEventArgs) Me.DeleteItem(item, example)
                    tableFlowPanel.Add(nameLabel)
                    tableFlowPanel.Add(quantityNud)
                    tableFlowPanel.Add(priceLabel)
                    tableFlowPanel.Add(totalLabel)
                    tableFlowPanel.Add(deleteButton)
                Next

                ' add grand total
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel("Grand Total"))
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel())
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel())
                tableFlowPanel.Add(grandTotalLabel)
                tableFlowPanel.Add(New Nevron.Nov.UI.NLabel())
                Return tableFlowPanel
            End Function
        End Class
        
        #EndRegion
    End Class
End Namespace
