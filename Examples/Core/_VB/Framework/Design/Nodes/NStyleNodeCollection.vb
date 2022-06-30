Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom

Namespace Nevron.Nov.Examples.Framework
    Public MustInherit Class NStyleNodeCollectionBase
        Inherits Nevron.Nov.Dom.NNodeCollection(Of NStyleNode)
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
            Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase.NStyleNodeCollectionBaseSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase), Nevron.Nov.Dom.NNodeCollection(Of NStyleNode).NNodeCollectionSchema)
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStyleNodeCollectionBase.
		''' </summary>
		Public Shared ReadOnly NStyleNodeCollectionBaseSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class

    Public Class NStyleNodeCollectionList
        Inherits Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase
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
            Nevron.Nov.Examples.Framework.NStyleNodeCollectionList.NStyleNodeCollectionListSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStyleNodeCollectionList), Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase.NStyleNodeCollectionBaseSchema)

			' Designer
			Call Nevron.Nov.Examples.Framework.NStyleNodeCollectionList.NStyleNodeCollectionListSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.Framework.NStyleNodeCollectionList.NStyleNodeCollectionListDesigner)))
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStyleNodeCollectionList.
		''' </summary>
		Public Shared ReadOnly NStyleNodeCollectionListSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Designer"

		''' <summary>
		''' Designer for NStyleNodeCollectionList.
		''' </summary>
		Public Class NStyleNodeCollectionListDesigner
            Inherits Nevron.Nov.Editors.NDesigner

            Public Sub New()
                MyBase.HierarchyEmbeddableEditor = Nevron.Nov.Editors.NChildrenHierarchyEditor.ListBoxTemplate
            End Sub
        End Class

		#EndRegion
	End Class

    Public Class NStyleNodeCollectionTree
        Inherits Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase
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
            Nevron.Nov.Examples.Framework.NStyleNodeCollectionTree.NStyleNodeCollectionTreeSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStyleNodeCollectionTree), Nevron.Nov.Examples.Framework.NStyleNodeCollectionBase.NStyleNodeCollectionBaseSchema)

			' Designer
			Call Nevron.Nov.Examples.Framework.NStyleNodeCollectionTree.NStyleNodeCollectionTreeSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.Framework.NStyleNodeCollectionTree.NStyleNodeCollectionTreeDesigner)))
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStyleNodeCollectionTree.
		''' </summary>
		Public Shared ReadOnly NStyleNodeCollectionTreeSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Designer"

		''' <summary>
		''' Designer for NStyleNodeCollectionTree.
		''' </summary>
		Public Class NStyleNodeCollectionTreeDesigner
            Inherits Nevron.Nov.Editors.NDesigner
        End Class

		#EndRegion
	End Class
End Namespace
