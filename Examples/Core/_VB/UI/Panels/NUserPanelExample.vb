Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NUserPanelExample
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
            Nevron.Nov.Examples.UI.NUserPanelExample.NUserPanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NUserPanelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim panel As Nevron.Nov.UI.NUserPanel = New Nevron.Nov.UI.NUserPanel()
            panel.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            panel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit

			' create a button that is anchored at the left-top
			' the button is sized to its desired size
			Dim button0 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Anchor Left Top")
            button0.SetValue(Nevron.Nov.UI.NButton.XProperty, 10.0)
            button0.SetValue(Nevron.Nov.UI.NButton.YProperty, 10.0)
            button0.SetFx("Width", "DesiredWidth")
            button0.SetFx("Height", "DesiredHeight")
            panel.Add(button0)

			' create a button that is anchored at the right-top
			' the button is sized to its desired size, 
			' and the X is computed ralatively to the parent right side.
			Dim button1 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Anchor Right Top")
            button1.SetFx("X", "$Parent.Width - Width - 10")
            button1.SetValue(Nevron.Nov.UI.NButton.YProperty, 10.0)
            button1.SetFx("Width", "DesiredWidth")
            button1.SetFx("Height", "DesiredHeight")
            panel.Add(button1)

			' create a button that is anchored at the right-bottom
			' the button is sized to its desired size, 
			' and the X and Y are computed ralatively to the parent right bottom sides.
			Dim button2 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Anchor Right Bottom")
            button2.SetFx("X", "$Parent.Width - Width - 10")
            button2.SetFx("Y", "$Parent.Height - Height - 10")
            button2.SetFx("Width", "DesiredWidth")
            button2.SetFx("Height", "DesiredHeight")
            panel.Add(button2)

			' create a button that is anchored at the left-bottom
			' the button is sized to its desired size, 
			' and the Y is computed ralatively to the parent bottom side.
			Dim button3 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Anchor Right Bottom")
            button3.SetValue(Nevron.Nov.UI.NButton.XProperty, 10.0)
            button3.SetFx("Y", "$Parent.Height - Height - 10")
            button3.SetFx("Width", "DesiredWidth")
            button3.SetFx("Height", "DesiredHeight")
            panel.Add(button3)

			' create a button which is anchored to inner corners of the other four buttons
			Dim button4 As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Anchor All")
            button4.SetFx("X", "MAX($Parent.0.X + $Parent.0.Width, $Parent.3.X + $Parent.3.Width)")
            button4.SetFx("Y", "MAX($Parent.0.Y + $Parent.0.Height, $Parent.1.Y + $Parent.1.Height)")
            button4.SetFx("Width", "MIN($Parent.1.X, $Parent.2.X) - X")
            button4.SetFx("Height", "MIN($Parent.2.Y, $Parent.3.Y) - Y")
            panel.Add(button4)
            Return panel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a panel and how to add widgets to it.
	The example also shows how to assign expressions to some properties of the
	widgets so that they become anchored to the sides of the panel.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NUserPanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
