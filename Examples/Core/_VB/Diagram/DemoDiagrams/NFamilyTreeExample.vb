Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Diagram.Layout
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Diagram
    Public Class NFamilyTreeExample
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
            Nevron.Nov.Examples.Diagram.NFamilyTreeExample.NFamilyTreeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Diagram.NFamilyTreeExample), NExampleBaseSchema)
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
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' Get the family tree drawing extension
			Dim familyTreeExtension As Nevron.Nov.Diagram.NFamilyTreeExtension = Me.m_DrawingView.Content.Extensions.FindByType(Of Nevron.Nov.Diagram.NFamilyTreeExtension)()

			' Create property editors
			Dim propertyEditors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((familyTreeExtension), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(familyTreeExtension, Nevron.Nov.Diagram.NFamilyTreeExtension.DateFormatProperty, Nevron.Nov.Diagram.NFamilyTreeExtension.ShowPhotosProperty)

            For i As Integer = 0 To propertyEditors.Count - 1
                stack.Add(propertyEditors(i))
            Next

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and arrange Family Tree diagrams. Use the controls
	on the right to change the family tree settings. You can also click the ""Settings"" button
	in the ""Family Tree"" contextual ribbon tab.
</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitDiagram(ByVal drawingDocument As Nevron.Nov.Diagram.NDrawingDocument)
			' Get drawing and the active page
			Dim drawing As Nevron.Nov.Diagram.NDrawing = drawingDocument.Content
            Dim page As Nevron.Nov.Diagram.NPage = drawing.ActivePage

			' Set the family tree extension to the drawing to activate the "Family Tree" ribbon tab
			drawing.Extensions = New Nevron.Nov.Diagram.NDiagramExtensionCollection()
            drawing.Extensions.Add(New Nevron.Nov.Diagram.NFamilyTreeExtension())

			' Create 3 person shapes
			Dim fatherShape As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Male, "Abraham", "Lincoln", New Nevron.Nov.NDateTime(1809, 02, 12), New Nevron.Nov.NDateTime(1865, 04, 15))
            page.Items.Add(fatherShape)
            Dim motherShape As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Female, "Mary", "Todd", New Nevron.Nov.NDateTime(1811), Nothing)
            page.Items.Add(motherShape)
            Dim childShape1 As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Male, "Thomas", "Lincoln", New Nevron.Nov.NDateTime(1853, 4, 4), New Nevron.Nov.NDateTime(1871))
            page.Items.Add(childShape1)
            Dim childShape2 As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Male, "Robert Todd", "Lincoln", New Nevron.Nov.NDateTime(1843, 8, 1), New Nevron.Nov.NDateTime(1926, 7, 26))
            page.Items.Add(childShape2)
            Dim childShape3 As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Male, "William Wallace", "Lincoln", New Nevron.Nov.NDateTime(1850, 12, 21), New Nevron.Nov.NDateTime(1862, 2, 20))
            page.Items.Add(childShape3)
            Dim childShape4 As Nevron.Nov.Diagram.NPersonShape = New Nevron.Nov.Diagram.NPersonShape(Nevron.Nov.Diagram.ENGender.Male, "Edward Baker", "Lincoln", New Nevron.Nov.NDateTime(1846, 3, 10), New Nevron.Nov.NDateTime(1850, 2, 1))
            page.Items.Add(childShape4)

			' Create a family shape
			Dim familyShape As Nevron.Nov.Diagram.NFamilyShape = New Nevron.Nov.Diagram.NFamilyShape()
            familyShape.Marriage = New Nevron.Nov.Diagram.NFamilyTreeEvent(New Nevron.Nov.NDateTime(1842, 11, 4))
            page.Items.Add(familyShape)
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(fatherShape, familyShape))
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(motherShape, familyShape))
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(familyShape, childShape1))
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(familyShape, childShape2))
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(familyShape, childShape3))
            page.Items.Add(Nevron.Nov.Examples.Diagram.NFamilyTreeExample.CreateConnector(familyShape, childShape4))

			' Arrange the family tree shapes
			Dim layout As Nevron.Nov.Diagram.Layout.NFamilyGraphLayout = New Nevron.Nov.Diagram.Layout.NFamilyGraphLayout()
            Dim shapes As Object() = page.GetShapes(False).ToArray(Of Object)()
            layout.Arrange(shapes, New Nevron.Nov.Diagram.Layout.NDrawingLayoutContext(page))

			' Size the page to its content
			page.SizeToContent()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DrawingView As Nevron.Nov.Diagram.NDrawingView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFamilyTreeExample.
		''' </summary>
		Public Shared ReadOnly NFamilyTreeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateConnector(ByVal fromShape As Nevron.Nov.Diagram.NShape, ByVal toShape As Nevron.Nov.Diagram.NShape) As Nevron.Nov.Diagram.NRoutableConnector
            Dim connector As Nevron.Nov.Diagram.NRoutableConnector = New Nevron.Nov.Diagram.NRoutableConnector()
            connector.GlueBeginToShape(fromShape)
            connector.GlueEndToShape(toShape)
            Return connector
        End Function

		#EndRegion
	End Class
End Namespace
