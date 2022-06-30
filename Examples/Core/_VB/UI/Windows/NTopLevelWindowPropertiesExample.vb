Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTopLevelWindowPropertiesExample
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
            Nevron.Nov.Examples.UI.NTopLevelWindowPropertiesExample.NTopLevelWindowPropertiesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTopLevelWindowPropertiesExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create and initialize a top level window
			Me.m_Window = New Nevron.Nov.UI.NTopLevelWindow()
            Me.m_Window.Title = "Top Level Window"
            Me.m_Window.RemoveFromParentOnClose = True
            Me.m_Window.AllowXResize = True
            Me.m_Window.AllowYResize = True
            Me.m_Window.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 300)
            AddHandler Me.m_Window.QueryManualStartPosition, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowQueryManualStartPosition)
            AddHandler Me.m_Window.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowClosed)

			' Create the top level window's content
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("This is a top level window.")
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            stack.Add(label)
            Dim closeButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Close")
            closeButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler closeButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnCloseButtonClick)
            stack.Add(closeButton)
            Me.m_Window.Content = stack

			' Create example content
			Me.m_SettingsStack = New Nevron.Nov.UI.NStackPanel()
            Me.m_SettingsStack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Window), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Window, Nevron.Nov.UI.NTopLevelWindow.TitleProperty, Nevron.Nov.UI.NTopLevelWindow.StartPositionProperty, Nevron.Nov.UI.NTopLevelWindow.XProperty, Nevron.Nov.UI.NTopLevelWindow.YProperty, Nevron.Nov.UI.NStylePropertyEx.ExtendedLookPropertyEx, Nevron.Nov.UI.NTopLevelWindow.ModalProperty, Nevron.Nov.UI.NTopLevelWindow.ShowInTaskbarProperty, Nevron.Nov.UI.NTopLevelWindow.ShowTitleBarProperty, Nevron.Nov.UI.NTopLevelWindow.ShowControlBoxProperty, Nevron.Nov.UI.NTopLevelWindow.AllowMinimizeProperty, Nevron.Nov.UI.NTopLevelWindow.AllowMaximizeProperty, Nevron.Nov.UI.NTopLevelWindow.AllowXResizeProperty, Nevron.Nov.UI.NTopLevelWindow.AllowYResizeProperty)

            ' Change the text of the extended look property editor
            label = CType(editors(CInt((4))).GetFirstDescendant(New Nevron.Nov.Dom.NInstanceOfSchemaFilter(Nevron.Nov.UI.NLabel.NLabelSchema)), Nevron.Nov.UI.NLabel)
            label.Text = "Extended Look:"

            ' Add the created property editors to the stack
			Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                Me.m_SettingsStack.Add(editors(i))
                i += 1
            End While

			' Create a button that opens the window
			Dim openWindowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open Window...")
            openWindowButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            AddHandler openWindowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenWindowButtonClick)
            Me.m_SettingsStack.Add(openWindowButton)
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(Me.m_SettingsStack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create, configure and open top level windows. Use the controls
	above to configure the properties of the top level window and then click ""Open Window...""
	button to show it.
</p>
"
        End Function

        Protected Friend Overrides Sub OnClosing()
            MyBase.OnClosing()
            Me.m_Window.Close()
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnOpenWindowButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            DisplayWindow.Windows.Add(Me.m_Window)
            Me.m_Window.Open()
            Me.m_SettingsStack.Enabled = False
        End Sub

        Private Sub OnCloseButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_Window.Close()
        End Sub

        Private Sub OnWindowQueryManualStartPosition(ByVal args As Nevron.Nov.Dom.NEventArgs)
			' Get the top level window which queries for position
			Dim window As Nevron.Nov.UI.NTopLevelWindow = CType(args.TargetNode, Nevron.Nov.UI.NTopLevelWindow)

			' Set the top level window bounds (in DIPs)
			window.Bounds = New Nevron.Nov.Graphics.NRectangle(window.X, window.Y, window.DefaultWidth, window.DefaultHeight)
        End Sub

        Private Sub OnWindowClosed(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_SettingsStack.Enabled = True
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Window As Nevron.Nov.UI.NTopLevelWindow
        Private m_SettingsStack As Nevron.Nov.UI.NStackPanel

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTopLevelWindowPropertiesExample.
		''' </summary>
		Public Shared ReadOnly NTopLevelWindowPropertiesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
