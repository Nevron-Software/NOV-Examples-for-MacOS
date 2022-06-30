Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.UI.ThemeBuilder

Namespace Nevron.Nov.Examples.UI
    Public Class NThemeBuilderExample
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
            Nevron.Nov.Examples.UI.NThemeBuilderExample.NThemeBuilderExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NThemeBuilderExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim showThemeBuilderButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open NOV Theme Builder")
            showThemeBuilderButton.Margins = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing, Nevron.Nov.NDesign.VerticalSpacing)
            showThemeBuilderButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            showThemeBuilderButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler showThemeBuilderButton.Click, AddressOf Me.OnShowThemeBuilderButtonClick
            Call Nevron.Nov.UI.NStylePropertyEx.SetRelativeFontSize(showThemeBuilderButton, Nevron.Nov.UI.ENRelativeFontSize.Large)
            Return showThemeBuilderButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to open the NOV Theme Builder via code. Click the <b>Open NOV Theme Builder</b> button
to show the theme builder in a new window.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnShowThemeBuilderButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Create a Theme Builder widget
			Dim themeBuilderWidget As Nevron.Nov.UI.ThemeBuilder.NThemeBuilderWidget = New Nevron.Nov.UI.ThemeBuilder.NThemeBuilderWidget()

			' Create and open a top level window with the Theme Builder widget
			Dim window As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(DisplayWindow)
            window.StartPosition = Nevron.Nov.UI.ENWindowStartPosition.CenterScreen
            window.PreferredSize = New Nevron.Nov.Graphics.NSize(1000, 700)
            window.SetupApplicationWindow("NOV Theme Builder")
            window.Modal = True
            window.Content = themeBuilderWidget

			' Open the window and show the Theme Builder start up dialog
			window.Open()
            themeBuilderWidget.ShowStartUpDialog()
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NThemeBuilderExample.
		''' </summary>
		Public Shared ReadOnly NThemeBuilderExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
