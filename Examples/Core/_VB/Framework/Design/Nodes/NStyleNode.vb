Imports System
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NStyleNode
        Inherits Nevron.Nov.Dom.NNode
		#Region"Constructors"

		''' <summary>
		''' Default constructor.
		''' </summary>
		Public Sub New()
            Me.m_sName = "Style Node " & (System.Math.Min(System.Threading.Interlocked.Increment(Nevron.Nov.Examples.Framework.NStyleNode.Counter), Nevron.Nov.Examples.Framework.NStyleNode.Counter - 1)).ToString()
        End Sub
		''' <summary>
		''' Static constructor.
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Framework.NStyleNode.defaultLinearGradientFill = New Nevron.Nov.Graphics.NLinearGradientFill()
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultLinearGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Red))
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultLinearGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.Yellow))
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultLinearGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Indigo))
            Nevron.Nov.Examples.Framework.NStyleNode.defaultRadialGradientFill = New Nevron.Nov.Graphics.NRadialGradientFill()
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultRadialGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.0F, Nevron.Nov.Graphics.NColor.Red))
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultRadialGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(0.5F, Nevron.Nov.Graphics.NColor.Yellow))
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultRadialGradientFill.GradientStops.Add(New Nevron.Nov.Graphics.NGradientStop(1.0F, Nevron.Nov.Graphics.NColor.Indigo))
            Nevron.Nov.Examples.Framework.NStyleNode.defaultAdvancedGradientFill = New Nevron.Nov.Graphics.NAdvancedGradientFill()
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultAdvancedGradientFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.NAngle.Zero, 0, 0, 1, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Call Nevron.Nov.Examples.Framework.NStyleNode.defaultAdvancedGradientFill.Points.Add(New Nevron.Nov.Graphics.NAdvancedGradientPoint(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.NAngle.Zero, 1, 1, 1, Nevron.Nov.Graphics.ENAdvancedGradientPointShape.Circle))
            Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NStyleNode), Nevron.Nov.Dom.NNode.NNodeSchema)

			' Properties - fill
			Nevron.Nov.Examples.Framework.NStyleNode.FillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("Fill", GetType(Nevron.Nov.Graphics.NFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultFill)
            Nevron.Nov.Examples.Framework.NStyleNode.ColorFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("ColorFill", GetType(Nevron.Nov.Graphics.NColorFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultColorFill)
            Nevron.Nov.Examples.Framework.NStyleNode.StockGradientFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("StockGradientFill", GetType(Nevron.Nov.Graphics.NStockGradientFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultStockGradientFill)
            Nevron.Nov.Examples.Framework.NStyleNode.LinearGradientFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("LinearGradientFill", GetType(Nevron.Nov.Graphics.NLinearGradientFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultLinearGradientFill)
            Nevron.Nov.Examples.Framework.NStyleNode.RadialGradientFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("RadialGradientFill", GetType(Nevron.Nov.Graphics.NRadialGradientFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultRadialGradientFill)
            Nevron.Nov.Examples.Framework.NStyleNode.AdvancedGradientFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("AdvancedGradientFill", GetType(Nevron.Nov.Graphics.NAdvancedGradientFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultAdvancedGradientFill)
            Nevron.Nov.Examples.Framework.NStyleNode.HatchFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("HatchFill", GetType(Nevron.Nov.Graphics.NHatchFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultHatchFill)
            Nevron.Nov.Examples.Framework.NStyleNode.ImageFillProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("ImageFill", GetType(Nevron.Nov.Graphics.NImageFill), Nevron.Nov.Examples.Framework.NStyleNode.defaultImageFill)

			' Broperties - border
			Nevron.Nov.Examples.Framework.NStyleNode.BorderProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("Border", GetType(Nevron.Nov.UI.NBorder), Nevron.Nov.Examples.Framework.NStyleNode.defaultBorder)

			' Broperties - stroke
			Nevron.Nov.Examples.Framework.NStyleNode.StrokeProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("Stroke", GetType(Nevron.Nov.Graphics.NStroke), Nevron.Nov.Examples.Framework.NStyleNode.defaultStroke)

			' Properties - font
			Nevron.Nov.Examples.Framework.NStyleNode.FontProperty = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema.AddSlot("Font", GetType(Nevron.Nov.Graphics.NFont), Nevron.Nov.Examples.Framework.NStyleNode.defaultFont)

			' Constants
			Nevron.Nov.Examples.Framework.NStyleNode.Designers = New Nevron.Nov.Editors.NDesigner() {New Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeHStackDesigner(), New Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeVStackDesigner(), New Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeTabDesigner(), New Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeMixedDesigner()}
        End Sub

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets or sets the value of the Fill property.
		''' </summary>
		Public Property Fill As Nevron.Nov.Graphics.NFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.FillProperty), Nevron.Nov.Graphics.NFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.FillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the color fill style of the node.
		''' </summary>
		Public Property ColorFill As Nevron.Nov.Graphics.NColorFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.ColorFillProperty), Nevron.Nov.Graphics.NColorFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NColorFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.ColorFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the TwoColorGradientFill property.
		''' </summary>
		Public Property StockGradientFill As Nevron.Nov.Graphics.NStockGradientFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.StockGradientFillProperty), Nevron.Nov.Graphics.NStockGradientFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NStockGradientFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.StockGradientFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the LinearGradientFill property.
		''' </summary>
		Public Property LinearGradientFill As Nevron.Nov.Graphics.NLinearGradientFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.LinearGradientFillProperty), Nevron.Nov.Graphics.NLinearGradientFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NLinearGradientFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.LinearGradientFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the TwoColorGradientFill property.
		''' </summary>
		Public Property RadialGradientFill As Nevron.Nov.Graphics.NRadialGradientFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.RadialGradientFillProperty), Nevron.Nov.Graphics.NRadialGradientFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NRadialGradientFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.RadialGradientFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the AdvancedGradientFill property.
		''' </summary>
		Public Property AdvancedGradientFill As Nevron.Nov.Graphics.NAdvancedGradientFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.AdvancedGradientFillProperty), Nevron.Nov.Graphics.NAdvancedGradientFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NAdvancedGradientFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.AdvancedGradientFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the HatchFill property.
		''' </summary>
		Public Property HatchFill As Nevron.Nov.Graphics.NHatchFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.HatchFillProperty), Nevron.Nov.Graphics.NHatchFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NHatchFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.HatchFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the ImageFill property.
		''' </summary>
		Public Property ImageFill As Nevron.Nov.Graphics.NImageFill
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.ImageFillProperty), Nevron.Nov.Graphics.NImageFill)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NImageFill)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.ImageFillProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the border of the node.
		''' </summary>
		Public Property Border As Nevron.Nov.UI.NBorder
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.BorderProperty), Nevron.Nov.UI.NBorder)
            End Get
            Set(ByVal value As Nevron.Nov.UI.NBorder)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.BorderProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gtes/Sets the stroke style of the node.
		''' </summary>
		Public Property Stroke As Nevron.Nov.Graphics.NStroke
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.StrokeProperty), Nevron.Nov.Graphics.NStroke)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NStroke)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.StrokeProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Font property.
		''' </summary>
		Public Property Font As Nevron.Nov.Graphics.NFont
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NStyleNode.FontProperty), Nevron.Nov.Graphics.NFont)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NFont)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NStyleNode.FontProperty, value)
            End Set
        End Property

		#EndRegion

		#Region"Overrides"

		Public Overrides Function ToString() As String
            Return Me.m_sName
        End Function

		#EndRegion

		#Region"Fields"

		Friend m_sName As String

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NStyleNode.
		''' </summary>
		Public Shared ReadOnly NStyleNodeSchema As Nevron.Nov.Dom.NSchema
		''' <summary>
		''' Reference to the Fill property.
		''' </summary>
		Public Shared ReadOnly FillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the ColorFill property.
		''' </summary>
		Public Shared ReadOnly ColorFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the HatchFill property.
		''' </summary>
		Public Shared ReadOnly HatchFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the TwoColorGradientFill property.
		''' </summary>
		Public Shared ReadOnly StockGradientFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the LinearGradientFill property.
		''' </summary>
		Public Shared ReadOnly LinearGradientFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the RadialGradientFill property.
		''' </summary>
		Public Shared ReadOnly RadialGradientFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the AdvancedGradientFill property.
		''' </summary>
		Public Shared ReadOnly AdvancedGradientFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the ImageFill property.
		''' </summary>
		Public Shared ReadOnly ImageFillProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Border property.
		''' </summary>
		Public Shared ReadOnly BorderProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Stroke property.
		''' </summary>
		Public Shared ReadOnly StrokeProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Font property.
		''' </summary>
		Public Shared ReadOnly FontProperty As Nevron.Nov.Dom.NProperty

		#EndRegion

		#Region"Static"

		Public Shared Counter As Integer = 1

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly defaultBorder As Nevron.Nov.UI.NBorder = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
        Private Shared ReadOnly defaultFill As Nevron.Nov.Graphics.NFill = Nothing
        Private Shared ReadOnly defaultColorFill As Nevron.Nov.Graphics.NColorFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.MediumBlue)
        Private Shared ReadOnly defaultStockGradientFill As Nevron.Nov.Graphics.NStockGradientFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.FromCenter, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.NColor.White)
        Private Shared ReadOnly defaultLinearGradientFill As Nevron.Nov.Graphics.NLinearGradientFill
        Private Shared ReadOnly defaultRadialGradientFill As Nevron.Nov.Graphics.NRadialGradientFill
        Private Shared ReadOnly defaultAdvancedGradientFill As Nevron.Nov.Graphics.NAdvancedGradientFill
        Private Shared ReadOnly defaultHatchFill As Nevron.Nov.Graphics.NHatchFill = New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.LightHorizontal, Nevron.Nov.Graphics.NColor.Black, Nevron.Nov.Graphics.NColor.White)
        Private Shared ReadOnly defaultImageFill As Nevron.Nov.Graphics.NImageFill = New Nevron.Nov.Graphics.NImageFill()
        Private Shared ReadOnly defaultStroke As Nevron.Nov.Graphics.NStroke = New Nevron.Nov.Graphics.NStroke()
        Private Shared ReadOnly defaultFont As Nevron.Nov.Graphics.NFont = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10, Nevron.Nov.Graphics.ENFontStyle.Regular)
        Public Shared ReadOnly Designers As Nevron.Nov.Editors.NDesigner()

		#EndRegion

		#Region"Designers"

		''' <summary>
		''' Designer for NStyleNode.
		''' </summary>
		Public MustInherit Class NStyleNodeDesigner
            Inherits Nevron.Nov.Editors.NDesigner
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
                MyBase.Schema = Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeSchema
                Me.m_Name = "Style Node Editor"
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.FillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.ColorFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.HatchFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.StockGradientFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.LinearGradientFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.RadialGradientFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.AdvancedGradientFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.ImageFillProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.FillStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.BorderProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.StrokeStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.StrokeProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.StrokeStylesCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NStyleNode.FontProperty, Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.TextStylesCategory)
            End Sub

            Public Overrides Function ToString() As String
                Return Me.m_Name
            End Function

            Protected m_Name As String
            Protected Shared ReadOnly FillStylesCategory As Nevron.Nov.NLocalizedString = New Nevron.Nov.NLocalizedString("Fill Styles")
            Protected Shared ReadOnly StrokeStylesCategory As Nevron.Nov.NLocalizedString = New Nevron.Nov.NLocalizedString("Stroke Styles")
            Protected Shared ReadOnly TextStylesCategory As Nevron.Nov.NLocalizedString = New Nevron.Nov.NLocalizedString("Text Styles")
        End Class

        Public Class NStyleNodeHStackDesigner
            Inherits Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner

            Public Sub New()
                MyBase.m_Name = "Horizontal Stack Category Editor"
                MyBase.SetCategoryEditor(Nevron.Nov.NLocalizedString.Empty, Nevron.Nov.Editors.NStackCategoryEditor.HorizontalEmbedChildEditorsTemplate)
            End Sub
        End Class

        Public Class NStyleNodeVStackDesigner
            Inherits Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner

            Public Sub New()
                MyBase.m_Name = "Vertical Stack Category Editor"
                MyBase.SetCategoryEditor(Nevron.Nov.NLocalizedString.Empty, Nevron.Nov.Editors.NStackCategoryEditor.VerticalEmbedChildEditorsTemplate)
            End Sub
        End Class

        Public Class NStyleNodeTabDesigner
            Inherits Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner

            Public Sub New()
                MyBase.m_Name = "Tab Category Editor"
                MyBase.SetCategoryEditor(Nevron.Nov.NLocalizedString.Empty, Nevron.Nov.Editors.NTabCategoryEditor.HeadersTopTemplate)
            End Sub
        End Class

        Public Class NStyleNodeMixedDesigner
            Inherits Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner

            Public Sub New()
                MyBase.m_Name = "Mixed Category Editor"
                MyBase.SetCategoryEditor(Nevron.Nov.NLocalizedString.Empty, Nevron.Nov.Editors.NStackCategoryEditor.VerticalEmbedChildEditorsTemplate)
                MyBase.SetCategoryEditor(Nevron.Nov.Examples.Framework.NStyleNode.NStyleNodeDesigner.StrokeStylesCategory, Nevron.Nov.Editors.NTabCategoryEditor.HeadersTopTemplate)
            End Sub
        End Class

		#EndRegion
	End Class
End Namespace
