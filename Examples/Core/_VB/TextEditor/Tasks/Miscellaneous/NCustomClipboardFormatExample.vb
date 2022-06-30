Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Serialization
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.Formats
Imports Nevron.Nov.UI
Imports System
Imports System.IO

Namespace Nevron.Nov.Examples.Text
    ''' <summary>
    ''' The example demonstrates how to create a custom clipboard format that allows the user to selectively copy/paste text, images or both.
    ''' </summary>
    Public Class NCustomClipboardFormatExample
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
            Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormatExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NCustomClipboardFormatExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()
            Me.m_RichText.Selection.ClipboardTextFormats = New Nevron.Nov.Text.NClipboardTextFormat() {New Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat(Me)}
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("The example demonstrates how to implement a custom clipboard format."))
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("This example demonstrates a scenario where the user can selectively copy/paste just text or images."))

            For i As Integer = 0 To 3 - 1
                Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph("This paragraph contains text and")
                section.Blocks.Add(paragraph)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                Dim imageInline As Nevron.Nov.Text.NImageInline = New Nevron.Nov.Text.NImageInline()
                imageInline.Image = Nevron.Nov.Text.NResources.Image_Artistic_FishBowl_jpg
                imageInline.PreferredWidth = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 250)
                imageInline.PreferredHeight = New Nevron.Nov.NMultiLength(Nevron.Nov.ENMultiLengthUnit.Dip, 200)
                paragraph.Inlines.Add(imageInline)
                paragraph.Inlines.Add(New Nevron.Nov.Text.NLineBreakInline())
                paragraph.Inlines.Add(New Nevron.Nov.Text.NTextInline("image inline content."))
            Next

            Return Me.m_RichText
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_ContentTypeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ContentTypeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Text"))
            Me.m_ContentTypeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Image"))
            Me.m_ContentTypeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Text and Image"))
            Me.m_ContentTypeComboBox.SelectedIndex = 0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Custom Clipboard Content: ", Me.m_ContentTypeComboBox))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates a scenario where the user can selectively copy/paste text, images, or both. The purpose of the example is to demonstrate how to implement a custom clipboard format.</p>"
        End Function

        #EndRegion

        #Region"Custom Clipboard Example Code"

        ''' <summary>
        ''' Represents a custom clipboard format
        ''' </summary>
        Public Class NCustomClipboardFormat
            Inherits Nevron.Nov.Text.NClipboardTextFormat
            #Region"Constructors"

            ''' <summary>
            ''' Initializer constructor
            ''' </summary>
            ''' <paramname="example"></param>
            Public Sub New(ByVal example As Nevron.Nov.Examples.Text.NCustomClipboardFormatExample)
                Me.m_Example = example
            End Sub
            ''' <summary>
            ''' Static constructor
            ''' </summary>
            Shared Sub New()
                ' create the Data Format associated with MyFirstDataEchangeObject
                Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat.s_DataFormat = Nevron.Nov.UI.NDataFormat.Create("CustomClipboardFormat", New Nevron.Nov.FunctionResult(Of Byte(), Nevron.Nov.UI.NDataFormat, Object)(AddressOf Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat.SerializeDataObject), New Nevron.Nov.FunctionResult(Of Object, Nevron.Nov.UI.NDataFormat, Byte())(AddressOf Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat.DeserializeDataObject))
            End Sub
            #EndRegion

            #Region"Overrides"

            ''' <summary>
            ''' Imports a document from a data object
            ''' </summary>
            ''' <paramname="obj"></param>
            ''' <returns></returns>
            Public Overrides Function FromDataObject(ByVal obj As Object) As Nevron.Nov.Text.NRichTextDocument
                Return CType(obj, Nevron.Nov.Text.NRichTextDocument)
            End Function
            ''' <summary>
            ''' Exports the specified document to the specified data object
            ''' </summary>
            ''' <paramname="document"></param>
            ''' <paramname="dataObject"></param>
            ''' <returns></returns>
            Public Overrides Sub ToDataObject(ByVal document As Nevron.Nov.Text.NRichTextDocument, ByVal dataObject As Nevron.Nov.UI.NDataObject)
                ' create a clone of the document so that we can modify it
                document = CType(document.DeepClone(), Nevron.Nov.Text.NRichTextDocument)

                ' TODO Implement your own document filtering
                Select Case Me.m_Example.m_ContentTypeComboBox.SelectedIndex
                    Case 0 ' text
                            ' remove all images from the document
                            Dim inlines As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = document.GetDescendants(Nevron.Nov.Text.NImageInline.NImageInlineSchema)

                        For i As Integer = 0 To inlines.Count - 1
                            Dim imageInline As Nevron.Nov.Text.NImageInline = CType(inlines(i), Nevron.Nov.Text.NImageInline)
                            Dim par As Nevron.Nov.Text.NParagraph = CType(imageInline.ParentBlock, Nevron.Nov.Text.NParagraph)
                            par.Inlines.Remove(imageInline)
                        Next

                    Case 1 ' image
                            ' remove all text inlines from the document
                            Dim inlines As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Dom.NNode) = document.GetDescendants(Nevron.Nov.Text.NTextInline.NTextInlineSchema)

                        For i As Integer = 0 To inlines.Count - 1
                            Dim textInline As Nevron.Nov.Text.NTextInline = CType(inlines(i), Nevron.Nov.Text.NTextInline)
                            Dim par As Nevron.Nov.Text.NParagraph = CType(textInline.ParentBlock, Nevron.Nov.Text.NParagraph)
                            par.Inlines.Remove(textInline)
                        Next
                        ' do nothing
                        Case 2 ' image and text
                End Select

                dataObject.SetData(Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat.s_DataFormat, document)
            End Sub
            ''' <summary>
            ''' The underling text format
            ''' </summary>
            Public Overrides ReadOnly Property TextFormat As Nevron.Nov.Text.Formats.NTextFormat
                Get
                    Return Nothing
                End Get
            End Property
            ''' <summary>
            ''' The underling text format
            ''' </summary>
            Public Overrides ReadOnly Property DataFormat As Nevron.Nov.UI.NDataFormat
                Get
                    Return Nevron.Nov.Examples.Text.NCustomClipboardFormatExample.NCustomClipboardFormat.s_DataFormat
                End Get
            End Property

            #EndRegion

            #Region"DataFormat Implementation"

            ''' <summary>
            ''' Serialization function for the data format
            ''' </summary>
            ''' <paramname="format"></param>
            ''' <paramname="obj"></param>
            ''' <returns></returns>
            Private Shared Function SerializeDataObject(ByVal format As Nevron.Nov.UI.NDataFormat, ByVal obj As Object) As Byte()
                Dim document As Nevron.Nov.Text.NRichTextDocument = CType(obj, Nevron.Nov.Text.NRichTextDocument)
                Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream(10240)
                Dim serializer As Nevron.Nov.Serialization.NDomNodeSerializer = New Nevron.Nov.Serialization.NDomNodeSerializer()
                serializer.SaveToStream(New Nevron.Nov.Dom.NNode() {document}, stream, Nevron.Nov.Serialization.ENPersistencyFormat.Xml)
                Return stream.ToArray()
            End Function
            ''' <summary>
            ''' Deserialization function for the custom data format
            ''' </summary>
            ''' <paramname="format"></param>
            ''' <paramname="bytes"></param>
            ''' <returns></returns>
            Private Shared Function DeserializeDataObject(ByVal format As Nevron.Nov.UI.NDataFormat, ByVal bytes As Byte()) As Object
                Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream(bytes)
                Dim serializer As Nevron.Nov.Serialization.NDomNodeDeserializer = New Nevron.Nov.Serialization.NDomNodeDeserializer()
                Dim myObject As Nevron.Nov.Text.NRichTextDocument = CType(serializer.LoadFromStream(stream, Nevron.Nov.Serialization.ENPersistencyFormat.Xml)(0), Nevron.Nov.Text.NRichTextDocument)
                Return myObject
            End Function

            #EndRegion

            #Region"Fields"

            Private m_Example As Nevron.Nov.Examples.Text.NCustomClipboardFormatExample

            #EndRegion

            #Region"Static Fields"

            Friend Shared s_DataFormat As Nevron.Nov.UI.NDataFormat

            #EndRegion
        End Class
    
        #EndRegion

        #Region"Fields"

        Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_ContentTypeComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCustomClipboardFormatExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
