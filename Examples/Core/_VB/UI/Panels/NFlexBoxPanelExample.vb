Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports Nevron.Nov.Layout

Namespace Nevron.Nov.Examples.UI
    Public Class NFlexBoxPanelExample
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
            Nevron.Nov.Examples.UI.NFlexBoxPanelExample.NFlexBoxPanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NFlexBoxPanelExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim labelPadding As Nevron.Nov.Graphics.NMargins = New Nevron.Nov.Graphics.NMargins(2)
            Me.m_FlexBoxPanel = New Nevron.Nov.UI.NFlexBoxPanel()
            Me.m_FlexBoxPanel.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_FlexBoxPanel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_FlexBoxPanel.PreferredHeight = 200

			' Set the Grow and Shrink extended properties of the first label explicitly
			Dim label1 As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Label 1 - Grow: 1, Shrink: 1")
            label1.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Gold)
            label1.Padding = labelPadding
            Call Nevron.Nov.Layout.NFlexBoxLayout.SetGrow(label1, 1)
            Call Nevron.Nov.Layout.NFlexBoxLayout.SetShrink(label1, 1)
            Me.m_FlexBoxPanel.Add(label1)

			' Pass the values of the Grow and Shrink extended properties of the second label
			' to the Add method of the panel
			Dim label2 As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Label 2 - Grow: 3, Shrink: 3")
            label2.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            label2.Padding = labelPadding
            Me.m_FlexBoxPanel.Add(label2, 3, 3)

			' The third label will have the default values for Grow and Shrink - 0
			Dim label3 As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Label 3 - Grow: 0, Shrink: 0")
            label3.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Red)
            label3.Padding = labelPadding
            Me.m_FlexBoxPanel.Add(label3)
            Return Me.m_FlexBoxPanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' properties stack
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_FlexBoxPanel), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_FlexBoxPanel, Nevron.Nov.UI.NFlexBoxPanel.EnabledProperty, Nevron.Nov.UI.NFlexBoxPanel.HorizontalPlacementProperty, Nevron.Nov.UI.NFlexBoxPanel.VerticalPlacementProperty, Nevron.Nov.UI.NFlexBoxPanel.DirectionProperty, Nevron.Nov.UI.NFlexBoxPanel.VerticalSpacingProperty, Nevron.Nov.UI.NFlexBoxPanel.HorizontalSpacingProperty, Nevron.Nov.UI.NFlexBoxPanel.UniformWidthsProperty, Nevron.Nov.UI.NFlexBoxPanel.UniformHeightsProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' items stack
			Dim itemsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_GrowUpDown = New Nevron.Nov.UI.NNumericUpDown(0, 100, 0)
            itemsStack.Add(Nevron.Nov.UI.NPairBox.Create("Grow Factor: ", Me.m_GrowUpDown))
            Me.m_ShrinkUpDown = New Nevron.Nov.UI.NNumericUpDown(0, 100, 0)
            itemsStack.Add(Nevron.Nov.UI.NPairBox.Create("Shrink Factor: ", Me.m_ShrinkUpDown))
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
            stack.Add(New Nevron.Nov.UI.NGroupBox("Items", New Nevron.Nov.UI.NUniSizeBoxGroup(itemsStack)))
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a flexbox layout panel and add widgets to it.
	You can control the parameters of the layout algorithm using the controls on the right.
	The Grow and Shrink factors of the widgets determine the portion of area to use for growing/shrinking.
	They are by default set to 0, which means that the widget will not grow or shrink.
</p>
<p>
	To test how the Grow and Shrink factors affect the layout, click the <b>Remove All Items</b> button
	on the right, then set Grow and Shrink factor and click any of the <b>Add ... Item</b> buttons.
	Then update the Grow and Shrink factor if you want, click any of the <b>Add ... Item</b> buttons again and so on.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub SetGrowAndShrink(ByVal widget As Nevron.Nov.UI.NWidget)
            Dim grow As Double = Me.m_GrowUpDown.Value

            If grow <> 0 Then
                Call Nevron.Nov.Layout.NFlexBoxLayout.SetGrow(widget, grow)
            End If

            Dim shrink As Double = Me.m_ShrinkUpDown.Value

            If shrink <> 0 Then
                Call Nevron.Nov.Layout.NFlexBoxLayout.SetShrink(widget, shrink)
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddSmallItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Small " & Me.m_FlexBoxPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(5, 5)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(25, 25)
            Me.SetGrowAndShrink(item)
            Me.m_FlexBoxPanel.Add(item)
        End Sub

        Private Sub OnAddLargeItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Large " & Me.m_FlexBoxPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(20, 20)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(60, 60)
            Me.SetGrowAndShrink(item)
            Me.m_FlexBoxPanel.Add(item)
        End Sub

        Private Sub OnAddRandomItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim range As Integer = 30
            Dim rnd As System.Random = New System.Random()
            Dim item As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Random " & Me.m_FlexBoxPanel.Count.ToString())
            item.MinSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range), rnd.[Next](range))
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range) + range, rnd.[Next](range) + range)
            Me.SetGrowAndShrink(item)
            Me.m_FlexBoxPanel.Add(item)
        End Sub

        Private Sub OnRemoveAllItemsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_FlexBoxPanel.Clear()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_FlexBoxPanel As Nevron.Nov.UI.NFlexBoxPanel
        Private m_GrowUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ShrinkUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NFlexBoxPanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
