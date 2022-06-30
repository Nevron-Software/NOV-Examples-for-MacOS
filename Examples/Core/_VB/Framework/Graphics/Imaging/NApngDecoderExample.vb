Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NApngDecoderExample
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
            Nevron.Nov.Examples.Framework.NApngDecoderExample.NApngDecoderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NApngDecoderExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim bouncingBeachBall As Nevron.Nov.Graphics.NImage = AnimateImage(NResources.Image_AnimatedPNGs_BouncingBeachBall_png)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Bouncing beach ball:", New Nevron.Nov.UI.NImageBox(bouncingBeachBall)))
            Dim smiley As Nevron.Nov.Graphics.NImage = AnimateImage(NResources.Image_AnimatedPNGs_Smiley_png)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Smiley:", New Nevron.Nov.UI.NImageBox(smiley)))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates NOV's animated PNG (APNG) image decoding capabilities and the built-in support for animated PNGs.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function AnimateImage(ByVal image As Nevron.Nov.Graphics.NImage) As Nevron.Nov.Graphics.NImage
            Dim encodedImageSource As Nevron.Nov.Graphics.NEncodedImageSource = CType(image.ImageSource, Nevron.Nov.Graphics.NEncodedImageSource)
            encodedImageSource.AnimateFrames = True
            Return image
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NApngDecoderExample.
		''' </summary>
		Public Shared ReadOnly NApngDecoderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
