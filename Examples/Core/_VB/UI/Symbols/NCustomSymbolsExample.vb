Imports System
Imports System.Globalization
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCustomSymbolsExample
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
            Nevron.Nov.Examples.UI.NCustomSymbolsExample.NCustomSymbolsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCustomSymbolsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_SymbolsTable = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_SymbolsTable.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Me.m_SymbolsTable.HorizontalSpacing = Nevron.Nov.NDesign.HorizontalSpacing * 3
            Me.m_SymbolsTable.VerticalSpacing = Nevron.Nov.NDesign.VerticalSpacing * 3
            Me.m_SymbolsTable.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            Me.m_SymbolsTable.MaxOrdinal = 2
            Me.m_SymbolsTable.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing, Nevron.Nov.NDesign.HorizontalSpacing * 6, Nevron.Nov.NDesign.VerticalSpacing)
            Me.RecreateSymbols()
            Dim scrollContent As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent(Me.m_SymbolsTable)
            scrollContent.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Return scrollContent
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' Create the color box
			Me.m_ColorBox = New Nevron.Nov.UI.NColorBox()
            Me.m_ColorBox.SelectedColor = Nevron.Nov.Examples.UI.NCustomSymbolsExample.DefaultSymbolColor
            AddHandler Me.m_ColorBox.SelectedColorChanged, AddressOf Me.OnColorBoxSelectedColorChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Color:", Me.m_ColorBox))

			' Create the size radio button group
			Dim radioStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim size As Double = Nevron.Nov.Examples.UI.NCustomSymbolsExample.InitialSize

            For i As Integer = 0 To 4 - 1
                Dim sizeStr As String = size.ToString(System.Globalization.CultureInfo.InvariantCulture)
                Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton(sizeStr & "x" & sizeStr)
                radioStack.Add(radioButton)
                size *= 2
            Next

            Me.m_RadioGroup = New Nevron.Nov.UI.NRadioButtonGroup(radioStack)
            Me.m_RadioGroup.SelectedIndex = 1
            AddHandler Me.m_RadioGroup.SelectedIndexChanged, AddressOf Me.OnRadioGroupSelectedIndexChanged
            Dim pairBox As Nevron.Nov.UI.NPairBox = Nevron.Nov.UI.NPairBox.Create("Size:", Me.m_RadioGroup)
            pairBox.Box1.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.Add(pairBox)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	Nevron Open Vision provides support for drawing of vector based shapes called symbols. The advantage of such vector
	based shapes over regular raster images is that they do not blur and look nice at any size. This example demonstrates
	how to create and use custom symbols. Use the radio buttons on the right to see the symbols at different sizes.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub RecreateSymbols()
            Dim color As Nevron.Nov.Graphics.NColor = If(Me.m_ColorBox IsNot Nothing, Me.m_ColorBox.SelectedColor, Nevron.Nov.Examples.UI.NCustomSymbolsExample.DefaultSymbolColor)
            Dim length As Double = Nevron.Nov.Examples.UI.NCustomSymbolsExample.InitialSize * System.Math.Pow(2, If(Me.m_RadioGroup IsNot Nothing, Me.m_RadioGroup.SelectedIndex, 0))
            Dim size As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(length, length)
            Me.m_SymbolsTable.Clear()

			' Create a triangle up symbol
			Dim shape As Nevron.Nov.UI.NPolygonSymbolShape = New Nevron.Nov.UI.NPolygonSymbolShape(New Nevron.Nov.Graphics.NPoint() {New Nevron.Nov.Graphics.NPoint(0, size.Height), New Nevron.Nov.Graphics.NPoint(size.Width * 0.5, 0), New Nevron.Nov.Graphics.NPoint(size.Width, size.Height)}, Nevron.Nov.Graphics.ENFillRule.EvenOdd)
            shape.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Dim symbol1 As Nevron.Nov.UI.NSymbol = New Nevron.Nov.UI.NSymbol()
            symbol1.Add(shape)
            Me.AddSymbolBox(symbol1, "Triangle Up")

			' Create a rectangle with an ellipse
			Dim rectShape As Nevron.Nov.UI.NRectangleSymbolShape = New Nevron.Nov.UI.NRectangleSymbolShape(0, 0, size.Width, size.Height)
            rectShape.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Dim ellipseShape As Nevron.Nov.UI.NEllipseSymbolShape = New Nevron.Nov.UI.NEllipseSymbolShape(size.Width / 4, size.Height / 4, size.Width / 2, size.Height / 2)
            ellipseShape.Fill = New Nevron.Nov.Graphics.NColorFill(color.Invert())
            Dim symbol2 As Nevron.Nov.UI.NSymbol = New Nevron.Nov.UI.NSymbol()
            symbol2.Add(rectShape)
            symbol2.Add(ellipseShape)
            Me.AddSymbolBox(symbol2, "Rectangle with an ellipse")
        End Sub

        Private Sub AddSymbolBox(ByVal symbol As Nevron.Nov.UI.NSymbol, ByVal name As String)
            Me.m_SymbolsTable.Add(New Nevron.Nov.UI.NSymbolBox(symbol))
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(name)
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Me.m_SymbolsTable.Add(label)
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnColorBoxSelectedColorChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.RecreateSymbols()
        End Sub

        Private Sub OnRadioGroupSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.RecreateSymbols()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_SymbolsTable As Nevron.Nov.UI.NTableFlowPanel
        Private m_ColorBox As Nevron.Nov.UI.NColorBox
        Private m_RadioGroup As Nevron.Nov.UI.NRadioButtonGroup

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCustomSymbolsExample.
		''' </summary>
		Public Shared ReadOnly NCustomSymbolsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const InitialSize As Double = 16
        Private Shared ReadOnly DefaultSymbolColor As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.MediumBlue

		#EndRegion
	End Class
End Namespace
