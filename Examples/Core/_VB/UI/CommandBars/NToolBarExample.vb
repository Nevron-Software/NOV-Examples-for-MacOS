Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NToolBarExample
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
            Nevron.Nov.Examples.UI.NToolBarExample.NToolBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NToolBarExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ToolBar = New Nevron.Nov.UI.NToolBar()
            Me.m_ToolBar.Text = "My Toolbar"
            Me.m_ToolBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return Me.m_ToolBar
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
			
			' Create the tool bar button type radio group
			Dim buttonTypeStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim buttonTypes As Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType() = Nevron.Nov.NEnum.GetValues(Of Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType)()
            Dim i As Integer = 0, count As Integer = buttonTypes.Length

            While i < count
				' Get the current button type and its string representation
				Dim buttonType As Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType = buttonTypes(i)
                Dim text As String = Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(buttonType.ToString())

				' Create a radio button for the current button type
				Dim radioButton As Nevron.Nov.UI.NRadioButton = New Nevron.Nov.UI.NRadioButton(text)
                buttonTypeStack.Add(radioButton)
                i += 1
            End While

            Dim buttonTypeGroup As Nevron.Nov.UI.NRadioButtonGroup = New Nevron.Nov.UI.NRadioButtonGroup(buttonTypeStack)
            AddHandler buttonTypeGroup.SelectedIndexChanged, AddressOf Me.OnButtonTypeGroupSelectedIndexChanged
            buttonTypeGroup.SelectedIndex = 2
            stack.Add(New Nevron.Nov.UI.NGroupBox("Button Type", buttonTypeGroup))

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a toolbar and populate it with buttons, which consist of image and text.
	Using the controls to the right you can change the size and the visibility of the images, which will result in
	recreation of the toolbar buttons.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function GetSmallImage(ByVal text As String) As Nevron.Nov.Graphics.NImage
            Dim imageName As String = "RIMG_ToolBar_16x16_" & text.Replace(" ", System.[String].Empty) & "_png"
            Dim resourceRef As Nevron.Nov.NEmbeddedResourceRef = New Nevron.Nov.NEmbeddedResourceRef(NResources.Instance, imageName)
            Return New Nevron.Nov.Graphics.NImage(resourceRef)
        End Function

        Private Function GetLargeImage(ByVal text As String) As Nevron.Nov.Graphics.NImage
            Dim imageName As String = "RIMG_ToolBar_32x32_" & text.Replace(" ", System.[String].Empty) & "_png"
            Dim resourceRef As Nevron.Nov.NEmbeddedResourceRef = New Nevron.Nov.NEmbeddedResourceRef(NResources.Instance, imageName)
            Return New Nevron.Nov.Graphics.NImage(resourceRef)
        End Function

        Private Function CreatePairBox(ByVal image As Nevron.Nov.Graphics.NImage, ByVal text As String) As Nevron.Nov.UI.NPairBox
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(image, text)
            pairBox.Box2.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            pairBox.Spacing = 3
            Return pairBox
        End Function

        Private Function CreateButton(ByVal buttonType As Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType, ByVal text As String) As Nevron.Nov.UI.NButton
            Dim button As Nevron.Nov.UI.NButton = Nothing
            Dim image As Nevron.Nov.Graphics.NImage = Nothing

            Select Case buttonType
                Case Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType.Text
                    button = New Nevron.Nov.UI.NButton(text)
                Case Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType.SmallIcon
                    image = Me.GetSmallImage(text)
                    button = New Nevron.Nov.UI.NButton(image)
                Case Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType.SmallIconAndText
                    image = Me.GetSmallImage(text)
                    button = New Nevron.Nov.UI.NButton(Me.CreatePairBox(image, text))
                Case Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType.LargeIcon
                    image = Me.GetLargeImage(text)
                    button = New Nevron.Nov.UI.NButton(image)
                Case Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType.LargeIconAndText
                    image = Me.GetLargeImage(text)
                    button = New Nevron.Nov.UI.NButton(Me.CreatePairBox(image, text))
                Case Else
                    Throw New System.Exception("New ENToolBarButtonType?")
            End Select

            Call Nevron.Nov.UI.NCommandBar.SetText(button, text)
            Call Nevron.Nov.UI.NCommandBar.SetImage(button, image)
            AddHandler button.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnToolBarButtonClick)
            Return button
        End Function

        Private Sub RecreateToolBarButtons(ByVal buttonType As Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType)
            Me.m_ToolBar.Items.Clear()
            Dim i As Integer = 0, buttonCount As Integer = Nevron.Nov.Examples.UI.NToolBarExample.ButtonTexts.Length

            While i < buttonCount
                Dim buttonText As String = Nevron.Nov.Examples.UI.NToolBarExample.ButtonTexts(i)

                If Equals(buttonText, Nothing) OrElse buttonText.Length = 0 Then
                    Me.m_ToolBar.Items.Add(New Nevron.Nov.UI.NCommandBarSeparator())
                Else
                    Me.m_ToolBar.Items.Add(Me.CreateButton(buttonType, buttonText))
                End If

                i += 1
            End While

            Me.m_ToolBar.Items.Add(New Nevron.Nov.UI.NMenuDropDown("Test"))
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when the user has checked a new button type radio button.
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnButtonTypeGroupSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.RecreateToolBarButtons(CType(CInt(arg.NewValue), Nevron.Nov.Examples.UI.NToolBarExample.ENToolBarButtonType))
        End Sub
		''' <summary>
		''' Occurs when the user clicks on a tool bar button.
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnToolBarButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim buttonText As String = Nevron.Nov.UI.NCommandBar.GetText(CType(args.TargetNode, Nevron.Nov.UI.NButton))
            Me.m_EventsLog.LogEvent("<" & buttonText & "> button clicked")
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ToolBar As Nevron.Nov.UI.NToolBar
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NToolBarExample.
		''' </summary>
		Public Shared ReadOnly NToolBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly ButtonTexts As String() = New String() {"Open", "Save", "Save As", Nothing, "Print", "Options", Nothing, "Help"}

		#EndRegion

		#Region"Nested Types"

		Public Enum ENToolBarButtonType
            Text
            SmallIcon
            SmallIconAndText
            LargeIcon
            LargeIconAndText
        End Enum

		#EndRegion
	End Class
End Namespace
