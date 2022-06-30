Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to programmatically a sample report.
	''' </summary>
	Public Class NBottomlessModeExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' 
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' 
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Text.NBottomlessModeExample.NBottomlessModeExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NBottomlessModeExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()
            Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()
            section.Blocks.Add(New Nevron.Nov.Text.NParagraph("Type some content here"))
            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Web
            Me.m_RichText.ViewSettings.ExtendLineBreakWithSpaces = False
            Me.m_RichText.VScrollMode = Nevron.Nov.UI.ENScrollMode.Never
            Me.m_RichText.HScrollMode = Nevron.Nov.UI.ENScrollMode.Never
            Me.m_RichText.HRuler.Visibility = Nevron.Nov.UI.ENVisibility.Hidden
            Me.m_RichText.VRuler.Visibility = Nevron.Nov.UI.ENVisibility.Hidden
            Me.m_RichText.PreferredWidth = Double.NaN
            Me.m_RichText.PreferredHeight = Double.NaN
            Me.m_RichText.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            Me.m_RichText.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_RichText.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            Me.m_RichText.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Return Me.m_RichText
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create a bottomless text control.</p>"
        End Function

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NBottomlessModeExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
