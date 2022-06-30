Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Stacked Bar Example
	''' </summary>
	Public Class NStackedBarExample
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
            Nevron.Nov.Examples.Chart.NStackedBarExample.NStackedBarExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStackedBarExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Stacked Bar"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
            chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add interlace stripe
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            linearScale.Strips.Add(stripStyle)

			' add the first bar
			Me.m_Bar1 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar1.Name = "Bar1"
            Me.m_Bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            chart.Series.Add(Me.m_Bar1)

			' add the second bar
			Me.m_Bar2 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar2.Name = "Bar2"
            Me.m_Bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            chart.Series.Add(Me.m_Bar2)

			' add the third bar
			Me.m_Bar3 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar3.Name = "Bar3"
            Me.m_Bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            chart.Series.Add(Me.m_Bar3)

			' setup value formatting
			Me.m_Bar1.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            Me.m_Bar2.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")
            Me.m_Bar3.ValueFormatter = New Nevron.Nov.Dom.NNumericValueFormatter("0.###")

			' position data labels in the center of the bars
			Me.m_Bar1.DataLabelStyle = Me.CreateDataLabelStyle()
            Me.m_Bar2.DataLabelStyle = Me.CreateDataLabelStyle()
            Me.m_Bar3.DataLabelStyle = Me.CreateDataLabelStyle()
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))

			' pass some data
			Me.OnPositiveDataButtonClick(Nothing)
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim firstBarLabelFormatComboBox As Nevron.Nov.UI.NComboBox = Me.CreateLabelFormatComboBox()
            AddHandler firstBarLabelFormatComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFirstBarLabelFormatComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("First Bar Label Format: ", firstBarLabelFormatComboBox))
            Dim secondBarLabelFormatComboBox As Nevron.Nov.UI.NComboBox = Me.CreateLabelFormatComboBox()
            AddHandler secondBarLabelFormatComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnSecondBarLabelFormatComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Second Bar Label Format: ", secondBarLabelFormatComboBox))
            Dim thirdBarLabelFormatComboBox As Nevron.Nov.UI.NComboBox = Me.CreateLabelFormatComboBox()
            AddHandler thirdBarLabelFormatComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnThirdBarLabelFormatComboBoxSelectedIndexChanged)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Third Bar Label Format: ", thirdBarLabelFormatComboBox))
            Dim positiveDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive Values")
            AddHandler positiveDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveDataButtonClick)
            stack.Add(positiveDataButton)
            Dim positiveAndNegativeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Positive and Negative Values")
            AddHandler positiveAndNegativeDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnPositiveAndNegativeDataButtonClick)
            stack.Add(positiveAndNegativeDataButton)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a stacked bar chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnFirstBarLabelFormatComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(arg.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_Bar1.DataLabelStyle.Format = CStr(comboBox.Items(CInt((comboBox.SelectedIndex))).Tag)
        End Sub

        Private Sub OnSecondBarLabelFormatComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(arg.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_Bar2.DataLabelStyle.Format = CStr(comboBox.Items(CInt((comboBox.SelectedIndex))).Tag)
        End Sub

        Private Sub OnThirdBarLabelFormatComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim comboBox As Nevron.Nov.UI.NComboBox = CType(arg.TargetNode, Nevron.Nov.UI.NComboBox)
            Me.m_Bar3.DataLabelStyle.Format = CStr(comboBox.Items(CInt((comboBox.SelectedIndex))).Tag)
        End Sub

        Private Sub OnPositiveAndNegativeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar1.DataPoints.Clear()
            Me.m_Bar2.DataPoints.Clear()
            Me.m_Bar3.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100) - 50))
                Me.m_Bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100) - 50))
                Me.m_Bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](100) - 50))
            Next
        End Sub

        Private Sub OnPositiveDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Bar1.DataPoints.Clear()
            Me.m_Bar2.DataPoints.Clear()
            Me.m_Bar3.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 12 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
                Me.m_Bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
                Me.m_Bar3.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](90) + 10))
            Next
        End Sub

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' Creates a new data label style object
		''' </summary>
		''' <returns></returns>
		Private Function CreateDataLabelStyle() As Nevron.Nov.Chart.NDataLabelStyle
            Dim dataLabelStyle As Nevron.Nov.Chart.NDataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle()
            dataLabelStyle.VertAlign = Nevron.Nov.ENVerticalAlignment.Center
            dataLabelStyle.ArrowLength = 0
            Return dataLabelStyle
        End Function
		''' <summary>
		''' Gets a format string from the specified index
		''' </summary>
		''' <paramname="index"></param>
		''' <returns></returns>
		Private Function GetFormatStringFromIndex(ByVal index As Integer) As String
            Select Case index
                Case 0
                    Return "<value>"
                Case 1
                    Return "<total>"
                Case 2
                    Return "<cumulative>"
                Case 3
                    Return "<percent>"
                Case Else
                    Return ""
            End Select
        End Function
		''' <summary>
		''' Creates a label format combo box
		''' </summary>
		''' <returns></returns>
		Private Function CreateLabelFormatComboBox() As Nevron.Nov.UI.NComboBox
            Dim comboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            Dim comboBoxItem As Nevron.Nov.UI.NComboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Value")
            comboBoxItem.Tag = "<value>"
            comboBox.Items.Add(comboBoxItem)
            comboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Total")
            comboBoxItem.Tag = "<total>"
            comboBox.Items.Add(comboBoxItem)
            comboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Cumulative")
            comboBoxItem.Tag = "<cumulative>"
            comboBox.Items.Add(comboBoxItem)
            comboBoxItem = New Nevron.Nov.UI.NComboBoxItem("Percent")
            comboBoxItem.Tag = "<percent>"
            comboBox.Items.Add(comboBoxItem)
            comboBox.SelectedIndex = 0
            Return comboBox
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Bar1 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar2 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar3 As Nevron.Nov.Chart.NBarSeries

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStackedBarExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
