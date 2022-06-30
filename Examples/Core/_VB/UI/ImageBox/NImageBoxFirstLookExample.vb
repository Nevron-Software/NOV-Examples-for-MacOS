Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NImageBoxFirstLookExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.UI.NImageBoxFirstLookExample.NImageBoxFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NImageBoxFirstLookExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create an image box
			Me.m_ImageBox = New Nevron.Nov.UI.NImageBox(NResources.Image_SampleImage_png)
            Me.m_ImageBox.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_ImageBox.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_ImageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ImageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return Me.m_ImageBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_ImageBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_ImageBox, Nevron.Nov.UI.NImageBox.HorizontalPlacementProperty, Nevron.Nov.UI.NImageBox.VerticalPlacementProperty, Nevron.Nov.UI.NImageBox.BackgroundFillProperty, Nevron.Nov.UI.NImageBox.ImageMappingProperty, Nevron.Nov.UI.NImageBox.ImageRenderModeProperty, Nevron.Nov.UI.NImageBox.ImageProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the features of the Nevron image box widget. Use the controls to the right to load an image
	and change the image box settings.
</p>
"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_ImageBox As Nevron.Nov.UI.NImageBox

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NImageBoxFirstLookExample
		''' </summary>
		Public Shared ReadOnly NImageBoxFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
