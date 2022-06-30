Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NWrappableCommandBarsExample
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
            Nevron.Nov.Examples.UI.NWrappableCommandBarsExample.NWrappableCommandBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NWrappableCommandBarsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim manager As Nevron.Nov.UI.NCommandBarManager = New Nevron.Nov.UI.NCommandBarManager()

			' create two lanes
			Dim lane0 As Nevron.Nov.UI.NCommandBarLane = New Nevron.Nov.UI.NCommandBarLane()
            manager.TopDock.Add(lane0)

			' create some toolbars in the second lane
			For i As Integer = 0 To 10 - 1
                Dim toolBar As Nevron.Nov.UI.NToolBar = New Nevron.Nov.UI.NToolBar()
                lane0.Add(toolBar)
                toolBar.Text = "Bar" & i.ToString()

                For j As Integer = 0 To 8 - 1
                    Dim name As String = "BTN " & i.ToString() & "." & j.ToString()
                    Dim item As Nevron.Nov.UI.NWidget

                    If j = 2 Then
                        item = New Nevron.Nov.UI.NColorBox()
                    ElseIf j = 3 Then
                        Dim msb As Nevron.Nov.UI.NMenuSplitButton = New Nevron.Nov.UI.NMenuSplitButton()
                        msb.ActionButton.Content = Nevron.Nov.UI.NWidget.FromObject("Send/Receive")
                        msb.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Send Receive All"))
                        msb.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Send All"))
                        msb.Menu.Items.Add(New Nevron.Nov.UI.NMenuItem("Receive All"))
                        item = msb
                    ElseIf j = 4 Then
                        Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
                        comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 1"))
                        comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 2"))
                        comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 3"))
                        comboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item 4"))
                        item = comboBox
                    Else
                        item = New Nevron.Nov.UI.NButton(name)
                    End If

                    Call Nevron.Nov.UI.NCommandBar.SetText(item, name)
                    toolBar.Items.Add(item)

                    If j = 2 OrElse j = 6 Then
                        toolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
                    End If
                Next

                If i = 2 Then
                    toolBar.Wrappable = True
                End If
            Next

            manager.Content = New Nevron.Nov.UI.NLabel("Content Goes Here")
            manager.Content.AllowFocus = True
            AddHandler manager.Content.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnContentMouseDown)
            manager.Content.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            manager.Content.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            manager.Content.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            AddHandler manager.Content.GotFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnContentGotFocus)
            AddHandler manager.Content.LostFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnContentLostFocus)
            Return manager
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates multiline and wrappable toolbars.
</p>
"
        End Function

		#EndRegion
		
		#Region"Event Handlers"

		Private Sub OnContentLostFocus(ByVal args As Nevron.Nov.UI.NFocusChangeEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
        End Sub

        Private Sub OnContentGotFocus(ByVal args As Nevron.Nov.UI.NFocusChangeEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
        End Sub

        Private Sub OnContentMouseDown(ByVal args As Nevron.Nov.UI.NMouseButtonEventArgs)
            TryCast(args.TargetNode, Nevron.Nov.UI.NLabel).Focus()
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NWrappableCommandBarsExample.
		''' </summary>
		Public Shared ReadOnly NWrappableCommandBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
