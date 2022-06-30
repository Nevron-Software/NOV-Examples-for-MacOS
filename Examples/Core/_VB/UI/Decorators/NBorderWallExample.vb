Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NBorderWallExample
        Inherits NExampleBase
        #Region"Constructors"

        Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NBorderWallExample.NBorderWallExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NBorderWallExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a tab control for the different border walls
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            Dim boxBorderPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Box Border")
            tab.TabPages.Add(boxBorderPage)
            Dim crossBorderPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Cross Border")
            tab.TabPages.Add(crossBorderPage)
            Dim openBorderPage As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Opened Border")
            tab.TabPages.Add(openBorderPage)

            ' create the three elements that demonstrate the border walls
            Me.m_BoxBorderElement = New Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget()
            boxBorderPage.Content = Me.m_BoxBorderElement
            Me.m_BoxBorderElement.BorderWallType = Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Rectangle
            Me.m_CrossBorderElement = New Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget()
            crossBorderPage.Content = Me.m_CrossBorderElement
            Me.m_CrossBorderElement.BorderWallType = Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Cross
            Me.m_OpenedBorderElement = New Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget()
            openBorderPage.Content = Me.m_OpenedBorderElement
            Me.m_OpenedBorderElement.BorderWallType = Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Opened

            ' init the custom border elements
            Dim elements As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget() = Me.GetCustomBorderElements()
            Dim colors As Nevron.Nov.UI.NUIThemeColorMap = New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic)

            For i As Integer = 0 To elements.Length - 1
                elements(CInt((i))).BorderThickness = New Nevron.Nov.Graphics.NMargins(2)
                elements(CInt((i))).Border = Nevron.Nov.UI.NBorder.CreateRaised3DBorder(colors)
                elements(CInt((i))).Margins = New Nevron.Nov.Graphics.NMargins(10)
            Next

            Return tab
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            
            ' create the predefined borders combo and populate it
            stack.Add(Me.CreateLabel("Apply Predefined Border:"))
            Me.m_PredefinedBorderCombo = New Nevron.Nov.UI.NComboBox()
            stack.Add(Me.m_PredefinedBorderCombo)
            Me.m_PredefinedBorderCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("3D Raised Border"))
            Me.m_PredefinedBorderCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("3D Sunken Border"))
            Me.m_PredefinedBorderCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Filled Border"))
            Me.m_PredefinedBorderCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Filled Border with Outlines"))
            Me.m_PredefinedBorderCombo.SelectedIndex = 0
            AddHandler Me.m_PredefinedBorderCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnPredefinedBorderComboSelectedIndexChanged)

            ' create the combos for the border thickness
            stack.Add(Me.CreateLabel("Border Thickness:"))
            Me.m_BorderThicknessCombo = Me.CreateBorderSideThicknessCombo()
            stack.Add(Me.m_BorderThicknessCombo)
            stack.Add(Me.CreateLabel("Left Side Thickness:"))
            Me.m_LeftSideThicknessCombo = Me.CreateBorderSideThicknessCombo()
            stack.Add(Me.m_LeftSideThicknessCombo)
            stack.Add(Me.CreateLabel("Right Side Thickness:"))
            Me.m_RightSideThicknessCombo = Me.CreateBorderSideThicknessCombo()
            stack.Add(Me.m_RightSideThicknessCombo)
            stack.Add(Me.CreateLabel("Top Side Thickness:"))
            Me.m_TopSideThicknessCombo = Me.CreateBorderSideThicknessCombo()
            stack.Add(Me.m_TopSideThicknessCombo)
            stack.Add(Me.CreateLabel("Bottom Side Thickness:"))
            Me.m_BottomSideThicknessCombo = Me.CreateBorderSideThicknessCombo()
            stack.Add(Me.m_BottomSideThicknessCombo)
            stack.Add(Me.CreateLabel("Inner Corner Radius:"))
            Me.m_InnerRadiusCombo = Me.CreateCornerRadiusCombo()
            stack.Add(Me.m_InnerRadiusCombo)
            stack.Add(Me.CreateLabel("Outer Corner Radius:"))
            Me.m_OuterRadiusCombo = Me.CreateCornerRadiusCombo()
            stack.Add(Me.m_OuterRadiusCombo)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
