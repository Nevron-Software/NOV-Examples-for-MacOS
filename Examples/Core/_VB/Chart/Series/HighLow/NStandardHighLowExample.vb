Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard HighLow Example
	''' </summary>
	Public Class NStandardHighLowExample
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
            Nevron.Nov.Examples.Chart.NStandardHighLowExample.NStandardHighLowExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardHighLowExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard High Low"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = TryCast(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim strip As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.ENNamedColor.Beige), Nothing, True, 0, 0, 1, 1)
            strip.Interlaced = True
            Me.m_HighLow = New Nevron.Nov.Chart.NHighLowSeries()
            Me.m_HighLow.Name = "High-Low Series"
            Me.m_HighLow.HighFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Gray)
            Me.m_HighLow.LowFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Orange)
            Me.m_HighLow.HighStroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Black)
            Me.m_HighLow.LowStroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Red)
            Me.m_HighLow.Stroke = New Nevron.Nov.Graphics.NStroke(2, Nevron.Nov.Graphics.NColor.Black)
            Me.m_HighLow.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            Me.m_HighLow.Palette = New Nevron.Nov.Chart.NTwoColorPalette(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Green)
            Me.GenerateData()
            Me.m_Chart.Series.Add(Me.m_HighLow)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim appearanceModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            appearanceModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENHighLowAppearanceMode)()
            AddHandler appearanceModeComboBox.SelectedIndexChanged, AddressOf Me.OnAppearanceModeComboBoxSelectedIndexChanged
            appearanceModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENHighLowAppearanceMode.HighLow)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Appearance Mode:", appearanceModeComboBox))
            Dim showDropLinesCheckBox As Nevron.Nov.UI.NCheckBox = New Nevron.Nov.UI.NCheckBox("Show Droplines")
            AddHandler showDropLinesCheckBox.CheckedChanged, AddressOf Me.OnShowDropLinesCheckBoxCheckedChanged
            stack.Add(showDropLinesCheckBox)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard high low series.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub GenerateData()
            Me.m_HighLow.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 20 - 1
                Dim d1 As Double = System.Math.Log(i + 1) + 0.1 * random.NextDouble()
                Dim d2 As Double = d1 + System.Math.Cos(0.33 * i) + 0.1 * random.NextDouble()
                Me.m_HighLow.DataPoints.Add(New Nevron.Nov.Chart.NHighLowDataPoint(d1, d2))
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnAppearanceModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HighLow.AppearanceMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENHighLowAppearanceMode)
        End Sub

        Private Sub OnShowDropLinesCheckBoxCheckedChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_HighLow.ShowDropLines = CType(arg.TargetNode, Nevron.Nov.UI.NCheckBox).Checked
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_HighLow As Nevron.Nov.Chart.NHighLowSeries
        Private m_Chart As Nevron.Nov.Chart.NCartesianChart

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardHighLowExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
