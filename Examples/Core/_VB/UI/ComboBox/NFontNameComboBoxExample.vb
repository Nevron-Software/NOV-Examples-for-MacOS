Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NFontNameComboBoxExample
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
            Nevron.Nov.Examples.UI.NFontNameComboBoxExample.NFontNameComboBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NFontNameComboBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Overrides - Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create the combo box
			Me.m_FontNameComboBox = New Nevron.Nov.UI.NFontNameThumbnailComboBox()
            Me.m_FontNameComboBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_FontNameComboBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_FontNameComboBox.DropDownStyle = Nevron.Nov.UI.ENComboBoxStyle.DropDownList

			' select the first item
			Me.m_FontNameComboBox.SelectedIndex = 0
            AddHandler Me.m_FontNameComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontNameChanged)
            Return Me.m_FontNameComboBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' Create the commands
			Dim selectFontNameTitle As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Selected Font Name:")
            stack.Add(selectFontNameTitle)
            Me.m_SelectFontName = New Nevron.Nov.UI.NLabel("")
            stack.Add(Me.m_SelectFontName)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to use the built-in font name combo box, which allows the user to select a font name from the list of available fonts.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnFontNameChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NFontNameThumbnailComboBox = CType(args.TargetNode, Nevron.Nov.UI.NFontNameThumbnailComboBox)
            Me.m_SelectFontName.Text = comboBox.SelectedFontName
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_FontNameComboBox As Nevron.Nov.UI.NFontNameThumbnailComboBox
        Private m_SelectFontName As Nevron.Nov.UI.NLabel

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NFontNameComboBoxExample.
		''' </summary>
		Public Shared ReadOnly NFontNameComboBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