This example demonstrates how to create different types of borders and apply them to widgets. 
It also demonstrates how to override the border wall of a widget and provide a custom one.
Using the controls to the right	you can change the type and appearance of the generated borders.
</p>
"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnPredefinedBorderComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_PredefinedBorderCombo.SelectedIndex = -1 Then Return
            Dim innerRadius As Double = Me.m_InnerRadiusCombo.SelectedIndex
            Dim outerRadius As Double = Me.m_OuterRadiusCombo.SelectedIndex

            ' apply a predefined border
            Dim elements As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget() = Me.GetCustomBorderElements()

            For i As Integer = 0 To elements.Length - 1
                Dim border As Nevron.Nov.UI.NBorder = Nothing

                Select Case Me.m_PredefinedBorderCombo.SelectedIndex
                    Case 0 ' 3D Raised Border
                        border = Nevron.Nov.UI.NBorder.CreateRaised3DBorder(New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic))
                    Case 1 ' 3D Sunken Border
                        border = Nevron.Nov.UI.NBorder.CreateSunken3DBorder(New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic))
                    Case 2 ' Filled Border
                        border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
                    Case 3 ' Filled Border with Outlines
                        border = Nevron.Nov.UI.NBorder.CreateFilledBorder(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue), New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black), New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Black))
                End Select

                border.SetRadiuses(innerRadius, outerRadius)
                elements(CInt((i))).Border = border
            Next
        End Sub

        Private Sub OnSideThicknessComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim combo As Nevron.Nov.UI.NComboBox = TryCast(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Dim sideThickness As Double = combo.SelectedIndex
            Dim bt As Nevron.Nov.Graphics.NMargins = Me.m_BoxBorderElement.BorderThickness

            If combo Is Me.m_BorderThicknessCombo Then
                bt = New Nevron.Nov.Graphics.NMargins(sideThickness)
            ElseIf combo Is Me.m_LeftSideThicknessCombo Then
                bt.Left = sideThickness
            ElseIf combo Is Me.m_RightSideThicknessCombo Then
                bt.Right = sideThickness
            ElseIf combo Is Me.m_TopSideThicknessCombo Then
                bt.Top = sideThickness
            ElseIf combo Is Me.m_BottomSideThicknessCombo Then
                bt.Bottom = sideThickness
            End If

            Dim elements As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget() = Me.GetCustomBorderElements()

            For i As Integer = 0 To elements.Length - 1
                elements(CInt((i))).BorderThickness = bt
            Next
        End Sub

        Private Sub OnCornerRadiusComboSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.OnPredefinedBorderComboSelectedIndexChanged(Nothing)
        End Sub

        #EndRegion

        #Region"Implementation"

        Private Function CreateLabel(ByVal text As String) As Nevron.Nov.UI.NLabel
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Return label
        End Function

        Private Function CreateBorderSideThicknessCombo() As Nevron.Nov.UI.NComboBox
            Dim combo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 30 - 1
                combo.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString() & " dip"))
            Next

            combo.SelectedIndex = 2
            AddHandler combo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSideThicknessComboSelectedIndexChanged)
            Return combo
        End Function

        Private Function CreateCornerRadiusCombo() As Nevron.Nov.UI.NComboBox
            Dim combo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 0 To 30 - 1
                combo.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString() & " dip"))
            Next

            combo.SelectedIndex = 0
            AddHandler combo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnCornerRadiusComboSelectedIndexChanged)
            Return combo
        End Function

        Private Function GetCustomBorderElements() As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget()
            Return New Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget() {Me.m_BoxBorderElement, Me.m_CrossBorderElement, Me.m_OpenedBorderElement}
        End Function

        #EndRegion

        #Region"Fields"

        Private m_BoxBorderElement As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget
        Private m_CrossBorderElement As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget
        Private m_OpenedBorderElement As Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget
        Private m_PredefinedBorderCombo As Nevron.Nov.UI.NComboBox
        Private m_BorderThicknessCombo As Nevron.Nov.UI.NComboBox
        Private m_LeftSideThicknessCombo As Nevron.Nov.UI.NComboBox
        Private m_RightSideThicknessCombo As Nevron.Nov.UI.NComboBox
        Private m_TopSideThicknessCombo As Nevron.Nov.UI.NComboBox
        Private m_BottomSideThicknessCombo As Nevron.Nov.UI.NComboBox
        Private m_InnerRadiusCombo As Nevron.Nov.UI.NComboBox
        Private m_OuterRadiusCombo As Nevron.Nov.UI.NComboBox

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NBorderWallExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Nested Types"

        ''' <summary>
        ''' Enumerates the demonstrated border walls
        ''' </summary>
        Public Enum ENCustomBorderWallType
            Rectangle
            Cross
            Opened
        End Enum
        ''' <summary>
        ''' A widget that overrides the default border wall of widgets 
        ''' to demonstrate all possible corners and to demonstrate opened walls.
        ''' </summary>
        Public Class NCustomBorderWallWidget
            Inherits Nevron.Nov.UI.NWidget
            #Region"Constructors"

            Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget.NCustomBorderWallWidgetSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget), Nevron.Nov.UI.NWidget.NWidgetSchema)
                Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget.BorderWallTypeProperty = Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget.NCustomBorderWallWidgetSchema.AddSlot("BorderWallType", Nevron.Nov.Dom.NDomType.FromType(GetType(Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType)), Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Rectangle)
            End Sub

            #EndRegion

            #Region"Properties"

            Public Property BorderWallType As Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType
                Get
                    Return CType(MyBase.GetValue(Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget.BorderWallTypeProperty), Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType)
                End Get
                Set(ByVal value As Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType)
                    MyBase.SetValue(Nevron.Nov.Examples.UI.NBorderWallExample.NCustomBorderWallWidget.BorderWallTypeProperty, value)
                End Set
            End Property

            #EndRegion

            #Region"Protected Overrides - Border Wall"

            Protected Overrides Function CreateBorderWall() As Nevron.Nov.UI.NBorderWall
                Select Case Me.BorderWallType
                    Case Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Rectangle
                        Return MyBase.CreateBorderWall()
                    Case Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Cross
                        Return Me.CreateCrossBorderWall()
                    Case Nevron.Nov.Examples.UI.NBorderWallExample.ENCustomBorderWallType.Opened
                        Return Me.CreateOpenedBorderWall()
                    Case Else
                        Throw New System.Exception("New ENCustomBorderWallType?")
                End Select
            End Function

            #EndRegion

            #Region"Implementation - Border Wall"

            Private Function CreateCrossBorderWall() As Nevron.Nov.UI.NBorderWall
                Dim r0 As Nevron.Nov.Graphics.NRectangle = MyBase.GetContentEdge()
                Dim bt As Nevron.Nov.Graphics.NMargins = MyBase.BorderThickness
                bt.Left = System.Math.Min(bt.Left, r0.Width / 6)
                bt.Right = System.Math.Min(bt.Right, r0.Width / 6)
                bt.Top = System.Math.Min(bt.Top, r0.Height / 6)
                bt.Bottom = System.Math.Min(bt.Bottom, r0.Height / 6)
                Dim r1 As Nevron.Nov.Graphics.NRectangle = bt.GetInnerRect(r0)
                Dim r2 As Nevron.Nov.Graphics.NRectangle = Nevron.Nov.Graphics.NRectangle.FromLTRB(r1.X + r1.Width / 3 - (bt.Left / 2), r1.Y + r1.Height / 3 - (bt.Top / 2), r1.Right - r1.Width / 3 + (bt.Right / 2), r1.Bottom - r1.Height / 3 + (bt.Bottom / 2))
                Dim r3 As Nevron.Nov.Graphics.NRectangle = bt.GetInnerRect(r2)
                Dim x0 As Double = r0.X
                Dim x1 As Double = r1.X
                Dim x2 As Double = r2.X
                Dim x3 As Double = r3.X
                Dim x4 As Double = r3.Right
                Dim x5 As Double = r2.Right
                Dim x6 As Double = r1.Right
                Dim x7 As Double = r0.Right
                Dim y0 As Double = r0.Y
                Dim y1 As Double = r1.Y
                Dim y2 As Double = r2.Y
                Dim y3 As Double = r3.Y
                Dim y4 As Double = r3.Bottom
                Dim y5 As Double = r2.Bottom
                Dim y6 As Double = r1.Bottom
                Dim y7 As Double = r0.Bottom
                Dim wall As Nevron.Nov.UI.NBorderWall = New Nevron.Nov.UI.NBorderWall(True)
                wall.AddLeftTopCorner(New Nevron.Nov.Graphics.NRectangle(x2, y0, bt.Left, bt.Top))
                wall.AddTopSide(New Nevron.Nov.Graphics.NRectangle(x3, y0, x4 - x3, bt.Top))
                wall.AddTopRightCorner(New Nevron.Nov.Graphics.NRectangle(x4, y0, bt.Right, bt.Top))
                wall.AddRightSide(New Nevron.Nov.Graphics.NRectangle(x4, y1, bt.Right, y2 - y1))
                wall.AddRightTopCorner(New Nevron.Nov.Graphics.NRectangle(x4, y2, bt.Right, bt.Top))
                wall.AddTopSide(New Nevron.Nov.Graphics.NRectangle(x5, y2, x6 - x5, bt.Top))
                wall.AddTopRightCorner(New Nevron.Nov.Graphics.NRectangle(x6, y2, bt.Right, bt.Top))
                wall.AddRightSide(New Nevron.Nov.Graphics.NRectangle(x6, y3, bt.Right, y4 - y3))
                wall.AddRightBottomCorner(New Nevron.Nov.Graphics.NRectangle(x6, y4, bt.Right, bt.Bottom))
                wall.AddBottomSide(New Nevron.Nov.Graphics.NRectangle(x5, y4, x6 - x5, bt.Bottom))
                wall.AddBottomRightCorner(New Nevron.Nov.Graphics.NRectangle(x4, y4, bt.Right, bt.Bottom))
                wall.AddRightSide(New Nevron.Nov.Graphics.NRectangle(x4, y5, bt.Right, y6 - y5))
                wall.AddRightBottomCorner(New Nevron.Nov.Graphics.NRectangle(x4, y6, bt.Right, bt.Bottom))
                wall.AddBottomSide(New Nevron.Nov.Graphics.NRectangle(x3, y6, x4 - x3, bt.Bottom))
                wall.AddBottomLeftCorner(New Nevron.Nov.Graphics.NRectangle(x2, y6, bt.Left, bt.Bottom))
                wall.AddLeftSide(New Nevron.Nov.Graphics.NRectangle(x2, y5, bt.Left, y6 - y5))
                wall.AddLeftBottomCorner(New Nevron.Nov.Graphics.NRectangle(x2, y4, bt.Left, bt.Bottom))
                wall.AddBottomSide(New Nevron.Nov.Graphics.NRectangle(x1, y4, x2 - x1, bt.Bottom))
                wall.AddBottomLeftCorner(New Nevron.Nov.Graphics.NRectangle(x0, y4, bt.Left, bt.Bottom))
                wall.AddLeftSide(New Nevron.Nov.Graphics.NRectangle(x0, y3, bt.Left, y4 - y3))
                wall.AddLeftTopCorner(New Nevron.Nov.Graphics.NRectangle(x0, y2, bt.Left, bt.Top))
                wall.AddTopSide(New Nevron.Nov.Graphics.NRectangle(x1, y2, x2 - x1, bt.Top))
                wall.AddTopLeftCorner(New Nevron.Nov.Graphics.NRectangle(x2, y2, bt.Left, bt.Top))
                wall.AddLeftSide(New Nevron.Nov.Graphics.NRectangle(x2, y1, bt.Left, y2 - y1))
                Return wall
            End Function

            Private Function CreateOpenedBorderWall() As Nevron.Nov.UI.NBorderWall
                Dim outer As Nevron.Nov.Graphics.NRectangle = MyBase.GetBorderEdge()
                Dim inner As Nevron.Nov.Graphics.NRectangle = MyBase.GetContentEdge()
                Dim wall As Nevron.Nov.UI.NBorderWall = New Nevron.Nov.UI.NBorderWall(False)
                Dim leftSide As Double = inner.X - outer.X
                Dim topSide As Double = inner.Y - outer.Y
                Dim rightSide As Double = outer.Right - inner.Right
                Dim bottomSide As Double = outer.Bottom - inner.Bottom
                Dim topClipStart As Double = inner.X + inner.Width / 3
                Dim topClipEnd As Double = inner.Right - inner.Width / 3
                wall.AddTopSide(New Nevron.Nov.Graphics.NRectangle(topClipEnd, outer.Y, inner.Right - topClipEnd, topSide))
                wall.AddTopRightCorner(New Nevron.Nov.Graphics.NRectangle(inner.Right, outer.Y, rightSide, topSide))
                wall.AddRightSide(New Nevron.Nov.Graphics.NRectangle(inner.Right, inner.Y, rightSide, inner.Height))
                wall.AddRightBottomCorner(New Nevron.Nov.Graphics.NRectangle(inner.Right, inner.Bottom, rightSide, bottomSide))
                wall.AddBottomSide(New Nevron.Nov.Graphics.NRectangle(inner.X, inner.Bottom, inner.Width, bottomSide))
                wall.AddBottomLeftCorner(New Nevron.Nov.Graphics.NRectangle(outer.X, inner.Bottom, leftSide, bottomSide))
                wall.AddLeftSide(New Nevron.Nov.Graphics.NRectangle(outer.X, inner.Y, leftSide, inner.Height))
                wall.AddLeftTopCorner(New Nevron.Nov.Graphics.NRectangle(outer.X, outer.Y, leftSide, topSide))
                wall.AddTopSide(New Nevron.Nov.Graphics.NRectangle(inner.X, outer.Y, topClipStart - inner.X, topSide))
                Return wall
            End Function

            #EndRegion

            #Region"Schema"

            Public Shared ReadOnly NCustomBorderWallWidgetSchema As Nevron.Nov.Dom.NSchema
            Public Shared ReadOnly BorderWallTypeProperty As Nevron.Nov.Dom.NProperty

            #EndRegion
        End Class

        #EndRegion
    End Class
End Namespace
