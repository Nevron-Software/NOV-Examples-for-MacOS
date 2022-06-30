Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Text

Namespace Nevron.Nov.Examples.Framework
    Public Class NSimpleNode
        Inherits Nevron.Nov.Dom.NNode
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
            Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NSimpleNode), Nevron.Nov.Dom.NNode.NNodeSchema)

			' Properties
			Nevron.Nov.Examples.Framework.NSimpleNode.BooleanValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("BooleanValue", Nevron.Nov.Dom.NDomType.[Boolean], Nevron.Nov.Examples.Framework.NSimpleNode.defaultBoolean)
            Nevron.Nov.Examples.Framework.NSimpleNode.IntegerValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("IntegerValue", Nevron.Nov.Dom.NDomType.Int32, Nevron.Nov.Examples.Framework.NSimpleNode.defaultInteger)
            Nevron.Nov.Examples.Framework.NSimpleNode.LongValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("LongValue", Nevron.Nov.Dom.NDomType.Int64, Nevron.Nov.Examples.Framework.NSimpleNode.defaultLong)
            Nevron.Nov.Examples.Framework.NSimpleNode.UnsignedIntegerValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("UnsignedIntegerValue", Nevron.Nov.Dom.NDomType.UInt32, Nevron.Nov.Examples.Framework.NSimpleNode.defaultUnsignedInteger)
            Nevron.Nov.Examples.Framework.NSimpleNode.SingleValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("SingleValue", Nevron.Nov.Dom.NDomType.[Single], Nevron.Nov.Examples.Framework.NSimpleNode.defaultSingle)
            Nevron.Nov.Examples.Framework.NSimpleNode.DoubleValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("DoubleValue", Nevron.Nov.Dom.NDomType.[Double], Nevron.Nov.Examples.Framework.NSimpleNode.defaultDouble)
            Nevron.Nov.Examples.Framework.NSimpleNode.SpecifiedDoubleValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("SpecifiedDoubleValue", Nevron.Nov.Dom.NDomType.[Double], Nevron.Nov.Examples.Framework.NSimpleNode.defaultSpecifiedDouble)
            Nevron.Nov.Examples.Framework.NSimpleNode.ComboBoxEnumValueProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("ComboBoxEnum", GetType(Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum), Nevron.Nov.Examples.Framework.NSimpleNode.defaultComboBoxEnum)
            Nevron.Nov.Examples.Framework.NSimpleNode.HRadioGroupEnumProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("HRadioGroupEnum", GetType(Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum), Nevron.Nov.Examples.Framework.NSimpleNode.defaultHRadioGroupEnum)
            Nevron.Nov.Examples.Framework.NSimpleNode.VRadioGroupEnumProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("VRadioGroupEnum", GetType(Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum), Nevron.Nov.Examples.Framework.NSimpleNode.defaultVRadioGroupEnum)
            Nevron.Nov.Examples.Framework.NSimpleNode.AngleProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Angle", Nevron.Nov.Dom.NDomType.NAngle, Nevron.Nov.Examples.Framework.NSimpleNode.defaultAngle)
            Nevron.Nov.Examples.Framework.NSimpleNode.ColorProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Color", Nevron.Nov.Dom.NDomType.NColor, Nevron.Nov.Examples.Framework.NSimpleNode.defaultColor)
            Nevron.Nov.Examples.Framework.NSimpleNode.AdvancedColorProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("AdvancedColor", Nevron.Nov.Dom.NDomType.NColor, Nevron.Nov.Examples.Framework.NSimpleNode.defaultAdvancedColor)
            Nevron.Nov.Examples.Framework.NSimpleNode.LengthProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Length", Nevron.Nov.Dom.NDomType.NLength, Nevron.Nov.Examples.Framework.NSimpleNode.defaultLength)
            Nevron.Nov.Examples.Framework.NSimpleNode.MarginsProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Margins", Nevron.Nov.Dom.NDomType.NMargins, Nevron.Nov.Examples.Framework.NSimpleNode.defaultMargins)
            Nevron.Nov.Examples.Framework.NSimpleNode.PointProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Point", Nevron.Nov.Dom.NDomType.NPoint, Nevron.Nov.Examples.Framework.NSimpleNode.defaultPoint)
            Nevron.Nov.Examples.Framework.NSimpleNode.RectangleProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Rectangle", Nevron.Nov.Dom.NDomType.NRectangle, Nevron.Nov.Examples.Framework.NSimpleNode.defaultRectangle)
            Nevron.Nov.Examples.Framework.NSimpleNode.SizeProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("Size", Nevron.Nov.Dom.NDomType.NSize, Nevron.Nov.Examples.Framework.NSimpleNode.defaultSize)
            Nevron.Nov.Examples.Framework.NSimpleNode.MultiLengthProperty = Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.AddSlot("MultiLength", GetType(Nevron.Nov.NMultiLength), Nevron.Nov.Examples.Framework.NSimpleNode.defaultMultiLength)

			' Designer
			Call Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner)))
        End Sub

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets or sets the value of the Boolean property.
		''' </summary>
		Public Property BooleanValue As Boolean
            Get
                Return CBool(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.BooleanValueProperty))
            End Get
            Set(ByVal value As Boolean)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.BooleanValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Integer property.
		''' </summary>
		Public Property IntegerValue As Integer
            Get
                Return CInt(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.IntegerValueProperty))
            End Get
            Set(ByVal value As Integer)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.IntegerValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Long property.
		''' </summary>
		Public Property LongValue As Long
            Get
                Return CLng(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.LongValueProperty))
            End Get
            Set(ByVal value As Long)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.LongValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Single property.
		''' </summary>
		Public Property SingleValue As Single
            Get
                Return CSng(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SingleValueProperty))
            End Get
            Set(ByVal value As Single)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SingleValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the UnsignedInteger property.
		''' </summary>
		Public Property UnsignedIntegerValue As UInteger
            Get
                Return CUInt(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.UnsignedIntegerValueProperty))
            End Get
            Set(ByVal value As UInteger)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.UnsignedIntegerValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the double property.
		''' </summary>
		Public Property DoubleValue As Double
            Get
                Return CDbl(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.DoubleValueProperty))
            End Get
            Set(ByVal value As Double)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.DoubleValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the SpecifiedDouble property.
		''' </summary>
		Public Property SpecifiedDoubleValue As Double
            Get
                Return CDbl(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SpecifiedDoubleValueProperty))
            End Get
            Set(ByVal value As Double)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SpecifiedDoubleValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the ComboBoxEnum property.
		''' </summary>
		Public Property ComboBoxEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.ComboBoxEnumValueProperty), Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
            End Get
            Set(ByVal value As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.ComboBoxEnumValueProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the HRadioGroupEnum property.
		''' </summary>
		Public Property HRadioGroupEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.HRadioGroupEnumProperty), Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
            End Get
            Set(ByVal value As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.HRadioGroupEnumProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the VRadioGroupEnum property.
		''' </summary>
		Public Property VRadioGroupEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.VRadioGroupEnumProperty), Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
            End Get
            Set(ByVal value As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.VRadioGroupEnumProperty, value)
            End Set
        End Property

		''' <summary>
		''' Gets or sets the value of the Angle property.
		''' </summary>
		Public Property Angle As Nevron.Nov.NAngle
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.AngleProperty), Nevron.Nov.NAngle)
            End Get
            Set(ByVal value As Nevron.Nov.NAngle)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.AngleProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the color property.
		''' </summary>
		Public Property Color As Nevron.Nov.Graphics.NColor
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.ColorProperty), Nevron.Nov.Graphics.NColor)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NColor)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.ColorProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the AdvancedColor property.
		''' </summary>
		Public Property AdvancedColor As Nevron.Nov.Graphics.NColor
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.AdvancedColorProperty), Nevron.Nov.Graphics.NColor)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NColor)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.AdvancedColorProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Length property.
		''' </summary>
		Public Property Length As Nevron.Nov.NLength
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.LengthProperty), Nevron.Nov.NLength)
            End Get
            Set(ByVal value As Nevron.Nov.NLength)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.LengthProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Point property.
		''' </summary>
		Public Property Point As Nevron.Nov.Graphics.NPoint
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.PointProperty), Nevron.Nov.Graphics.NPoint)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NPoint)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.PointProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the size property.
		''' </summary>
		Public Property Size As Nevron.Nov.Graphics.NSize
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SizeProperty), Nevron.Nov.Graphics.NSize)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NSize)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.SizeProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the rectangle property.
		''' </summary>
		Public Property Rectangle As Nevron.Nov.Graphics.NRectangle
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.RectangleProperty), Nevron.Nov.Graphics.NRectangle)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NRectangle)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.RectangleProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the Margins property.
		''' </summary>
		Public Property Margins As Nevron.Nov.Graphics.NMargins
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.MarginsProperty), Nevron.Nov.Graphics.NMargins)
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NMargins)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.MarginsProperty, value)
            End Set
        End Property
		''' <summary>
		''' Gets or sets the value of the MultiLength property.
		''' </summary>
		Public Property MultiLength As Nevron.Nov.NMultiLength
            Get
                Return CType(MyBase.GetValue(Nevron.Nov.Examples.Framework.NSimpleNode.MultiLengthProperty), Nevron.Nov.NMultiLength)
            End Get
            Set(ByVal value As Nevron.Nov.NMultiLength)
                MyBase.SetValue(Nevron.Nov.Examples.Framework.NSimpleNode.MultiLengthProperty, value)
            End Set
        End Property

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NSimpleNode.
		''' </summary>
		Public Shared ReadOnly NSimpleNodeSchema As Nevron.Nov.Dom.NSchema
		''' <summary>
		''' Reference to the Angle property.
		''' </summary>
		Public Shared ReadOnly AngleProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Boolean property.
		''' </summary>
		Public Shared ReadOnly BooleanValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Integer property.
		''' </summary>
		Public Shared ReadOnly IntegerValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Long property.
		''' </summary>
		Public Shared ReadOnly LongValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the UnsignedInteger property.
		''' </summary>
		Public Shared ReadOnly UnsignedIntegerValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Single property.
		''' </summary>
		Public Shared ReadOnly SingleValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Double property.
		''' </summary>
		Public Shared ReadOnly DoubleValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the SpecifiedDouble property.
		''' </summary>
		Public Shared ReadOnly SpecifiedDoubleValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Color property.
		''' </summary>
		Public Shared ReadOnly ColorProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the AdvancedColor property.
		''' </summary>
		Public Shared ReadOnly AdvancedColorProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Point property.
		''' </summary>
		Public Shared ReadOnly PointProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the ComboBoxEnum property.
		''' </summary>
		Public Shared ReadOnly ComboBoxEnumValueProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the HRadioGroupEnum property.
		''' </summary>
		Public Shared ReadOnly HRadioGroupEnumProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the VRadioGroupEnum property.
		''' </summary>
		Public Shared ReadOnly VRadioGroupEnumProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Length property.
		''' </summary>
		Public Shared ReadOnly LengthProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Size property.
		''' </summary>
		Public Shared ReadOnly SizeProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Margins property.
		''' </summary>
		Public Shared ReadOnly MarginsProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the Rectangle property.
		''' </summary>
		Public Shared ReadOnly RectangleProperty As Nevron.Nov.Dom.NProperty
		''' <summary>
		''' Reference to the MultiLength property.
		''' </summary>
		Public Shared ReadOnly MultiLengthProperty As Nevron.Nov.Dom.NProperty

		#EndRegion

		#Region"Default Values"

		Private Const defaultBoolean As Boolean = True
        Private Const defaultInteger As Integer = 0
        Private Const defaultLong As Long = 0
        Private Const defaultUnsignedInteger As UInteger = 0
        Private Const defaultSingle As Single = 0
        Private Const defaultDouble As Double = 0
        Private Const defaultSpecifiedDouble As Double = System.[Double].NaN
        Private Const defaultComboBoxEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum = Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum.Option1
        Private Const defaultHRadioGroupEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum = Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum.Option1
        Private Const defaultVRadioGroupEnum As Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum = Nevron.Nov.Examples.Framework.NSimpleNode.ENSampleEnum.Option1
        Private Shared ReadOnly defaultAngle As Nevron.Nov.NAngle = Nevron.Nov.NAngle.Zero
        Private Shared ReadOnly defaultColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.White
        Private Shared ReadOnly defaultAdvancedColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
        Private Shared ReadOnly defaultLength As Nevron.Nov.NLength = Nevron.Nov.NLength.Zero
        Private Shared ReadOnly defaultMargins As Nevron.Nov.Graphics.NMargins = Nevron.Nov.Graphics.NMargins.Zero
        Private Shared ReadOnly defaultPoint As Nevron.Nov.Graphics.NPoint = Nevron.Nov.Graphics.NPoint.Zero
        Private Shared ReadOnly defaultRectangle As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NRectangle.Zero
        Private Shared ReadOnly defaultSize As Nevron.Nov.Graphics.NSize = Nevron.Nov.Graphics.NSize.Zero
        Private Shared ReadOnly defaultMultiLength As Nevron.Nov.NMultiLength = Nevron.Nov.NMultiLength.NewFixed(0)

		#EndRegion

		#Region"Nested Types"

		Public Enum ENSampleEnum
            Option1
            Option2
            Option3
            Option4
        End Enum

		#EndRegion

		#Region"Designer"

		''' <summary>
		''' Designer for NSimpleNode.
		''' </summary>
		Public Class NSimpleNodeDesigner
            Inherits Nevron.Nov.Editors.NDesigner
			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
				' Categories
				MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NSimpleNode.AdvancedColorProperty, Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner.ColorsCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NSimpleNode.ColorProperty, Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner.ColorsCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NSimpleNode.ComboBoxEnumValueProperty, Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner.EnumsCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NSimpleNode.HRadioGroupEnumProperty, Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner.EnumsCategory)
                MyBase.SetPropertyCategory(Nevron.Nov.Examples.Framework.NSimpleNode.VRadioGroupEnumProperty, Nevron.Nov.Examples.Framework.NSimpleNode.NSimpleNodeDesigner.EnumsCategory)

				' Category Editors
				MyBase.SetCategoryEditor(Nevron.Nov.NLocalizedString.Empty, Nevron.Nov.Editors.NTabCategoryEditor.HeadersTopTemplate)

				' Property Editors
				MyBase.SetPropertyEditor(Nevron.Nov.Examples.Framework.NSimpleNode.SpecifiedDoubleValueProperty, Nevron.Nov.Editors.NSpecifiedDoublePropertyEditor.ZeroTemplate)
                MyBase.SetPropertyEditor(Nevron.Nov.Examples.Framework.NSimpleNode.AdvancedColorProperty, Nevron.Nov.Editors.NColorPropertyEditor.AdvancedTemplate)
                MyBase.SetPropertyEditor(Nevron.Nov.Examples.Framework.NSimpleNode.HRadioGroupEnumProperty, Nevron.Nov.Editors.NEnumPropertyEditor.HorizontalRadioGroupTemplate)
                MyBase.SetPropertyEditor(Nevron.Nov.Examples.Framework.NSimpleNode.VRadioGroupEnumProperty, Nevron.Nov.Editors.NEnumPropertyEditor.VerticalRadioGroupTemplate)
            End Sub

            Private Shared ReadOnly ColorsCategory As Nevron.Nov.NLocalizedString = New Nevron.Nov.NLocalizedString("Colors")
            Private Shared ReadOnly EnumsCategory As Nevron.Nov.NLocalizedString = New Nevron.Nov.NLocalizedString("Enums")
        End Class

		#EndRegion
	End Class
End Namespace
