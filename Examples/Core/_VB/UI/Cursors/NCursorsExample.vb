Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NCursorsExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NCursorsExample.NCursorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NCursorsExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim predefinedCursors As System.Array = Nevron.Nov.NEnum.GetValues(GetType(Nevron.Nov.UI.ENPredefinedCursor))
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim predefinedGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Predefined")
            stack.Add(predefinedGroupBox)
            Dim splitter As Nevron.Nov.UI.NSplitter = New Nevron.Nov.UI.NSplitter()
            splitter.SplitMode = Nevron.Nov.UI.ENSplitterSplitMode.Proportional
            splitter.SplitFactor = 0.5R
            predefinedGroupBox.Content = splitter

            For i As Integer = 0 To 2 - 1
                Dim pstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                pstack.VerticalSpacing = 1

                Select Case i
                    Case 0
                        splitter.Pane1.Content = New Nevron.Nov.UI.NGroupBox("Use Native If Possible", pstack)
                    Case 1
                        splitter.Pane2.Content = New Nevron.Nov.UI.NGroupBox("Use Built-In", pstack)
                    Case Else
                        Throw New System.Exception("More cases?")
                End Select

                For j As Integer = 0 To predefinedCursors.Length - 1
                    Dim predefinedCursor As Nevron.Nov.UI.ENPredefinedCursor = CType(predefinedCursors.GetValue(j), Nevron.Nov.UI.ENPredefinedCursor)
                    Dim element As Nevron.Nov.UI.NWidget = Me.CreateDemoElement(Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(predefinedCursor.ToString()))
                    element.Cursor = New Nevron.Nov.UI.NCursor(predefinedCursor, i = 0)
                    pstack.Add(element)
                Next
            Next

            Dim customElement As Nevron.Nov.UI.NWidget = Me.CreateDemoElement("Custom")
            customElement.Cursor = NResources.Cursor_CustomCursor_cur
            Dim customGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Custom", customElement)
            stack.Add(customGroupBox)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example shows how to use cursors. NOV supports 2 types of cursors - <b>predefined</b> and <b>custom</b>. Predefined cursors can
	be native or built-in. A custom cursor can be loaded from a stream or you can manually specify the values of its pixels.
</p>
"
        End Function
		#EndRegion

		#Region"Implementation"

		Private Function CreateDemoElement(ByVal text As String) As Nevron.Nov.UI.NWidget
            Dim element As Nevron.Nov.UI.NContentHolder = New Nevron.Nov.UI.NContentHolder(text)
            element.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black, 2, 5)
            element.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            element.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.PapayaWhip)
            element.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Black)
            element.Padding = New Nevron.Nov.Graphics.NMargins(1)
            Return element
        End Function

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCursorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
