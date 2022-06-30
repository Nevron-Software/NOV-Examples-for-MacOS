Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NStyleEditorsExample
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
            Nevron.Nov.Examples.Framework.NStyleEditorsExample.NStyleEditorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStyleEditorsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            tab.TabPages.Add(CreateTabPage("Fill Styles", NStyleNode.FillProperty, NStyleNode.ColorFillProperty, NStyleNode.StockGradientFillProperty, NStyleNode.LinearGradientFillProperty, NStyleNode.RadialGradientFillProperty, NStyleNode.AdvancedGradientFillProperty, NStyleNode.HatchFillProperty, NStyleNode.ImageFillProperty))
            tab.TabPages.Add(CreateTabPage("Stroke Styles", NStyleNode.StrokeProperty))
            tab.TabPages.Add(CreateTabPage("Borders", NStyleNode.BorderProperty))
            tab.TabPages.Add(CreateTabPage("Text Styles", NStyleNode.FontProperty))
            Return tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how the designers of the style related nodes look. Select the tab page of
    the style category you are interested in and click the button next to a style node to see its designer.    
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateTabPage(ByVal title As String, ParamArray properties As Nevron.Nov.Dom.NProperty()) As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage(title)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Examples.Framework.NStyleEditorsExample.Designer.CreatePropertyEditors(New NStyleNode(), properties)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Return page
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStyleEditorsExample.
		''' </summary>
		Public Shared ReadOnly NStyleEditorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly Designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(NStyleNode.NStyleNodeSchema)

		#EndRegion
	End Class
End Namespace
