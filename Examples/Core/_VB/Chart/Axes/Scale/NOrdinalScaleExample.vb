Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Ordinal Scale Example
	''' </summary>
	Public Class NOrdinalScaleExample
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
            Nevron.Nov.Examples.Chart.NOrdinalScaleExample.NOrdinalScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NOrdinalScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Ordinal Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlaced stripe to the Y axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            stripStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige)
            stripStyle.Interlaced = True
            linearScale.Strips.Add(stripStyle)

			' add some series
			Dim dataItemsCount As Integer = 6
            Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
            bar.InflateMargins = True
            bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To dataItemsCount - 1
                bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 30)))
            Next

            Me.m_Chart.Series.Add(bar)
            Dim ordinalScale As Nevron.Nov.Chart.NOrdinalScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale)
            ordinalScale.Labels.OverlapResolveLayouts = New Nevron.Nov.Dom.NDomArray(Of Nevron.Nov.Chart.ENLevelLabelsLayout)(New Nevron.Nov.Chart.ENLevelLabelsLayout() {Nevron.Nov.Chart.ENLevelLabelsLayout.AutoScale})
            Dim labels As Nevron.Nov.DataStructures.NList(Of String) = New Nevron.Nov.DataStructures.NList(Of String)()

            For j As Integer = 0 To dataItemsCount - 1
                labels.Add("Category " & j.ToString())
            Next

            ordinalScale.Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(labels)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim displayDataPointsBetweenTicksCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Display Data Points Between Ticks")
            AddHandler displayDataPointsBetweenTicksCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnDisplayDataPointsBetweenTicksCheckBoxCheckedChanged)
            displayDataPointsBetweenTicksCheckBox.Checked = True
            stack.Add(displayDataPointsBetweenTicksCheckBox)
            Dim autoLabelsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Auto Labels")
            AddHandler autoLabelsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAutoLabelsCheckBoxCheckedChanged)
            autoLabelsCheckBox.Checked = True
            stack.Add(autoLabelsCheckBox)
            Dim invertedCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Inverted")
            AddHandler invertedCheckBox.CheckedChanged, AddressOf Me.OnInvertedCheckBoxCheckedChanged
            invertedCheckBox.Checked = False
            stack.Add(invertedCheckBox)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create an ordinal scale.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAutoLabelsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale).Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter())
            Else
                Dim labels As Nevron.Nov.DataStructures.NList(Of String) = New Nevron.Nov.DataStructures.NList(Of String)()
                Dim dataPointCount As Integer = CType(Me.m_Chart.Series(CInt((0))), Nevron.Nov.Chart.NBarSeries).DataPoints.Count

                For j As Integer = 0 To dataPointCount - 1
                    labels.Add("Category " & j.ToString())
                Next

                CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale).Labels.TextProvider = New Nevron.Nov.Chart.NOrdinalScaleLabelTextProvider(labels)
            End If
        End Sub

        Private Sub OnDisplayDataPointsBetweenTicksCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale).DisplayDataPointsBetweenTicks = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnInvertedCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NOrdinalScale).Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Implementation"


		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NOrdinalScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
