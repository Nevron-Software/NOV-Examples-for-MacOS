Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NUniSizeBoxExample
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
            Nevron.Nov.Examples.UI.NUniSizeBoxExample.NUniSizeBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NUniSizeBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_PairBoxes = New Nevron.Nov.UI.NPairBox(Nevron.Nov.Examples.UI.NUniSizeBoxExample.Texts.Length / 2 - 1) {}

            For i As Integer = 0 To Me.m_PairBoxes.Length - 1
                Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel(Nevron.Nov.Examples.UI.NUniSizeBoxExample.Texts(i * 2)), New Nevron.Nov.UI.NLabel(Nevron.Nov.Examples.UI.NUniSizeBoxExample.Texts(i * 2 + 1)), True)
                pairBox.Box1.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Blue)
                pairBox.Box1.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                pairBox.Box2.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                pairBox.Box2.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
                Me.m_PairBoxes(i) = pairBox
                stack.Add(pairBox)
            Next

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim i As Integer = 0, count As Integer = Me.m_PairBoxes.Length

            While i < count
				' Create the pair box property editors
				Dim pairBox As Nevron.Nov.UI.NPairBox = Me.m_PairBoxes(i)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((pairBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(pairBox, Nevron.Nov.UI.NPairBox.FillModeProperty, Nevron.Nov.UI.NPairBox.FitModeProperty)
                Dim box1 As Nevron.Nov.UI.NUniSizeBox = CType(pairBox.Box1, Nevron.Nov.UI.NUniSizeBox)
                editors.Add(Nevron.Nov.Editors.NDesigner.GetDesigner(CType((box1), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(box1, Nevron.Nov.UI.NUniSizeBox.UniSizeModeProperty))
                Dim box2 As Nevron.Nov.UI.NUniSizeBox = CType(pairBox.Box2, Nevron.Nov.UI.NUniSizeBox)
                editors.Add(Nevron.Nov.Editors.NDesigner.GetDesigner(CType((box2), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(box2, Nevron.Nov.UI.NUniSizeBox.UniSizeModeProperty))

				' Create the properties stack panel
				Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                Dim j As Integer = 0, editorCount As Integer = editors.Count

                While j < editorCount
                    propertyStack.Add(editors(j))
                    j += 1
                End While

				' Add the box 1 preferred height editor
				Dim editor As Nevron.Nov.Editors.NPropertyEditor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((box1.Content), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(box1.Content, Nevron.Nov.UI.NWidget.PreferredHeightProperty)
                Dim label As Nevron.Nov.UI.NLabel = editor.GetFirstDescendant(Of Nevron.Nov.UI.NLabel)()
                label.Text = "Box 1 Preferred Height:"
                propertyStack.Add(editor)

				' Add the box 2 preferred height editor
				editor = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((box2.Content), Nevron.Nov.Dom.NNode)).CreatePropertyEditor(box2.Content, Nevron.Nov.UI.NWidget.PreferredHeightProperty)
                label = editor.GetFirstDescendant(Of Nevron.Nov.UI.NLabel)()
                label.Text = "Box 2 Preferred Height:"
                propertyStack.Add(editor)

				' Create a group box for the properties
				Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Pair Box " & (i + 1).ToString())
                groupBox.Content = propertyStack
                stack.Add(groupBox)
                i += 1
            End While

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create several pair boxes and place them in an alignable element container.
	Alignable element containers let the user specify how to size the alignable elements in the container.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_PairBoxes As Nevron.Nov.UI.NPairBox()

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NUniSizeBoxExample.
		''' </summary>
		Public Shared ReadOnly NUniSizeBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly Texts As String() = New String() {"This is box 1.1", "I'm box 1.2 and I'm wider", "Box 2.1", "Box 2.2", "I am box 3.1 and I am the widest one", "The last box - 3.2"}

		#EndRegion
	End Class
End Namespace
