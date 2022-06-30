Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Axis sections example
	''' </summary>
	Public Class NAxisSectionsExample
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
            Nevron.Nov.Examples.Chart.NAxisSectionsExample.NAxisSectionsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NAxisSectionsExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Axis Sections"
            Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XYLinear)

			' create a point series
			Dim point As Nevron.Nov.Chart.NPointSeries = New Nevron.Nov.Chart.NPointSeries()
            point.Name = "Point Series"
            point.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            point.Size = 5
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 30 - 1
                point.DataPoints.Add(New Nevron.Nov.Chart.NPointDataPoint(random.[Next](100), random.[Next](100)))
            Next

            point.InflateMargins = True
            Me.m_Chart.Series.Add(point)

			' tell the x axis to display major and minor grid lines
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = New Nevron.Nov.Chart.NLinearScale()
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = linearScale
            linearScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            linearScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Solid
            linearScale.MinorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            linearScale.MinorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            linearScale.MinorTickCount = 1

			' tell the y axis to display major and minor grid lines
			linearScale = New Nevron.Nov.Chart.NLinearScale()
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale = linearScale
            linearScale.MajorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            linearScale.MajorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Solid
            linearScale.MinorGridLines = New Nevron.Nov.Chart.NScaleGridLines()
            linearScale.MinorGridLines.Stroke.DashStyle = Nevron.Nov.Graphics.ENDashStyle.Dot
            linearScale.MinorTickCount = 1
            Dim labelStyle As Nevron.Nov.UI.NTextStyle

			' configure the first horizontal section
			Me.m_FirstHorizontalSection = New Nevron.Nov.Chart.NScaleSection()
            Me.m_FirstHorizontalSection.Range = New Nevron.Nov.Graphics.NRange(2, 8)
            Me.m_FirstHorizontalSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Red, 60))
            Me.m_FirstHorizontalSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Red)
            Me.m_FirstHorizontalSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkRed)
            Me.m_FirstHorizontalSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dot)
            labelStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.DarkRed)
            labelStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            Me.m_FirstHorizontalSection.LabelTextStyle = labelStyle

			' configure the second horizontal section
			Me.m_SecondHorizontalSection = New Nevron.Nov.Chart.NScaleSection()
            Me.m_SecondHorizontalSection.Range = New Nevron.Nov.Graphics.NRange(14, 18)
            Me.m_SecondHorizontalSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Green, 60))
            Me.m_SecondHorizontalSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Green)
            Me.m_SecondHorizontalSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkGreen)
            Me.m_SecondHorizontalSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.ENDashStyle.Dot)
            labelStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.DarkGreen)
            labelStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            Me.m_SecondHorizontalSection.LabelTextStyle = labelStyle

			' configure the first vertical section
			Me.m_FirstVerticalSection = New Nevron.Nov.Chart.NScaleSection()
            Me.m_FirstVerticalSection.Range = New Nevron.Nov.Graphics.NRange(20, 40)
            Me.m_FirstVerticalSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Blue, 60))
            Me.m_FirstVerticalSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Blue)
            Me.m_FirstVerticalSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkBlue)
            Me.m_FirstVerticalSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.ENDashStyle.Dot)
            labelStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.DarkBlue)
            labelStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            Me.m_FirstVerticalSection.LabelTextStyle = labelStyle

			' configure the second vertical section
			Me.m_SecondVerticalSection = New Nevron.Nov.Chart.NScaleSection()
            Me.m_SecondVerticalSection.Range = New Nevron.Nov.Graphics.NRange(70, 90)
            Me.m_SecondVerticalSection.RangeFill = New Nevron.Nov.Graphics.NColorFill(New Nevron.Nov.Graphics.NColor(Nevron.Nov.Graphics.NColor.Orange, 60))
            Me.m_SecondVerticalSection.MajorGridStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Orange)
            Me.m_SecondVerticalSection.MajorTickStroke = New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkOrange)
            Me.m_SecondVerticalSection.MinorTickStroke = New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Orange, Nevron.Nov.Graphics.ENDashStyle.Dot)
            labelStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Orange, Nevron.Nov.Graphics.NColor.DarkOrange)
            labelStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            Me.m_SecondVerticalSection.LabelTextStyle = labelStyle
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_ShowFirstYSectionCheckBox = New Nevron.Nov.UI.NCheckBox("Show Y Section [20, 40]")
            stack.Add(Me.m_ShowFirstYSectionCheckBox)
            Me.m_ShowFirstYSectionCheckBox.Checked = True
            Me.m_ShowSecondYSectionCheckBox = New Nevron.Nov.UI.NCheckBox("Show Y Section [70, 90]")
            stack.Add(Me.m_ShowSecondYSectionCheckBox)
            Me.m_ShowSecondYSectionCheckBox.Checked = True
            Me.m_ShowFirstXSectionCheckBox = New Nevron.Nov.UI.NCheckBox("Show X Section [2, 8]")
            stack.Add(Me.m_ShowFirstXSectionCheckBox)
            Me.m_ShowFirstXSectionCheckBox.Checked = True
            Me.m_ShowSecondXSectionCheckBox = New Nevron.Nov.UI.NCheckBox("Show X Section [14, 18]")
            stack.Add(Me.m_ShowSecondXSectionCheckBox)
            Me.m_ShowSecondXSectionCheckBox.Checked = True
            AddHandler Me.m_ShowFirstYSectionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateSections)
            AddHandler Me.m_ShowSecondYSectionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateSections)
            AddHandler Me.m_ShowFirstXSectionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateSections)
            AddHandler Me.m_ShowSecondXSectionCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateSections)
            Me.OnUpdateSections(Nothing)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates axis sections. Axis sections allow you to alter the appearance of different axis elements if they fall in a specified range.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnUpdateSections(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim standardScale As Nevron.Nov.Chart.NStandardScale

			' configure horizontal axis sections
			standardScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NStandardScale)
            standardScale.Sections.Clear()

            If Me.m_ShowFirstXSectionCheckBox.Checked Then
                standardScale.Sections.Add(Me.m_FirstHorizontalSection)
            End If

            If Me.m_ShowSecondXSectionCheckBox.Checked Then
                standardScale.Sections.Add(Me.m_SecondHorizontalSection)
            End If

			' configure vertical axis sections
			standardScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NStandardScale)
            standardScale.Sections.Clear()

            If Me.m_ShowFirstYSectionCheckBox.Checked Then
                standardScale.Sections.Add(Me.m_FirstVerticalSection)
            End If

            If Me.m_ShowSecondYSectionCheckBox.Checked Then
                standardScale.Sections.Add(Me.m_SecondVerticalSection)
            End If
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_ShowFirstYSectionCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ShowSecondYSectionCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ShowFirstXSectionCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ShowSecondXSectionCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_FirstVerticalSection As Nevron.Nov.Chart.NScaleSection
        Private m_SecondVerticalSection As Nevron.Nov.Chart.NScaleSection
        Private m_FirstHorizontalSection As Nevron.Nov.Chart.NScaleSection
        Private m_SecondHorizontalSection As Nevron.Nov.Chart.NScaleSection

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NAxisSectionsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
