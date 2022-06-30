Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Standard Funnel Example
	''' </summary>
	Public Class NStandardFunnelExample
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
            Nevron.Nov.Examples.Chart.NStandardFunnelExample.NStandardFunnelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardFunnelExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NStandardFunnelExample.CreateFunnelChartView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard Funnel"
            Dim funnelChart As Nevron.Nov.Chart.NFunnelChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NFunnelChart)
            Me.m_FunnelSeries = New Nevron.Nov.Chart.NFunnelSeries()
            funnelChart.Series.Add(Me.m_FunnelSeries)
            Me.m_FunnelSeries.DataPoints.Add(New Nevron.Nov.Chart.NFunnelDataPoint(20.0, "Awareness"))
            Me.m_FunnelSeries.DataPoints.Add(New Nevron.Nov.Chart.NFunnelDataPoint(10.0, "First Hear"))
            Me.m_FunnelSeries.DataPoints.Add(New Nevron.Nov.Chart.NFunnelDataPoint(15.0, "Further Learn"))
            Me.m_FunnelSeries.DataPoints.Add(New Nevron.Nov.Chart.NFunnelDataPoint(7.0, "Liking"))
            Me.m_FunnelSeries.DataPoints.Add(New Nevron.Nov.Chart.NFunnelDataPoint(28.0, "Decision"))
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, True))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim funnelShapeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            funnelShapeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENFunnelShape)()
            AddHandler funnelShapeCombo.SelectedIndexChanged, AddressOf Me.OnFunnelShapeComboSelectedIndexChanged
            funnelShapeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENFunnelShape.Trapezoid)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Funnel Shape:", funnelShapeCombo))
            Dim labelAligmentModeCombo As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            labelAligmentModeCombo.FillFromEnum(Of Nevron.Nov.Chart.ENFunnelLabelMode)()
            AddHandler labelAligmentModeCombo.SelectedIndexChanged, AddressOf Me.OnLabelAligmentModeComboSelectedIndexChanged
            labelAligmentModeCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENFunnelLabelMode.Center)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Label Alignment:", labelAligmentModeCombo))
            Dim labelArrowLengthUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            labelArrowLengthUpDown.Value = Me.m_FunnelSeries.LabelArrowLength
            AddHandler labelArrowLengthUpDown.ValueChanged, AddressOf Me.OnLabelArrowLengthUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Label Arrow Length:", labelArrowLengthUpDown))
            Dim pointGapUpDown As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            pointGapUpDown.Value = Me.m_FunnelSeries.PointGapPercent
            AddHandler pointGapUpDown.ValueChanged, AddressOf Me.OnPointGapUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Point Gap Percent:", pointGapUpDown))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard funnel chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLabelArrowLengthUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FunnelSeries.LabelArrowLength = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnLabelAligmentModeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FunnelSeries.LabelMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENFunnelLabelMode)
        End Sub

        Private Sub OnPointGapUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FunnelSeries.PointGapPercent = CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value
        End Sub

        Private Sub OnFunnelShapeComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FunnelSeries.Shape = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENFunnelShape)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_FunnelSeries As Nevron.Nov.Chart.NFunnelSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardFunnelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateFunnelChartView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Funnel)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
