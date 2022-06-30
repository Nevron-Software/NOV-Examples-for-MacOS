Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NCategoryEditorsExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
            Me.m_Node = New NStyleNode()
        End Sub
		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Framework.NCategoryEditorsExample.NCategoryEditorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NCategoryEditorsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Dim designers As Nevron.Nov.Editors.NDesigner() = NStyleNode.Designers
            Dim i As Integer = 0, count As Integer = designers.Length

            While i < count
                Dim designer As Nevron.Nov.Editors.NDesigner = designers(i)
                Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(designer.ToString())
                stack.Add(button)
                button.Tag = designer
                AddHandler button.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnButtonClick)
                i += 1
            End While

            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to use category editor templates in node designers to achieve different designer layouts.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = TryCast(args.TargetNode, Nevron.Nov.UI.NButton)
            If button Is Nothing Then Return
            Dim designer As Nevron.Nov.Editors.NDesigner = CType(button.Tag, Nevron.Nov.Editors.NDesigner)
            Dim editor As Nevron.Nov.Editors.NEditor = designer.CreateInstanceEditor(Me.m_Node)
            Dim window As Nevron.Nov.Editors.NEditorWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(Of Nevron.Nov.Editors.NEditorWindow)()
            window.Editor = editor
            window.Modal = False
            window.Open()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Node As NStyleNode

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCategoryEditorsExample.
		''' </summary>
		Public Shared ReadOnly NCategoryEditorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
