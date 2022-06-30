Imports System.Text
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Text
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Text
	''' <summary>
	''' The example demonstrates how to get document statistics
	''' </summary>
	Public Class NDocumentStatisticsExample
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
            Nevron.Nov.Examples.Text.NDocumentStatisticsExample.NDocumentStatisticsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Text.NDocumentStatisticsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create the rich text
			Me.m_RichText = New Nevron.Nov.Text.NRichTextView()
            Me.m_RichText.AcceptsTab = True
            Me.m_RichText.Content.Sections.Clear()

			' set to print to have page count statistics
			Me.m_RichText.Content.Layout = Nevron.Nov.Text.ENTextLayout.Print

			' add some content
			Dim section As Nevron.Nov.Text.NSection = New Nevron.Nov.Text.NSection()

            For i As Integer = 0 To 10 - 1
                Dim text As String = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborumstring."
                section.Blocks.Add(New Nevron.Nov.Text.NParagraph(text))
            Next

            Me.m_RichText.Content.Sections.Add(section)
            Me.m_RichText.HRuler.Visibility = Nevron.Nov.UI.ENVisibility.Visible
            Me.m_RichText.VRuler.Visibility = Nevron.Nov.UI.ENVisibility.Visible
            Return Me.m_RichText
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim statisticsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Get Statistics")
            AddHandler statisticsButton.Click, AddressOf Me.StatisticsButton_Click
            stack.Add(statisticsButton)
            Me.m_StatisticsTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_StatisticsTextBox.Multiline = True
            Me.m_StatisticsTextBox.[ReadOnly] = True
            stack.Add(Me.m_StatisticsTextBox)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to get document statistics about character count, word count etc.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub StatisticsButton_Click(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim builder As System.Text.StringBuilder = New System.Text.StringBuilder()
            builder.AppendLine("Character Count: " & Me.m_RichText.Content.Statistics.CharactersWithSpacesCount.ToString())
            builder.AppendLine("Word Count: " & Me.m_RichText.Content.Statistics.WordCount.ToString())
            builder.AppendLine("Line Count: " & Me.m_RichText.Content.Statistics.LineCount.ToString())
            builder.AppendLine("Paragraph Count: " & Me.m_RichText.Content.Statistics.ParagraphCount.ToString())
            builder.AppendLine("Page Count: " & Me.m_RichText.Content.Statistics.PageCount.ToString())
            Me.m_StatisticsTextBox.Text = builder.ToString()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RichText As Nevron.Nov.Text.NRichTextView
        Private m_StatisticsTextBox As Nevron.Nov.UI.NTextBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDocumentStatisticsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
