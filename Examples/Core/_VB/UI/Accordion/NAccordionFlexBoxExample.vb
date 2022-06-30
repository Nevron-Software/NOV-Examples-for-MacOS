Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NAccordionFlexBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NAccordionFlexBoxExample.NAccordionFlexBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NAccordionFlexBoxExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' Create an accordion
			Me.m_Accordion = New Nevron.Nov.UI.NAccordion()
            Me.m_Accordion.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_Accordion.MinWidth = 300
            Me.m_Accordion.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_Accordion.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)

            ' Create an accordion flex box panel to hold the expandable sections of the accordion.
            ' Note that the accordion is designed like a radio button group, allowing the user to use any layout 
            ' to arrange the expandable sections managed by the accordion.
            Dim panel As Nevron.Nov.UI.NAccordionFlexBoxPanel = New Nevron.Nov.UI.NAccordionFlexBoxPanel()
            Me.m_Accordion.Content = panel

            ' Create the sections
            panel.Add(CreateAccordionSection(NResources.Image__16x16_Mail_png, "Mail", Me.CreateMailTreeView(), False))
            panel.Add(CreateAccordionSection(NResources.Image__16x16_Calendar_png, "Calendar", Me.CreateCalendar(), True))
            panel.Add(CreateAccordionSection(NResources.Image__16x16_Contacts_png, "Contacts", Me.CreateContactsTreeView(), False))
            panel.Add(CreateAccordionSection(NResources.Image__16x16_Tasks_png, "Tasks", Me.CreateTasksView(), False))
            Return Me.m_Accordion
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' properties
			Dim properties As Nevron.Nov.Dom.NProperty() = New Nevron.Nov.Dom.NProperty() {Nevron.Nov.UI.NAccordion.ShowSymbolProperty}
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_Accordion), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_Accordion, properties)

            For i As Integer = 0 To editors.Count - 1
                stack.Add(editors(i))
            Next

			' create the events list box
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how make the expanded section of an Accordion widget to occupy the whole available area.
    To do that you should set the <b>Content</b> property of the accordion to an <b>NAccordionFlexBoxPanel</b> instance
    and then add expandable sections to that accordion flex box panel. Red border is set to the accordion, so that you
    can see its bounds.
</p>
"
        End Function

		#EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates an accordion section.
        ''' </summary>
        ''' <paramname="image"></param>
        ''' <paramname="text"></param>
        ''' <paramname="content"></param>
        ''' <paramname="expanded"></param>
        ''' <returns></returns>
        Private Function CreateAccordionSection(ByVal image As Nevron.Nov.Graphics.NImage, ByVal text As String, ByVal content As Nevron.Nov.UI.NWidget, ByVal expanded As Boolean) As Nevron.Nov.UI.NExpandableSection
            Dim header As Nevron.Nov.UI.NPairBox = Nevron.Nov.UI.NPairBox.Create(image, text)
            Dim section As Nevron.Nov.UI.NExpandableSection = New Nevron.Nov.UI.NExpandableSection(header, content)
            section.Expanded = expanded
            Return section
        End Function

        ''' <summary>
        ''' Creates a dummy tree view, that contains the items of an imaginary Mail
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateMailTreeView() As Nevron.Nov.UI.NTreeView
            Dim treeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()
            Dim rootItem As Nevron.Nov.UI.NTreeViewItem = CreateTreeViewItem("Personal Folers", NResources.Image__16x16_folderHome_png)
            treeView.Items.Add(rootItem)
            Dim texts As String() = New String() {"Deleted Items", "Drafts", "Inbox", "Junk E-mails", "Outbox", "RSS Feeds", "Sent Items", "Search Folders"}
            Dim icons As Nevron.Nov.Graphics.NImage() = New Nevron.Nov.Graphics.NImage() {NResources.Image__16x16_folderDeleted_png, NResources.Image__16x16_folderDrafts_png, NResources.Image__16x16_folderInbox_png, NResources.Image__16x16_folderJunk_png, NResources.Image__16x16_folderOutbox_png, NResources.Image__16x16_folderRss_png, NResources.Image__16x16_folderSent_png, NResources.Image__16x16_folderSearch_png}

            For i As Integer = 0 To texts.Length - 1
                rootItem.Items.Add(Me.CreateTreeViewItem(texts(i), icons(i)))
            Next

            treeView.ExpandAll(True)
            treeView.BorderThickness = New Nevron.Nov.Graphics.NMargins(0)
            Return treeView
        End Function
        ''' <summary>
        ''' Creates the Contacts tree view
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateContactsTreeView() As Nevron.Nov.UI.NTreeView
            Dim names As String() = New String() {"Emily Swan", "John Smith", "Lindsay Collier", "Kevin Johnson", "Shannon Flynn"}
            Return SetupTreeView(names, NResources.Image__16x16_Contacts_png)
        End Function
        ''' <summary>
        ''' Creates a dummy callendar for the Calendar section.
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateCalendar() As Nevron.Nov.UI.NCalendar
            Dim calendar As Nevron.Nov.UI.NCalendar = New Nevron.Nov.UI.NCalendar()
            calendar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            calendar.Margins = New Nevron.Nov.Graphics.NMargins(10)
            calendar.BorderThickness = New Nevron.Nov.Graphics.NMargins(0)
            Return calendar
        End Function
        ''' <summary>
        ''' Creates the Tasks tree view
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateTasksView() As Nevron.Nov.UI.NTreeView
            Dim tasks As String() = New String() {"Meet John", "Montly report", "Pickup kids", "Make backup"}
            Return SetupTreeView(tasks, NResources.Image__16x16_Tasks_png)
        End Function

        Private Function SetupTreeView(ByVal texts As String(), ByVal image As Nevron.Nov.Graphics.NImage) As Nevron.Nov.UI.NTreeView
            Dim treeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()
            treeView.BorderThickness = New Nevron.Nov.Graphics.NMargins(0)

            For i As Integer = 0 To texts.Length - 1
                treeView.Items.Add(Me.CreateTreeViewItem(texts(i), CType(image.DeepClone(), Nevron.Nov.Graphics.NImage)))
            Next

            Return treeView
        End Function
        ''' <summary>
        ''' Creates a tree view item.
        ''' </summary>
        ''' <paramname="text"></param>
        ''' <paramname="icon"></param>
        ''' <returns></returns>
        Private Function CreateTreeViewItem(ByVal text As String, ByVal icon As Nevron.Nov.Graphics.NImage) As Nevron.Nov.UI.NTreeViewItem
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.HorizontalSpacing = 3

            If icon IsNot Nothing Then
                Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(icon)
                imageBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                imageBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                stack.Add(imageBox)
            End If

            If Not String.IsNullOrEmpty(text) Then
                Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
                label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
                stack.Add(label)
            End If

            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(stack)
            item.Margins = New Nevron.Nov.Graphics.NMargins(0, 5)
            Return item
        End Function

        #EndRegion

        #Region"Fields"

        Private m_EventsLog As NExampleEventsLog
        Private m_Accordion As Nevron.Nov.UI.NAccordion

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAccordionFlexBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
