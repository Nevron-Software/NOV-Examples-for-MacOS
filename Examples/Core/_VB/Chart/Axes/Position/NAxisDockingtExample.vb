Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis docking example
	''' </summary>
	Public Class NAxisDockingExample
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
            Nevron.Nov.Examples.Chart.NAxisDockingExample.NAxisDockingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisDockingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Docking"
            Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_RedAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, Nevron.Nov.Graphics.NColor.Red)
            chart.Axes.Add(Me.m_RedAxis)
            Me.m_GreenAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, Nevron.Nov.Graphics.NColor.Green)
            chart.Axes.Add(Me.m_GreenAxis)

			' Add a custom vertical axis
			Me.m_BlueAxis = Me.CreateLinearAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left, Nevron.Nov.Graphics.NColor.Blue)
            chart.Axes.Add(Me.m_BlueAxis)
            chart.Axes.Add(Nevron.Nov.Chart.NCartesianChart.CreateDockedAxis(Nevron.Nov.Chart.ENCartesianAxisDockZone.Bottom, Nevron.Nov.Chart.ENScaleType.Orindal))
			
			' create three line series and dispay them on three vertical axes (red, green and blue axis)
			Dim line1 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.DarkRed, 10, 20)
            chart.Series.Add(line1)
            Dim line2 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.DarkGreen, 50, 100)
            chart.Series.Add(line2)
            Dim line3 As Nevron.Nov.Chart.NLineSeries = Me.CreateLineSeries(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.DarkBlue, 100, 200)
            chart.Series.Add(line3)
            line1.VerticalAxis = Me.m_RedAxis
            line2.VerticalAxis = Me.m_GreenAxis
            line3.VerticalAxis = Me.m_BlueAxis
            Return chartView
        End Function

        Private Function CreateAxisZoneCombo() As Nevron.Nov.UI.NComboBox
            Dim axisZoneComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            axisZoneComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Left"))
            axisZoneComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Right"))
            axisZoneComboBox.SelectedIndex = 0
            AddHandler axisZoneComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAxisZoneComboBoxSelectedIndexChanged)
            Return axisZoneComboBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_RedAxisZoneComboBox = Me.CreateAxisZoneCombo()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Red Axis Dock Zone:", Me.m_RedAxisZoneComboBox))
            Me.m_GreenAxisZoneComboBox = Me.CreateAxisZoneCombo()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Green Axis Dock Zone:", Me.m_GreenAxisZoneComboBox))
            Me.m_BlueAxisZoneComboBox = Me.CreateAxisZoneCombo()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Blue Axis Dock Zone:", Me.m_BlueAxisZoneComboBox))
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to dock axes to different axis dock zones.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateLinearAxis(ByVal dockZone As Nevron.Nov.Chart.ENCartesianAxisDockZone, ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.Chart.NCartesianAxis
            Dim axis As Nevron.Nov.Chart.NCartesianAxis = New Nevron.Nov.Chart.NCartesianAxis()
            axis.Scale = Me.CreateLinearScale(color)
            axis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(dockZone)
            Return axis
        End Function

        Private Function CreateLinearScale(ByVal color As Nevron.Nov.Graphics.NColor) As Nevron.Nov.Chart.NLinearScale
            Dim linearScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            linearScale.Ruler.Stroke = New Nevron.Nov.Graphics.NStroke(1, color)
            linearScale.InnerMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(color)
            linearScale.OuterMajorTicks.Stroke = New Nevron.Nov.Graphics.NStroke(color)
            linearScale.MajorGridLines.Visible = False
            linearScale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(color)
            Return linearScale
        End Function

        Private Function CreateLineSeries(ByVal lightColor As Nevron.Nov.Graphics.NColor, ByVal color As Nevron.Nov.Graphics.NColor, ByVal begin As Integer, ByVal [end] As Integer) As Nevron.Nov.Chart.NLineSeries
			' Add a line series
			Dim line As Nevron.Nov.Chart.NLineSeries = New Nevron.Nov.Chart.NLineSeries()

            For i As Integer = 0 To 5 - 1
                line.DataPoints.Add(New Nevron.Nov.Chart.NLineDataPoint(Me.m_Random.[Next](begin, [end])))
            Next

            line.Stroke = New Nevron.Nov.Graphics.NStroke(2, color)
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.Format = "<value>"
            dataLabelStyle.TextStyle.Background.Visible = False
            dataLabelStyle.ArrowStroke.Width = 0
            dataLabelStyle.ArrowLength = 10
            dataLabelStyle.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arial", 8)
            dataLabelStyle.TextStyle.Background.Visible = True
            line.DataLabelStyle = dataLabelStyle
            Dim markerStyle As Nevron.Nov.Chart.NMarkerStyle = New Nevron.Nov.Chart.NMarkerStyle()
            markerStyle.Visible = True
            markerStyle.Border = New Nevron.Nov.Graphics.NStroke(color)
            markerStyle.Fill = New Nevron.Nov.Graphics.NColorFill(lightColor)
            markerStyle.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            markerStyle.Size = New Nevron.Nov.Graphics.NSize(5, 5)
            line.MarkerStyle = markerStyle
            Return line
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAxisZoneComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If Me.m_RedAxisZoneComboBox.SelectedIndex = 0 Then
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left)
            Else
                Me.m_RedAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Right)
            End If

            If Me.m_GreenAxisZoneComboBox.SelectedIndex = 0 Then
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left)
            Else
                Me.m_GreenAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Right)
            End If

            If Me.m_BlueAxisZoneComboBox.SelectedIndex = 0 Then
                Me.m_BlueAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Left)
            Else
                Me.m_BlueAxis.Anchor = New Nevron.Nov.Chart.NDockCartesianAxisAnchor(Nevron.Nov.Chart.ENCartesianAxisDockZone.Right)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Random As System.Random = New System.Random()
        Private m_RedAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_GreenAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_BlueAxis As Nevron.Nov.Chart.NCartesianAxis
        Private m_RedAxisZoneComboBox As Nevron.Nov.UI.NComboBox
        Private m_GreenAxisZoneComboBox As Nevron.Nov.UI.NComboBox
        Private m_BlueAxisZoneComboBox As Nevron.Nov.UI.NComboBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisDockingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
