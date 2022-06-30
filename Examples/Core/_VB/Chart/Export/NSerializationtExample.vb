Imports System
Imports System.IO
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Chart.Formats
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Serialization Example
	''' </summary>
	Public Class NSerializationExample
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
            Nevron.Nov.Examples.Chart.NSerializationExample.NSerializationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NSerializationExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_ChartView = New Nevron.Nov.Chart.NChartView()
            Me.m_ChartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			Me.m_ChartView.Surface.Titles(CInt((0))).Text = "Serialization"

			' configure chart
			Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)
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
            Me.m_Bar1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            chart.Series.Add(Me.m_Bar1)

			' add the second bar
			Me.m_Bar2 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar2.Name = "Bar2"
            Me.m_Bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            Me.m_Bar2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            chart.Series.Add(Me.m_Bar2)

			' add the third bar
			Me.m_Bar3 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Bar3.Name = "Bar3"
            Me.m_Bar3.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Stacked
            Me.m_Bar3.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
            chart.Series.Add(Me.m_Bar3)
            Me.m_ChartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Me.FillRandomData()
            Return Me.m_ChartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim changeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change Data")
            AddHandler changeDataButton.Click, AddressOf Me.OnChangeDataButtonClick
            stack.Add(changeDataButton)
            Dim saveStateToFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save State To File...")
            AddHandler saveStateToFileButton.Click, AddressOf Me.OnSaveStateToFileButtonClick
            stack.Add(saveStateToFileButton)
            Dim loadStateFromFileButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Load State From File...")
            AddHandler loadStateFromFileButton.Click, AddressOf Me.OnLoadStateFromFileButtonClick
            stack.Add(loadStateFromFileButton)
            Dim saveStateToStreamButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save State To Stream")
            AddHandler saveStateToStreamButton.Click, AddressOf Me.OnSaveStateToStreamButtonClick
            stack.Add(saveStateToStreamButton)
            Dim loadStateFromStreamButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Load State from Stream")
            AddHandler loadStateFromStreamButton.Click, AddressOf Me.OnLoadStateFromStreamButtonClick
            stack.Add(loadStateFromStreamButton)
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to save / load the chart state from a file or stream.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		''' <summary>
		''' 
		''' </summary>
		Private Sub FillRandomData()
            Dim barSeriesList As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Chart.NBarSeries) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Chart.NBarSeries)()

			' collect all bar series in the view
			For chartIndex As Integer = 0 To Me.m_ChartView.Surface.Charts.Length - 1
                Dim chart As Nevron.Nov.Chart.NCartesianChart = CType(Me.m_ChartView.Surface.Charts(chartIndex), Nevron.Nov.Chart.NCartesianChart)

                For seriesIndex As Integer = 0 To chart.Series.Count - 1
                    Dim barSeries As Nevron.Nov.Chart.NBarSeries = TryCast(chart.Series(seriesIndex), Nevron.Nov.Chart.NBarSeries)

                    If barSeries IsNot Nothing Then
                        barSeriesList.Add(barSeries)
                        barSeries.DataPoints.Clear()
                    End If
                Next
            Next

			' fill all bar series with random data
			Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 5 - 1

                For j As Integer = 0 To barSeriesList.Count - 1
                    Dim barSeries As Nevron.Nov.Chart.NBarSeries = barSeriesList(j)
                    barSeries.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 100)))
                Next
            Next
        End Sub

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLoadStateFromFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_ChartView.OpenFile()
        End Sub

        Private Sub OnSaveStateToFileButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_ChartView.SaveAs()
        End Sub

        Private Sub OnLoadStateFromStreamButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            If Me.m_Stream IsNot Nothing Then
                Me.m_Stream.Seek(0, System.IO.SeekOrigin.Begin)
                Me.m_ChartView.LoadFromStream(Me.m_Stream)
            End If
        End Sub

        Private Sub OnSaveStateToStreamButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.m_Stream = New System.IO.MemoryStream()
            Me.m_ChartView.SaveToStream(Me.m_Stream, Nevron.Nov.Chart.Formats.NChartFormat.NevronXml)
        End Sub

        Private Sub OnChangeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.FillRandomData()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_ChartView As Nevron.Nov.Chart.NChartView
        Private m_Bar1 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar2 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar3 As Nevron.Nov.Chart.NBarSeries
        Private m_Stream As System.IO.MemoryStream

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NSerializationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
