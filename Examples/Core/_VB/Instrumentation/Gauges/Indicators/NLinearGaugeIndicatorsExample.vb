Imports Nevron.Nov.Chart
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Gauge
	''' <summary>
	''' This example demonstrates how to add indicators to a linear gauge 
	''' </summary>
	Public Class NLinearGaugeIndicatorsExample
        Inherits NExampleBase
        #Region"Constructors"

        ''' <summary>
        ''' 
        ''' </summary>
        Public Sub New()
        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        Shared Sub New()
            Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.NLinearGaugeIndicatorsExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample), NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left

			' create a linear gauge
			Me.m_LinearGauge = New Nevron.Nov.Chart.NLinearGauge()
            stack.Add(Me.m_LinearGauge)
            Me.m_LinearGauge.CapEffect = New Nevron.Nov.Chart.NGelCapEffect()
            Me.m_LinearGauge.Border = Me.CreateBorder()
            Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20)
            Me.m_LinearGauge.BorderThickness = New Nevron.Nov.Graphics.NMargins(6)
            Me.m_LinearGauge.BackgroundFill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Gray, Nevron.Nov.Graphics.NColor.Black)
            Me.m_LinearGauge.PreferredSize = New Nevron.Nov.Graphics.NSize(400, 150)
            Me.m_LinearGauge.Axes.Clear()
            Dim celsiusRange As Nevron.Nov.Graphics.NRange = New Nevron.Nov.Graphics.NRange(-40.0, 60.0)

			' add celsius and farenheit axes
			Dim celsiusAxis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            celsiusAxis.Range = celsiusRange
            celsiusAxis.Anchor = New Nevron.Nov.Chart.NModelGaugeAxisAnchor(-5, Nevron.Nov.ENVerticalAlignment.Center, Nevron.Nov.Chart.ENScaleOrientation.Left, 0.0F, 100.0F)
            Me.m_LinearGauge.Axes.Add(celsiusAxis)
            Dim farenheitAxis As Nevron.Nov.Chart.NGaugeAxis = New Nevron.Nov.Chart.NGaugeAxis()
            farenheitAxis.Range = New Nevron.Nov.Graphics.NRange(Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(celsiusRange.Begin), Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(celsiusRange.[End]))
            farenheitAxis.Anchor = New Nevron.Nov.Chart.NModelGaugeAxisAnchor(5, Nevron.Nov.ENVerticalAlignment.Center, Nevron.Nov.Chart.ENScaleOrientation.Right, 0.0F, 100.0F)
            Me.m_LinearGauge.Axes.Add(farenheitAxis)

			' configure the scales
			Dim celsiusScale As Nevron.Nov.Chart.NLinearScale = CType(celsiusAxis.Scale, Nevron.Nov.Chart.NLinearScale)
            Me.ConfigureScale(celsiusScale, "°C")
            celsiusScale.Sections.Add(Me.CreateSection(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Red, New Nevron.Nov.Graphics.NRange(40, 60)))
            celsiusScale.Sections.Add(Me.CreateSection(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.SkyBlue, New Nevron.Nov.Graphics.NRange(-40, -20)))
            Dim farenheitScale As Nevron.Nov.Chart.NLinearScale = CType(farenheitAxis.Scale, Nevron.Nov.Chart.NLinearScale)
            Me.ConfigureScale(farenheitScale, "°F")
            farenheitScale.Sections.Add(Me.CreateSection(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Red, New Nevron.Nov.Graphics.NRange(Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(40), Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(60))))
            farenheitScale.Sections.Add(Me.CreateSection(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.SkyBlue, New Nevron.Nov.Graphics.NRange(Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(-40), Nevron.Nov.Examples.Gauge.NLinearGaugeIndicatorsExample.CelsiusToFarenheit(-20))))

			' now add two indicators
			Me.m_Indicator1 = New Nevron.Nov.Chart.NRangeIndicator()
            Me.m_Indicator1.Value = 10
            Me.m_Indicator1.Stroke.Color = Nevron.Nov.Graphics.NColor.DarkBlue
            Me.m_Indicator1.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.Vertical, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.LightBlue, Nevron.Nov.Graphics.NColor.Blue)
            Me.m_Indicator1.BeginWidth = 10
            Me.m_Indicator1.EndWidth = 10
            Me.m_LinearGauge.Indicators.Add(Me.m_Indicator1)
            Me.m_Indicator2 = New Nevron.Nov.Chart.NMarkerValueIndicator()
            Me.m_Indicator2.Value = 33
