Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Custom Scale Breaks Example
	''' </summary>
	Public Class NCustomScaleBreaksExample
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
            Nevron.Nov.Examples.Chart.NCustomScaleBreaksExample.NCustomScaleBreaksExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NCustomScaleBreaksExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Custom Scale Breaks"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
			
			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)
            Dim random As System.Random = New System.Random()

			' create three random point series
			For i As Integer = 0 To 3 - 1
                Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
                point.UseXValues = True
                point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                point.Size = 5

				' fill in some random data
				For j As Integer = 0 To 30 - 1
                    point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(5 + random.[Next](90), 5 + random.[Next](90)))
                Next

                Me.m_Chart.Series.Add(point)
            Next

			' create scale breaks

			Dim xScale As Nevron.Nov.Chart.NScale = Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale
            Me.m_FirstHorzScaleBreak = Me.CreateCustomScaleBreak(Nevron.Nov.Graphics.NColor.Orange, New Nevron.Nov.Graphics.NRange(10, 20))
            xScale.ScaleBreaks.Add(Me.m_FirstHorzScaleBreak)
            Me.m_SecondHorzScaleBreak = Me.CreateCustomScaleBreak(Nevron.Nov.Graphics.NColor.Green, New Nevron.Nov.Graphics.NRange(80, 90))
            xScale.ScaleBreaks.Add(Me.m_SecondHorzScaleBreak)
            Dim yScale As Nevron.Nov.Chart.NScale = Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale
            Me.m_FirstVertScaleBreak = Me.CreateCustomScaleBreak(Nevron.Nov.Graphics.NColor.Red, New Nevron.Nov.Graphics.NRange(10, 20))
            yScale.ScaleBreaks.Add(Me.m_FirstVertScaleBreak)
            Me.m_SecondVertScaleBreak = Me.CreateCustomScaleBreak(Nevron.Nov.Graphics.NColor.Blue, New Nevron.Nov.Graphics.NRange(80, 90))
            yScale.ScaleBreaks.Add(Me.m_SecondVertScaleBreak)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            stack.Add(New Nevron.Nov.UI.NLabel("First Horizontal Scale Break"))
            Dim firstHScaleBreakBegin As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler firstHScaleBreakBegin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFirstHScaleBreakBeginValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin:", firstHScaleBreakBegin))
            Dim firstHScaleBreakEnd As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler firstHScaleBreakEnd.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFirstHScaleBreakEndValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End:", firstHScaleBreakEnd))
            stack.Add(New Nevron.Nov.UI.NLabel("Second Horizontal Scale Break"))
            Dim secondHScaleBreakBegin As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler secondHScaleBreakBegin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSecondHScaleBreakBeginValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin:", secondHScaleBreakBegin))
            Dim secondHScaleBreakEnd As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler secondHScaleBreakEnd.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSecondHScaleBreakEndValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End:", secondHScaleBreakEnd))
            stack.Add(New Nevron.Nov.UI.NLabel("First Vertical Scale Break"))
            Dim firstVScaleBreakBegin As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler firstVScaleBreakBegin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFirstVScaleBreakBeginValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin:", firstVScaleBreakBegin))
            Dim firstVScaleBreakEnd As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler firstVScaleBreakEnd.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFirstVScaleBreakEndValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End:", firstVScaleBreakEnd))
            stack.Add(New Nevron.Nov.UI.NLabel("Second Vertical Scale Break"))
            Dim secondVScaleBreakBegin As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler secondVScaleBreakBegin.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSecondVScaleBreakBeginValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Begin:", secondVScaleBreakBegin))
            Dim secondVScaleBreakEnd As Nevron.Nov.UI.NNumericUpDown = New Nevron.Nov.UI.NNumericUpDown()
            AddHandler secondVScaleBreakEnd.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSecondVScaleBreakEndValueChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("End:", secondVScaleBreakEnd))
            firstHScaleBreakBegin.Value = 10
            firstHScaleBreakEnd.Value = 20
            secondHScaleBreakBegin.Value = 80
            secondHScaleBreakEnd.Value = 90
            firstVScaleBreakBegin.Value = 10
            firstVScaleBreakEnd.Value = 20
            secondVScaleBreakBegin.Value = 80
            secondVScaleBreakEnd.Value = 90
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to add custom scale breaks.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateCustomScaleBreak(ByVal color As Nevron.Nov.Graphics.NColor, ByVal range As Nevron.Nov.Graphics.NRange) As Nevron.Nov.Chart.NCustomScaleBreak
            Dim scaleBreak As Nevron.Nov.Chart.NCustomScaleBreak = New Nevron.Nov.Chart.NCustomScaleBreak()
            scaleBreak.Fill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(color, 124))
            scaleBreak.Length = 10
            scaleBreak.Range = range
            Return scaleBreak
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSecondVScaleBreakEndValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_SecondVertScaleBreak.Range = New Nevron.Nov.Graphics.NRange(Me.m_SecondVertScaleBreak.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnSecondVScaleBreakBeginValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_SecondVertScaleBreak.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_SecondVertScaleBreak.Range.[End])
        End Sub

        Private Sub OnFirstVScaleBreakEndValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FirstVertScaleBreak.Range = New Nevron.Nov.Graphics.NRange(Me.m_FirstVertScaleBreak.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnFirstVScaleBreakBeginValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FirstVertScaleBreak.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_FirstVertScaleBreak.Range.[End])
        End Sub

        Private Sub OnSecondHScaleBreakEndValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_SecondHorzScaleBreak.Range = New Nevron.Nov.Graphics.NRange(Me.m_SecondHorzScaleBreak.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnSecondHScaleBreakBeginValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_SecondHorzScaleBreak.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_SecondHorzScaleBreak.Range.[End])
        End Sub

        Private Sub OnFirstHScaleBreakEndValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FirstHorzScaleBreak.Range = New Nevron.Nov.Graphics.NRange(Me.m_FirstHorzScaleBreak.Range.Begin, CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value)
        End Sub

        Private Sub OnFirstHScaleBreakBeginValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_FirstHorzScaleBreak.Range = New Nevron.Nov.Graphics.NRange(CType(arg.TargetNode, Nevron.Nov.UI.NNumericUpDown).Value, Me.m_FirstHorzScaleBreak.Range.[End])
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_FirstHorzScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak
        Private m_SecondHorzScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak
        Private m_FirstVertScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak
        Private m_SecondVertScaleBreak As Nevron.Nov.Chart.NCustomScaleBreak

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NCustomScaleBreaksExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
