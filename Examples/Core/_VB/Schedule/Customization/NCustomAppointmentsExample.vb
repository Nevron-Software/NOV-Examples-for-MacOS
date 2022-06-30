Imports System
Imports Nevron.Nov.Barcode
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.Schedule.Formats
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NCustomAppointmentsExample
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
            Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.NCustomAppointmentsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple schedule
			Dim scheduleViewWithRibbon As Nevron.Nov.Schedule.NScheduleViewWithRibbon = New Nevron.Nov.Schedule.NScheduleViewWithRibbon()
            AddHandler scheduleViewWithRibbon.Registered, AddressOf Me.OnScheduleViewWithRibbonRegistered
            Me.m_ScheduleView = scheduleViewWithRibbon.View
            Me.m_ScheduleView.Document.PauseHistoryService()

            Try
                Me.InitSchedule(Me.m_ScheduleView.Content)
            Finally
                Me.m_ScheduleView.Document.ResumeHistoryService()
            End Try

			' Return the commanding widget
			Return scheduleViewWithRibbon
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    This example demonstrates how to create a custom appointments class, which inherits from NAppointment and customizes
	the content of the generated appointment widget. If you scan the generated QR code with your smartphone you will be
	asked whether to add it to your smartphone's calendar.
</p>
<p>
	This example also shows how to add a custom property to the appointment and add it to the appointment designer, which
	opens when you double click the appointment.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim today As System.DateTime = System.DateTime.Today
            schedule.ViewMode = Nevron.Nov.Schedule.ENScheduleViewMode.Day
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Travel to Work", today.AddHours(6.5), today.AddHours(7.5)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Meeting with John", today.AddHours(8), today.AddHours(10)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Conference", today.AddHours(10.5), today.AddHours(11.5)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Lunch", today.AddHours(12), today.AddHours(14)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("News Reading", today.AddHours(12.5), today.AddHours(13.5)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Video Presentation", today.AddHours(14.5), today.AddHours(15.5)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Web Meeting", today.AddHours(16), today.AddHours(17)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Travel back home", today.AddHours(17.5), today.AddHours(19)))
            schedule.Appointments.Add(New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment("Family Dinner", today.AddHours(20), today.AddHours(21)))

			' Increase the height of the day view mode
			schedule.DayViewMode.Height = 2000
        End Sub

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' Called when the schedule view with ribbon is added to a document.
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnScheduleViewWithRibbonRegistered(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' Evaluate the document
			CType(arg.TargetNode, Nevron.Nov.Dom.NDocumentNode).OwnerDocument.Evaluate()

			' Scroll the schedule to 6 AM
			Me.m_ScheduleView.Content.ScrollToTime(System.TimeSpan.FromHours(6))
        End Sub

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NCustomAppointmentsExample.
		''' </summary>
		Public Shared ReadOnly NCustomAppointmentsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Nested Types"

		Public Class CustomAppointment
            Inherits Nevron.Nov.Schedule.NAppointment
			#Region"Constructors"

			Public Sub New()
            End Sub

            Public Sub New(ByVal subject As String, ByVal start As System.DateTime, ByVal [end] As System.DateTime)
                MyBase.New(subject, start, [end])
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomAppointmentSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment), Nevron.Nov.Schedule.NAppointment.NAppointmentSchema)

				' Properties
				Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomTextProperty = Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomAppointmentSchema.AddSlot("CustomText", Nevron.Nov.Dom.NDomType.[String], Nothing)

				' Set designer
				Call Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomAppointmentSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomAppointmentDesigner)))
            End Sub

			#EndRegion

			#Region"Properties"

			''' <summary>
			''' Gets/Sets the value of the CustomText property.
			''' </summary>
			Public Property CustomText As String
                Get
                    Return CStr(MyBase.GetValue(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomTextProperty))
                End Get
                Set(ByVal value As String)
                    MyBase.SetValue(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomTextProperty, value)
                End Set
            End Property

			#EndRegion

			#Region"Protected Overrides"

			Protected Overrides Function CreateWidget() As Nevron.Nov.Schedule.NAppointmentWidget
                Return New Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointmentWidget()
            End Function

			#EndRegion

			#Region"Schema"

			Public Shared ReadOnly CustomAppointmentSchema As Nevron.Nov.Dom.NSchema
			''' <summary>
			''' Reference to the CustomText property.
			''' </summary>
			Public Shared ReadOnly CustomTextProperty As Nevron.Nov.Dom.NProperty

			#EndRegion

			#Region"Nested Types - Designer"

			Public Class CustomAppointmentDesigner
                Inherits Nevron.Nov.Schedule.NAppointment.NAppointmentDesigner

                Public Sub New()
                    MyBase.SetPropertyBrowsable(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointment.CustomTextProperty, True)
                End Sub
            End Class

			#EndRegion
		End Class

        Public Class CustomAppointmentWidget
            Inherits Nevron.Nov.Schedule.NAppointmentWidget
			#Region"Constructors"

			Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointmentWidget.CustomAppointmentWidgetSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NCustomAppointmentsExample.CustomAppointmentWidget), Nevron.Nov.Schedule.NAppointmentWidget.NAppointmentWidgetSchema)
            End Sub

			#EndRegion

			#Region"Protected Overrides"

			Protected Overrides Function CreateContent() As Nevron.Nov.UI.NWidget
				' Create a label
				Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel()
                label.TextWrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
                label.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
                label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 12, Nevron.Nov.Graphics.ENFontStyle.Underline)

				' Create a barcode
				Dim barcode As Nevron.Nov.Barcode.NMatrixBarcode = New Nevron.Nov.Barcode.NMatrixBarcode()
                barcode.Scale = 2
                barcode.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
                barcode.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center

				' Place the label and the barcode in a pair box
				Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(label, barcode, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2)
                pairBox.Spacing = Nevron.Nov.NDesign.HorizontalSpacing
                pairBox.FillMode = Nevron.Nov.Layout.ENStackFillMode.First
                pairBox.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
                pairBox.Padding = New Nevron.Nov.Graphics.NMargins(Nevron.Nov.NDesign.HorizontalSpacing)
                Return pairBox
            End Function

            Protected Overrides Sub OnAppointmentSubjectChanged(ByVal oldSubject As String, ByVal newSubject As String)
                Dim label As Nevron.Nov.UI.NLabel = CType(MyBase.GetFirstDescendant(Nevron.Nov.UI.NLabel.NLabelSchema), Nevron.Nov.UI.NLabel)
                label.Text = newSubject
                Me.UpdateBarcode()
            End Sub

            Protected Overrides Sub OnRegistered()
                MyBase.OnRegistered()
                Me.UpdateBarcode()
            End Sub

			#EndRegion

			#Region"Implementation"

			Private Sub OnAppointmentChanged(ByVal args As Nevron.Nov.Dom.NEventArgs)
                Dim valueChangeArgs As Nevron.Nov.Dom.NValueChangeEventArgs = TryCast(args, Nevron.Nov.Dom.NValueChangeEventArgs)
                If valueChangeArgs Is Nothing Then Return

				' If the start or the end time of the appointment has changed, update the barcode
				Dim [property] As Nevron.Nov.Dom.NProperty = valueChangeArgs.[Property]

                If [property] Is Nevron.Nov.Schedule.NAppointmentBase.StartProperty OrElse [property] Is Nevron.Nov.Schedule.NAppointmentBase.EndProperty Then
                    Me.UpdateBarcode()
                    Dim schedule As Nevron.Nov.Schedule.NSchedule = CType(Me.GetFirstAncestor(Nevron.Nov.Schedule.NSchedule.NScheduleSchema), Nevron.Nov.Schedule.NSchedule)
                    schedule.ScrollToTime(System.TimeSpan.FromHours(6))
                End If
            End Sub

            Private Sub UpdateBarcode()
                Dim appointment As Nevron.Nov.Schedule.NAppointmentBase = Me.Appointment
                If appointment Is Nothing Then Return

				' Serialize the appointment to a string
				Dim text As String = Nevron.Nov.Schedule.Formats.NScheduleFormat.iCalendar.SerializeAppointment(appointment, False)

				' Update the text of the matrix barcode widget
				Dim barcode As Nevron.Nov.Barcode.NMatrixBarcode = CType(MyBase.GetFirstDescendant(Nevron.Nov.Barcode.NMatrixBarcode.NMatrixBarcodeSchema), Nevron.Nov.Barcode.NMatrixBarcode)
                barcode.Text = text
            End Sub

			#EndRegion

			#Region"Schema"

			Public Shared ReadOnly CustomAppointmentWidgetSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
