Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NBordersExample
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
            Nevron.Nov.Examples.UI.NBordersExample.NBordersExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NBordersExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' Create a table layout panel
            Dim table As Nevron.Nov.UI.NTableFlowPanel = New Nevron.Nov.UI.NTableFlowPanel()
            table.Padding = New Nevron.Nov.Graphics.NMargins(10)
            table.BackgroundFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.White)
            table.MaxOrdinal = 3
            table.HorizontalSpacing = 10
            table.VerticalSpacing = 10
            table.ColFillMode = Nevron.Nov.Layout.ENStackFillMode.Equal
            table.ColFitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            table.RowFitMode = Nevron.Nov.Layout.ENStackFitMode.Equal
            table.RowFillMode = Nevron.Nov.Layout.ENStackFillMode.None
            table.UniformWidths = Nevron.Nov.Layout.ENUniformSize.Max
            table.UniformHeights = Nevron.Nov.Layout.ENUniformSize.Max

            ' add some predefined borders
            ' 3D Borders
            Dim map As Nevron.Nov.UI.NUIThemeColorMap = New Nevron.Nov.UI.NUIThemeColorMap(Nevron.Nov.UI.ENUIThemeScheme.WindowsClassic)
            table.Add(Me.CreateBorderedWidget("3D Border", Nevron.Nov.UI.NBorder.Create3DBorder(Nevron.Nov.Graphics.NColor.Green.Lighten().Lighten(), Nevron.Nov.Graphics.NColor.Green.Lighten(), Nevron.Nov.Graphics.NColor.Green.Darken(), Nevron.Nov.Graphics.NColor.Green)))
            table.Add(Me.CreateBorderedWidget("Raised Border (using Theme Colors)", Nevron.Nov.UI.NBorder.CreateRaised3DBorder(map)))
            table.Add(Me.CreateBorderedWidget("Sunken Border (using Theme Colors)", Nevron.Nov.UI.NBorder.CreateSunken3DBorder(map)))
            
            ' Filled Borders
            table.Add(Me.CreateBorderedWidget("Solid Color", Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)))
            table.Add(Me.CreateBorderedWidget("Solid Color With Rounded Corners", Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Blue, 10, 13)))
            table.Add(Me.CreateBorderedWidget("Gradient Filling With Outline", Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NFill.CreatePredefined(Nevron.Nov.Graphics.ENPredefinedFillPattern.GradientVertical, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Blue), New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green), New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green))))

            ' Outer Outline Borders
            table.Add(Me.CreateBorderedWidget("Outer Outline Border", Nevron.Nov.UI.NBorder.CreateOuterOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dash))))
            table.Add(Me.CreateBorderedWidget("Outer Outline Border with Rounding", Nevron.Nov.UI.NBorder.CreateOuterOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dash), 10)))
            table.Add(Me.CreateBorderedWidget("Outer Outline Border", Nevron.Nov.UI.NBorder.CreateOuterOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dash))))
            table.Add(Me.CreateBorderedWidget("Outer Outline Border with Rounding", Nevron.Nov.UI.NBorder.CreateOuterOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dash), 10)))

            ' Inner Outline Borders
            table.Add(Me.CreateBorderedWidget("Inner Outline Border", Nevron.Nov.UI.NBorder.CreateInnerOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.ENDashStyle.Dash))))
            table.Add(Me.CreateBorderedWidget("Inner Outline Border with Rounding", Nevron.Nov.UI.NBorder.CreateInnerOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.ENDashStyle.Dash), 10)))
            table.Add(Me.CreateBorderedWidget("Inner Outline Border", Nevron.Nov.UI.NBorder.CreateInnerOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.ENDashStyle.Dash))))
            table.Add(Me.CreateBorderedWidget("Inner Outline Border with Rounding", Nevron.Nov.UI.NBorder.CreateInnerOutlineBorder(New Nevron.Nov.Graphics.NStroke(1, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.ENDashStyle.Dash), 10)))

            ' Double border
            table.Add(Me.CreateBorderedWidget("Double Border", Nevron.Nov.UI.NBorder.CreateDoubleBorder(Nevron.Nov.Graphics.NColor.Blue)))
            table.Add(Me.CreateBorderedWidget("Double Border with Two Colors", Nevron.Nov.UI.NBorder.CreateDoubleBorder(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.Red)))
            table.Add(Me.CreateBorderedWidget("Double Border with Two Colors and Rounding", Nevron.Nov.UI.NBorder.CreateDoubleBorder(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.Red, 10, 12)))

            ' Two color borders
            table.Add(Me.CreateBorderedWidget("Two Colors Border", Nevron.Nov.UI.NBorder.CreateTwoColorBorder(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.Red)))
            table.Add(Me.CreateBorderedWidget("Two Colors Border with Rounding", Nevron.Nov.UI.NBorder.CreateTwoColorBorder(Nevron.Nov.Graphics.NColor.Blue, Nevron.Nov.Graphics.NColor.Red, 10, 12)))

            ' Three color borders
            table.Add(Me.CreateBorderedWidget("Three Colors Border", Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.Blue)))
            table.Add(Me.CreateBorderedWidget("Three Colors Border with Rounding", Nevron.Nov.UI.NBorder.CreateThreeColorBorder(Nevron.Nov.Graphics.NColor.Red, Nevron.Nov.Graphics.NColor.Green, Nevron.Nov.Graphics.NColor.Blue, 10, 12)))
            Return table
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates some of the static methods of NBorder that help you quickly create commonly used types of borders.
</p>
"
        End Function

        #EndRegion

        #Region"Implementation"

        ''' <summary>
        ''' Creates a simple label that demonstrates the specified border.
        ''' </summary>
        ''' <paramname="text"></param>
        ''' <paramname="border"></param>
        ''' <returns></returns>
        Private Function CreateBorderedWidget(ByVal text As String, ByVal border As Nevron.Nov.UI.NBorder) As Nevron.Nov.UI.NWidget
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Fit
            label.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Fit
            label.Padding = New Nevron.Nov.Graphics.NMargins(10)
            label.Border = border
            label.BorderThickness = New Nevron.Nov.Graphics.NMargins(5)
            Return label
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NBordersExample.
        ''' </summary>
        Public Shared ReadOnly NBordersExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
