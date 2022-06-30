Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTopLevelWindowEventsExample
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
            Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.NTopLevelWindowEventsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChildWindowIndex = 1

			' Create the example's content
			Dim openWindowButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open Window...")
            openWindowButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            openWindowButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler openWindowButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenChildWindowButtonClicked)
            Return openWindowButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First

			' Create the opened windows tree view
			Me.m_TreeView = New Nevron.Nov.UI.NTreeView()
            AddHandler Me.m_TreeView.SelectedPathChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTreeViewSelectedPathChanged)
            stack.Add(Me.m_TreeView)

			' create some command buttons
			Me.m_ButtonsStack = New Nevron.Nov.UI.NStackPanel()
            Me.m_ButtonsStack.HorizontalSpacing = 3
            Me.m_ButtonsStack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            Me.m_ButtonsStack.Add(New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.ActivateButtonText))
            Me.m_ButtonsStack.Add(New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.FocusButtonText))
            Me.m_ButtonsStack.Add(New Nevron.Nov.UI.NButton(Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.CloseButtonText))

			' capture the button click for the buttons at stack panel level
			Me.m_ButtonsStack.AddEventHandler(Nevron.Nov.UI.NButton.ClickEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NEventArgs)(New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowActionButtonClicked)))
            Me.m_ButtonsStack.Enabled = False
            stack.Add(Me.m_ButtonsStack)
            Dim openedWindowsGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Opened Windows", stack)

			' Add the events log
			Me.m_EventsLog = New NExampleEventsLog()

			' Add the opened windows group box and the events log to a splitter
			Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter(openedWindowsGroupBox, Me.m_EventsLog, Nevron.Nov.UI.ENSplitterSplitMode.OffsetFromNearSide, 250)
            splitter.Orientation = Nevron.Nov.Layout.ENHVOrientation.Vertical
            Return splitter
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and open and manage top level windows. The events that
	occur during the lifetime of each window are logged and displayed in a list box on the right.
