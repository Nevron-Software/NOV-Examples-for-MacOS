Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NBackgroundsExample
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
            Nevron.Nov.Examples.UI.NBackgroundsExample.NBackgroundsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NBackgroundsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a table layout panel
			Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.MaxOrdinal = 3
            table.HorizontalSpacing = 10
            table.VerticalSpacing = 10
            table.ColFillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            table.ColFitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            table.RowFitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            table.RowFillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            table.UniformWidths = Nevron.Nov.Layout.ENUniformSize.Max
            table.UniformHeights = Nevron.Nov.Layout.ENUniformSize.Max

			' Create widgets to demonstrate the different background types
			table.Add(Me.CreateWidget("Solid Background", New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)))
            table.Add(Me.CreateWidget("Hatch Background", New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.Cross, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.PapayaWhip)))
            table.Add(Me.CreateWidget("Gradient Background", New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.FromCenter, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.PapayaWhip)))
            table.Add(Me.CreateWidget("Predefined Advanced Gradient", Nevron.Nov.Graphics.NAdvancedGradientFill.Create(Nevron.Nov.Graphics.ENAdvancedGradientColorScheme.Fire1, 5)))
            Dim agFill As Nevron.Nov.Graphics.NAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            agFill.BackgroundColor = Nevron.Nov.Graphics.NColor.Black
            agFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.2F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
            agFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Green, New Nevron.Nov.NAngle(90, Nevron.Nov.NUnit.Degree), 0.5F, 0.5F, 0.2F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Line))
            agFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.NAngle.Zero, 0.5F, 0.5F, 0.5F, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            table.Add(Me.CreateWidget("Custom Advanced Gradient", agFill))
            Dim imageFill As Nevron.Nov.Graphics.NImageFill = New Nevron.Nov.Graphics.NImageFill(NResources.Image__24x24_Shortcuts_png)
            imageFill.TextureMapping = New Nevron.Nov.Graphics.NAlignTextureMapping()
            table.Add(Me.CreateWidget("Normal Image Background", imageFill))
            imageFill = New Nevron.Nov.Graphics.NImageFill(NResources.Image__24x24_Shortcuts_png)
            imageFill.TextureMapping = New Nevron.Nov.Graphics.NAlignTextureMapping()
            table.Add(Me.CreateWidget("Fit Image Background", imageFill))
            imageFill = New Nevron.Nov.Graphics.NImageFill(NResources.Image__24x24_Shortcuts_png)
            imageFill.TextureMapping = New Nevron.Nov.Graphics.NStretchTextureMapping()
            table.Add(Me.CreateWidget("Stretched Image Background", imageFill))
            imageFill = New Nevron.Nov.Graphics.NImageFill(NResources.Image__24x24_Shortcuts_png)
            imageFill.TextureMapping = New Nevron.Nov.Graphics.NTileTextureMapping()
            table.Add(Me.CreateWidget("Tiled Image Background", imageFill))
            Return table
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create different types of backgrounds and apply them to widgets.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateWidget(ByVal text As String, ByVal backgroundFill As Nevron.Nov.Graphics.NFill) As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            Dim widget As Nevron.Nov.UI.NWidget = New Nevron.Nov.UI.NWidget()
            widget.BackgroundFill = backgroundFill
            stack.Add(widget)
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            stack.Add(label)
            Return stack
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NBackgroundsExample.
		''' </summary>
		Public Shared ReadOnly NBackgroundsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
