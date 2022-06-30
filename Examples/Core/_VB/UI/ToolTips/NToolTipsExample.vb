Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTooltipsExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NTooltipsExample.NTooltipsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTooltipsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create the host
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalSpacing = 2
            Dim contentGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Content")
            stack.Add(contentGroupBox)
            Dim contentStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            contentStack.VerticalSpacing = 2
            contentGroupBox.Content = contentStack
            Dim textTooltip As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Text tooltip")
            textTooltip.Tooltip = New Nevron.Nov.UI.NTooltip("Tooltip text")
            contentStack.Add(textTooltip)
            Dim imageTooltip As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Image tooltip")
            imageTooltip.Tooltip = New Nevron.Nov.UI.NTooltip(NResources.Image__48x48_Book_png)
            contentStack.Add(imageTooltip)
            Dim richTooltip As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Rich tooltip")
            Dim richTooltipContent As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            richTooltipContent.Add(New Nevron.Nov.UI.NLabel("The tooltip can contain any type of Nevron Open Vision Content"))
            richTooltipContent.Add(New Nevron.Nov.UI.NImageBox(NResources.Image__48x48_Book_png))
            richTooltip.Tooltip = New Nevron.Nov.UI.NTooltip(richTooltipContent)
            contentStack.Add(richTooltip)
            Dim dynamicContentTooltip As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Dynamic content")
            dynamicContentTooltip.Tooltip = New Nevron.Nov.Examples.UI.NTooltipsExample.NDynamicContentTooltip()
            contentStack.Add(dynamicContentTooltip)
            Dim behaviorGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Behavior")
            stack.Add(behaviorGroupBox)
            Dim behaviorStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            behaviorStack.VerticalSpacing = 2
            behaviorGroupBox.Content = behaviorStack
            Dim followMouse As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Follow mouse")
            followMouse.Tooltip = New Nevron.Nov.UI.NTooltip("I am following the mouse")
            followMouse.Tooltip.FollowMouse = True
            behaviorStack.Add(followMouse)
            Dim instantShowTooltip As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Shown instantly")
            instantShowTooltip.Tooltip = New Nevron.Nov.UI.NTooltip("I was shown instantly")
            instantShowTooltip.Tooltip.FirstShowDelay = 0
            instantShowTooltip.Tooltip.NextShowDelay = 0
            behaviorStack.Add(instantShowTooltip)
            Dim doNotCloseOnClick As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Do not close on click")
            doNotCloseOnClick.Tooltip = New Nevron.Nov.UI.NTooltip("I am not closed on click")
            doNotCloseOnClick.Tooltip.CloseOnMouseDown = False
            behaviorStack.Add(doNotCloseOnClick)
            Dim positionGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Position")
            stack.Add(positionGroupBox)
            Dim positionTable As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            positionGroupBox.Content = positionTable
            positionTable.HorizontalSpacing = 2
            positionTable.VerticalSpacing = 2
            positionTable.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            positionTable.MaxOrdinal = 3

            For Each pos As Nevron.Nov.UI.ENTooltipPosition In Nevron.Nov.NEnum.GetValues(GetType(Nevron.Nov.UI.ENTooltipPosition))
                Dim posElement As Nevron.Nov.UI.NWidget = Me.CreateDemoElement(pos.ToString())
                posElement.Tooltip = New Nevron.Nov.UI.NTooltip(pos.ToString())
                posElement.Tooltip.FirstShowDelay = 0
                posElement.Tooltip.NextShowDelay = 0
                posElement.Tooltip.Position = pos
                posElement.Tooltip.FollowMouse = True
                posElement.Tooltip.ShowDuration = -1
                positionTable.Add(posElement)
            Next

            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use tooltips. The tooltip is a container for the information
	that can be displayed when the mouse moves over a certain element or area. The content of the tooltip can
	be set through its constructor or through its <b>Content</b> property. The content can be any object and is
	converted to an <b>NWidget</b> by the <b>GetContent()</b> method of the <b>NTooltip</b> class. The <b>NTooltip</b>
	class provides several properties that allows you to control the tooltip position, show delay and duration,
	whether it should follow the mouse cursor or not and so on.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateDemoElement(ByVal text As String) As Nevron.Nov.UI.NWidget
            Dim element As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(text)
            element.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black, 2, 5)
            element.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            element.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)
            element.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            element.Padding = New Nevron.Nov.Graphics.NMargins(10)
            Return element
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTooltipsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types - NDynamicContentTooltip"

		''' <summary>
		''' A tooltip that shows as content the current date and time
		''' </summary>
		Public Class NDynamicContentTooltip
            Inherits Nevron.Nov.UI.NTooltip
			#Region"Constructors"

			Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.UI.NTooltipsExample.NDynamicContentTooltip.NDynamicContentTooltipSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTooltipsExample.NDynamicContentTooltip), Nevron.Nov.UI.NTooltip.NTooltipSchema)
            End Sub

			#EndRegion

			#Region"Overrides - GetContent()"

			Public Overrides Function GetContent() As Nevron.Nov.UI.NWidget
                Dim now As System.DateTime = System.DateTime.Now
                Return New Nevron.Nov.UI.NLabel("I was shown at: " & now.ToString("T"))
            End Function

			#EndRegion

			#Region"Schema"

			Public Shared ReadOnly NDynamicContentTooltipSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
