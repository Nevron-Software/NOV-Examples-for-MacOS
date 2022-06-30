Imports System
Imports System.Globalization
Imports System.IO
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI
Imports Nevron.Nov.Xml

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' TreeMap Legend Example
	''' </summary>
	Public Class NTreeMapLegendExample
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
            Nevron.Nov.Examples.Chart.NTreeMapLegendExample.NTreeMapLegendExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NTreeMapLegendExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NTreeMapLegendExample.CreateTreeMapView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "TreeMap Legend"
            Dim treeMap As Nevron.Nov.Chart.NTreeMapChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NTreeMapChart)

			' Get the country list XML stream
			Dim stream As System.IO.Stream = NResources.Instance.GetResourceStream("RSTR_TreeMapDataSmall_xml")

			' Load an xml document from the stream
			Dim xmlDocument As Nevron.Nov.Xml.NXmlDocument = Nevron.Nov.Xml.NXmlDocument.LoadFromStream(stream)
            Me.m_RootTreeMapNode = New Nevron.Nov.Chart.NGroupTreeMapNode()
            Dim palette As Nevron.Nov.Chart.NThreeColorPalette = New Nevron.Nov.Chart.NThreeColorPalette()
            palette.OriginColor = Nevron.Nov.Graphics.NColor.White
            palette.BeginColor = Nevron.Nov.Graphics.NColor.Red
            palette.EndColor = Nevron.Nov.Graphics.NColor.Green
            Me.m_RootTreeMapNode.Label = "Tree Map - Industry by Sector"
            Me.m_RootTreeMapNode.Palette = palette
            Me.m_RootTreeMapNode.LegendView = New Nevron.Nov.Chart.NGroupTreeMapNodeLegendView()
            treeMap.RootTreeMapNode = Me.m_RootTreeMapNode
            Dim rootElement As Nevron.Nov.Xml.NXmlElement = CType(xmlDocument.GetChildAt(0), Nevron.Nov.Xml.NXmlElement)

            For i As Integer = 0 To rootElement.ChildrenCount - 1
                Dim industry As Nevron.Nov.Xml.NXmlElement = CType(rootElement.GetChildAt(i), Nevron.Nov.Xml.NXmlElement)
                Dim treeMapSeries As Nevron.Nov.Chart.NGroupTreeMapNode = New Nevron.Nov.Chart.NGroupTreeMapNode()
                treeMapSeries.BorderThickness = New Nevron.Nov.Graphics.NMargins(4.0)
                treeMapSeries.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
                treeMapSeries.Padding = New Nevron.Nov.Graphics.NMargins(2.0)
                Me.m_RootTreeMapNode.ChildNodes.Add(treeMapSeries)
                treeMapSeries.Label = industry.GetAttributeValue("Name")
                treeMapSeries.Tooltip = New Nevron.Nov.UI.NTooltip(treeMapSeries.Label)

                For j As Integer = 0 To industry.ChildrenCount - 1
                    Dim company As Nevron.Nov.Xml.NXmlElement = CType(industry.GetChildAt(j), Nevron.Nov.Xml.NXmlElement)
                    Dim value As Double = System.[Double].Parse(company.GetAttributeValue("Size"), System.Globalization.CultureInfo.InvariantCulture)
                    Dim change As Double = System.[Double].Parse(company.GetAttributeValue("Change"), System.Globalization.CultureInfo.InvariantCulture)
                    Dim label As String = company.GetAttributeValue("Name")
                    Dim node As Nevron.Nov.Chart.NValueTreeMapNode = New Nevron.Nov.Chart.NValueTreeMapNode(value, change, label)
                    node.Format = "<label> <change_percent>"
                    node.ChangeValueType = Nevron.Nov.Chart.ENChangeValueType.Percentage
                    node.Tooltip = New Nevron.Nov.UI.NTooltip(label)
                    treeMapSeries.ChildNodes.Add(node)
                Next
            Next

            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim group As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Dim legendModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            legendModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENTreeMapNodeLegendMode)()
            AddHandler legendModeComboBox.SelectedIndexChanged, AddressOf Me.OnLegendModeComboBoxSelectedIndexChanged
            legendModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENTreeMapNodeLegendMode.Group)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Legend Mode:", legendModeComboBox))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to control the treemap legend.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnLegendModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RootTreeMapNode.LegendView.Mode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENTreeMapNodeLegendMode)

            Select Case Me.m_RootTreeMapNode.LegendView.Mode
                Case Nevron.Nov.Chart.ENTreeMapNodeLegendMode.None, Nevron.Nov.Chart.ENTreeMapNodeLegendMode.Group, Nevron.Nov.Chart.ENTreeMapNodeLegendMode.ValueNodes, Nevron.Nov.Chart.ENTreeMapNodeLegendMode.GroupAndChildNodes
                    Me.m_RootTreeMapNode.LegendView.Format = "<label> <value>"
                Case Nevron.Nov.Chart.ENTreeMapNodeLegendMode.Palette
                    Me.m_RootTreeMapNode.LegendView.Format = "<change_begin>"
            End Select
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RootTreeMapNode As Nevron.Nov.Chart.NGroupTreeMapNode

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTreeMapLegendExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Private Shared Function CreateTreeMapView() As Nevron.Nov.Chart.NChartView
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.TreeMap)
            Return chartView
        End Function

		#EndRegion
	End Class
End Namespace
