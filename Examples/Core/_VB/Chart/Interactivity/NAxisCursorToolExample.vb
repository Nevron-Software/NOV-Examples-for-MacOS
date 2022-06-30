Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Tools
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis Cursor Tool Example
	''' </summary>
	Public Class NAxisCursorToolExample
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
            Nevron.Nov.Examples.Chart.NAxisCursorToolExample.NAxisCursorToolExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisCursorToolExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Cursor Tool"

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
            Me.m_AxisCursorsTool = New Nevron.Nov.Chart.Tools.NAxisCursorTool()
            Me.m_AxisCursorsTool.Enabled = True
            AddHandler Me.m_AxisCursorsTool.HorizontalValueChanged, AddressOf Me.OnAxisCursorsToolHorizontalValueChanged
            AddHandler Me.m_AxisCursorsTool.VerticalValueChanged, AddressOf Me.OnAxisCursorsToolVerticalValueChanged
            interactor.Add(Me.m_AxisCursorsTool)
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
            Dim autoHideCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Auto Hide")
            AddHandler autoHideCheckBox.CheckedChanged, AddressOf Me.OnAutoHideCheckBoxCheckedChanged
            stack.Add(autoHideCheckBox)
            Dim invertScaleCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Invert Scale")
            AddHandler invertScaleCheckBox.CheckedChanged, AddressOf Me.OnInvertScaleCheckBoxCheckedChanged
            stack.Add(invertScaleCheckBox)
            Me.m_HorizontalValueLabel = New Nevron.Nov.UI.NLabel()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Horizontal Value:", Me.m_HorizontalValueLabel))
            Me.m_VerticalValueLabel = New Nevron.Nov.UI.NLabel()
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Vertical Value:", Me.m_VerticalValueLabel))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to use the axis cursors tool.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAutoHideCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
        End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="arg"></param>
		Private Sub OnSnapToMajorTicksCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            If CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked Then
                Me.m_AxisCursorsTool.HorizontalValueSnapper = New Nevron.Nov.Chart.NAxisMajorTickSnapper()
                Me.m_AxisCursorsTool.VerticalValueSnapper = New Nevron.Nov.Chart.NAxisMajorTickSnapper()
            Else
                Me.m_AxisCursorsTool.HorizontalValueSnapper = Nothing
                Me.m_AxisCursorsTool.VerticalValueSnapper = Nothing
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

        Private Sub OnAxisCursorsToolVerticalValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim tool As Nevron.Nov.Chart.Tools.NAxisCursorTool = CType(arg.TargetNode, Nevron.Nov.Chart.Tools.NAxisCursorTool)

            If Double.IsNaN(tool.VerticalValue) Then
                Me.m_VerticalValueLabel.Text = String.Empty
            Else
                Me.m_VerticalValueLabel.Text = tool.VerticalValue.ToString()
            End If
        End Sub

        Private Sub OnAxisCursorsToolHorizontalValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim tool As Nevron.Nov.Chart.Tools.NAxisCursorTool = CType(arg.TargetNode, Nevron.Nov.Chart.Tools.NAxisCursorTool)

            If Double.IsNaN(tool.HorizontalValue) Then
                Me.m_HorizontalValueLabel.Text = String.Empty
            Else
                Me.m_HorizontalValueLabel.Text = tool.HorizontalValue.ToString()
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_AxisCursorsTool As Nevron.Nov.Chart.Tools.NAxisCursorTool
        Private m_HorizontalValueLabel As Nevron.Nov.UI.NLabel
        Private m_VerticalValueLabel As Nevron.Nov.UI.NLabel

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisCursorToolExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
