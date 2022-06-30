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
	''' Standard TreeMap Example
	''' </summary>
	Public Class NStandardTreeMapExample
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
            Nevron.Nov.Examples.Chart.NStandardTreeMapExample.NStandardTreeMapExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NStandardTreeMapExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = Nevron.Nov.Examples.Chart.NStandardTreeMapExample.CreateTreeMapView()

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Standard TreeMap"
            Dim treeMap As Nevron.Nov.Chart.NTreeMapChart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NTreeMapChart)

			' Get the country list XML stream
			Dim stream As System.IO.Stream = NResources.Instance.GetResourceStream("RSTR_TreeMapData_xml")

			' Load an xml document from the stream
			Dim xmlDocument As Nevron.Nov.Xml.NXmlDocument = Nevron.Nov.Xml.NXmlDocument.LoadFromStream(stream)
            Me.m_RootTreeMapNode = New Nevron.Nov.Chart.NGroupTreeMapNode()
            Dim palette As Nevron.Nov.Chart.NThreeColorPalette = New Nevron.Nov.Chart.NThreeColorPalette()
            palette.OriginColor = Nevron.Nov.Graphics.NColor.White
            palette.BeginColor = Nevron.Nov.Graphics.NColor.Red
            palette.EndColor = Nevron.Nov.Graphics.NColor.Green
            Me.m_RootTreeMapNode.Label = "Tree Map - Industry by Sector"
            Me.m_RootTreeMapNode.Palette = palette
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
            Dim horizontalFillModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            horizontalFillModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENTreeMapHorizontalFillMode)()
            AddHandler horizontalFillModeComboBox.SelectedIndexChanged, AddressOf Me.OnHorizontalFillModeComboBoxSelectedIndexChanged
            horizontalFillModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENTreeMapHorizontalFillMode.LeftToRight)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Horizontal Fill Mode:", horizontalFillModeComboBox))
            Dim verticalFillModeComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            verticalFillModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENTreeMapVerticalFillMode)()
            AddHandler verticalFillModeComboBox.SelectedIndexChanged, AddressOf Me.OnVerticalFillModeComboBoxSelectedIndexChanged
            verticalFillModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENTreeMapVerticalFillMode.TopToBottom)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Vertical Fill Mode:", verticalFillModeComboBox))
            Dim sortOrderComboBox As Nevron.Nov.UI.NComboBox = New Nevron.Nov.UI.NComboBox()
            sortOrderComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENTreeMapNodeSortOrder)()
            AddHandler sortOrderComboBox.SelectedIndexChanged, AddressOf Me.OnSortOrderComboBoxSelectedIndexChanged
            sortOrderComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENTreeMapNodeSortOrder.Ascending)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Sort Order:", sortOrderComboBox))
            Return group
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a standard treemap chart.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnHorizontalFillModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RootTreeMapNode.HorizontalFillMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENTreeMapHorizontalFillMode)
        End Sub

        Private Sub OnVerticalFillModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RootTreeMapNode.VerticalFillMode = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENTreeMapVerticalFillMode)
        End Sub

        Private Sub OnSortOrderComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_RootTreeMapNode.SortOrder = CType(CType(arg.TargetNode, Nevron.Nov.UI.NComboBox).SelectedIndex, Nevron.Nov.Chart.ENTreeMapNodeSortOrder)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_RootTreeMapNode As Nevron.Nov.Chart.NGroupTreeMapNode

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NStandardTreeMapExampleSchema As Nevron.Nov.Dom.NSchema

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
