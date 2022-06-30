Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Point Drop Lines Example
	''' </summary>
	Public Class NPointDropLinesExample
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
            Nevron.Nov.Examples.Chart.NPointDropLinesExample.NPointDropLinesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NPointDropLinesExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Point Drop Lines"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' setup X axis
			Dim scaleX As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleX.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleX.MajorGridLines.Visible = True
            scaleX.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scaleX

			' setup Y axis
			Dim scaleY As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            scaleY.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            scaleY.MajorGridLines.Visible = True
            scaleY.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scaleY

			' add a point series
			Me.m_Point = New Nevron.Nov.Chart.NPointSeries()
            Me.m_Point.Name = "Point Series"
            Me.m_Point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_Point.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_Point.Size = 10
            Me.m_Point.Shape = Nevron.Nov.Chart.ENPointShape.Ellipse
            Me.m_Point.UseXValues = True
            Me.m_Chart.Series.Add(Me.m_Point)

            For i As Integer = 0 To 360 - 1 Step 5
                Dim value As Double = System.Math.Sin(Nevron.Nov.NAngle.Degree2Rad * i)
                Me.m_Point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(i, value))
            Next

            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim showVerticalDropLinesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Vertical Drop Lines")
            AddHandler showVerticalDropLinesCheckBox.CheckedChanged, AddressOf Me.OnShowVerticalDropLinesCheckBoxCheckedChanged
            stack.Add(showVerticalDropLinesCheckBox)
            Dim verticalDropLinesOriginModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            verticalDropLinesOriginModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENDropLineOriginMode)()
            AddHandler verticalDropLinesOriginModeCombo.SelectedIndexChanged, AddressOf Me.OnVerticalDropLinesOriginModeComboSelectedIndexChanged
            verticalDropLinesOriginModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENDropLineOriginMode.CustomValue)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin Mode:", verticalDropLinesOriginModeCombo))
            Dim verticalDropLinesOriginUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler verticalDropLinesOriginUpDown.ValueChanged, AddressOf Me.OnVerticalDropLinesOriginUpDownValueChanged
            verticalDropLinesOriginUpDown.Value = 180.0
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin", verticalDropLinesOriginUpDown))
            Dim showHorizontalDropLinesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Horizontal Drop Lines")
            AddHandler showHorizontalDropLinesCheckBox.CheckedChanged, AddressOf Me.OnShowHorizontalDropLinesCheckBoxCheckedChanged
            showHorizontalDropLinesCheckBox.Checked = True
            stack.Add(showHorizontalDropLinesCheckBox)
            Dim horizontalDropLinesOriginModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            horizontalDropLinesOriginModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENDropLineOriginMode)()
            AddHandler horizontalDropLinesOriginModeCombo.SelectedIndexChanged, AddressOf Me.OnHorizontalDropLinesOriginModeComboSelectedIndexChanged
            horizontalDropLinesOriginModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENDropLineOriginMode.CustomValue)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin Mode:", horizontalDropLinesOriginModeCombo))
            Dim horizontalDropLinesOriginUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler horizontalDropLinesOriginUpDown.ValueChanged, AddressOf Me.OnHorizontalDropLinesOriginUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Origin:", horizontalDropLinesOriginUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to enable point drop lines.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

        Private Sub OnHorizontalDropLinesOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.HorizontalDropLineOrigin = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnVerticalDropLinesOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.VerticalDropLineOrigin = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnHorizontalDropLinesOriginModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.HorizontalDropLineOriginMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENDropLineOriginMode)
        End Sub

        Private Sub OnVerticalDropLinesOriginModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.VerticalDropLineOriginMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENDropLineOriginMode)
        End Sub

        Private Sub OnShowHorizontalDropLinesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.ShowHorizontalDropLines = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

        Private Sub OnShowVerticalDropLinesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Point.ShowVerticalDropLines = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Point As Nevron.Nov.Chart.NPointSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NPointDropLinesExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
