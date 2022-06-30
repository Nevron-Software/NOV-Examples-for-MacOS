Imports System
Imports System.Globalization
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NValueTypeEditorsExample
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
            Nevron.Nov.Examples.Framework.NValueTypeEditorsExample.NValueTypeEditorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NValueTypeEditorsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            Me.m_SimpleNode = New NSimpleNode()

			' Primitive types
			tab.TabPages.Add(Me.CreateBooleanPage())
            tab.TabPages.Add(Me.CreateInt32Page())
            tab.TabPages.Add(Me.CreateInt64Page())
            tab.TabPages.Add(Me.CreateUInt32Page())
            tab.TabPages.Add(Me.CreateSinglePage())
            tab.TabPages.Add(Me.CreateDoublePage())
            tab.TabPages.Add(Me.CreateEnumPage())

			' Nevron types
			tab.TabPages.Add(Me.CreateAnglePage())
            tab.TabPages.Add(Me.CreateColorPage())
            tab.TabPages.Add(Me.CreateGraphicsCorePage())
            tab.TabPages.Add(Me.CreateTextPage())
            Return tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the value type property editors. Select the tab page for the value types
    you are interested in and your will see their property editors.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation - Primitive Types"

		Private Function CreateBooleanPage() As Nevron.Nov.UI.NTabPage
            Dim tabPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Boolean")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            tabPage.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Default")
            stack.Add(groupBox)
            Dim editor As Nevron.Nov.Editors.NBooleanPropertyEditor = CType(CreateEditor(NSimpleNode.BooleanValueProperty), Nevron.Nov.Editors.NBooleanPropertyEditor)
            groupBox.Content = editor
            Return tabPage
        End Function

        Private Function CreateInt32Page() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Integer")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(CreateSample("Default", NSimpleNode.IntegerValueProperty, System.[Double].NaN, System.[Double].NaN, System.[Double].NaN, -1))
            stack.Add(CreateSample("Example 1", NSimpleNode.IntegerValueProperty, 2, -10, 10, -1))
            stack.Add(CreateSample("Example 2", NSimpleNode.IntegerValueProperty, 10, 200, 300, -1))
            Return page
        End Function

        Private Function CreateInt64Page() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Long")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(CreateSample("Default", NSimpleNode.LongValueProperty, System.[Double].NaN, System.[Double].NaN, System.[Double].NaN, -1))
            stack.Add(CreateSample("Example 1", NSimpleNode.LongValueProperty, 2, -10, 10, -1))
            stack.Add(CreateSample("Example 2", NSimpleNode.LongValueProperty, 10, 200, 300, -1))
            Return page
        End Function

        Private Function CreateUInt32Page() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Unsigned Integer")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(CreateSample("Default", NSimpleNode.UnsignedIntegerValueProperty, System.[Double].NaN, System.[Double].NaN, System.[Double].NaN, -1))
            stack.Add(CreateSample("Example 1", NSimpleNode.UnsignedIntegerValueProperty, 2, 0, 10, -1))
            stack.Add(CreateSample("Example 2", NSimpleNode.UnsignedIntegerValueProperty, 10, 200, 300, -1))
            Return page
        End Function

        Private Function CreateSinglePage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Single")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(CreateSample("Default", NSimpleNode.SingleValueProperty, System.[Double].NaN, System.[Double].NaN, System.[Double].NaN, -1))
            stack.Add(CreateSample("Example 1", NSimpleNode.SingleValueProperty, 2, -10, 10, 0))
            stack.Add(CreateSample("Example 2", NSimpleNode.SingleValueProperty, 0.2, -1, 1, 1))
            stack.Add(CreateSample("Example 3", NSimpleNode.SingleValueProperty, 0.03, 4, 5, 2))
            Return page
        End Function

        Private Function CreateDoublePage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Double")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Add(CreateSample("Default", NSimpleNode.DoubleValueProperty, System.[Double].NaN, System.[Double].NaN, System.[Double].NaN, -1))
            stack.Add(CreateSample("Example 1", NSimpleNode.DoubleValueProperty, 2, -10, 10, 0))
            stack.Add(CreateSample("Example 2", NSimpleNode.DoubleValueProperty, 0.2, -1, 1, 1))
            stack.Add(CreateSample("Example 3", NSimpleNode.DoubleValueProperty, 0.03, 4, 5, 2))
            stack.Add(CreateSample("Specified Double", NSimpleNode.SpecifiedDoubleValueProperty, 1, System.[Double].MinValue, System.[Double].MaxValue, 2))
            Return page
        End Function

        Private Function CreateEnumPage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Enum")
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            page.Content = stack
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Examples.Framework.NValueTypeEditorsExample.SimpleNodeDesigner.CreatePropertyEditors(Me.m_SimpleNode, NSimpleNode.ComboBoxEnumValueProperty, NSimpleNode.HRadioGroupEnumProperty, NSimpleNode.VRadioGroupEnumProperty)
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox(editors(CInt((0))).EditedProperty.ToString())
            stack.Add(groupBox)
            groupBox.Content = editors(0)
            Dim i As Integer = 1, count As Integer = editors.Count

            While i < count
                Dim editor As Nevron.Nov.Editors.NPropertyEditor = editors(i)
                stack.Add(editor)
                i += 1
            End While

            Return page
        End Function

        Private Function CreateSample(ByVal title As String, ByVal [property] As Nevron.Nov.Dom.NProperty, ByVal [step] As Double, ByVal min As Double, ByVal max As Double, ByVal decimalPlaces As Integer) As Nevron.Nov.UI.NGroupBox
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox(title)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            groupBox.Content = stack
            stack.VerticalSpacing = 10
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            propertyStack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim editor As Nevron.Nov.Editors.NNumberPropertyEditor = Me.CreateEditor([property], [step], min, max, decimalPlaces)
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Step = ", editor.[Step], True))
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Minimum = ", editor.Minimum, True))
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Maximum = ", editor.Maximum, True))

            If TypeOf editor Is Nevron.Nov.Editors.NFloatingNumberPropertyEditor Then
                propertyStack.Add(New Nevron.Nov.UI.NPairBox("Decimal Places = ", CType(editor, Nevron.Nov.Editors.NFloatingNumberPropertyEditor).DecimalPlaces, True))
            End If

            Dim i As Integer = 0, count As Integer = propertyStack.Count

            While i < count
                Dim pairBox As Nevron.Nov.UI.NPairBox = CType(propertyStack(i), Nevron.Nov.UI.NPairBox)
                Dim box1 As Nevron.Nov.UI.NUniSizeBox = CType(pairBox.Box1, Nevron.Nov.UI.NUniSizeBox)
                box1.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Right
                i += 1
            End While

            stack.Add(editor)
            Return groupBox
        End Function

        Private Function CreateEditor(ByVal [property] As Nevron.Nov.Dom.NProperty, ByVal [step] As Double, ByVal min As Double, ByVal max As Double, ByVal decimalPlaces As Integer) As Nevron.Nov.Editors.NNumberPropertyEditor
            Dim editor As Nevron.Nov.Editors.NNumberPropertyEditor = CType(Me.CreateEditor([property]), Nevron.Nov.Editors.NNumberPropertyEditor)
            Dim node As NSimpleNode = CType(editor.EditedNode, NSimpleNode)

            If System.[Double].IsNaN([step]) = False Then
                editor.[Step] = [step]
            End If

            If System.[Double].IsNaN(min) = False Then
                editor.Minimum = min
            End If

            If System.[Double].IsNaN(max) = False Then
                editor.Maximum = max
            End If

            If decimalPlaces <> -1 AndAlso TypeOf editor Is Nevron.Nov.Editors.NFloatingNumberPropertyEditor Then
                CType(editor, Nevron.Nov.Editors.NFloatingNumberPropertyEditor).DecimalPlaces = decimalPlaces
            End If

			' Ensure the value is in the range [min, max]
			Dim value As Double = System.Convert.ToDouble(node.GetValue([property]))

            If value < min Then
                node.SetValue([property], System.Convert.ChangeType(editor.Minimum, [property].DomType.CLRType, System.Globalization.CultureInfo.InvariantCulture))
            End If

            If value > max Then
                node.SetValue([property], System.Convert.ChangeType(editor.Maximum, [property].DomType.CLRType, System.Globalization.CultureInfo.InvariantCulture))
            End If

            Return editor
        End Function

		#EndRegion

		#Region"Implementation - Nevron Types"

		Private Function CreateAnglePage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Angle")
            Dim stack As Nevron.Nov.UI.NStackPanel = Me.CreateStackPanel()
            page.Content = stack
            Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Default")
            stack.Add(groupBox)
            Dim editor As Nevron.Nov.Editors.NAnglePropertyEditor = CType(CreateEditor(NSimpleNode.AngleProperty), Nevron.Nov.Editors.NAnglePropertyEditor)
            groupBox.Content = editor
            Return page
        End Function

        Private Function CreateColorPage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Color")
            Dim stack As Nevron.Nov.UI.NStackPanel = Me.CreateStackPanel()
            page.Content = stack

			' Create a default (drop down) color editor
			Dim groupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Default (drop down)")
            stack.Add(groupBox)
            Dim editor As Nevron.Nov.Editors.NColorPropertyEditor = CType(CreateEditor(NSimpleNode.ColorProperty), Nevron.Nov.Editors.NColorPropertyEditor)
            groupBox.Content = editor

			' Create an advanced color editor
			editor = CType(CreateEditor(NSimpleNode.AdvancedColorProperty), Nevron.Nov.Editors.NColorPropertyEditor)
            stack.Add(editor)
            Return page
        End Function

        Private Function CreateGraphicsCorePage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Graphics Core")
            Dim stack As Nevron.Nov.UI.NStackPanel = Me.CreateStackPanel()
            page.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Examples.Framework.NValueTypeEditorsExample.SimpleNodeDesigner.CreatePropertyEditors(Me.m_SimpleNode, NSimpleNode.PointProperty, NSimpleNode.SizeProperty, NSimpleNode.RectangleProperty, NSimpleNode.MarginsProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Return page
        End Function

        Private Function CreateTextPage() As Nevron.Nov.UI.NTabPage
            Dim page As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Text")
            Dim stack As Nevron.Nov.UI.NStackPanel = Me.CreateStackPanel()
            page.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(Nevron.Nov.Examples.Framework.NValueTypeEditorsExample.SimpleNodeDesigner.CreatePropertyEditor(Me.m_SimpleNode, NSimpleNode.MultiLengthProperty))
            Return page
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateEditor(ByVal [property] As Nevron.Nov.Dom.NProperty) As Nevron.Nov.Editors.NPropertyEditor
            Return Nevron.Nov.Examples.Framework.NValueTypeEditorsExample.SimpleNodeDesigner.CreatePropertyEditor(Me.m_SimpleNode, [property])
        End Function

        Private Function CreateStackPanel() As Nevron.Nov.UI.NStackPanel
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Return stack
        End Function

		#EndRegion

		#Region"Fields"

		Private m_SimpleNode As NSimpleNode

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NValueTypeEditorsExample.
		''' </summary>
		Public Shared ReadOnly NValueTypeEditorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly SimpleNodeDesigner As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(NSimpleNode.NSimpleNodeSchema)

		#EndRegion
	End Class
End Namespace
