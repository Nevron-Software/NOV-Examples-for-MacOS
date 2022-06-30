Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NNodeDesignersExample
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
            Nevron.Nov.Examples.Framework.NNodeDesignersExample.NNodeDesignersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NNodeDesignersExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
			
			' Reset the style node counter
			NStyleNode.Counter = 1

			' Create the show designer buttons
			stack.Add(CreateShowDesignerButton(New NSimpleNode()))
            stack.Add(CreateShowDesignerButton(New NStyleNode()))
            stack.Add(CreateShowDesignerButton(Me.CreateStyleNodesTree()))
            stack.Add(CreateShowDesignerButton(Me.CreateStyleNodesList()))
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to show the designer for a given Nevron DOM node.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateShowDesignerButton(ByVal node As Nevron.Nov.Dom.NNode) As Nevron.Nov.UI.NButton
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(Nevron.Nov.Editors.NDesigner.GetDesigner(CType((node), Nevron.Nov.Dom.NNode)).ToString())
            button.Tag = node
            AddHandler button.Click, AddressOf Me.OnShowDesignerClick
            Return button
        End Function

        Private Function CreateStyleNodesList() As NStyleNodeCollectionList
            Dim collection As NStyleNodeCollectionList = New NStyleNodeCollectionList()
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Red))
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Green))
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Blue))
            Return collection
        End Function

        Private Function CreateStyleNodesTree() As NStyleNodeCollectionTree
            Dim collection As NStyleNodeCollectionTree = New NStyleNodeCollectionTree()
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Red))
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Green))
            collection.Add(Me.CreateStyleNode(Nevron.Nov.Graphics.NColor.Blue))
            Return collection
        End Function

        Private Function CreateStyleNode(ByVal color As Nevron.Nov.Graphics.NColor) As NStyleNode
            Dim styleNode As NStyleNode = New NStyleNode()
            styleNode.ColorFill = New Nevron.Nov.Graphics.NColorFill(color)
            Return styleNode
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowDesignerClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Try
                Dim button As Nevron.Nov.UI.NButton = CType(args.TargetNode, Nevron.Nov.UI.NButton)
                Dim node As Nevron.Nov.Dom.NNode = CType(button.Tag, Nevron.Nov.Dom.NNode)
                Dim editorWindow As Nevron.Nov.Editors.NEditorWindow = Nevron.Nov.Editors.NEditorWindow.CreateForInstance(node, Nothing, button.DisplayWindow, Nothing)

                If TypeOf node Is NStyleNodeCollectionTree Then
                    editorWindow.PreferredSize = New Nevron.Nov.Graphics.NSize(500, 360)
                End If

                editorWindow.Open()
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteException("OnShowDesignerClick failed.", ex)
            End Try
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NNodeDesignersExample.
		''' </summary>
		Public Shared ReadOnly NNodeDesignersExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
