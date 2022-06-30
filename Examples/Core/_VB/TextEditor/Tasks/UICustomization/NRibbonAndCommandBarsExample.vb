Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.Text.UI
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
    Public Class NRibbonAndCommandBarsExample
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
            Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.NRibbonAndCommandBarsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' Populate the rich text
			Me.PopulateRichText()

			' Create and execute a ribbon UI builder
			Me.m_RibbonBuilder = New Nevron.Nov.Text.UI.NRichTextRibbonBuilder()
            Return Me.m_RibbonBuilder.CreateUI(Me.m_RichText)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
			' Switch UI button
			Dim switchUIButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.SwitchToCommandBars)
            switchUIButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler switchUIButton.Click, AddressOf Me.OnSwitchUIButtonClick
            Return switchUIButton
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to switch the NOV Rich Text commanding interface between ribbon and command bars.</p>"
        End Function

        Private Sub PopulateRichText()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            Me.m_RichText.Content.Sections.Add(section)
            section.Blocks.Add(Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.GetDescriptionBlock("Rich Text Ribbon and Command Bars", "This example demonstrates how to customize the NOV rich text ribbon.", 1))
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Sub SetUI(ByVal oldUiHolder As Nevron.Nov.UI.NCommandUIHolder, ByVal widget As Nevron.Nov.UI.NWidget)
            If TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NTabPage Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NTabPage).Content = widget
            ElseIf TypeOf oldUiHolder.ParentNode Is Nevron.Nov.UI.NPairBox Then
                CType(oldUiHolder.ParentNode, Nevron.Nov.UI.NPairBox).Box1 = widget
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSwitchUIButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim switchUIButton As Nevron.Nov.UI.NButton = CType(arg.TargetNode, Nevron.Nov.UI.NButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(switchUIButton.Content, Nevron.Nov.UI.NLabel)

			' Remove the rich text view from its parent
			Dim uiHolder As Nevron.Nov.UI.NCommandUIHolder = Me.m_RichText.GetFirstAncestor(Of Nevron.Nov.UI.NCommandUIHolder)()
            Me.m_RichText.ParentNode.RemoveChild(Me.m_RichText)

            If Equals(label.Text, Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.SwitchToRibbon) Then
				' We are in "Command Bars" mode, so switch to "Ribbon"
				label.Text = Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.SwitchToCommandBars

				' Create the ribbon
				Me.SetUI(uiHolder, Me.m_RibbonBuilder.CreateUI(Me.m_RichText))
            Else
				' We are in "Ribbon" mode, so switch to "Command Bars"
				label.Text = Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.SwitchToRibbon

				' Create the command bars
				If Me.m_CommandBarBuilder Is Nothing Then
                    Me.m_CommandBarBuilder = New Nevron.Nov.Text.UI.NRichTextCommandBarBuilder()
                End If

                Me.SetUI(uiHolder, Me.m_CommandBarBuilder.CreateUI(Me.m_RichText))
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_RibbonBuilder As Nevron.Nov.Text.UI.NRichTextRibbonBuilder
        Private m_CommandBarBuilder As Nevron.Nov.Text.UI.NRichTextCommandBarBuilder

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NRibbonAndCommandBarsExample.
		''' </summary>
		Public Shared ReadOnly NRibbonAndCommandBarsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function GetDescriptionParagraph(ByVal text As String) As Nevron.Nov.Text.NParagraph
            Return New Nevron.Nov.Text.NParagraph(text)
        End Function

        Private Shared Function GetTitleParagraphNoBorder(ByVal text As String, ByVal level As Integer) As Nevron.Nov.Text.NParagraph
            Dim fontSize As Double = 10
            Dim fontStyle As Nevron.Nov.Graphics.ENFontStyle = Nevron.Nov.Graphics.ENFontStyle.Regular

            Select Case level
                Case 1
                    fontSize = 16
                    fontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
                Case 2
                    fontSize = 10
                    fontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
            End Select

            Dim paragraph As Nevron.Nov.Text.NParagraph = New Nevron.Nov.Text.NParagraph()
            paragraph.HorizontalAlignment = Nevron.Nov.Text.ENAlign.Left
            paragraph.FontSize = fontSize
            paragraph.FontStyle = fontStyle
            Dim textInline As Nevron.Nov.Text.NTextInline = New Nevron.Nov.Text.NTextInline(text)
            textInline.FontStyle = fontStyle
            textInline.FontSize = fontSize
            paragraph.Inlines.Add(textInline)
            Return paragraph
        End Function

        Private Shared Function GetDescriptionBlock(ByVal title As String, ByVal description As String, ByVal level As Integer) As Nevron.Nov.Text.NGroupBlock
            Dim color As Nevron.Nov.Graphics.NColor = Nevron.Nov.Graphics.NColor.Black
            Dim paragraph As Nevron.Nov.Text.NParagraph = Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.GetTitleParagraphNoBorder(title, level)
            Dim groupBlock As Nevron.Nov.Text.NGroupBlock = New Nevron.Nov.Text.NGroupBlock()
            groupBlock.ClearMode = Nevron.Nov.Text.ENClearMode.All
            groupBlock.Blocks.Add(paragraph)
            groupBlock.Blocks.Add(Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.GetDescriptionParagraph(description))
            groupBlock.Border = Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.CreateLeftTagBorder(color)
            groupBlock.BorderThickness = Nevron.Nov.Examples.Text.NRibbonAndCommandBarsExample.DefaultBorderThickness
            Return groupBlock
        End Function
		''' <summary>
		''' Creates a left tag border with the specified border
		''' </summary>
		''' <paramname="color"></param>
		''' <returns></returns>
		Private Shared Function CreateLeftTagBorder(ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.UI.NBorder
            Dim border As Nevron.Nov.UI.NBorder = New Nevron.Nov.UI.NBorder()
            border.LeftSide = New Nevron.Nov.UI.NBorderSide()
            border.LeftSide.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Return border
        End Function

		#EndRegion

		#Region"Constants"

		Private Const SwitchToCommandBars As String = "Switch to Command Bars"
        Private Const SwitchToRibbon As String = "Switch to Ribbon"
        Private Shared ReadOnly DefaultBorderThickness As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(5.0, 0.0, 0.0, 0.0)

		#EndRegion
	End Class
End Namespace
