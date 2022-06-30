Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NDragAndDropExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NDragAndDropExample.NDragAndDropExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NDragAndDropExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' sources
			Dim sourcesGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Drag Drop Sources")

            If True Then
                Dim sourcesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                sourcesGroup.Content = sourcesStack
                Dim textSource1 As Nevron.Nov.UI.NContentHolder = Me.CreateDemoElement("Drag Source Text 1")
                Dim dataObject1 As Nevron.Nov.UI.NDataObject = New Nevron.Nov.UI.NDataObject()
                dataObject1.SetData(Nevron.Nov.UI.NDataFormat.TextFormatString, "Text string 1")
                textSource1.Tag = dataObject1
                sourcesStack.Add(textSource1)
                AddHandler textSource1.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnSourceMouseDown)
                Dim textSource2 As Nevron.Nov.UI.NContentHolder = Me.CreateDemoElement("Drag Source Text 2")
                Dim dataObject2 As Nevron.Nov.UI.NDataObject = New Nevron.Nov.UI.NDataObject()
                dataObject2.SetData(Nevron.Nov.UI.NDataFormat.TextFormatString, "Text string 2")
                textSource2.Tag = dataObject2
                sourcesStack.Add(textSource2)
                AddHandler textSource2.MouseDown, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NMouseButtonEventArgs)(AddressOf Me.OnSourceMouseDown)
            End If

			' targets
			Dim targetsGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Drop Targets")

            If True Then
                Dim targetsStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                targetsGroup.Content = targetsStack
                Dim dropTextTarget As Nevron.Nov.UI.NContentHolder = Me.CreateDemoElement("Drop Text On Me")
                targetsStack.Add(dropTextTarget)
                AddHandler dropTextTarget.DragOver, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragActionEventArgs)(AddressOf Me.OnDragOverTextTarget)
                AddHandler dropTextTarget.DragDrop, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragActionEventArgs)(AddressOf Me.OnDragDropTextTarget)
            End If

			' create the source and targets splitter
			Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5R
            splitter.Pane1.Content = sourcesGroup
            splitter.Pane2.Content = targetsGroup

			' create the inspector on the bottom
			Dim inspectorGroup As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Data Object Ispector")
            Dim inspector As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            inspectorGroup.Content = inspector
            AddHandler inspector.DragEnter, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragOverChangeEventArgs)(AddressOf Me.OnInspectorDragEnter)
            AddHandler inspector.DragLeave, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragOverChangeEventArgs)(AddressOf Me.OnInspectorDragLeave)
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.Add(splitter)
            stack.Add(inspectorGroup)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim tab As Nevron.Nov.UI.NTab = New Nevron.Nov.UI.NTab()
            Me.m_SourceEventsLog = New NExampleEventsLog()
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Source Events", Me.m_SourceEventsLog))
            Me.m_TargetEventsLog = New NExampleEventsLog()
            tab.TabPages.Add(New Nevron.Nov.UI.NTabPage("Target Events", Me.m_TargetEventsLog))
            Return tab
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to use Drag and Drop in NOV.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateTextDragDropSources() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Private Function CreateImageDragDropSource() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Private Function CreateDemoElement(ByVal text As String) As Nevron.Nov.UI.NContentHolder
            Dim element As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(text)
            element.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black, 2, 5)
            element.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            element.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)
            element.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            element.Padding = New Nevron.Nov.Graphics.NMargins(1)
            Return element
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSourceMouseDown(ByVal args As Nevron.Nov.UI.NMouseButtonEventArgs)
            If Nevron.Nov.UI.NDragDrop.CanRequestDragDrop() Then
                Dim dataObject As Nevron.Nov.UI.NDataObject = CType(args.CurrentTargetNode.Tag, Nevron.Nov.UI.NDataObject)
                Dim dropSource As Nevron.Nov.UI.NDragDropSource = New Nevron.Nov.UI.NDragDropSource(Nevron.Nov.UI.ENDragDropEffects.All)
                AddHandler dropSource.DragStarting, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragDropSourceEventArgs)(AddressOf Me.OnDropSourceDragStarting)
                AddHandler dropSource.DragEnded, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NDragEndedEventArgs)(AddressOf Me.OnDropSourceDragEnded)
                AddHandler dropSource.QueryDragAction, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NQueryDragActionEventArgs)(AddressOf Me.OnDropSourceQueryDragAction)
                Call Nevron.Nov.UI.NDragDrop.RequestDragDrop(dropSource, dataObject)
            End If
        End Sub

        Private Sub OnDragOverTextTarget(ByVal args As Nevron.Nov.UI.NDragActionEventArgs)
			' first you need to check whether the data object is of interest
			If args.DataObject.ContainsData(Nevron.Nov.UI.NDataFormat.TextFormatString) Then
				' if the Ctrl is pressed, you must typically copy the data if you can.
				If Nevron.Nov.UI.NKeyboard.DefaultCommandPressed And (args.AllowedEffect And Nevron.Nov.UI.ENDragDropEffects.Copy) = Nevron.Nov.UI.ENDragDropEffects.Copy Then
                    args.Effect = Nevron.Nov.UI.ENDragDropEffects.Copy
                    Return
                End If

				' if the Alt is pressed, you must typically link the data if you can.
				If Nevron.Nov.UI.NKeyboard.AltPressed And (args.AllowedEffect And Nevron.Nov.UI.ENDragDropEffects.Link) = Nevron.Nov.UI.ENDragDropEffects.Link Then
                    args.Effect = Nevron.Nov.UI.ENDragDropEffects.Link
                    Return
                End If

				' by default you need to move data if you can
				If (args.AllowedEffect And Nevron.Nov.UI.ENDragDropEffects.Move) <> Nevron.Nov.UI.ENDragDropEffects.None Then
                    args.Effect = Nevron.Nov.UI.ENDragDropEffects.Move
                    Return
                End If
            End If

			' the source either did not publish a data object with a format we are interested in,
			' or we cannot perform the action that is standartly performed for the current keyboard modifies state,
			' or the source did not allow the action that we can perfrom for the current keyboard modifies state.
			' in all these cases we set the effect to none.
			args.Effect = Nevron.Nov.UI.ENDragDropEffects.None
            Return
        End Sub

        Private Sub OnDragDropTextTarget(ByVal args As Nevron.Nov.UI.NDragEventArgs)
            Dim data As Object = args.DataObject.GetData(Nevron.Nov.UI.NDataFormat.TextFormatString)

            If data IsNot Nothing Then
                Dim contentHolder As Nevron.Nov.UI.NContentHolder = TryCast(args.CurrentTargetNode, Nevron.Nov.UI.NContentHolder)
                contentHolder.Content = New Nevron.Nov.UI.NLabel("Dropped Text:{" & CStr(data) & "}. Drop another text...")
            End If
        End Sub

        Private Sub OnInspectorDragEnter(ByVal args As Nevron.Nov.UI.NDragOverChangeEventArgs)
            Dim formats As Nevron.Nov.UI.NDataFormat() = args.DataObject.GetFormats()
            Dim inspector As Nevron.Nov.UI.NListBox = TryCast(args.CurrentTargetNode, Nevron.Nov.UI.NListBox)
            inspector.Items.Clear()

            For i As Integer = 0 To formats.Length - 1
                inspector.Items.Add(New Nevron.Nov.UI.NListBoxItem(formats(CInt((i))).ToString()))
            Next
        End Sub

        Private Sub OnInspectorDragLeave(ByVal args As Nevron.Nov.UI.NDragOverChangeEventArgs)
            Dim inspector As Nevron.Nov.UI.NListBox = TryCast(args.CurrentTargetNode, Nevron.Nov.UI.NListBox)
            inspector.Items.Clear()
        End Sub

        Private Sub OnDropSourceQueryDragAction(ByVal args As Nevron.Nov.UI.NQueryDragActionEventArgs)
            Me.m_SourceEventsLog.LogEvent("QueryDragAction " & args.Reason.ToString())
        End Sub

        Private Sub OnDropSourceDragStarting(ByVal args As Nevron.Nov.UI.NDragDropSourceEventArgs)
            Me.m_SourceEventsLog.LogEvent("DragStarting")
        End Sub

        Private Sub OnDropSourceDragEnded(ByVal args As Nevron.Nov.UI.NDragEndedEventArgs)
            Me.m_SourceEventsLog.LogEvent("DragEnded. Final Effect was: " & args.FinalEffect.ToString())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_SourceEventsLog As NExampleEventsLog
        Private m_TargetEventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDragAndDropExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
