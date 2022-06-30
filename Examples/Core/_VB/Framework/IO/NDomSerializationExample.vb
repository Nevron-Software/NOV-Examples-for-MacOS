Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Examples.Text
Imports Nevron.Nov.Serialization
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Friend Class NTestNode
        Inherits Nevron.Nov.Dom.NNode

        Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NTestNode.NTestNodeSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NTestNode), Nevron.Nov.Dom.NNode.NNodeSchema)
            Nevron.Nov.Examples.Framework.NTestNode.ExtendedPropertyEx = Nevron.Nov.Dom.NProperty.CreateExtended(Nevron.Nov.Examples.Framework.NTestNode.NTestNodeSchema, "Extended", Nevron.Nov.Dom.NDomType.[Boolean], True)
        End Sub

        Public Shared ReadOnly ExtendedPropertyEx As Nevron.Nov.Dom.NProperty
        Public Shared NTestNodeSchema As Nevron.Nov.Dom.NSchema
    End Class
	''' <summary>
	''' The example demonstrates how to modify the table borders, spacing etc.
	''' </summary>
	Public Class NDomSerializationExample
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
            Nevron.Nov.Examples.Framework.NDomSerializationExample.NDomSerializationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NDomSerializationExample), NExampleBaseSchema)
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
            Dim saveStateButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save")
            AddHandler saveStateButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSaveStateButtonClick)
            stack.Add(saveStateButton)
            Me.m_LoadStateButton = New Nevron.Nov.UI.NButton("Load")
            Me.m_LoadStateButton.Enabled = False
            AddHandler Me.m_LoadStateButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnLoadStateButtonClick)
            stack.Add(Me.m_LoadStateButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to use DOM serialization in order to serialize / deserialize NOV NNode derived objects.</p>
<p>Press the Save button on the right to save the contents of the document and load to restore the contents to the last saved one.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Type some text here..."))
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSaveStateButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Try
                Me.m_MemoryStream = New System.IO.MemoryStream()
                Dim serializer As Nevron.Nov.Serialization.NDomNodeSerializer = New Nevron.Nov.Serialization.NDomNodeSerializer()
                Dim testNode As Nevron.Nov.Examples.Framework.NTestNode = New Nevron.Nov.Examples.Framework.NTestNode()
                testNode.SetValue(Nevron.Nov.Examples.Framework.NTestNode.ExtendedPropertyEx, False)
                serializer.SaveToStream(New Nevron.Nov.Dom.NNode() {testNode}, Me.m_MemoryStream, Nevron.Nov.Serialization.ENPersistencyFormat.Binary)

'				serializer.SaveToStream(new NNode[] { m_RichText.Content }, m_MemoryStream, ENPersistencyFormat.Binary);

				Me.m_LoadStateButton.Enabled = True
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteLine(ex.Message)
            End Try
        End Sub

        Private Sub OnLoadStateButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            If Me.m_MemoryStream Is Nothing Then Return
            Me.m_MemoryStream.Seek(0, System.IO.SeekOrigin.Begin)

            Try
                Dim deserializer As Nevron.Nov.Serialization.NDomNodeDeserializer = New Nevron.Nov.Serialization.NDomNodeDeserializer()
' 				NDocumentBlock root = (NDocumentBlock)deserializer.LoadFromStream(m_MemoryStream, ENPersistencyFormat.Binary)[0];
' 
' 				if (root != null)
' 				{
' 					m_RichText.Document = new NRichTextDocument(root);
' 				}
			Dim root As Nevron.Nov.Examples.Framework.NTestNode = CType(deserializer.LoadFromStream(Me.m_MemoryStream, Nevron.Nov.Serialization.ENPersistencyFormat.Binary)(0), Nevron.Nov.Examples.Framework.NTestNode)
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteLine(ex.Message)
            End Try
        End Sub

        Private Sub OnLoadDocumentButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_LoadStateButton As Nevron.Nov.UI.NButton
        Private m_MemoryStream As System.IO.MemoryStream

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDomSerializationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
