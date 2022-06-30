Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors

Namespace Nevron.Nov.Examples.UI
    Public Class NPageSizeAndOrientationFirstLookExample
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
            Nevron.Nov.Examples.UI.NPageSizeAndOrientationFirstLookExample.NPageSizeAndOrientationFirstLookExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NPageSizeAndOrientationFirstLookExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create the host
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.VerticalSpacing = 5

            ' Page size button
            Me.m_PageSizeDD = New Nevron.Nov.UI.NPageSizeDropDown()
            Me.m_PageSizeDD.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            AddHandler Me.m_PageSizeDD.SelectedPageSizeChanged, AddressOf Me.OnPageSizeDDSelectedPageSizeChanged
            stack.Add(Me.m_PageSizeDD)

            ' Page orientation button
            Me.m_PageOrientationDD = New Nevron.Nov.UI.NPageOrientationDropDown()
            AddHandler Me.m_PageOrientationDD.SelectedPageOrientationChanged, AddressOf Me.OnPageOrientationDDSelectedPageOrientationChanged
            stack.Add(Me.m_PageOrientationDD)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim lookComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            lookComboBox.FillFromEnum(Of Nevron.Nov.UI.ENExtendedLook)()
            AddHandler lookComboBox.SelectedIndexChanged, AddressOf Me.OnLookComboBoxSelectedIndexChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Look:", lookComboBox))

            ' Add the events log
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates the Page Size selection drop down.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPageOrientationDDSelectedPageOrientationChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected page orientation changed to: " & Me.m_PageOrientationDD.SelectedPageOrientation.ToString())
        End Sub

        Private Sub OnPageSizeDDSelectedPageSizeChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Selected page size changed to: " & Me.m_PageSizeDD.SelectedPageSize.ToString())
        End Sub

        Private Sub OnLookComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim look As Nevron.Nov.UI.ENExtendedLook = CType(arg.NewValue, Nevron.Nov.UI.ENExtendedLook)
            Call Nevron.Nov.UI.NStylePropertyEx.SetExtendedLook(Me.m_PageSizeDD, look)
            Call Nevron.Nov.UI.NStylePropertyEx.SetExtendedLook(Me.m_PageOrientationDD, look)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_PageSizeDD As Nevron.Nov.UI.NPageSizeDropDown
        Private m_PageOrientationDD As Nevron.Nov.UI.NPageOrientationDropDown
        Private m_EventsLog As NExampleEventsLog

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NPageSizeAndOrientationFirstLookExample.
        ''' </summary>
        Public Shared ReadOnly NPageSizeAndOrientationFirstLookExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
