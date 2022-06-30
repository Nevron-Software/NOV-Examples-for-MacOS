Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' XY Scatter Point Example
	''' </summary>
	Public Class NXYScatterPointExample
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
            Nevron.Nov.Examples.Chart.NXYScatterPointExample.NXYScatterPointExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NXYScatterPointExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "XY Scatter Point"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim scaleX As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleX.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleX.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scaleX

			' setup Y axis
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleY.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleY.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scaleY

			' add a point series
			Me.m_Point = New Nevron.Nov.Chart.NPointSeries()
            Me.m_Point.Name = "Point Series"
            Me.m_Point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Point.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.DarkOrange, 160))
            Me.m_Point.Size = 5
            Me.m_Point.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            Me.m_Point.UseXValues = True
            Me.m_Chart.Series.Add(Me.m_Point)
            Me.OnNewDataButtonClick(Nothing)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim inflateMarginsCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler inflateMarginsCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnInflateMarginsCheckBoxCheckedChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Inflate Margins: ", inflateMarginsCheckBox))
            Dim verticalAxisRoundToTick As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox()
            AddHandler verticalAxisRoundToTick.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnAxesRoundToTickCheckedChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Axes Round To Tick: ", verticalAxisRoundToTick))
            Dim newDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("New Data")
            AddHandler newDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnNewDataButtonClick)
            stack.Add(newDataButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a xy scatter point chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnNewDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Point.DataPoints.Clear()
            Dim dataPoints As Nevron.Nov.Chart.NDataPointCollection(Of Nevron.Nov.Chart.NPointDataPoint) = Me.m_Point.DataPoints
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 1000 - 1
                Dim u1 As Double = random.NextDouble()
                Dim u2 As Double = random.NextDouble()
                If u1 = 0 Then u1 += 0.0001
                If u2 = 0 Then u2 += 0.0001
                Dim z0 As Double = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Cos(2 * System.Math.PI * u2)
                Dim z1 As Double = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Sin(2 * System.Math.PI * u2)
                dataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(z0, z1))
            Next
        End Sub

        Private Sub OnAxesRoundToTickCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            For i As Integer = 0 To Me.m_Chart.Axes.Count - 1
                Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CInt((i))).Scale, Nevron.Nov.Chart.NLinearScale)

                If linearScale IsNot Nothing Then
                    If TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                        linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.MajorTick
                        linearScale.InflateViewRangeBegin = True
                        linearScale.InflateViewRangeEnd = True
                    Else
                        linearScale.ViewRangeInflateMode = Nevron.Nov.Chart.ENScaleViewRangeInflateMode.Logical
                    End If
                End If
            Next
        End Sub

        Private Sub OnInflateMarginsCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.InflateMargins = TryCast(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NXYScatterPointExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
