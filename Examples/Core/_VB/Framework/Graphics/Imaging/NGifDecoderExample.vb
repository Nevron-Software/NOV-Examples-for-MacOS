Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Layout

Namespace Nevron.Nov.Examples.Framework
    Public Class NGifDecoderExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NGifDecoderExample.NGifDecoderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NGifDecoderExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim imageNames As Nevron.Nov.DataStructures.NList(Of String) = NImageDecodingExampleHelper.GetImageNames("GifSuite", "gif")
            Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            table.HorizontalSpacing = 30
            table.VerticalSpacing = 30
            table.MaxOrdinal = 2
            Dim rowCount As Integer = imageNames.Count

            For i As Integer = 0 To rowCount - 1
                Dim nameLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(imageNames(i))
                nameLabel.MaxWidth = 200
                Dim imgSrc As Nevron.Nov.Graphics.NEmbeddedResourceImageSource = New Nevron.Nov.Graphics.NEmbeddedResourceImageSource(New Nevron.Nov.NEmbeddedResourceRef(NResources.Instance, imageNames(i)))
                imgSrc.AnimateFrames = True
                Dim novImageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(New Nevron.Nov.Graphics.NImage(imgSrc))
                novImageBox.ImageMapping = New Nevron.Nov.Graphics.NAlignTextureMapping(Nevron.Nov.ENHorizontalAlignment.Center, Nevron.Nov.ENVerticalAlignment.Center)
                table.Add(nameLabel)
                table.Add(novImageBox)
            Next

			' The table must be scrollable
			Dim scroll As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scroll.Content = table
            Return scroll
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates NOV's GIF image decoding capabilities and the built-in support for animated GIFs.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NGifDecoderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
