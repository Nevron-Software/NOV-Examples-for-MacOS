Imports Nevron.Nov.Dom
Imports Nevron.Nov.UI
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures
Imports System.IO

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The BMP images displayed in this example are created by Jason Summers (jason1@pobox.com).
	''' For more information: http://entropymine.com/jason/bmpsuite/
	''' </summary>
	Public Class NBmpDecoderExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NBmpDecoderExample.NBmpDecoderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NBmpDecoderExample), NExampleBase.NExampleBaseSchema)
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
            Dim imageNames As Nevron.Nov.DataStructures.NList(Of String) = Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.GetImageNames("BmpSuite", "bmp")
            Dim descriptions As Nevron.Nov.DataStructures.NMap(Of String, String) = Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.LoadDescriptions(NResources.String_BmpSuite_txt)

            For i As Integer = 0 To colCount - 1
                Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(colHeadings(i))
                label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 9, Nevron.Nov.Graphics.ENFontStyle.Bold)
                table.Add(label)
            Next

            Dim rowCount As Integer = imageNames.Count

            For i As Integer = 0 To rowCount - 1
                Dim resourceName As String = imageNames(i)
                Dim description As String = Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.GetImageDescription(descriptions, resourceName)
                Dim nameLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(resourceName)
                nameLabel.MaxWidth = 200
                Dim descriptionLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(description)
                descriptionLabel.MaxWidth = 200
                descriptionLabel.TextWrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
                Dim novImage As Nevron.Nov.Graphics.NImage = Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.LoadImage(resourceName, Nevron.Nov.Graphics.ENCodecPreference.OnlyNOV)
                Dim novImageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(novImage)
                novImageBox.ImageMapping = New Nevron.Nov.Graphics.NAlignTextureMapping(Nevron.Nov.ENHorizontalAlignment.Center, Nevron.Nov.ENVerticalAlignment.Center)
                Dim nativeImage As Nevron.Nov.Graphics.NImage = Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.LoadImage(resourceName, Nevron.Nov.Graphics.ENCodecPreference.PreferNative)
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
	This example demonstrates NOV's BMP image decoding capabilities.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBmpDecoderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class

    Friend Module NImageDecodingExampleHelper
        Friend Function GetImageNames(ByVal suiteName As String, ByVal extension As String) As Nevron.Nov.DataStructures.NList(Of String)
            Dim names As String() = NResources.Instance.GetResourceNames()
            Dim resources As Nevron.Nov.DataStructures.NList(Of String) = New Nevron.Nov.DataStructures.NList(Of String)()
            Dim i As Integer = 0, count As Integer = names.Length

            While i < count

                If names(CInt((i))).EndsWith(extension) AndAlso names(CInt((i))).Contains(suiteName) Then
                    resources.Add(names(i))
                End If

                i += 1
            End While

            Return resources
        End Function

        Friend Function LoadDescriptions(ByVal descriptionTextFileContent As String) As Nevron.Nov.DataStructures.NMap(Of String, String)
            Dim descriptions As Nevron.Nov.DataStructures.NMap(Of String, String) = New Nevron.Nov.DataStructures.NMap(Of String, String)()

            Using reader As System.IO.StringReader = New System.IO.StringReader(descriptionTextFileContent)
                Dim line As String

                While Not Equals((CSharpImpl.__Assign(line, reader.ReadLine())), Nothing)
                    Dim dashIndex As Integer = line.IndexOf("-", 0)

                    If dashIndex > 0 Then
                        Dim name As String = line.Remove(CInt((dashIndex))).Trim()
                        Dim description As String = line.Remove(CInt((0)), CInt((dashIndex + 1))).Trim()
                        descriptions.Add(name, description)
                    End If
                End While
            End Using

            Return descriptions
        End Function

        Friend Function ResourceNameToFileName(ByVal resourceName As String) As String
            Dim index As Integer = resourceName.LastIndexOf("_"c)
            index = resourceName.LastIndexOf("_"c, index - 1)
            Return resourceName.Substring(CInt((index + 1))).Replace(CChar(("_"c)), CChar(("."c))).ToLower()
        End Function

        Friend Function GetImageDescription(ByVal descriptions As Nevron.Nov.DataStructures.NMap(Of String, String), ByVal resourceName As String) As String
            Dim desc As String
            If descriptions.TryGet(Nevron.Nov.Examples.Framework.NImageDecodingExampleHelper.ResourceNameToFileName(resourceName), desc) Then Return desc
            Return String.Empty
        End Function

        Friend Function LoadImage(ByVal resourceName As String, ByVal decoderPref As Nevron.Nov.Graphics.ENCodecPreference) As Nevron.Nov.Graphics.NImage
            Dim resource As Nevron.Nov.NEmbeddedResource = NResources.Instance.GetResource(resourceName)
            Dim imageData As Nevron.Nov.Graphics.NImageData = New Nevron.Nov.Graphics.NImageData(resource.Data)

            Try
                Dim raster As Nevron.Nov.Graphics.NRaster = imageData.Decode(decoderPref)
                Return New Nevron.Nov.Graphics.NImage(raster)
            Catch
                Return NResources.Image_ErrorImage_png
            End Try
        End Function

        Private Class CSharpImpl
            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Module
End Namespace
