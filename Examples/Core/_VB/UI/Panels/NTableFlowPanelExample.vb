Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTableFlowPanelExample
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
            Nevron.Nov.Examples.UI.NTableFlowPanelExample.NTableFlowPanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTableFlowPanelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_TablePanel = New Nevron.Nov.UI.NTableFlowPanel()
            Me.m_TablePanel.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_TablePanel.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)

            For i As Integer = 1 To 16
                Me.m_TablePanel.Add(New Nevron.Nov.UI.NButton("Button " & i.ToString()))
            Next

            Return Me.m_TablePanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TablePanel), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TablePanel, Nevron.Nov.UI.NTableFlowPanel.EnabledProperty, Nevron.Nov.UI.NTableFlowPanel.HorizontalPlacementProperty, Nevron.Nov.UI.NTableFlowPanel.VerticalPlacementProperty, Nevron.Nov.UI.NTableFlowPanel.DirectionProperty, Nevron.Nov.UI.NTableFlowPanel.VerticalSpacingProperty, Nevron.Nov.UI.NTableFlowPanel.HorizontalSpacingProperty, Nevron.Nov.UI.NTableFlowPanel.MaxOrdinalProperty, Nevron.Nov.UI.NTableFlowPanel.RowFillModeProperty, Nevron.Nov.UI.NTableFlowPanel.RowFitModeProperty, Nevron.Nov.UI.NTableFlowPanel.ColFillModeProperty, Nevron.Nov.UI.NTableFlowPanel.ColFitModeProperty, Nevron.Nov.UI.NTableFlowPanel.InvertedProperty, Nevron.Nov.UI.NTableFlowPanel.UniformWidthsProperty, Nevron.Nov.UI.NTableFlowPanel.UniformHeightsProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
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
	This example demonstrates how to create a table flow layout panel and add
	widgets to it. You can control the parameters of the layout algorithm
	using the controls to the right.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddSmallItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Small." & Me.m_TablePanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(5, 5)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(25, 25)
            Me.m_TablePanel.Add(item)
        End Sub

        Private Sub OnAddLargeItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Large." & Me.m_TablePanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(20, 20)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(60, 60)
            Me.m_TablePanel.Add(item)
        End Sub

        Private Sub OnAddRandomItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim range As Integer = 30
            Dim rnd As System.Random = New System.Random()
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random." & Me.m_TablePanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range), rnd.[Next](range))
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range) + range, rnd.[Next](range) + range)
            Me.m_TablePanel.Add(item)
        End Sub

        Private Sub OnRemoveAllItemsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_TablePanel.Clear()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TablePanel As Nevron.Nov.UI.NTableFlowPanel

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTableFlowPanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
