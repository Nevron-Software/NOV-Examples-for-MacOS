Imports System
Imports Nevron.Nov.Grid
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI
Imports Nevron.Nov.Chart

Namespace Nevron.Nov.Examples.Grid
    Public Class NCustomColumnFormatsExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' Default constructor.
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' Static constructor.
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Grid.NCustomColumnFormatsExample.NCustomColumnFormatsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NCustomColumnFormatsExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable(New Nevron.Nov.Data.NFieldInfo("Company", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("RegionSales", GetType(System.[Double]())))
            Dim rnd As System.Random = New System.Random()

            For i As Integer = 0 To 1000 - 1
                Dim arr As System.[Double]() = New System.[Double](9) {}

                For j As Integer = 0 To 10 - 1
                    arr(j) = rnd.[Next](100)
                Next

                dataTable.AddRow(NDummyDataSource.RandomCompanyName(), arr)
            Next

            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid
            AddHandler grid.AutoCreateColumn, Sub(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
                                                  If Equals(arg.DataColumn.FieldName, "RegionSales") Then
                                                      Dim pieColumnFormat As Nevron.Nov.Grid.NCustomColumnFormat = New Nevron.Nov.Grid.NCustomColumnFormat()
                                                      pieColumnFormat.FormatDefaultDataCellDelegate = Sub(ByVal theDataCell As Nevron.Nov.Grid.NDataCell)
                                                                                                          Dim widget As Nevron.Nov.UI.NWidget = New Nevron.Nov.UI.NWidget()
                                                                                                          widget.PreferredSize = New Nevron.Nov.Graphics.NSize(400, 300)
                                                                                                      End Sub

                                                      pieColumnFormat.CreateValueDataCellViewDelegate = Function(ByVal theDataCell As Nevron.Nov.Grid.NDataCell, ByVal value As Object)
                                                                                                            Dim values As Double() = CType(value, Double())
                                                                                                            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
                                                                                                            chartView.PreferredSize = New Nevron.Nov.Graphics.NSize(300, 60)
                                                                                                            Dim cartesianChart As Nevron.Nov.Chart.NCartesianChart = New Nevron.Nov.Chart.NCartesianChart()
                                                                                                            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(cartesianChart, Nevron.Nov.Layout.ENDockArea.Center)
                                                                                                            chartView.Surface.Content = cartesianChart
                                                                                                            cartesianChart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)
                                                                                                            cartesianChart.Legend = Nothing
                                                                                                            cartesianChart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Visible = False
                                                                                                            Dim yAxis As Nevron.Nov.Chart.NCartesianAxis = cartesianChart.Axes(Nevron.Nov.Chart.ENCartesianAxis.PrimaryY)
                                                                                                            Dim labelStyle As Nevron.Nov.Chart.NValueScaleLabelStyle = New Nevron.Nov.Chart.NValueScaleLabelStyle()
                                                                                                            labelStyle.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 8)
                                                                                                            CType(yAxis.Scale, Nevron.Nov.Chart.NLinearScale).Labels.Style = labelStyle
                                                                                                            Dim barSeries As Nevron.Nov.Chart.NBarSeries = New Nevron.Nov.Chart.NBarSeries()
                                                                                                            barSeries.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)
                                                                                                            barSeries.InflateMargins = False
                                                                                                            cartesianChart.Series.Add(barSeries)
                                                                                                            Dim count As Integer = values.Length

                                                                                                            For i As Integer = 0 To count - 1
                                                                                                                barSeries.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(values(i)))
                                                                                                            Next

                                                                                                            Return chartView
                                                                                                        End Function

                                                      arg.DataColumn.Format = pieColumnFormat
                                                  End If
                                              End Sub

            grid.DataSource = New Nevron.Nov.Data.NDataSource(dataTable)
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates custom column formatting.
</p>
<p>
    The custom column format is represented by the <b>NCustomColumnFormat</b> class. It exposes a set of delegates that you can hook to create your own widgets that represent the row values.
</p>
<p>
    In this example we are using the <b>NOV Chart for .NET</b> to display <b>Double[]</b> values.
<p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCustomColumnFormatsExample.
        ''' </summary>
        Public Shared ReadOnly NCustomColumnFormatsExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
