Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NGroupBoxExample
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
            Nevron.Nov.Examples.UI.NGroupBoxExample.NGroupBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NGroupBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ContentPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_ContentPanel.VerticalSpacing = 3

			' Create the first group box
			Dim groupBox1 As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Group Box 1")
            Me.m_ContentPanel.Add(groupBox1)
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Button inside group box")
            groupBox1.Content = button

			' Create the second group box
			Dim groupBox2 As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Group Box 2 - Centered Header")
            groupBox2.Header.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Me.m_ContentPanel.Add(groupBox2)
            Dim stack1 As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            groupBox2.Content = stack1
            stack1.Add(New Nevron.Nov.UI.NLabel("Label 1 in stack"))
            stack1.Add(New Nevron.Nov.UI.NLabel("Label 2 in stack"))
            stack1.Add(New Nevron.Nov.UI.NLabel("Label 3 in stack"))

            ' Create the third group box
            Dim groupBox3 As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Group Box 3 - Expandable")
            groupBox3.Expandable = True
            groupBox3.Content = New Nevron.Nov.UI.NImageBox(NResources.Image_Artistic_FishBowl_jpg)
            Me.m_ContentPanel.Add(groupBox3)
            Return Me.m_ContentPanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim checkBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enabled", True)
            checkBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler checkBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnabledCheckBoxCheckedChanged)
            Return checkBox
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a group box. The group box is a widget that consists of 2 widgets –
	<b>Header</b> and <b>Content</b>.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnabledCheckBoxCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ContentPanel.Enabled = CBool(args.NewValue)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ContentPanel As Nevron.Nov.UI.NStackPanel

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NGroupBoxExample.
		''' </summary>
		Public Shared ReadOnly NGroupBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
