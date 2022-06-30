Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NStackPanelExample
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
            Nevron.Nov.Examples.UI.NStackPanelExample.NStackPanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NStackPanelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_StackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_StackPanel.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_StackPanel.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)

            For i As Integer = 1 To 16
                Me.m_StackPanel.Add(New Nevron.Nov.UI.NButton("Button " & i.ToString()))
            Next

            Return Me.m_StackPanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' properties stack
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_StackPanel), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_StackPanel, Nevron.Nov.UI.NStackPanel.EnabledProperty, Nevron.Nov.UI.NStackPanel.HorizontalPlacementProperty, Nevron.Nov.UI.NStackPanel.VerticalPlacementProperty, Nevron.Nov.UI.NStackPanel.DirectionProperty, Nevron.Nov.UI.NStackPanel.VerticalSpacingProperty, Nevron.Nov.UI.NStackPanel.HorizontalSpacingProperty, Nevron.Nov.UI.NStackPanel.FitModeProperty, Nevron.Nov.UI.NStackPanel.FillModeProperty, Nevron.Nov.UI.NStackPanel.UniformWidthsProperty, Nevron.Nov.UI.NStackPanel.UniformHeightsProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                Dim editor As Nevron.Nov.Editors.NPropertyEditor = editors(i)

                If editor.EditedProperty Is Nevron.Nov.UI.NStackPanel.HorizontalSpacingProperty Then
                    editor.SetEditedLocalValue(Nevron.Nov.NDesign.HorizontalSpacing)
                ElseIf editor.EditedProperty Is Nevron.Nov.UI.NStackPanel.VerticalSpacingProperty Then
                    editor.SetEditedLocalValue(Nevron.Nov.NDesign.VerticalSpacing)
                End If

                propertiesStack.Add(editor)
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' items stack
			Dim itemsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim addSmallItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Small Item")
            AddHandler addSmallItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddSmallItemButtonClick)
            itemsStack.Add(addSmallItemButton)
            Dim addLargeItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Large Item")
            AddHandler addLargeItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddLargeItemButtonClick)
            itemsStack.Add(addLargeItemButton)
            Dim addRandomItemButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Add Random Item")
            AddHandler addRandomItemButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnAddRandomItemButtonClick)
            itemsStack.Add(addRandomItemButton)
            Dim removeAllItemsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Remove All Items")
            AddHandler removeAllItemsButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnRemoveAllItemsButtonClick)
            itemsStack.Add(removeAllItemsButton)
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", itemsStack))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a stack layout panel and add
	widgets to it. You can control the parameters of the layout algorithm
	using the controls to the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddSmallItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Small." & Me.m_StackPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(5, 5)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(25, 25)
            Me.m_StackPanel.Add(item)
        End Sub

        Private Sub OnAddLargeItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Large." & Me.m_StackPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(20, 20)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(60, 60)
            Me.m_StackPanel.Add(item)
        End Sub

        Private Sub OnAddRandomItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim range As Integer = 30
            Dim rnd As System.Random = New System.Random()
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random." & Me.m_StackPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range), rnd.[Next](range))
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range) + range, rnd.[Next](range) + range)
            Me.m_StackPanel.Add(item)
        End Sub

        Private Sub OnRemoveAllItemsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_StackPanel.Clear()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_StackPanel As Nevron.Nov.UI.NStackPanel

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStackPanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