</p>
"
        End Function

        Protected Friend Overrides Sub OnClosing()
            MyBase.OnClosing()

			' Loop through the tree view items to close all opened windows
			Dim items As Nevron.Nov.UI.NTreeViewItemCollection = Me.m_TreeView.Items

            For i As Integer = items.Count - 1 To 0 Step -1
                CType(items(CInt((i))).Tag, Nevron.Nov.UI.NTopLevelWindow).Close()
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		Private Function CreateOpenChildWindowButton() As Nevron.Nov.UI.NButton
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Open Child Window...")
            button.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            button.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Bottom
            AddHandler button.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnOpenChildWindowButtonClicked)
            Return button
        End Function
		''' <summary>
		''' Creates and opens a child window of the specified owner window.
		''' </summary>
		''' <paramname="ownerWindow"></param>
		Private Sub OpenChildWindow(ByVal ownerWindow As Nevron.Nov.UI.NWindow)
			' Create the window
			Dim window As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(ownerWindow)
            window.Title = "Window " & System.Math.Min(System.Threading.Interlocked.Increment(Me.m_ChildWindowIndex), Me.m_ChildWindowIndex - 1)
            window.PreferredSize = Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.WindowSize

			' subscribe for window state events
			AddHandler window.Opened, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowStateEvent)
            AddHandler window.Activated, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowStateEvent)
            AddHandler window.Deactivated, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowStateEvent)
            AddHandler window.Closing, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowStateEvent)
            AddHandler window.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnWindowStateEvent)

			' subscribe for window UI events
			AddHandler window.GotFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnWindowUIEvent)
            AddHandler window.LostFocus, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NFocusChangeEventArgs)(AddressOf Me.OnWindowUIEvent)

			' Create its content
			Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.First
            Dim ownerName As String = If(TypeOf ownerWindow Is Nevron.Nov.UI.NTopLevelWindow, CType(ownerWindow, Nevron.Nov.UI.NTopLevelWindow).Title, "Examples Window")
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel("Child Of """ & ownerName & """")
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            stack.Add(label)
            stack.Add(Me.CreateOpenChildWindowButton())
            window.Content = stack

			' Open the window
			Me.AddTreeViewItemForWindow(window)
            window.Open()

            If TypeOf ownerWindow Is Nevron.Nov.UI.NTopLevelWindow Then
                window.X = ownerWindow.X + 25
                window.Y = ownerWindow.Y + 25
            End If
        End Sub

        Private Sub AddTreeViewItemForWindow(ByVal window As Nevron.Nov.UI.NTopLevelWindow)
            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(window.Title)
            item.Tag = window
            window.Tag = item
            Dim parentWindow As Nevron.Nov.UI.NTopLevelWindow = TryCast(window.ParentWindow, Nevron.Nov.UI.NTopLevelWindow)

            If parentWindow Is Nothing Then
                Me.m_TreeView.Items.Add(item)
            Else
                Dim parentItem As Nevron.Nov.UI.NTreeViewItem = CType(parentWindow.Tag, Nevron.Nov.UI.NTreeViewItem)
                parentItem.Items.Add(item)
                parentItem.Expanded = True
            End If
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnOpenChildWindowButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim button As Nevron.Nov.UI.NButton = CType(args.TargetNode, Nevron.Nov.UI.NButton)
            Me.OpenChildWindow(button.DisplayWindow)
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnTreeViewSelectedPathChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim treeView As Nevron.Nov.UI.NTreeView = CType(args.TargetNode, Nevron.Nov.UI.NTreeView)
            Me.m_ButtonsStack.Enabled = treeView.SelectedItem IsNot Nothing
        End Sub
		''' <summary>
		''' Event handler for window state events (Opened, Activated, Deactivated, Closing, Closed)
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnWindowStateEvent(ByVal args As Nevron.Nov.Dom.NEventArgs)
            If args.EventPhase <> Nevron.Nov.Dom.ENEventPhase.AtTarget Then Return
            Dim window As Nevron.Nov.UI.NTopLevelWindow = CType(args.CurrentTargetNode, Nevron.Nov.UI.NTopLevelWindow)
            Dim eventName As String = args.[Event].Name
            Me.m_EventsLog.LogEvent(window.Title & " " & eventName.Substring(eventName.LastIndexOf("."c) + 1))

            If args.[Event] Is Nevron.Nov.UI.NTopLevelWindow.ActivatedEvent Then
				' Select the corresponding item from the tree view
				Dim item As Nevron.Nov.UI.NTreeViewItem = CType(window.Tag, Nevron.Nov.UI.NTreeViewItem)
                Me.m_TreeView.SelectedItem = item
            ElseIf args.[Event] Is Nevron.Nov.UI.NTopLevelWindow.ClosedEvent Then
				' Remove the corresponding item from the tree view
				Dim item As Nevron.Nov.UI.NTreeViewItem = CType(window.Tag, Nevron.Nov.UI.NTreeViewItem)
                Dim items As Nevron.Nov.UI.NTreeViewItemCollection = CType(item.ParentNode, Nevron.Nov.UI.NTreeViewItemCollection)
                items.Remove(item)
            End If
        End Sub
		''' <summary>
		''' 
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnWindowUIEvent(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim window As Nevron.Nov.UI.NTopLevelWindow = CType(args.CurrentTargetNode, Nevron.Nov.UI.NTopLevelWindow)
            Dim eventName As String = args.[Event].Name
            eventName = eventName.Substring(eventName.LastIndexOf("."c) + 1)
            Me.m_EventsLog.LogEvent(window.Title & " " & eventName & " from target: " & args.TargetNode.[GetType]().Name)
        End Sub
		''' <summary>
		''' Called when some of the window action buttons has been clicked.
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnWindowActionButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            If Me.m_TreeView.SelectedItem Is Nothing Then Return
            Dim window As Nevron.Nov.UI.NTopLevelWindow = TryCast(Me.m_TreeView.SelectedItem.Tag, Nevron.Nov.UI.NTopLevelWindow)
            If window Is Nothing Then Return
            Dim button As Nevron.Nov.UI.NButton = CType(args.TargetNode, Nevron.Nov.UI.NButton)
            Dim label As Nevron.Nov.UI.NLabel = CType(button.Content, Nevron.Nov.UI.NLabel)

            Select Case label.Text
                Case Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.ActivateButtonText
                    window.Activate()
                Case Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.FocusButtonText
                    window.Focus()
                Case Nevron.Nov.Examples.UI.NTopLevelWindowEventsExample.CloseButtonText
                    window.Close()
            End Select
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChildWindowIndex As Integer
        Private m_TreeView As Nevron.Nov.UI.NTreeView
        Private m_ButtonsStack As Nevron.Nov.UI.NStackPanel
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NTopLevelWindowEventsExample.
		''' </summary>
		Public Shared ReadOnly NTopLevelWindowEventsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly WindowSize As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(300, 300)
        Private Const ActivateButtonText As String = "Activate"
        Private Const FocusButtonText As String = "Focus"
        Private Const CloseButtonText As String = "Close"

		#EndRegion
	End Class
End Namespace
