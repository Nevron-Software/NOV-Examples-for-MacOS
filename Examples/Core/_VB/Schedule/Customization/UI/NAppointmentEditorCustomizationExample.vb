Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Schedule
Imports Nevron.Nov.Schedule.Commands
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Schedule
    Public Class NAppointmentEditorCustomizationExample
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
            Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.NAppointmentEditorCustomizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' Create a simple schedule
			Dim scheduleViewWithRibbon As Nevron.Nov.Schedule.NScheduleViewWithRibbon = New Nevron.Nov.Schedule.NScheduleViewWithRibbon()
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
<p>This example demonstrates how to replace the appointment edit dialog with a custom one.</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub InitSchedule(ByVal schedule As Nevron.Nov.Schedule.NSchedule)
            Dim start As System.DateTime = System.DateTime.Now

			' Replace the default Add Appointment command action with a custom one
			Dim addAppointmentCommandAction As Nevron.Nov.UI.NCommandAction = Me.m_ScheduleView.Commander.GetCommandAction(Nevron.Nov.Schedule.NScheduleView.AddAppointmentCommand)
            Me.m_ScheduleView.Commander.Remove(addAppointmentCommandAction)
            Me.m_ScheduleView.Commander.Add(New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAddAppointmentCommandAction())

			' Replace the default Appointment Edit tool with a custom one
			Dim appointmentEditTool As Nevron.Nov.UI.NTool = Me.m_ScheduleView.Interactor.GetTool(Nevron.Nov.Schedule.NAppointmentEditTool.NAppointmentEditToolSchema)
            Dim index As Integer = Me.m_ScheduleView.Interactor.IndexOf(appointmentEditTool)
            Me.m_ScheduleView.Interactor.RemoveAt(index)
            Dim customEditAppointmentTool As Nevron.Nov.UI.NTool = New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAppointmentEditTool()
            customEditAppointmentTool.Enabled = True
            Me.m_ScheduleView.Interactor.Insert(index, customEditAppointmentTool)

			' Create some custom appointments
			Dim appointmentWithLocation As Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation = New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation("Appointment with Location", start, start.AddHours(2))
            appointmentWithLocation.Location = "New York"
            schedule.Appointments.Add(appointmentWithLocation)
            Dim appointmentWithImage As Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage = New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage("Appointment with Image", start.AddHours(3), start.AddHours(5))
            appointmentWithImage.Image = Nevron.Nov.Schedule.NResources.Image_MobileComputers_UMPC_jpg
            appointmentWithImage.Category = Nevron.Nov.NLoc.[Get]("Orange")
            schedule.Appointments.Add(appointmentWithImage)
            schedule.ScrollToTime(start.TimeOfDay)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ScheduleView As Nevron.Nov.Schedule.NScheduleView

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NAppointmentEditorCustomizationExample.
		''' </summary>
		Public Shared ReadOnly NAppointmentEditorCustomizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Nested Types"

		''' <summary>
		''' A custom appointment class.
		''' </summary>
		Public Class AppointmentWithLocation
            Inherits Nevron.Nov.Schedule.NAppointment
			#Region"Constructors"

			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Initializing constructor.
			''' </summary>
			''' <paramname="subject"></param>
			''' <paramname="start"></param>
			''' <paramname="end"></param>
			Public Sub New(ByVal subject As String, ByVal start As System.DateTime, ByVal [end] As System.DateTime)
                MyBase.New(subject, start, [end])
            End Sub

			''' <summary>
			''' Static constructor.
			''' </summary>
			Shared Sub New()
                Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.CustomAppointmentSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation), Nevron.Nov.Schedule.NAppointment.NAppointmentSchema)

				' Properties
				Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.LocationProperty = Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.CustomAppointmentSchema.AddSlot("Location", Nevron.Nov.Dom.NDomType.[String], Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.defaultLocation)

				' Designer
				Call Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.CustomAppointmentSchema.SetMetaUnit(New Nevron.Nov.Editors.NDesignerMetaUnit(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.CustomAppointmentDesigner)))
            End Sub

			#EndRegion

			#Region"Properties"

			''' <summary>
			''' Gets/Sets the location of the appointment.
			''' </summary>
			Public Property Location As String
                Get
                    Return CStr(MyBase.GetValue(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.LocationProperty))
                End Get
                Set(ByVal value As String)
                    MyBase.SetValue(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.LocationProperty, value)
                End Set
            End Property

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomAppointment.
			''' </summary>
			Public Shared ReadOnly CustomAppointmentSchema As Nevron.Nov.Dom.NSchema
			''' <summary>
			''' Reference to the Location property.
			''' </summary>
			Public Shared ReadOnly LocationProperty As Nevron.Nov.Dom.NProperty

			#EndRegion
			
			#Region"Default Values"

			Private Const defaultLocation As String = Nothing

			#EndRegion

			#Region"Nested Types - Designer"

			Public Class CustomAppointmentDesigner
                Inherits Nevron.Nov.Schedule.NAppointment.NAppointmentDesigner

                Protected Overrides Sub ConfigureGeneralCategory()
                    MyBase.ConfigureGeneralCategory()
                    MyBase.SetPropertyBrowsable(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithLocation.LocationProperty, True)
                End Sub
            End Class

			#EndRegion
		End Class

		''' <summary>
		''' A custom appointment class.
		''' </summary>
		Public Class AppointmentWithImage
            Inherits Nevron.Nov.Schedule.NAppointmentBase
			#Region"Constructors"

			''' <summary>
			''' Default constructor.
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Initializing constructor.
			''' </summary>
			''' <paramname="subject"></param>
			''' <paramname="start"></param>
			''' <paramname="end"></param>
			Public Sub New(ByVal subject As String, ByVal start As System.DateTime, ByVal [end] As System.DateTime)
                MyBase.New(subject, start, [end])
            End Sub

			''' <summary>
			''' Static constructor.
			''' </summary>
			Shared Sub New()
                Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.AppointmentWithImageSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage), Nevron.Nov.Schedule.NAppointmentBase.NAppointmentBaseSchema)

				' Properties
				Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.ImageProperty = Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.AppointmentWithImageSchema.AddSlot("Image", GetType(Nevron.Nov.Graphics.NImage), Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.defaultImage)
            End Sub

			#EndRegion

			#Region"Properties"

			''' <summary>
			''' Gets/Sets the image for this appointment.
			''' </summary>
			Public Property Image As Nevron.Nov.Graphics.NImage
                Get
                    Return CType(MyBase.GetValue(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.ImageProperty), Nevron.Nov.Graphics.NImage)
                End Get
                Set(ByVal value As Nevron.Nov.Graphics.NImage)
                    MyBase.SetValue(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage.ImageProperty, value)
                End Set
            End Property

			#EndRegion

			#Region"Public Overrides - Edit Dialog"

			''' <summary>
			''' Creates a custom appointment edit dialog.
			''' </summary>
			''' <returns></returns>
			Public Overrides Function CreateEditDialog() As Nevron.Nov.UI.NTopLevelWindow
                Dim schedule As Nevron.Nov.Schedule.NSchedule = CType(MyBase.GetFirstAncestor(Nevron.Nov.Schedule.NSchedule.NScheduleSchema), Nevron.Nov.Schedule.NSchedule)
                Dim window As Nevron.Nov.UI.NWindow = If(schedule IsNot Nothing, schedule.DisplayWindow, Nothing)

				' Create a dialog window
				Dim dialog As Nevron.Nov.UI.NTopLevelWindow = Nevron.Nov.NApplication.CreateTopLevelWindow(Nevron.Nov.UI.NWindow.GetFocusedWindowIfNull(window))
                dialog.SetupDialogWindow("Appointment with Image Editor", True)
                Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
                stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
                stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

				' Add an image box with the image
				Dim imageBox As Nevron.Nov.UI.NImageBox = New Nevron.Nov.UI.NImageBox(CType(Nevron.Nov.NSystem.SafeDeepClone(Me.Image), Nevron.Nov.Graphics.NImage))
                stack.Add(imageBox)

				' Add property editors for some of the appointment properties
				Dim designer As Nevron.Nov.Editors.NDesigner = Nevron.Nov.Editors.NDesigner.GetDesigner(Me)
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = designer.CreatePropertyEditors(Me, Nevron.Nov.Schedule.NAppointmentBase.SubjectProperty, Nevron.Nov.Schedule.NAppointmentBase.StartProperty, Nevron.Nov.Schedule.NAppointmentBase.EndProperty)

                For i As Integer = 0 To editors.Count - 1
                    stack.Add(editors(i))
                Next

				' Add a button strip with OK and Cancel buttons
				Dim buttonStrip As Nevron.Nov.UI.NButtonStrip = New Nevron.Nov.UI.NButtonStrip()
                buttonStrip.AddOKCancelButtons()
                stack.Add(buttonStrip)
                dialog.Content = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
                Return dialog
            End Function

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with AppointmentWithImage.
			''' </summary>
			Public Shared ReadOnly AppointmentWithImageSchema As Nevron.Nov.Dom.NSchema
			''' <summary>
			''' Reference to the Image property.
			''' </summary>
			Public Shared ReadOnly ImageProperty As Nevron.Nov.Dom.NProperty

			#EndRegion

			#Region"Default Values"

			Private Const defaultImage As Nevron.Nov.Graphics.NImage = Nothing

			#EndRegion
		End Class

        Public Class CustomAddAppointmentCommandAction
            Inherits Nevron.Nov.Schedule.Commands.NAddAppointmentCommandAction
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
                Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAddAppointmentCommandAction.CustomAddAppointmentCommandActionSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAddAppointmentCommandAction), Nevron.Nov.Schedule.Commands.NAddAppointmentCommandAction.NAddAppointmentCommandActionSchema)
            End Sub

			#EndRegion

			#Region"Protected Overrides"

			Protected Overrides Function CreateAppointmentForGridCell(ByVal gridCell As Nevron.Nov.Schedule.NScheduleGridCell) As Nevron.Nov.Schedule.NAppointmentBase
                Dim appointment As Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage = New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage(Nothing, gridCell.StartTime, gridCell.EndTime)
                appointment.Image = Nevron.Nov.Schedule.NResources.Image_Artistic_Plane_png
                Return appointment
            End Function

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomAddAppointmentCommandAction.
			''' </summary>
			Public Shared ReadOnly CustomAddAppointmentCommandActionSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

        Public Class CustomAppointmentEditTool
            Inherits Nevron.Nov.Schedule.NAppointmentEditTool
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
                Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAppointmentEditTool.CustomAppointmentEditToolSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.CustomAppointmentEditTool), Nevron.Nov.Schedule.NAppointmentEditTool.NAppointmentEditToolSchema)
            End Sub

			#EndRegion

			#Region"Protected Overrides"

			Protected Overrides Function CreateAppointmentForGridCell(ByVal gridCell As Nevron.Nov.Schedule.NScheduleGridCell) As Nevron.Nov.Schedule.NAppointmentBase
                Dim appointment As Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage = New Nevron.Nov.Examples.Schedule.NAppointmentEditorCustomizationExample.AppointmentWithImage(Nothing, gridCell.StartTime, gridCell.EndTime)
                appointment.Image = Nevron.Nov.Schedule.NResources.Image_Artistic_Plane_png
                Return appointment
            End Function

			#EndRegion

			#Region"Schema"

			''' <summary>
			''' Schema associated with CustomAppointmentEditTool.
			''' </summary>
			Public Shared ReadOnly CustomAppointmentEditToolSchema As Nevron.Nov.Dom.NSchema

			#EndRegion
		End Class

		#EndRegion
	End Class
End Namespace
