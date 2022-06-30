Imports System
Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Hierarchical Scale Example
	''' </summary>
	Public Class NHierarchicalScaleExample
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
            Nevron.Nov.Examples.Chart.NHierarchicalScaleExample.NHierarchicalScaleExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Chart.NHierarchicalScaleExample), NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim chartView As Nevron.Nov.Chart.NChartView = New Nevron.Nov.Chart.NChartView()
            chartView.Surface.CreatePredefinedChart(Nevron.Nov.Chart.ENPredefinedChartType.Cartesian)

			' configure title
			chartView.Surface.Titles(CInt((0))).Text = "Hierarchical Scale"

			' configure chart
			Me.m_Chart = CType(chartView.Surface.Charts(0), Nevron.Nov.Chart.NCartesianChart)

			' configure axes
			Me.m_Chart.SetPredefinedCartesianAxes(Nevron.Nov.Chart.ENPredefinedCartesianAxis.XOrdinalYLinear)

			' add the first bar
			Me.m_Bar1 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(Me.m_Bar1)
            Me.m_Bar1.Name = "Coca Cola"
            Me.m_Bar1.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Series
            Me.m_Bar1.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' add the second bar
			Me.m_Bar2 = New Nevron.Nov.Chart.NBarSeries()
            Me.m_Chart.Series.Add(Me.m_Bar2)
            Me.m_Bar2.Name = "Pepsi"
            Me.m_Bar2.MultiBarMode = Nevron.Nov.Chart.ENMultiBarMode.Clustered
            Me.m_Bar2.DataLabelStyle = New Nevron.Nov.Chart.NDataLabelStyle(False)

			' add custom labels to the Y axis
			Dim linearScale As Nevron.Nov.Chart.NLinearScale = CType(Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryY), Nevron.Nov.Chart.ENCartesianAxis)).Scale, Nevron.Nov.Chart.NLinearScale)

			' add interlace stripe
			Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip(New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Beige), Nothing, True, 0, 0, 1, 1)
            stripStyle.Interlaced = True
            linearScale.Strips.Add(stripStyle)
            chartView.Document.StyleSheets.ApplyTheme(New Nevron.Nov.Chart.NChartTheme(Nevron.Nov.Chart.ENChartPalette.Bright, False))
            Return chartView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim boxGroup As Nevron.Nov.UI.NUniSizeBoxGroup = New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
            Me.m_FirstRowGridStyleComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_FirstRowGridStyleComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENFirstRowGridStyle)()
            AddHandler Me.m_FirstRowGridStyleComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateScale)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Grid Style:", Me.m_FirstRowGridStyleComboBox))
            Me.m_FirstRowTickModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_FirstRowTickModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENRangeLabelTickMode)()
            AddHandler Me.m_FirstRowTickModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateScale)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Tick Mode:", Me.m_FirstRowTickModeComboBox))
            Me.m_GroupRowGridStyleComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_GroupRowGridStyleComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENGroupRowGridStyle)()
            AddHandler Me.m_GroupRowGridStyleComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateScale)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Grid Style:", Me.m_GroupRowGridStyleComboBox))
            Me.m_GroupRowTickModeComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_GroupRowTickModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENRangeLabelTickMode)()
            AddHandler Me.m_GroupRowTickModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateScale)
            stack.Add(Nevron.Nov.UI.NPairBox.Create("Tick Mode:", Me.m_GroupRowTickModeComboBox))
            Me.m_CreateSeparatorForEachLevelCheckBox = New Nevron.Nov.UI.NCheckBox("Create Separator For Each Level")
            AddHandler Me.m_CreateSeparatorForEachLevelCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnUpdateScale)
            stack.Add(Me.m_CreateSeparatorForEachLevelCheckBox)
            Dim changeDataButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Change Data")
            AddHandler changeDataButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnChangeDataButtonClick)
            stack.Add(changeDataButton)
            Me.m_FirstRowGridStyleComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENFirstRowGridStyle.Individual)
            Me.m_FirstRowTickModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENRangeLabelTickMode.Separators)
            Me.m_GroupRowGridStyleComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENGroupRowGridStyle.Individual)
            Me.m_GroupRowTickModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENRangeLabelTickMode.Separators)
            Me.m_CreateSeparatorForEachLevelCheckBox.Checked = True
            Me.OnChangeDataButtonClick(Nothing)
            Me.OnUpdateScale(Nothing)
            Return boxGroup
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>This example demonstrates how to create a hierarchical scale.</p>"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnChangeDataButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
			' fill with random data
			Me.m_Bar1.DataPoints.Clear()
            Me.m_Bar2.DataPoints.Clear()
            Dim random As System.Random = New System.Random()

            For i As Integer = 0 To 24 - 1
                Me.m_Bar1.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 200)))
                Me.m_Bar2.DataPoints.Add(New Nevron.Nov.Chart.NBarDataPoint(random.[Next](10, 300)))
            Next
        End Sub

        Private Sub OnUpdateScale(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
			' add custom labels to the X axis
			Dim scale As Nevron.Nov.Chart.NHierarchicalScale = New Nevron.Nov.Chart.NHierarchicalScale()
            Dim nodes As Nevron.Nov.Chart.NHierarchicalScaleNodeCollection = scale.ChildNodes
            scale.FirstRowGridStyle = CType(Me.m_FirstRowGridStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENFirstRowGridStyle)
            scale.GroupRowGridStyle = CType(Me.m_GroupRowGridStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENGroupRowGridStyle)
            scale.InnerMajorTicks.Visible = False
            scale.OuterMajorTicks.Visible = False
            Dim months As String() = New String() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

            For i As Integer = 0 To 2 - 1
                Dim yearNode As Nevron.Nov.Chart.NHierarchicalScaleNode = New Nevron.Nov.Chart.NHierarchicalScaleNode(0, (i + 2007).ToString())
                yearNode.LabelStyle.TickMode = CType(Me.m_GroupRowTickModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENRangeLabelTickMode)
                nodes.AddChild(yearNode)

                For j As Integer = 0 To 4 - 1
                    Dim quarterNode As Nevron.Nov.Chart.NHierarchicalScaleNode = New Nevron.Nov.Chart.NHierarchicalScaleNode(3, "Q" & (j + 1).ToString())
                    quarterNode.LabelStyle.TickMode = CType(Me.m_GroupRowTickModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENRangeLabelTickMode)
                    yearNode.Nodes.Add(quarterNode)

                    For k As Integer = 0 To 3 - 1
                        Dim monthNode As Nevron.Nov.Chart.NHierarchicalScaleNode = New Nevron.Nov.Chart.NHierarchicalScaleNode(1, months(j * 3 + k))
                        monthNode.LabelStyle.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(90)
                        monthNode.LabelStyle.TickMode = CType(Me.m_FirstRowTickModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENRangeLabelTickMode)
                        quarterNode.Nodes.Add(monthNode)
                    Next
                Next
            Next

			' update control state
			Me.m_FirstRowTickModeComboBox.Enabled = CType(Me.m_FirstRowGridStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENFirstRowGridStyle) = Nevron.Nov.Chart.ENFirstRowGridStyle.Individual
            Me.m_GroupRowTickModeComboBox.Enabled = CType(Me.m_GroupRowGridStyleComboBox.SelectedIndex, Nevron.Nov.Chart.ENGroupRowGridStyle) = Nevron.Nov.Chart.ENGroupRowGridStyle.Individual
            Dim stripStyle As Nevron.Nov.Chart.NScaleStrip = New Nevron.Nov.Chart.NScaleStrip()
            stripStyle.Length = 3
            stripStyle.Interval = 3
            stripStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.LightGray, 0.5F))
            scale.Strips.Add(stripStyle)
            scale.CreateSeparatorForEachLevel = Me.m_CreateSeparatorForEachLevelCheckBox.Checked
            Me.m_Chart.Axes(CType((Nevron.Nov.Chart.ENCartesianAxis.PrimaryX), Nevron.Nov.Chart.ENCartesianAxis)).Scale = scale
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_Chart As Nevron.Nov.Chart.NCartesianChart
        Private m_Bar1 As Nevron.Nov.Chart.NBarSeries
        Private m_Bar2 As Nevron.Nov.Chart.NBarSeries
        Private m_FirstRowGridStyleComboBox As Nevron.Nov.UI.NComboBox
        Private m_FirstRowTickModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_GroupRowGridStyleComboBox As Nevron.Nov.UI.NComboBox
        Private m_GroupRowTickModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_CreateSeparatorForEachLevelCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NHierarchicalScaleExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
