Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Chart Themes Example
	''' </summary>
	Public Class NChartThemesExample
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
            Nevron.Nov.Examples.Chart.NChartThemesExample.NChartThemesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NChartThemesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Chart Themes"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            linearScale.Strips.Add(strip)

			' add a bar series
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 6 - 1
                Dim bar As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
                bar.Name = "Bar" & i.ToString()
                bar.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
                bar.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                bar.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
                chart.Series.Add(bar)

                For j As Integer = 0 To 6 - 1
                    bar.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                Next
            Next

            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_ChartThemesComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_ChartThemesComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENChartPalette)()
            AddHandler Me.m_ChartThemesComboBox.SelectedIndexChanged, AddressOf Me.OnChartThemesComboBoxSelectedIndexChanged
            stack.Add(Me.m_ChartThemesComboBox)
            Me.m_ColorDataPointsCheckBox = New Nevron.Nov.UI.NCheckBox("Color Data Points")
            AddHandler Me.m_ColorDataPointsCheckBox.CheckedChanged, AddressOf Me.OnColorDataPointsCheckBoxCheckedChanged
            stack.Add(Me.m_ColorDataPointsCheckBox)
            Me.m_ChartThemesComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENChartPalette.Autumn)
            Me.m_ColorDataPointsCheckBox.Checked = True
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to apply different chart color themes.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnColorDataPointsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(CType(Me.m_ChartThemesComboBox.SelectedIndex, Nevron.Nov.Chart.ENChartPalette), Me.m_ColorDataPointsCheckBox.Checked))
        End Sub

        Private Sub OnChartThemesComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(CType(Me.m_ChartThemesComboBox.SelectedIndex, Nevron.Nov.Chart.ENChartPalette), Me.m_ColorDataPointsCheckBox.Checked))
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_ChartThemesComboBox As Nevron.Nov.UI.NComboBox
        Private m_ColorDataPointsCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NChartThemesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
