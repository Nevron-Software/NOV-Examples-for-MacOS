Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Tools
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Rectangle Zoom Tool Example
	''' </summary>
	Public Class NZoomingAndScrollingExample
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
            Nevron.Nov.Examples.Chart.NZoomingAndScrollingExample.NZoomingAndScrollingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NZoomingAndScrollingExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Rectangle Zoom Tool"

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
            Me.m_Point.Shape = Nevron.Nov.Chart.ENPointShape.Rectangle
            Me.m_Point.UseXValues = True
            Me.m_Chart.Series.Add(Me.m_Point)

			' add some sample data
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

            Me.m_Chart.Enabled = True
            Dim interactor As Nevron.Nov.UI.NInteractor = New Nevron.Nov.UI.NInteractor()
            Me.m_RectangleZoomTool = New Nevron.Nov.Chart.Tools.NRectangleZoomTool()
            Me.m_RectangleZoomTool.Enabled = True
            interactor.Add(Me.m_RectangleZoomTool)
            Dim dataPanTool As Nevron.Nov.Chart.Tools.NDataPanTool = New Nevron.Nov.Chart.Tools.NDataPanTool()
            dataPanTool.StartMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonDown
            dataPanTool.EndMouseButtonEvent = Nevron.Nov.UI.ENMouseButtonEvent.RightButtonUp
            dataPanTool.Enabled = True
            interactor.Add(dataPanTool)
            Me.m_Chart.Interactor = interactor
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim orientationComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            orientationComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENCartesianChartOrientation)()
            orientationComboBox.SelectedIndex = CInt(Me.m_Chart.Orientation)
            AddHandler orientationComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnOrientationComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Orientation:", orientationComboBox))
            Dim snapToMajorTicksCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Snap To Major Ticks")
            AddHandler snapToMajorTicksCheckBox.CheckedChanged, AddressOf Me.OnSnapToMajorTicksCheckBoxCheckedChanged
            stack.Add(snapToMajorTicksCheckBox)
            Dim invertScaleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Invert Scale")
            AddHandler invertScaleCheckBox.CheckedChanged, AddressOf Me.OnInvertScaleCheckBoxCheckedChanged
            stack.Add(invertScaleCheckBox)
            Dim showScrollbarsWhenZoomedCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Scrollbars When Zoomed")
            showScrollbarsWhenZoomedCheckBox.Checked = True
            AddHandler showScrollbarsWhenZoomedCheckBox.CheckedChanged, AddressOf Me.OnShowScrollbarsWhenZoomedCheckBoxCheckedChanged
            stack.Add(showScrollbarsWhenZoomedCheckBox)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to implement zooming and scrolling. Press the left mouse button over the chart and select an area.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSnapToMajorTicksCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_RectangleZoomTool.HorizontalValueSnapper = New Nevron.Nov.Chart.NAxisMajorTickSnapper()
                Me.m_RectangleZoomTool.VerticalValueSnapper = New Nevron.Nov.Chart.NAxisMajorTickSnapper()
            Else
                Me.m_RectangleZoomTool.HorizontalValueSnapper = Nothing
                Me.m_RectangleZoomTool.VerticalValueSnapper = Nothing
            End If
        End Sub

        Private Sub OnInvertScaleCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            For i As Integer = 0 To Me.m_Chart.Axes.Count - 1
                Me.m_Chart.Axes(CInt((i))).Scale.Invert = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Next
        End Sub

        Private Sub OnOrientationComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Chart.Orientation = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENCartesianChartOrientation)
        End Sub

        Private Sub OnShowScrollbarsWhenZoomedCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim showScrollbars As Boolean = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).ShowScrollbarWhenZoomed = showScrollbars
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).ShowScrollbarWhenZoomed = showScrollbars
        End Sub


		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_RectangleZoomTool As Nevron.Nov.Chart.Tools.NRectangleZoomTool

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NZoomingAndScrollingExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
