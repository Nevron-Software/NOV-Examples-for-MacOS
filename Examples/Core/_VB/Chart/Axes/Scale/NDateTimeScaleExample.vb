Imports System
Imports System.Globalization
Imports Nevron.Nov.Chart
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Date Time Scale Example
	''' </summary>
	Public Class NDateTimeScaleExample
        Inherits NExampleBase
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
        End Sub
		''' <summary>
		''' Static constructor
		''' </summary>
		Shared Sub New()
            Nevron.Nov.Examples.Chart.NDateTimeScaleExample.NDateTimeScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NDateTimeScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Date Time Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XDateTimeYLinear)

			' add interlaced stripe to the Y axis
			Dim yScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            yScale.Strips.Add(stripStyle)

			' Add a line series (data will be generated with example controls)
			Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()
            Me.m_Chart.Series.Add(line)
            line.UseXValues = True
            line.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            line.InflateMargins = True
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            line.MarkerStyle = markerStyle

			' create a date time scale
			Me.m_DateTimeScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NDateTimeScale)
            Me.m_DateTimeScale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 90)
            Me.m_DateTimeScale.Labels.Style.ContentAlignment = Nevron.Nov.ENContentAlignment.TopCenter
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("Allowed Date Time Units"))
            Dim dateTimeUnits As Nevron.Nov.NDateTimeUnit() = New Nevron.Nov.NDateTimeUnit() {Nevron.Nov.NDateTimeUnit.Century, Nevron.Nov.NDateTimeUnit.Decade, Nevron.Nov.NDateTimeUnit.Year, Nevron.Nov.NDateTimeUnit.HalfYear, Nevron.Nov.NDateTimeUnit.Quarter, Nevron.Nov.NDateTimeUnit.Month, Nevron.Nov.NDateTimeUnit.Week, Nevron.Nov.NDateTimeUnit.Day, Nevron.Nov.NDateTimeUnit.HalfDay, Nevron.Nov.NDateTimeUnit.Hour, Nevron.Nov.NDateTimeUnit.Minute, Nevron.Nov.NDateTimeUnit.Second, Nevron.Nov.NDateTimeUnit.Millisecond, Nevron.Nov.NDateTimeUnit.Tick}
            Me.m_DateTimeUnitListBox = New Nevron.Nov.UI.NListBox()

            For i As Integer = 0 To dateTimeUnits.Length - 1
                Dim dateTimeUnit As Nevron.Nov.NDateTimeUnit = dateTimeUnits(i)
                Dim dateTimeUnitCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox(Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(dateTimeUnit.DateTimeUnit.ToString()))
                dateTimeUnitCheckBox.Checked = True
                AddHandler dateTimeUnitCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDateTimeUnitCheckBoxCheckedChanged)
                dateTimeUnitCheckBox.Tag = dateTimeUnit
                Me.m_DateTimeUnitListBox.Items.Add(New Nevron.Nov.UI.NListBoxItem(dateTimeUnitCheckBox))
            Next

            stack.Add(Me.m_DateTimeUnitListBox)
            Me.OnDateTimeUnitCheckBoxCheckedChanged(Nothing)
            Dim enableUnitSensitiveFormattingCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Enable Unit Sensitive Formatting")
            AddHandler enableUnitSensitiveFormattingCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEnableUnitSensitiveFormattingCheckBoxCheckedChanged)
            stack.Add(enableUnitSensitiveFormattingCheckBox)
            enableUnitSensitiveFormattingCheckBox.Checked = True
            stack.Add(New Nevron.Nov.UI.NLabel("Start Date:"))
            Me.m_StartDateTimeBox = New Nevron.Nov.UI.NDateTimeBox()
            AddHandler Me.m_StartDateTimeBox.SelectedDateChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnStartDateTimeBoxSelectedDateChanged)
            stack.Add(Me.m_StartDateTimeBox)
            stack.Add(New Nevron.Nov.UI.NLabel("End Date:"))
            Me.m_EndDateTimeBox = New Nevron.Nov.UI.NDateTimeBox()
            AddHandler Me.m_EndDateTimeBox.SelectedDateChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnEndDateTimeBoxSelectedDateChanged)
            stack.Add(Me.m_EndDateTimeBox)
            Dim generateRandomDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Generate Random Data")
            AddHandler generateRandomDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnGenerateRandomDataButtonClick)
            stack.Add(generateRandomDataButton)
            Me.m_StartDateTimeBox.SelectedDate = System.DateTime.Now
            Me.m_EndDateTimeBox.SelectedDate = System.Globalization.CultureInfo.CurrentCulture.Calendar.AddYears(Me.m_StartDateTimeBox.SelectedDate, 2)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard date/time scale.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnEnableUnitSensitiveFormattingCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_DateTimeScale.Labels.TextProvider = New Nevron.Nov.Chart.NDateTimeUnitSensitiveLabelTextProvider()
            Else
                Me.m_DateTimeScale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NDateTimeValueFormatter(Nevron.Nov.ENDateTimeValueFormat.[Date]))
            End If
        End Sub

        Private Sub OnEndDateTimeBoxSelectedDateChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.OnGenerateRandomDataButtonClick(Nothing)
        End Sub

        Private Sub OnStartDateTimeBoxSelectedDateChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.OnGenerateRandomDataButtonClick(Nothing)
        End Sub

        Private Sub OnGenerateRandomDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim startDate As System.DateTime = Me.m_StartDateTimeBox.SelectedDate
            Dim endDate As System.DateTime = Me.m_EndDateTimeBox.SelectedDate

            If startDate > endDate Then
                Dim temp As System.DateTime = startDate
                startDate = endDate
                endDate = temp
            End If

			' Get the line series from the chart
			Dim line As Nevron.Nov.Chart.NLineSeries = CType(Me.m_Chart.Series(0), Nevron.Nov.Chart.NLineSeries)
            Dim span As System.TimeSpan = endDate - startDate
            span = New System.TimeSpan(span.Ticks / 30)
            line.DataPoints.Clear()

            If span.Ticks > 0 Then
                Dim random As System.Random = New System.Random()

                While startDate < endDate
                    line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(Nevron.Nov.NDateTimeHelpers.ToOADate(startDate), random.[Next](100)))
                    startDate += span
                End While
            End If
        End Sub

        Private Sub OnDateTimeUnitCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim dateTimeUnits As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.NDateTimeUnit) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.NDateTimeUnit)()

			' collect the checked date time units
			For i As Integer = 0 To Me.m_DateTimeUnitListBox.Items.Count - 1
                Dim dateTimeUnitCheckBox As Nevron.Nov.UI.NCheckBox = TryCast(Me.m_DateTimeUnitListBox.Items(CInt((i))).Content, Nevron.Nov.UI.NCheckBox)

                If dateTimeUnitCheckBox.Checked Then
                    dateTimeUnits.Add(CType(dateTimeUnitCheckBox.Tag, Nevron.Nov.NDateTimeUnit))
                End If
            Next

            Me.m_DateTimeScale.AutoDateTimeUnits = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.NDateTimeUnit)(dateTimeUnits.ToArray())
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_DateTimeScale As Nevron.Nov.Chart.NDateTimeScale
        Private m_DateTimeUnitListBox As Nevron.Nov.UI.NListBox
        Private m_StartDateTimeBox As Nevron.Nov.UI.NDateTimeBox
        Private m_EndDateTimeBox As Nevron.Nov.UI.NDateTimeBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NDateTimeScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
