Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NPrintDialogExample
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
            Nevron.Nov.Examples.UI.NPrintDialogExample.NPrintDialogExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NPrintDialogExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim printButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Print...")
            printButton.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            printButton.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler printButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPrintButtonClick)
            Return printButton
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

			' print range mode
			Me.m_PrintRangeModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_PrintRangeModeComboBox.FillFromEnum(Of Nevron.Nov.UI.ENPrintRangeMode)()
            Me.m_PrintRangeModeComboBox.SelectedIndex = 0
            stack.Add(New Nevron.Nov.UI.NPairBox("Print Range Mode:", Me.m_PrintRangeModeComboBox, True))

			' enable current page
			Me.m_EnableCurrentPageCheckBox = New Nevron.Nov.UI.NCheckBox()
            stack.Add(New Nevron.Nov.UI.NPairBox("Enable Current Page:", Me.m_EnableCurrentPageCheckBox, True))

			' enable selection
			Me.m_EnableSelectionCheckBox = New Nevron.Nov.UI.NCheckBox()
            stack.Add(New Nevron.Nov.UI.NPairBox("Enable Selection:", Me.m_EnableSelectionCheckBox, True))

			' enable custom page range
			Me.m_EnableCustomPageRangeCheckBox = New Nevron.Nov.UI.NCheckBox()
            stack.Add(New Nevron.Nov.UI.NPairBox("Enable Custom Page Range:", Me.m_EnableCustomPageRangeCheckBox, True))

			' collate
			Me.m_CollateCheckBox = New Nevron.Nov.UI.NCheckBox()
            stack.Add(New Nevron.Nov.UI.NPairBox("Collate:", Me.m_CollateCheckBox, True))

			' number of copies
			Me.m_NumberOfCopiesUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_NumberOfCopiesUpDown.DecimalPlaces = 0
            Me.m_NumberOfCopiesUpDown.[Step] = 1
            Me.m_NumberOfCopiesUpDown.Minimum = 1
            Me.m_NumberOfCopiesUpDown.Maximum = 100
            stack.Add(New Nevron.Nov.UI.NPairBox("Number of Copies:", Me.m_NumberOfCopiesUpDown, True))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
				<p>
					This example demonstrates how to create and use the print dialog provided by NOV.
				</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnPrintButtonClick(ByVal args As Nevron.Nov.Dom.NEventArgs)
            Dim printDocument As Nevron.Nov.UI.NPrintDocument = New Nevron.Nov.UI.NPrintDocument()
            printDocument.DocumentName = "Test Document 1"
            AddHandler printDocument.BeginPrint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NPrintDocument, Nevron.Nov.UI.NBeginPrintEventArgs)(AddressOf Me.OnBeginPrint)
            AddHandler printDocument.PrintPage, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NPrintDocument, Nevron.Nov.UI.NPrintPageEventArgs)(AddressOf Me.OnPrintPage)
            AddHandler printDocument.EndPrint, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NPrintDocument, Nevron.Nov.UI.NEndPrintEventArgs)(AddressOf Me.OnEndPrint)
            Dim printDialog As Nevron.Nov.UI.NPrintDialog = New Nevron.Nov.UI.NPrintDialog()
            printDialog.PrintRangeMode = CType(Me.m_PrintRangeModeComboBox.SelectedItem.Tag, Nevron.Nov.UI.ENPrintRangeMode)
            printDialog.EnableCustomPageRange = Me.m_EnableCustomPageRangeCheckBox.Checked
            printDialog.EnableCurrentPage = Me.m_EnableCurrentPageCheckBox.Checked
            printDialog.EnableSelection = Me.m_EnableSelectionCheckBox.Checked
            printDialog.CustomPageRange = New Nevron.Nov.Graphics.NRangeI(1, 100)
            printDialog.NumberOfCopies = CInt(Me.m_NumberOfCopiesUpDown.Value)
            printDialog.Collate = Me.m_CollateCheckBox.Checked
            printDialog.PrintDocument = printDocument
            AddHandler printDialog.Closed, New Nevron.Nov.[Function](Of Nevron.Nov.UI.NPrintDialogResult)(AddressOf Me.OnPrintDialogClosed)
            printDialog.RequestShow()
        End Sub

        Private Sub OnBeginPrint(ByVal sender As Nevron.Nov.UI.NPrintDocument, ByVal e As Nevron.Nov.UI.NBeginPrintEventArgs)
        End Sub

        Private Sub OnEndPrint(ByVal sender As Nevron.Nov.UI.NPrintDocument, ByVal e As Nevron.Nov.UI.NEndPrintEventArgs)
        End Sub

        Private Sub OnPrintPage(ByVal sender As Nevron.Nov.UI.NPrintDocument, ByVal e As Nevron.Nov.UI.NPrintPageEventArgs)
            Dim pageSizeDIP As Nevron.Nov.Graphics.NSize = New Nevron.Nov.Graphics.NSize(Me.Width, Me.Height)

            Try
                Dim pageMargins As Nevron.Nov.Graphics.NMargins = Nevron.Nov.Graphics.NMargins.Zero
                Dim clip As Nevron.Nov.Graphics.NRegion = Nevron.Nov.Graphics.NRegion.FromRectangle(New Nevron.Nov.Graphics.NRectangle(0, 0, e.PrintableArea.Width, e.PrintableArea.Height))
                Dim transform As Nevron.Nov.Graphics.NMatrix = New Nevron.Nov.Graphics.NMatrix(e.PrintableArea.X, e.PrintableArea.Y)
                Dim visitor As Nevron.Nov.Dom.NPaintVisitor = New Nevron.Nov.Dom.NPaintVisitor(e.Graphics, 300, transform, clip)
				
				' forward traverse the display tree
				Me.m_PrintRangeModeComboBox.DisplayWindow.VisitDisplaySubtree(visitor)
                e.HasMorePages = False
            Catch x As System.Exception
                Call Nevron.Nov.UI.NMessageBox.Show(x.Message, "Exception", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.[Error])
            End Try
        End Sub

        Private Sub OnPrintDialogClosed(ByVal result As Nevron.Nov.UI.NPrintDialogResult)
            If result.Result = Nevron.Nov.UI.ENCommonDialogResult.[Error] Then
                Call Nevron.Nov.UI.NMessageBox.Show("Error Message: " & result.ErrorException.Message, "Print Dialog Error", Nevron.Nov.UI.ENMessageBoxButtons.OK, Nevron.Nov.UI.ENMessageBoxIcon.[Error])
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_PrintRangeModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_EnableCustomPageRangeCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EnableCurrentPageCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_EnableSelectionCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_CollateCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_NumberOfCopiesUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NPrintDialogExample.
		''' </summary>
		Public Shared ReadOnly NPrintDialogExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
