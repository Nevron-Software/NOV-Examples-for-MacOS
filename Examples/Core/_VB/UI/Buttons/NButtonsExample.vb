Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NButtonsExample
        Inherits NExampleBase
        #Region"Constructors"

        Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NButtonsExample.NButtonsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NButtonsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create the host
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.VerticalSpacing = 5

            ' text only push button
            Dim textOnlyButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Text only button")
            AddHandler textOnlyButton.Click, AddressOf Me.OnButtonClicked
            stack.Add(textOnlyButton)

            ' image only push button
            Dim imageOnlyButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(NResources.Image__16x16_Contacts_png)
            AddHandler imageOnlyButton.Click, AddressOf Me.OnButtonClicked
            stack.Add(imageOnlyButton)

            ' image and text button
            Dim image2 As Nevron.Nov.Graphics.NImage = NResources.Image__16x16_Mail_png
            Dim imageAndTextButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(New Nevron.Nov.UI.NPairBox("Image before text", image2, Nevron.Nov.UI.ENPairBoxRelation.Box2BeforeBox1, False))
            AddHandler imageAndTextButton.Click, AddressOf Me.OnButtonClicked
            stack.Add(imageAndTextButton)

            ' repeat button
            Dim repeatButton As Nevron.Nov.UI.NRepeatButton = New Nevron.Nov.UI.NRepeatButton("Repeat button")
            AddHandler repeatButton.StartClicking, AddressOf Me.OnRepeatButtonStartClicking
            AddHandler repeatButton.Click, AddressOf Me.OnButtonClicked
            AddHandler repeatButton.EndClicking, AddressOf Me.OnRepeatButtonEndClicking
            stack.Add(repeatButton)

            ' toggle button
            Dim toggleButton As Nevron.Nov.UI.NToggleButton = New Nevron.Nov.UI.NToggleButton("Toggle button")
            AddHandler toggleButton.Click, AddressOf Me.OnButtonClicked
            stack.Add(toggleButton)

            ' disabled button
            Dim disabledButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Disabled Button")
            disabledButton.Enabled = False
            stack.Add(disabledButton)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

            ' create the events list box
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates the different types of buttons supported by NOV.
</p>
"
        End Function

        #EndRegion

        #Region"Event Handlers"

        Private Sub OnButtonClicked(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.m_EventsLog.LogEvent("Button clicked")
        End Sub

        Private Sub OnRepeatButtonStartClicking(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.m_EventsLog.LogEvent("Repeat Button Start Clicking")
        End Sub

        Private Sub OnRepeatButtonEndClicking(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Me.m_EventsLog.LogEvent("Repeat Button End Clicking")
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_EventsLog As NExampleEventsLog

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NButtonsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
