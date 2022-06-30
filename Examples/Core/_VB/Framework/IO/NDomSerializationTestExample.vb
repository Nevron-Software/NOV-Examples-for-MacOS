Imports System
Imports System.IO
Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Examples.Text
Imports Nevron.Nov.Serialization
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example automatically tests all all node types for serialization.
	''' </summary>
	Public Class NDomSerializationTestExample
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
            Nevron.Nov.Examples.Framework.NDomSerializationTestExample.NDomSerializationTestExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NDomSerializationTestExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_TextBox.Multiline = True
            Me.m_TextBox.VScrollMode = Nevron.Nov.UI.ENScrollMode.WhenNeeded
            Return Me.m_TextBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim testBinarySerializationButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Test Binary Serialization")
            testBinarySerializationButton.Tag = Nevron.Nov.Serialization.ENPersistencyFormat.Binary
            AddHandler testBinarySerializationButton.MouseDown, AddressOf Me.OnTestSerializationButtonMouseDown
            AddHandler testBinarySerializationButton.Click, AddressOf Me.OnTestSerializationButtonClick
            stack.Add(testBinarySerializationButton)
            Dim testXmlSerializationButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Test XML Serialization")
            testXmlSerializationButton.Tag = Nevron.Nov.Serialization.ENPersistencyFormat.Xml
            AddHandler testXmlSerializationButton.MouseDown, AddressOf Me.OnTestSerializationButtonMouseDown
            AddHandler testXmlSerializationButton.Click, AddressOf Me.OnTestSerializationButtonClick
            stack.Add(testXmlSerializationButton)
            Dim clearLogButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear Log")
            AddHandler clearLogButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearLogButtonClick)
            stack.Add(clearLogButton)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example performs automatic serialization test to all NNode derived objects in the Nevron.Nov.Presentation assembly.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnTestSerializationButtonMouseDown(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
			' Set Wait cursor
			Me.m_TextBox.DisplayWindow.Cursor = New Nevron.Nov.UI.NCursor(Nevron.Nov.UI.ENPredefinedCursor.Wait)
        End Sub

        Private Sub OnTestSerializationButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim persistencyFormat As Nevron.Nov.Serialization.ENPersistencyFormat = CType(arg.TargetNode.Tag, Nevron.Nov.Serialization.ENPersistencyFormat)
            Dim stopwatch As Nevron.Nov.NStopwatch

            Try
                Dim nodeType As System.Type = GetType(Nevron.Nov.Dom.NNode)
                Dim types As System.Type() = nodeType.Assembly.GetTypes()
                Dim nodeCount As Integer = 0, successfullySerialized As Integer = 0
                stopwatch = Nevron.Nov.NStopwatch.StartNew()
                Dim output As System.Text.StringBuilder = New System.Text.StringBuilder()
                Dim i As Integer = 0, count As Integer = types.Length

                While i < count
                    Dim type As System.Type = types(i)

					' not a NNode type, abstract or generic => skip
					If Not nodeType.IsAssignableFrom(type) OrElse type.IsAbstract OrElse type.IsGenericType Then Continue While
                    Dim node As Nevron.Nov.Dom.NNode

                    Try
                        nodeCount += 1
                        Dim typeInstance As Nevron.Nov.Dom.NNode = CType(System.Activator.CreateInstance(type), Nevron.Nov.Dom.NNode)

						' Serialize
						Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream()
                        Dim serializer As Nevron.Nov.Serialization.NDomNodeSerializer = New Nevron.Nov.Serialization.NDomNodeSerializer()
                        serializer.SerializeDefaultValues = True
                        serializer.SaveToStream(New Nevron.Nov.Dom.NNode() {typeInstance}, memoryStream, persistencyFormat)

						' Deserialize to check if the serialization has succeeded
						Dim deserializer As Nevron.Nov.Serialization.NDomNodeDeserializer = New Nevron.Nov.Serialization.NDomNodeDeserializer()
                        memoryStream = New System.IO.MemoryStream(memoryStream.ToArray())
                        node = deserializer.LoadFromStream(memoryStream, persistencyFormat)(0)
                        output.AppendLine("Sucessfully serialized node type [" & type.Name & "].")
                        successfullySerialized += 1
                    Catch ex As System.Exception
                        output.AppendLine("Failed to serialize node type [" & type.Name & "]. Exception was [" & ex.Message & "]")
                    End Try

                    i += 1
                End While

                stopwatch.[Stop]()
                output.AppendLine("==================================================")
                output.AppendLine("Nodes serialized: " & successfullySerialized.ToString() & " of " & nodeCount.ToString())
                output.AppendLine("Time elapsed: " & stopwatch.ElapsedMilliseconds.ToString() & " ms")
                Me.m_TextBox.Text = output.ToString()
                Me.m_TextBox.SetCaretPos(New Nevron.Nov.Text.NTextPosition(Me.m_TextBox.Text.Length, False))
                Me.m_TextBox.EnsureCaretVisible()
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteLine(ex.Message)
            End Try

			' Restore the default cursor
			Me.DisplayWindow.Cursor = Nothing
        End Sub

        Private Sub OnClearLogButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_TextBox.Text = System.[String].Empty
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDomSerializationTestExample.
		''' </summary>
		Public Shared ReadOnly NDomSerializationTestExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
