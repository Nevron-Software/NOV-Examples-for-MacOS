Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.UI
    Public Class NNavigationBarFirstLookExample
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
            Nevron.Nov.Examples.UI.NNavigationBarFirstLookExample.NNavigationBarFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NNavigationBarFirstLookExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' Create a list box
            Me.m_NavigationBar = New Nevron.Nov.UI.NNavigationBar()
            Me.m_NavigationBar.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_NavigationBar.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit

            ' create the Outlook Bar Panes
            CreateNavigationBarPane("Mail", "Mail", NResources.Image__24x24_Mail_png, NResources.Image__16x16_Mail_png, New Nevron.Nov.UI.NLabel("Mail Content"))
            CreateNavigationBarPane("Calendar", "Calendar", NResources.Image__24x24_Calendar_png, NResources.Image__16x16_Calendar_png, New Nevron.Nov.UI.NLabel("Calendar Content"))
            CreateNavigationBarPane("Contacts", "Contacts", NResources.Image__24x24_Contacts_png, NResources.Image__16x16_Contacts_png, New Nevron.Nov.UI.NLabel("Contacts Content"))
            CreateNavigationBarPane("Tasks", "Tasks", NResources.Image__24x24_Tasks_png, NResources.Image__16x16_Tasks_png, New Nevron.Nov.UI.NLabel("Tasks Content"))
            CreateNavigationBarPane("Notes", "Notes", NResources.Image__24x24_Notes_png, NResources.Image__16x16_Notes_png, New Nevron.Nov.UI.NLabel("Notes Content"))
            CreateNavigationBarPane("Folders", "Folders", NResources.Image__24x24_Folders_png, NResources.Image__16x16_Folders_png, New Nevron.Nov.UI.NLabel("Folders Content"))
            CreateNavigationBarPane("Shortcuts", "Shortcuts", NResources.Image__24x24_Shortcuts_png, NResources.Image__16x16_Shortcuts_png, New Nevron.Nov.UI.NLabel("Shortcuts Content"))

            ' Hook to list box selection events
            AddHandler Me.m_NavigationBar.SelectedIndexChanged, AddressOf Me.OnNavigationBarSelectedIndexChanged
            Return Me.m_NavigationBar
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            ' Create the properties group box
            stack.Add(Me.CreatePropertiesGroupBox())

            ' Create the events log
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a simple NavigationBar and populate it with items. You can use the controls
	on the right to modify various properties of the NavigationBar.
</p>
"
        End Function

        Private Function CreateNavigationBarPane(ByVal title As String, ByVal tooltip As String, ByVal largeImage As Nevron.Nov.Graphics.NImage, ByVal smallImage As Nevron.Nov.Graphics.NImage, ByVal content As Nevron.Nov.UI.NWidget) As Nevron.Nov.UI.NNavigationBarPane
            Dim pane As Nevron.Nov.UI.NNavigationBarPane = New Nevron.Nov.UI.NNavigationBarPane()

            ' set pane content
            pane.Content = content
            pane.Image = CType(smallImage.DeepClone(), Nevron.Nov.Graphics.NImage)
            pane.Text = title

            ' set header content
            Dim titleLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(title)
            titleLabel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit
            Dim headerContent As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(largeImage, titleLabel)
            headerContent.Spacing = 2
            pane.Header.Content = headerContent
            pane.Header.Tooltip = New Nevron.Nov.UI.NTooltip(tooltip)

            ' set icon content
            pane.Icon.Content = New Nevron.Nov.UI.NImageBox(smallImage)
            pane.Icon.Tooltip = New Nevron.Nov.UI.NTooltip(tooltip)

            ' add the pane
            Me.m_NavigationBar.Panes.Add(pane)
            Return pane
        End Function

        Private Sub OnNavigationBarSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected Index " & Me.m_NavigationBar.SelectedIndex)
        End Sub

        #EndRegion

        #Region"Implementation"

        Private Function CreatePropertiesGroupBox() As Nevron.Nov.UI.NGroupBox
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_NavigationBar), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_NavigationBar, Nevron.Nov.UI.NNavigationBar.EnabledProperty, Nevron.Nov.UI.NNavigationBar.HorizontalPlacementProperty, Nevron.Nov.UI.NNavigationBar.VerticalPlacementProperty, Nevron.Nov.UI.NNavigationBar.VisibleHeadersCountProperty, Nevron.Nov.UI.NNavigationBar.HeadersPaddingProperty, Nevron.Nov.UI.NNavigationBar.HeadersSpacingProperty, Nevron.Nov.UI.NNavigationBar.IconsPaddingProperty, Nevron.Nov.UI.NNavigationBar.IconsSpacingProperty)
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                propertiesStack.Add(editors(i))
                i += 1
            End While

            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            Return propertiesGroupBox
        End Function

        #EndRegion

        #Region"Event Handlers"

        #EndRegion

        #Region"Fields"

        Private m_NavigationBar As Nevron.Nov.UI.NNavigationBar
        Private m_EventsLog As NExampleEventsLog

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NNavigationBarFirstLookExample.
        ''' </summary>
        Public Shared ReadOnly NNavigationBarFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
