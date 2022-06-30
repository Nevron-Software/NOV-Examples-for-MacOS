Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Tools
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Hit testing Example
	''' </summary>
	Public Class NHitTestingExample
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
            Nevron.Nov.Examples.Chart.NHitTestingExample.NHitTestingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NHitTestingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Hit Testing"

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
            Me.m_Point.Size = 20
            Me.m_Point.Shape = Nevron.Nov.Chart.ENPointShape.Rectangle
            Me.m_Point.UseXValues = True
            Me.m_Chart.Series.Add(Me.m_Point)

			' add some sample data
			Dim dataPoints As Nevron.Nov.Chart.NDataPointCollection(Of Nevron.Nov.Chart.NPointDataPoint) = Me.m_Point.DataPoints
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 10 - 1
                Dim u1 As Double = random.NextDouble()
                Dim u2 As Double = random.NextDouble()
                If u1 = 0 Then u1 += 0.0001
                If u2 = 0 Then u2 += 0.0001
                Dim z0 As Double = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Cos(2 * System.Math.PI * u2)
                Dim z1 As Double = System.Math.Sqrt(-2 * System.Math.Log(u1)) * System.Math.Sin(2 * System.Math.PI * u2)
                Dim dataPoint As Nevron.Nov.Chart.NPointDataPoint = New Nevron.Nov.Chart.NPointDataPoint(z0, z1)
                AddHandler dataPoint.MouseDown, AddressOf Nevron.Nov.Examples.Chart.NHitTestingExample.OnDataPointMouseDown
                dataPoints.Add(dataPoint)
            Next

            Me.m_Chart.Enabled = True
            Dim interactor As Nevron.Nov.UI.NInteractor = New Nevron.Nov.UI.NInteractor()
            Me.m_AxisCursorsTool = New Nevron.Nov.Chart.Tools.NAxisCursorTool()
            Me.m_AxisCursorsTool.Enabled = True
            interactor.Add(Me.m_AxisCursorsTool)
            Me.m_Chart.Interactor = interactor
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim hitTestModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            hitTestModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENHitTestMode)()
            AddHandler hitTestModeComboBox.SelectedIndexChanged, AddressOf Me.OnHitTestModeComboBoxSelectedIndexChanged
            hitTestModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENScaleOrientation.Auto)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Hit Test Mode:", hitTestModeComboBox))
            Dim orientationComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            orientationComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCartesianChartOrientation)()
            orientationComboBox.SelectedIndex = CInt(Me.m_Chart.Orientation)
            AddHandler orientationComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOrientationComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Orientation:", orientationComboBox))
            Dim resetColorsButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Reset Colors")
            AddHandler resetColorsButton.Click, AddressOf Me.OnResetColorsButtonClick
            stack.Add(resetColorsButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to implement chart element hit testing.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnResetColorsButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim seriesCount As Integer = Me.m_Chart.Series.Count

            For i As Integer = 0 To seriesCount - 1
                Dim series As Nevron.Nov.Chart.NSeries = Me.m_Chart.Series(i)
                Dim dataPointCount As Integer = series.GetDataPointsChild().GetChildrenCount()

                For j As Integer = 0 To dataPointCount - 1
                    Dim dataPoint As Nevron.Nov.Chart.NDataPoint = CType(series.GetDataPointsChild().GetChildAt(j), Nevron.Nov.Chart.NDataPoint)
                    dataPoint.ClearLocalValue(Nevron.Nov.Chart.NDataPoint.FillProperty)
                    dataPoint.ClearLocalValue(Nevron.Nov.Chart.NDataPoint.StrokeProperty)
                Next
            Next
        End Sub

        Private Sub OnHitTestModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.HitTestMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENHitTestMode)
        End Sub

        Private Sub OnOrientationComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Orientation = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENCartesianChartOrientation)
        End Sub

        Public Shared Sub OnDataPointMouseDown(ByVal arg As Nevron.Nov.UI.NMouseButtonEventArgs)
            If TypeOf arg.TargetNode Is Nevron.Nov.Chart.NDataPoint Then
                CType(arg.TargetNode, Nevron.Nov.Chart.NDataPoint).Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
                CType(arg.TargetNode, Nevron.Nov.Chart.NDataPoint).Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Blue)
            ElseIf TypeOf arg.TargetNode Is Nevron.Nov.Chart.NSeries Then
                CType(arg.TargetNode, Nevron.Nov.Chart.NSeries).Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_AxisCursorsTool As Nevron.Nov.Chart.Tools.NAxisCursorTool

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NHitTestingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
