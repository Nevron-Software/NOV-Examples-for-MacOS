Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NMeasureUpDownExample
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
            Nevron.Nov.Examples.UI.NMeasureUpDownExample.NMeasureUpDownExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NMeasureUpDownExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_StackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_StackPanel.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_StackPanel.VerticalSpacing = 5
            Me.m_StackPanel.Add(Me.CreatePairBox("Angle:", Nevron.Nov.NUnit.Radian, Nevron.Nov.NUnit.Degree, Nevron.Nov.NUnit.Grad))
            Me.m_StackPanel.Add(Me.CreatePairBox("SI Length:", Nevron.Nov.NUnit.Millimeter, Nevron.Nov.NUnit.Centimeter, Nevron.Nov.NUnit.Meter, Nevron.Nov.NUnit.Kilometer))
            Me.m_StackPanel.Add(Me.CreatePairBox("Imperial Length:", Nevron.Nov.NUnit.Inch, Nevron.Nov.NUnit.Foot, Nevron.Nov.NUnit.Yard, Nevron.Nov.NUnit.Mile))
            Me.m_StackPanel.Add(Me.CreatePairBox("Mixed Length:", Nevron.Nov.NUnit.Millimeter, Nevron.Nov.NUnit.Centimeter, Nevron.Nov.NUnit.Meter, Nevron.Nov.NUnit.Kilometer, Nevron.Nov.NUnit.Inch, Nevron.Nov.NUnit.Foot, Nevron.Nov.NUnit.Yard, Nevron.Nov.NUnit.Mile))
            Me.m_StackPanel.Add(Me.CreatePairBox("Graphics Length:", Nevron.Nov.NUnit.Millimeter, Nevron.Nov.NUnit.Inch, Nevron.Nov.NUnit.DIP, Nevron.Nov.NUnit.Point))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(Me.m_StackPanel)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last
            Dim enabledCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enabled", True)
            enabledCheckBox.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            enabledCheckBox.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            AddHandler enabledCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnabledCheckBoxCheckedChanged)
            stack.Add(enabledCheckBox)
            Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use measure up downs. The measure up down extends the functionality
	of the numeric up down by adding a combo box for selecting a unit. If the units can be converted to each other,
	when the user selects a new unit from the unit combo box, the value will be automatically converted. The <b>Unit</b>
	property determines the currently selected measurement unit.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreatePairBox(ByVal text As String, ParamArray units As Nevron.Nov.NUnit()) As Nevron.Nov.UI.NPairBox
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Right
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Center
            Dim measureUpDown As Nevron.Nov.UI.NMeasureUpDown = New Nevron.Nov.UI.NMeasureUpDown(units)
            measureUpDown.Minimum = System.[Double].MinValue
            measureUpDown.Maximum = System.[Double].MaxValue
            measureUpDown.Value = 1
            measureUpDown.DecimalPlaces = 3
            measureUpDown.UnitComboBox.PreferredWidth = 40
            AddHandler measureUpDown.ValueChanged, AddressOf Me.OnMeasureUpDownValueChanged
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(label, measureUpDown, True)
            pairBox.Spacing = 3
            Return pairBox
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnMeasureUpDownValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim measureUpDown As Nevron.Nov.UI.NMeasureUpDown = CType(args.TargetNode, Nevron.Nov.UI.NMeasureUpDown)
            Dim value As Double = CDbl(args.NewValue)
            Dim unit As Nevron.Nov.NUnit = measureUpDown.SelectedUnit
            Dim unitString As String = unit.ToString()

            If value <> 1 Then
				' Make the unit string plural
				If unit = Nevron.Nov.NUnit.Inch Then
                    unitString = "inches"
                ElseIf unit = Nevron.Nov.NUnit.Foot Then
                    unitString = "feet"
                Else
                    unitString += "s"
                End If
            End If

			' Log the event
			Me.m_EventsLog.LogEvent("New value: " & value.ToString() & " " & unitString)
        End Sub

        Private Sub OnEnabledCheckBoxCheckedChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_StackPanel.Enabled = CBool(args.NewValue)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_StackPanel As Nevron.Nov.UI.NStackPanel
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NMeasureUpDownExample.
		''' </summary>
		Public Shared ReadOnly NMeasureUpDownExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

	End Class
End Namespace