'			m_Indicator2.ShapFillStyle = new NStockGradientFill(ENGradientStyle.Horizontal, ENGradientVariant.Variant1, NColor.White, NColor.Red);
'			m_Indicator2.Shape.StrokeStyle.Color = Color.DarkRed;
			Me.m_LinearGauge.Indicators.Add(Me.m_Indicator2)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim propertyStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NUniSizeBoxGroup(propertyStack))
            Me.m_RangeIndicatorValueUpDown = New Nevron.Nov.UI.NNumericUpDown()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Range Indicator Value:", Me.m_RangeIndicatorValueUpDown, True))
            Me.m_RangeIndicatorValueUpDown.Value = Me.m_Indicator1.Value
            AddHandler Me.m_RangeIndicatorValueUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeIndicatorValueUpDownValueChanged)
            Me.m_RangeIndicatorOriginModeComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Range Indicator Origin Mode:", Me.m_RangeIndicatorOriginModeComboBox, True))
            Me.m_RangeIndicatorOriginModeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENRangeIndicatorOriginMode)()
            Me.m_RangeIndicatorOriginModeComboBox.SelectedIndex = CInt(Nevron.Nov.Chart.ENRangeIndicatorOriginMode.ScaleMin)
            AddHandler Me.m_RangeIndicatorOriginModeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeIndicatorOriginModeComboBoxSelectedIndexChanged)
            Me.m_RangeIndicatorOriginUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RangeIndicatorOriginUpDown.Value = 0.0
            Me.m_RangeIndicatorOriginUpDown.Enabled = False
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Range Indicator Origin:", Me.m_RangeIndicatorOriginUpDown, True))
            AddHandler Me.m_RangeIndicatorOriginUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnRangeIndicatorOriginUpDownValueChanged)
            Me.m_ValueIndicatorUpDown = New Nevron.Nov.UI.NNumericUpDown()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Value Indicator Value:", Me.m_ValueIndicatorUpDown, True))
            Me.m_ValueIndicatorUpDown.Value = Me.m_Indicator2.Value
            AddHandler Me.m_ValueIndicatorUpDown.ValueChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorUpDownValueChanged)
            Me.m_ValueIndicatorShapeComboBox = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Value Indicator Shape", Me.m_ValueIndicatorShapeComboBox, True))
            Me.m_ValueIndicatorShapeComboBox.FillFromEnum(Of Nevron.Nov.Chart.ENScaleValueMarkerShape)()
            Me.m_ValueIndicatorShapeComboBox.SelectedIndex = CInt(Me.m_Indicator2.Shape)
            AddHandler Me.m_ValueIndicatorShapeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnValueIndicatorShapeComboBoxSelectedIndexChanged)
            Me.m_GaugeOrientationCombo = New Nevron.Nov.UI.NComboBox()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Gauge Orientation:", Me.m_GaugeOrientationCombo, True))
            Me.m_GaugeOrientationCombo.FillFromEnum(Of Nevron.Nov.Chart.ENLinearGaugeOrientation)()
            Me.m_GaugeOrientationCombo.SelectedIndex = CInt(Nevron.Nov.Chart.ENLinearGaugeOrientation.Horizontal)
            AddHandler Me.m_GaugeOrientationCombo.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnGaugeOrientationComboSelectedIndexChanged)
            Me.m_MarkerWidthUpDown = New Nevron.Nov.UI.NNumericUpDown()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Marker Width:", Me.m_MarkerWidthUpDown, True))
            Me.m_MarkerWidthUpDown.Value = Me.m_Indicator2.Width
            Me.m_MarkerHeightUpDown = New Nevron.Nov.UI.NNumericUpDown()
            propertyStack.Add(New Nevron.Nov.UI.NPairBox("Marker Height:", Me.m_MarkerHeightUpDown, True))
            Me.m_MarkerHeightUpDown.Value = Me.m_Indicator2.Height
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "<p>The example demonstrates how to create range and marker gauge indicators.</p>"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateSection(ByVal tickColor As Nevron.Nov.Graphics.NColor, ByVal labelColor As Nevron.Nov.Graphics.NColor, ByVal range As Nevron.Nov.Graphics.NRange) As Nevron.Nov.Chart.NScaleSection
            Dim scaleSection As Nevron.Nov.Chart.NScaleSection = New Nevron.Nov.Chart.NScaleSection()
            scaleSection.Range = range
            scaleSection.MajorTickFill = New Nevron.Nov.Graphics.NColorFill(tickColor)
            Dim labelStyle As Nevron.Nov.UI.NTextStyle = New Nevron.Nov.UI.NTextStyle()
            labelStyle.Fill = New Nevron.Nov.Graphics.NColorFill(labelColor)
            labelStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scaleSection.LabelTextStyle = labelStyle
            Return scaleSection
        End Function

        Private Sub ConfigureScale(ByVal scale As Nevron.Nov.Chart.NLinearScale, ByVal text As String)
            scale.SetPredefinedScale(Nevron.Nov.Chart.ENPredefinedScaleStyle.PresentationNoStroke)
            scale.Labels.Style.TextStyle.Font = New Nevron.Nov.Graphics.NFont("Arimo", 12.0, Nevron.Nov.Graphics.ENFontStyle.Bold)
            scale.Labels.Style.TextStyle.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            scale.Labels.Style.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.Scale, 90)
            scale.MinorTickCount = 4
            scale.Ruler.Stroke.Width = 0
            scale.Ruler.Fill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.FromColor(Nevron.Nov.Graphics.NColor.DarkGray, 0.4F))
            scale.Title.RulerAlignment = Nevron.Nov.ENHorizontalAlignment.Left
            scale.Title.Text = text
            scale.Title.Angle = New Nevron.Nov.Chart.NScaleLabelAngle(Nevron.Nov.Chart.ENScaleLabelAngleMode.View, 0)
            scale.Title.Offset = 0.0
            scale.Title.TextStyle.Font.Size = 12
            scale.Title.TextStyle.Font.Style = Nevron.Nov.Graphics.ENFontStyle.Bold
            scale.Title.TextStyle.Fill = New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.LightBlue)
            scale.InflateViewRangeBegin = False
            scale.InflateViewRangeEnd = False
        End Sub

        Private Shared Function FarenheitToCelsius(ByVal farenheit As Double) As Double
            Return (farenheit - 32.0) * 5.0 / 9.0
        End Function

        Private Shared Function CelsiusToFarenheit(ByVal celsius As Double) As Double
            Return (celsius * 9.0) / 5.0 + 32.0F
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnRangeIndicatorOriginModeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Indicator1.OriginMode = CType(Me.m_RangeIndicatorOriginModeComboBox.SelectedIndex, Nevron.Nov.Chart.ENRangeIndicatorOriginMode)

            If Me.m_Indicator1.OriginMode <> Nevron.Nov.Chart.ENRangeIndicatorOriginMode.Custom Then
                Me.m_RangeIndicatorOriginUpDown.Enabled = False
            Else
                Me.m_RangeIndicatorOriginUpDown.Enabled = True
            End If
        End Sub

        Private Sub OnGaugeOrientationComboSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_LinearGauge.Orientation = CType(Me.m_GaugeOrientationCombo.SelectedIndex, Nevron.Nov.Chart.ENLinearGaugeOrientation)

            If Me.m_LinearGauge.Orientation = Nevron.Nov.Chart.ENLinearGaugeOrientation.Horizontal Then
                Me.m_LinearGauge.PreferredSize = New Nevron.Nov.Graphics.NSize(400, 150)
                Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(20, 0, 10, 0)
            Else
                Me.m_LinearGauge.PreferredSize = New Nevron.Nov.Graphics.NSize(150, 400)
                Me.m_LinearGauge.Padding = New Nevron.Nov.Graphics.NMargins(0, 10, 0, 20)
            End If
        End Sub

        Private Sub OnValueIndicatorUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Indicator2.Value = Me.m_ValueIndicatorUpDown.Value
        End Sub

        Private Sub OnRangeIndicatorValueUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Indicator1.Value = Me.m_RangeIndicatorValueUpDown.Value
        End Sub

        Private Sub OnRangeIndicatorOriginUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Indicator1.Origin = Me.m_RangeIndicatorOriginUpDown.Value
        End Sub

        Private Sub OnValueIndicatorShapeComboBoxSelectedIndexChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_Indicator2.Shape = CType(Me.m_ValueIndicatorShapeComboBox.SelectedIndex, Nevron.Nov.Chart.ENScaleValueMarkerShape)
        End Sub

        Private Sub ShowMarkerEditorButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub

        Private Sub MarkerWidthUpDown_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.m_Indicator2.Width = Me.m_MarkerWidthUpDown.Value
        End Sub

        Private Sub MarkerHeightUpDown_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.m_Indicator2.Height = Me.m_MarkerHeightUpDown.Value
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_LinearGauge As Nevron.Nov.Chart.NLinearGauge
        Private m_Indicator1 As Nevron.Nov.Chart.NRangeIndicator
        Private m_Indicator2 As Nevron.Nov.Chart.NMarkerValueIndicator
        Private m_RangeIndicatorValueUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RangeIndicatorOriginUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_ValueIndicatorUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RangeIndicatorOriginModeComboBox As Nevron.Nov.UI.NComboBox
        Private m_ValueIndicatorShapeComboBox As Nevron.Nov.UI.NComboBox
        Private m_GaugeOrientationCombo As Nevron.Nov.UI.NComboBox
        Private m_MarkerWidthUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_MarkerHeightUpDown As Nevron.Nov.UI.NNumericUpDown

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NLinearGaugeIndicatorsExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Static Methods"

		Protected Function CreateBorder() As Nevron.Nov.UI.NBorder
            Return Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.LightGray, Nevron.Nov.Graphics.NColor.White, Nevron.Nov.Graphics.NColor.DarkGray, 10, 10)
        End Function

		#EndRegion
	End Class
End Namespace
