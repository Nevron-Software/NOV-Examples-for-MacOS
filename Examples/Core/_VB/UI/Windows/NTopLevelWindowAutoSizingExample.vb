Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTopLevelWindowAutoSizingExample
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
            Nevron.Nov.Examples.UI.NTopLevelWindowAutoSizingExample.NTopLevelWindowAutoSizingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTopLevelWindowAutoSizingExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' Create the example's content
			Dim openYAutoSizeWindowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open Y auto sizable Window...")
            openYAutoSizeWindowButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            openYAutoSizeWindowButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler openYAutoSizeWindowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenYAutoSizeWindowButtonClick)
            stack.Add(openYAutoSizeWindowButton)
            Dim openXAutoSizeWindowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open X auto sizable Window...")
            openXAutoSizeWindowButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            openXAutoSizeWindowButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler openXAutoSizeWindowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenXAutoSizeWindowButtonClick)
            stack.Add(openXAutoSizeWindowButton)
            Dim openAutoSizeWindowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open X and Y auto sizable and auto centered Window...")
            openAutoSizeWindowButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            openAutoSizeWindowButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler openAutoSizeWindowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenAutoSizeWindowButtonClick)
            stack.Add(openAutoSizeWindowButton)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
This example demonstrates how to create auto sizable and auto centered windows with expressions.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOpenXAutoSizeWindowButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim window As Nevron.Nov.UI.NTopLevelWindow = New Nevron.Nov.UI.NTopLevelWindow()
            window.Modal = True

			' allow the user to resize the Y window dimension
			window.AllowYResize = True

			' bind the window Width to the desired width of the window
			Dim bindingFx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(window, Nevron.Nov.UI.NTopLevelWindow.DesiredWidthProperty)
            bindingFx.Guard = True
            window.SetFx(Nevron.Nov.UI.NTopLevelWindow.WidthProperty, bindingFx)

			' create a wrap flow panel with Y direction
			Dim wrapPanel As Nevron.Nov.UI.NWrapFlowPanel = New Nevron.Nov.UI.NWrapFlowPanel()
            wrapPanel.Direction = Nevron.Nov.Layout.ENHVDirection.TopToBottom
            window.Content = wrapPanel

            For i As Integer = 0 To 10 - 1
                wrapPanel.Add(New Nevron.Nov.UI.NButton("Button" & i))
            Next

			' open the window
			DisplayWindow.Windows.Add(window)
            window.Open()
        End Sub

        Private Sub OnOpenYAutoSizeWindowButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim window As Nevron.Nov.UI.NTopLevelWindow = New Nevron.Nov.UI.NTopLevelWindow()
            window.Modal = True

			' allow the user to resize the X window dimension
			window.AllowXResize = True

			' bind the window Height to the desired height of the window
			Dim bindingFx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(window, Nevron.Nov.UI.NTopLevelWindow.DesiredHeightProperty)
            bindingFx.Guard = True
            window.SetFx(Nevron.Nov.UI.NTopLevelWindow.HeightProperty, bindingFx)

			' create a wrap flow panel (by default flows from left to right)
			Dim wrapPanel As Nevron.Nov.UI.NWrapFlowPanel = New Nevron.Nov.UI.NWrapFlowPanel()
            window.Content = wrapPanel

            For i As Integer = 0 To 10 - 1
                wrapPanel.Add(New Nevron.Nov.UI.NButton("Button" & i))
            Next

			' open the window
			DisplayWindow.Windows.Add(window)
            window.Open()
        End Sub

        Private Sub OnOpenAutoSizeWindowButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim window As Nevron.Nov.UI.NTopLevelWindow = New Nevron.Nov.UI.NTopLevelWindow()
            window.Modal = True

			' open the window in the center of its parent, 
			window.StartPosition = Nevron.Nov.UI.ENWindowStartPosition.CenterOwnerWindow

			' implement auto width and height sizing
			If True Then
				' bind the window Width to the DefaultWidth of the window
				Dim widthBindingFx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(window, Nevron.Nov.UI.NTopLevelWindow.DefaultWidthProperty)
                widthBindingFx.Guard = True
                window.SetFx(Nevron.Nov.UI.NTopLevelWindow.WidthProperty, widthBindingFx)

				' bind the window Height to the DefaultHeight of the window
				Dim heightBindingFx As Nevron.Nov.Dom.NBindingFx = New Nevron.Nov.Dom.NBindingFx(window, Nevron.Nov.UI.NTopLevelWindow.DesiredHeightProperty)
                heightBindingFx.Guard = True
                window.SetFx(Nevron.Nov.UI.NTopLevelWindow.HeightProperty, heightBindingFx)
            End If

			' implement auto center 
			If True Then
				' scratch X and Y define the window center 
				' they are implemented by simply calculating the center X and Y via formulas
				window.SetFx(Nevron.Nov.Dom.NScratchPropertyEx.XPropertyEx, "X+Width/2")
                window.SetFx(Nevron.Nov.Dom.NScratchPropertyEx.YPropertyEx, "Y+Height/2")

				' now that we have an automatic center, we need to write expressions that define the X and Y from that center. 
				' These are cyclic expressions - CenterX depends on X, and X depends on CenterX.
				' The expressions that are assigned to X and Y are guarded and permeable. 
				'    guard is needed because X and Y are updated when the user moves the window around.
				'    permeable is needed to allow the X and Y values to change when the user moves the window around.
				' When the the X and Y values change -> center changes -> X and Y expressions are triggered but they produce the same X and Y results and the cycle ends.
				' When the Width and Height change -> center changes -> X and Y expression are triggered but they produce the same X and Y results and the cycle ends.
				Dim xfx As Nevron.Nov.Dom.NFormulaFx = New Nevron.Nov.Dom.NFormulaFx(Nevron.Nov.Dom.NScratchPropertyEx.XPropertyEx.Name & "-Width/2")
                xfx.Guard = True
                xfx.Permeable = True
                window.SetFx(Nevron.Nov.UI.NTopLevelWindow.XProperty, xfx)
                Dim yfx As Nevron.Nov.Dom.NFormulaFx = New Nevron.Nov.Dom.NFormulaFx(Nevron.Nov.Dom.NScratchPropertyEx.YPropertyEx.Name & "-Height/2")
                yfx.Guard = True
                yfx.Permeable = True
                window.SetFx(Nevron.Nov.UI.NTopLevelWindow.YProperty, yfx)
            End If

			' create a dummy tab that sizes to the currently selected page,
			' and add two pages with different sizes to the tab.
			Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            window.Content = tab
            tab.SizeToSelectedPage = True
            Dim page1 As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Small Content")
            Dim btn As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("I am small")
            page1.Content = btn
            tab.TabPages.Add(page1)
            Dim page2 As Nevron.Nov.UI.NTabPage = New Nevron.Nov.UI.NTabPage("Large Content")
            Dim btn2 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("I am LARGE")
            btn2.PreferredSize = New Nevron.Nov.Graphics.NSize(200, 200)
            page2.Content = btn2
            tab.TabPages.Add(page2)

			' open the window
			DisplayWindow.Windows.Add(window)
            window.Open()
        End Sub
		
		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTopLevelWindowAutoSizingExample.
		''' </summary>
		Public Shared ReadOnly NTopLevelWindowAutoSizingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
