Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Multi Measure Radar example
	''' </summary>
	Public Class NMultiMeasureRadarExample
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
            Nevron.Nov.Examples.Chart.NMultiMeasureRadarExample.NMultiMeasureRadarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NMultiMeasureRadarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NMultiMeasureRadarExample.CreateRadarChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Radar Axis Titles"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NRadarChart)
            Me.m_Chart.RadarMode = Nevron.Nov.Chart.ENRadarMode.MultiMeasure
            Me.m_Chart.InnerRadius = 60

			' set some axis labels
			Me.AddAxis(Me.m_Chart, "Population", True)
            Me.AddAxis(Me.m_Chart, "Housing Units", True)
            Me.AddAxis(Me.m_Chart, "Water", False)
            Me.AddAxis(Me.m_Chart, "Land", True)
            Me.AddAxis(Me.m_Chart, "Population" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "Density", False)
            Me.AddAxis(Me.m_Chart, "Housing" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "Density", False)

			' sample data
			Dim data As Object() = New Object() {"Cascade County", 80357, 35225, 13.75, 2697.90, 29.8, 13.1, "Custer County", 11696, 5360, 10.09, 3783.13, 3.1, 1.4, "Dawson County", 9059, 4168, 9.99, 2373.14, 3.8, 1.8, "Jefferson County", 10049, 4199, 2.19, 1656.64, 6.1, 2.5, "Missoula County", 95802, 41319, 20.37, 2597.97, 36.9, 15.9, "Powell County", 7180, 2930, 6.74, 2325.94, 3.1, 1.3}

            For i As Integer = 0 To 6 - 1
                Dim radarLine As Nevron.Nov.Chart.NRadarLineSeries = New Nevron.Nov.Chart.NRadarLineSeries()
                Me.m_Chart.Series.Add(radarLine)
                Dim baseIndex As Integer = i * 7
                radarLine.Name = data(CInt((baseIndex))).ToString()
                baseIndex = baseIndex + 1

                For j As Integer = 0 To 6 - 1
                    radarLine.DataPoints.Add(New Nevron.Nov.Chart.NRadarLineDataPoint(System.Convert.ToDouble(data(baseIndex))))
                    baseIndex = baseIndex + 1
                Next

                radarLine.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
                markerStyle.Size = New Nevron.Nov.Graphics.NSize(4, 4)
                markerStyle.Visible = True
                markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Chart.NChartTheme.BrightPalette(i))
                radarLine.MarkerStyle = markerStyle
                radarLine.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Chart.NChartTheme.BrightPalette(i))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a multi measure radar chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

	

		#EndRegion

		#Region"Implementation"

		Private Sub AddAxis(ByVal radar As Nevron.Nov.Chart.NRadarChart, ByVal title As String, ByVal applyKFormatting As Boolean)
            Dim axis As Nevron.Nov.Chart.NRadarAxis = New Nevron.Nov.Chart.NRadarAxis()

			' set title
			axis.Title = title
            radar.Axes.Add(axis)
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(axis.Scale, Nevron.Nov.Chart.NLinearScale)
            linearScale.MajorGridLines.Visible = False

            If applyKFormatting Then
                linearScale.Labels.TextProvider = New Nevron.Nov.Chart.NFormattedScaleLabelTextProvider(New Nevron.Nov.Dom.NNumericValueFormatter("0,K"))
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NRadarChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NMultiMeasureRadarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateRadarChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Radar)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
