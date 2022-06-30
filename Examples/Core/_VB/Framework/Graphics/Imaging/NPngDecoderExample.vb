Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The PNG images displayed in this example are created by Willem van Schaik (willem@schaik.com).
	''' For more information: http://schaik.com/
	''' </summary>
	Public Class NPngDecoderExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NPngDecoderExample.NPngDecoderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NPngDecoderExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim colHeadings As String() = New String() {"Image", "Description", "Decoded with NOV Decoders", "Decoded with Native Decoders"}
            Dim colCount As Integer = colHeadings.Length
            Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            table.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            table.Padding = New Nevron.Nov.Graphics.NMargins(30)
            table.HorizontalSpacing = 30
            table.VerticalSpacing = 30
            table.MaxOrdinal = colCount
            Dim imageNames As Nevron.Nov.DataStructures.NList(Of String) = NImageDecodingExampleHelper.GetImageNames("PngSuite", "png")
            Dim descriptions As Nevron.Nov.DataStructures.NMap(Of String, String) = NImageDecodingExampleHelper.LoadDescriptions(NResources.String_PngSuite_txt)

            For i As Integer = 0 To colCount - 1
                Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(colHeadings(i))
                label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 9, Nevron.Nov.Graphics.ENFontStyle.Bold)
                table.Add(label)
            Next

            Dim rowCount As Integer = imageNames.Count

            For i As Integer = 0 To rowCount - 1
                Dim resourceName As String = imageNames(i)
                Dim description As String = NImageDecodingExampleHelper.GetImageDescription(descriptions, resourceName)
                Dim nameLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(resourceName)
                nameLabel.MaxWidth = 200
                Dim descriptionLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(description)
                descriptionLabel.MaxWidth = 200
                descriptionLabel.TextWrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
                Dim novImage As Nevron.Nov.Graphics.NImage = NImageDecodingExampleHelper.LoadImage(resourceName, Nevron.Nov.Graphics.ENCodecPreference.OnlyNOV)
                Dim novImageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(novImage)
                novImageBox.ImageMapping = New Nevron.Nov.Graphics.NAlignTextureMapping(Nevron.Nov.ENHorizontalAlignment.Center, Nevron.Nov.ENVerticalAlignment.Center)
                Dim nativeImage As Nevron.Nov.Graphics.NImage = NImageDecodingExampleHelper.LoadImage(resourceName, Nevron.Nov.Graphics.ENCodecPreference.PreferNative)
                Dim nativeImageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(nativeImage)
                nativeImageBox.ImageMapping = New Nevron.Nov.Graphics.NAlignTextureMapping(Nevron.Nov.ENHorizontalAlignment.Center, Nevron.Nov.ENVerticalAlignment.Center)
                table.Add(nameLabel)
                table.Add(descriptionLabel)
                table.Add(novImageBox)
                table.Add(nativeImageBox)
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
	This example demonstrates NOV's PNG image decoding capabilities.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPngDecoderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
