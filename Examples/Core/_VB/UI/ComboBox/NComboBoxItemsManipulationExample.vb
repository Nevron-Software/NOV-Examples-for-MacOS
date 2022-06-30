Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NComboBoxItemsManipulationExample
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
            Nevron.Nov.Examples.UI.NComboBoxItemsManipulationExample.NComboBoxItemsManipulationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NComboBoxItemsManipulationExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Overrides - Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' create the combo box
			Me.m_ComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ComboBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_ComboBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_ComboBox.DropDownStyle = Nevron.Nov.UI.ENComboBoxStyle.DropDownList

			' add a few items
			For i As Integer = 0 To 10 - 1
                Me.m_ComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item " & i.ToString()))
            Next

			' select the first item
			Me.m_ComboBox.SelectedIndex = 0

			' hook combo box selection events
			AddHandler Me.m_ComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnComboBoxSelectedIndexChanged)
            Return Me.m_ComboBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Create the commands
			stack.Add(Me.CreateCommandsGroupBox())

			' Create the events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a simple combo box with text only items and how to add/remove items.
	You can use the buttons on the right to add/remove items from the combo box's <b>Items</b> collection.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateCommandsGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim commandsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim addButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Item")
            AddHandler addButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddButtonClick)
            commandsStack.Add(addButton)
            Dim removeSelectedButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Remove Selected")
            AddHandler removeSelectedButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveSelectedButtonClick)
            commandsStack.Add(removeSelectedButton)
            Dim removeAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Remove All")
            AddHandler removeAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveAllButtonClick)
            commandsStack.Add(removeAllButton)
            Return New Nevron.Nov.UI.NGroupBox("Commands", commandsStack)
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnComboBoxSelectedIndexChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(args.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_EventsLog.LogEvent("Selected Index: " & comboBox.SelectedIndex.ToString())
        End Sub

        Private Sub OnAddButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_ComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Item " & Me.m_ComboBox.Items.Count))
        End Sub

        Private Sub OnRemoveSelectedButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            If Me.m_ComboBox.SelectedIndex <> -1 Then
                Me.m_ComboBox.Items.RemoveAt(Me.m_ComboBox.SelectedIndex)
                Me.m_ComboBox.SelectedIndex = -1
            End If
        End Sub

        Private Sub OnRemoveAllButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_ComboBox.Items.Clear()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ComboBox As Nevron.Nov.UI.NComboBox
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NComboBoxItemsManipulationExample.
		''' </summary>
		Public Shared ReadOnly NComboBoxItemsManipulationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
