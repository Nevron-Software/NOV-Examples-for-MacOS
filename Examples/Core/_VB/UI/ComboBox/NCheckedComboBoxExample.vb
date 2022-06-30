Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCheckedComboBoxExample
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
            Nevron.Nov.Examples.UI.NCheckedComboBoxExample.NCheckedComboBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCheckedComboBoxExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.Padding = New Nevron.Nov.Graphics.NMargins(5)
            Dim headerLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Geography Test")
            headerLabel.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Call Nevron.Nov.UI.NStylePropertyEx.SetRelativeFontSize(headerLabel, Nevron.Nov.UI.ENRelativeFontSize.XXLarge)  ' was huge
            stack.Add(headerLabel)
            Dim contentLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Place a check on the countries located in Europe and select the one whose capital is Berlin:")
            Call Nevron.Nov.UI.NStylePropertyEx.SetRelativeFontSize(contentLabel, Nevron.Nov.UI.ENRelativeFontSize.Large) ' was medium
            stack.Add(contentLabel)

			' Create a combo box with check boxes
			Dim comboBox As Nevron.Nov.UI.NCheckedComboBox = New Nevron.Nov.UI.NCheckedComboBox()
            comboBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            comboBox.AddCheckBoxItem("Argentina", False)
            comboBox.AddCheckBoxItem("Bulgaria", True)
            comboBox.AddCheckBoxItem("Canada", False)
            comboBox.AddCheckBoxItem("Germany", True)
            comboBox.AddCheckBoxItem("Japan", False)
            comboBox.AddCheckBoxItem("Mexico", False)
            comboBox.AddCheckBoxItem("Spain", True)
            comboBox.AddCheckBoxItem("USA", False)
            stack.Add(comboBox)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a class that inherits from NComboBox and has check boxes in the combo box items.
</p>
"
        End Function

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCheckedComboBoxExample.
		''' </summary>
		Public Shared ReadOnly NCheckedComboBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
