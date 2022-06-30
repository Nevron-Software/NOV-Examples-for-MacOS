Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NDockPanelExample
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
            Nevron.Nov.Examples.UI.NDockPanelExample.NDockPanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NDockPanelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a dock panel with red border
			Me.m_DockPanel = New Nevron.Nov.UI.NDockPanel()
            Me.m_DockPanel.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_DockPanel.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)

			' Create and dock several widgets
			Dim widget As Nevron.Nov.UI.NWidget = Me.CreateDockedWidget(Nevron.Nov.Layout.ENDockArea.Left)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(100, 100)
            Me.m_DockPanel.Add(widget)
            widget = Me.CreateDockedWidget(Nevron.Nov.Layout.ENDockArea.Top)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(100, 100)
            Me.m_DockPanel.Add(widget)
            widget = Me.CreateDockedWidget(Nevron.Nov.Layout.ENDockArea.Right)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(100, 100)
            Me.m_DockPanel.Add(widget)
            widget = Me.CreateDockedWidget(Nevron.Nov.Layout.ENDockArea.Bottom)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(100, 100)
            Me.m_DockPanel.Add(widget)
            widget = Me.CreateDockedWidget(Nevron.Nov.Layout.ENDockArea.Center)
            widget.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 300)
            Me.m_DockPanel.Add(widget)
            Return Me.m_DockPanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' properties stack
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_DockPanel), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_DockPanel, Nevron.Nov.UI.NDockPanel.EnabledProperty, Nevron.Nov.UI.NDockPanel.HorizontalPlacementProperty, Nevron.Nov.UI.NDockPanel.VerticalPlacementProperty, Nevron.Nov.UI.NDockPanel.VerticalSpacingProperty, Nevron.Nov.UI.NDockPanel.HorizontalSpacingProperty, Nevron.Nov.UI.NDockPanel.UniformWidthsProperty, Nevron.Nov.UI.NDockPanel.UniformHeightsProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' items stack
			Dim itemsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_DockAreaCombo = New Nevron.Nov.UI.NComboBox()
            Me.m_DockAreaCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Left"))
            Me.m_DockAreaCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Top"))
            Me.m_DockAreaCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Right"))
            Me.m_DockAreaCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Bottom"))
            Me.m_DockAreaCombo.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Center"))
            Me.m_DockAreaCombo.SelectedIndex = 1
            Dim dockAreaLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Dock Area:")
            dockAreaLabel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            itemsStack.Add(New Nevron.Nov.UI.NPairBox(dockAreaLabel, Me.m_DockAreaCombo, True))
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
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a dock layout panel and add
	widgets to it specifying the dock area each widget should be placed in.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Gets the currently selected dock area from the DockAreaCombo.
		''' </summary>
		''' <returns></returns>
		Private Function GetCurrentDockArea() As Nevron.Nov.Layout.ENDockArea
            Select Case Me.m_DockAreaCombo.SelectedIndex
                Case 0
                    Return Nevron.Nov.Layout.ENDockArea.Left
                Case 1
                    Return Nevron.Nov.Layout.ENDockArea.Top
                Case 2
                    Return Nevron.Nov.Layout.ENDockArea.Right
                Case 3
                    Return Nevron.Nov.Layout.ENDockArea.Bottom
                Case 4
                    Return Nevron.Nov.Layout.ENDockArea.Center
                Case Else
                    Return Nevron.Nov.Layout.ENDockArea.Top
            End Select
        End Function
		''' <summary>
		''' Sets the currently selected dock area to the specified widget.
		''' </summary>
		''' <paramname="widget"></param>
		Private Sub SetCurrentDockArea(ByVal widget As Nevron.Nov.UI.NWidget)
            Me.SetDockArea(widget, Me.GetCurrentDockArea())
        End Sub
		''' <summary>
		''' Sets a custom dock area to the specified widget and colors its background accordingly
		''' </summary>
		''' <paramname="widget"></param>
		''' <paramname="dockArea"></param>
		Private Sub SetDockArea(ByVal widget As Nevron.Nov.UI.NWidget, ByVal dockArea As Nevron.Nov.Layout.ENDockArea)
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(widget, dockArea)

            Select Case dockArea
                Case Nevron.Nov.Layout.ENDockArea.Bottom
                    widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(0, 162, 232))
                Case Nevron.Nov.Layout.ENDockArea.Center
                    widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(239, 228, 176))
                Case Nevron.Nov.Layout.ENDockArea.Left
                    widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(34, 177, 76))
                Case Nevron.Nov.Layout.ENDockArea.Right
                    widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(163, 73, 164))
                Case Nevron.Nov.Layout.ENDockArea.Top
                    widget.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(237, 28, 36))
                Case Else
                    Throw New System.Exception("New ENDockArea?")
            End Select
        End Sub

		''' <summary>
		''' Creates the default docked widget for this example, that is docked to the current dock area.
		''' </summary>
		''' <returns></returns>
		Private Function CreateDockedWidget() As Nevron.Nov.UI.NWidget
            Return Me.CreateDockedWidget(Me.GetCurrentDockArea())
        End Function
		''' <summary>
		''' Creates the default docked widget for this example, that is docked to the specified area.
		''' </summary>
		''' <paramname="dockArea"></param>
		''' <returns></returns>
		Private Function CreateDockedWidget(ByVal dockArea As Nevron.Nov.Layout.ENDockArea) As Nevron.Nov.UI.NWidget
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(dockArea.ToString() & "(" & Me.m_DockPanel.Count.ToString() & ")")
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim widget As Nevron.Nov.UI.NWidget = New Nevron.Nov.UI.NContentHolder(label)
            widget.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            widget.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.SetDockArea(widget, dockArea)
            Return widget
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAddSmallItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NWidget = Me.CreateDockedWidget()
            item.MinSize = New Nevron.Nov.Graphics.NSize(20, 20)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(60, 60)
            Me.m_DockPanel.Add(item)
        End Sub

        Private Sub OnAddLargeItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim item As Nevron.Nov.UI.NWidget = Me.CreateDockedWidget()
            item.MinSize = New Nevron.Nov.Graphics.NSize(40, 40)
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(100, 100)
            Me.m_DockPanel.Add(item)
        End Sub

        Private Sub OnAddRandomItemButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim range As Integer = 50
            Dim rnd As System.Random = New System.Random()
            Dim item As Nevron.Nov.UI.NWidget = Me.CreateDockedWidget()
            item.MinSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range), rnd.[Next](range))
            item.PreferredSize = New Nevron.Nov.Graphics.NSize(rnd.[Next](range) + range, rnd.[Next](range) + range)
            Me.m_DockPanel.Add(item)
        End Sub

        Private Sub OnRemoveAllItemsButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Me.m_DockPanel.Clear()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_DockAreaCombo As Nevron.Nov.UI.NComboBox
        Private m_DockPanel As Nevron.Nov.UI.NDockPanel

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NDockPanelExample.
		''' </summary>
		Public Shared ReadOnly NDockPanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
