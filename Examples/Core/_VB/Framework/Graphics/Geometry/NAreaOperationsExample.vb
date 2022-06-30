Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NAreaOperationsExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NAreaOperationsExample.NAreaOperationsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NAreaOperationsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_InputPaths = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NGraphicsPath)()
            Me.m_OutputPath = New Nevron.Nov.Graphics.NGraphicsPath()
            Me.m_Canvas = New Nevron.Nov.UI.NCanvas()
            Me.m_Canvas.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            Me.m_Canvas.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit
            AddHandler Me.m_Canvas.PrePaint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NCanvasPaintEventArgs)(AddressOf Me.OnCanvasPrePaint)
            Me.m_Canvas.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            Dim scrollContent As Nevron.Nov.UI.NScrollContent = New Nevron.Nov.UI.NScrollContent()
            scrollContent.Content = Me.m_Canvas

			' Add 3 circles
			Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddCircle(100, 100, 100)
            Me.m_InputPaths.Add(path)
            path = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddCircle(250, 100, 100)
            Me.m_InputPaths.Add(path)
            Return scrollContent
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.None
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.None
            Me.m_OperatorCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_OperatorCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Union"))
            Me.m_OperatorCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Intersect"))
            Me.m_OperatorCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Subtract"))
            Me.m_OperatorCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Exclusive OR"))
            Me.m_OperatorCombo.SelectedIndex = 0
            AddHandler Me.m_OperatorCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAreaOperatorComboSelectedIndexChanged)
            stack.Add(Me.m_OperatorCombo)

			' random path creation
			Dim randomRectButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Rectangle")
            AddHandler randomRectButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRandomRectButtonClick)
            stack.Add(randomRectButton)
            Dim randomEllipseButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Ellipse")
            AddHandler randomEllipseButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRandomEllipseButtonClick)
            stack.Add(randomEllipseButton)
            Dim randomTriangleButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random Triangle")
            AddHandler randomTriangleButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRandomTriangleButtonClick)
            stack.Add(randomTriangleButton)
            Dim clearButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Clear")
            AddHandler clearButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnClearButtonClick)
            stack.Add(clearButton)
            Me.m_ShowInputPathInteriors = New Nevron.Nov.UI.NCheckBox("Show Input Path Interiors")
            Me.m_ShowInputPathInteriors.Checked = True
            AddHandler Me.m_ShowInputPathInteriors.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowInputPathInteriorsCheckedChanged)
            stack.Add(Me.m_ShowInputPathInteriors)
            Me.m_ShowInputPathOutlines = New Nevron.Nov.UI.NCheckBox("Show Input Path Outlines")
            Me.m_ShowInputPathOutlines.Checked = True
            AddHandler Me.m_ShowInputPathOutlines.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowInputPathOutlinesCheckedChanged)
            stack.Add(Me.m_ShowInputPathOutlines)
            Me.m_ShowOutputPathOutline = New Nevron.Nov.UI.NCheckBox("Show Output Path Outline")
            Me.m_ShowOutputPathOutline.Checked = True
            AddHandler Me.m_ShowOutputPathOutline.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowInputPathInteriorsCheckedChanged)
            stack.Add(Me.m_ShowOutputPathOutline)
            Me.m_ShowOutputPathInterior = New Nevron.Nov.UI.NCheckBox("Show Output Path Interior")
            Me.m_ShowOutputPathInterior.Checked = True
            AddHandler Me.m_ShowOutputPathInterior.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnShowInputPathInteriorsCheckedChanged)
            stack.Add(Me.m_ShowOutputPathInterior)
            Me.UpdateOuputPath()
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
Demonstrates the Area Set Operations implemented by the NRegion class. Area Set Operations are performed on the closed areas represented by regions. 
Via these operations you can construct very complex solid geometries by combining primitive ones.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub UpdateOuputPath()
            Dim bounds As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle()
            Dim count As Integer = Me.m_InputPaths.Count

			' compute the ouput path and the bounds
			If count <> 0 Then
                Dim result As Nevron.Nov.Graphics.NRegion = Nevron.Nov.Graphics.NRegion.FromPath(Me.m_InputPaths(0), Nevron.Nov.Graphics.ENFillRule.EvenOdd)
                bounds = result.Bounds

                For i As Integer = 1 To count - 1
                    Dim operand As Nevron.Nov.Graphics.NRegion = Nevron.Nov.Graphics.NRegion.FromPath(Me.m_InputPaths(i), Nevron.Nov.Graphics.ENFillRule.EvenOdd)
                    bounds = Nevron.Nov.Graphics.NRectangle.Union(bounds, operand.Bounds)

                    Select Case Me.m_OperatorCombo.SelectedIndex
                        Case 0 ' union
                            result = result.Union(operand)
                        Case 1 ' intersection
                            result = result.Intersect(operand)
                        Case 2
                            result = result.Subtract(operand)
                        Case 3
                            result = result.ExclusiveOr(operand)
                    End Select
                Next

                Me.m_OutputPath = New Nevron.Nov.Graphics.NGraphicsPath(result.GetPath())
            Else
                Me.m_OutputPath = New Nevron.Nov.Graphics.NGraphicsPath()
            End If

			' normalize the coordinates
			For i As Integer = 0 To count - 1
                Dim path As Nevron.Nov.Graphics.NGraphicsPath = Me.m_InputPaths(i)
				'NRectangle pathBounds = path.GetBounds();
				path.Translate(-bounds.X, -bounds.Y)
            Next

            Me.m_OutputPath.Translate(-bounds.X, -bounds.Y)
            Me.m_Canvas.PreferredSize = New Nevron.Nov.Graphics.NSize(bounds.Width + 20, bounds.Height + 20)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnCanvasPrePaint(ByVal args As Nevron.Nov.UI.NCanvasPaintEventArgs)
            args.PaintVisitor.PushTransform(Nevron.Nov.Graphics.NMatrix.CreateTranslationMatrix(10, 10))

			' input path interiors
			If Me.m_ShowInputPathInteriors.Checked Then
                args.PaintVisitor.ClearStyles()
                args.PaintVisitor.SetFill(Nevron.Nov.Graphics.NColor.LightBlue)

                For i As Integer = 0 To Me.m_InputPaths.Count - 1
                    args.PaintVisitor.PaintPath(Me.m_InputPaths(i))
                Next
            End If

			' input path outlines
			If Me.m_ShowInputPathOutlines.Checked Then
                args.PaintVisitor.ClearStyles()
                args.PaintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 1)

                For i As Integer = 0 To Me.m_InputPaths.Count - 1
                    args.PaintVisitor.PaintPath(Me.m_InputPaths(i))
                Next
            End If

			' output path interior
			If Me.m_ShowOutputPathInterior.Checked Then
                args.PaintVisitor.ClearStyles()
                args.PaintVisitor.SetFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.LightCoral, 128))
                args.PaintVisitor.PaintPath(Me.m_OutputPath)
            End If

			' output path outline
			If Me.m_ShowOutputPathOutline.Checked Then
                args.PaintVisitor.ClearStyles()
                args.PaintVisitor.SetStroke(Nevron.Nov.Graphics.NColor.Black, 2)
                args.PaintVisitor.PaintPath(Me.m_OutputPath)
            End If

            args.PaintVisitor.PopTransform()
        End Sub

        Private Sub OnClearButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_InputPaths.Clear()
            Me.UpdateOuputPath()
        End Sub

        Private Sub OnRandomEllipseButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim rect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(Me.m_Random.[Next](500), Me.m_Random.[Next](500), Me.m_Random.[Next](500), Me.m_Random.[Next](500))
            rect.Normalize()
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddEllipse(rect)
            Me.m_InputPaths.Add(path)
            Me.UpdateOuputPath()
        End Sub

        Private Sub OnRandomRectButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim rect As Nevron.Nov.Graphics.NRectangle = New Nevron.Nov.Graphics.NRectangle(Me.m_Random.[Next](500), Me.m_Random.[Next](500), Me.m_Random.[Next](500), Me.m_Random.[Next](500))
            rect.Normalize()
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddRectangle(rect)
            Me.m_InputPaths.Add(path)
            Me.UpdateOuputPath()
        End Sub

        Private Sub OnRandomTriangleButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim p1 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(Me.m_Random.[Next](500), Me.m_Random.[Next](500))
            Dim p2 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(Me.m_Random.[Next](500), Me.m_Random.[Next](500))
            Dim p3 As Nevron.Nov.Graphics.NPoint = New Nevron.Nov.Graphics.NPoint(Me.m_Random.[Next](500), Me.m_Random.[Next](500))
            Dim path As Nevron.Nov.Graphics.NGraphicsPath = New Nevron.Nov.Graphics.NGraphicsPath()
            path.AddTriangle(New Nevron.Nov.Graphics.NTriangle(p1, p2, p3))
            Me.m_InputPaths.Add(path)
            Me.UpdateOuputPath()
        End Sub

        Private Sub OnShowInputPathInteriorsCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnShowInputPathOutlinesCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Canvas.InvalidateDisplay()
        End Sub

        Private Sub OnAreaOperatorComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.UpdateOuputPath()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Canvas As Nevron.Nov.UI.NCanvas
        Private m_OutputPath As Nevron.Nov.Graphics.NGraphicsPath
        Private m_InputPaths As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Graphics.NGraphicsPath)
        Private m_Random As System.Random = New System.Random(300)
        Private m_OperatorCombo As Nevron.Nov.UI.NComboBox
        Private m_ShowInputPathInteriors As Nevron.Nov.UI.NCheckBox
        Private m_ShowInputPathOutlines As Nevron.Nov.UI.NCheckBox
        Private m_ShowOutputPathOutline As Nevron.Nov.UI.NCheckBox
        Private m_ShowOutputPathInterior As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAreaOperationsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
