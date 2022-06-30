Imports System
Imports Nevron.Nov.Grid
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NColumnsConditionalFormattingExample
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
            Nevron.Nov.Examples.Grid.NColumnsConditionalFormattingExample.NColumnsConditionalFormattingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NColumnsConditionalFormattingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' bind the grid to the PersonsOrders data source
            grid.DataSource = NDummyDataSource.CreatePersonsOrdersDataSource()

            ' Formatting Rule 1 = applied to the Product Name column.
            ' make the Aptax, Joykix, Zun Zimtam,  Dingtincof cell backgrounds LightCoral, with a bold Font
            If True Then
                ' create the formatting rule and add it in the "Product Name" column
                Dim column As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Product Name")
                Dim formattingRule As Nevron.Nov.Grid.NFormattingRule = New Nevron.Nov.Grid.NFormattingRule()
                column.FormattingRules.Add(formattingRule)

                ' row condition
                Dim orCondition As Nevron.Nov.Grid.NOrGroupRowCondition = New Nevron.Nov.Grid.NOrGroupRowCondition()
                orCondition.Add(New Nevron.Nov.Grid.NOperatorRowCondition(New Nevron.Nov.Grid.NFieldRowValue("Product Name"), Nevron.Nov.Grid.ENRowConditionOperator.Equals, "Aptax"))
                orCondition.Add(New Nevron.Nov.Grid.NOperatorRowCondition(New Nevron.Nov.Grid.NFieldRowValue("Product Name"), Nevron.Nov.Grid.ENRowConditionOperator.Equals, "Joykix"))
                orCondition.Add(New Nevron.Nov.Grid.NOperatorRowCondition(New Nevron.Nov.Grid.NFieldRowValue("Product Name"), Nevron.Nov.Grid.ENRowConditionOperator.Equals, "Zun Zimtam"))
                orCondition.Add(New Nevron.Nov.Grid.NOperatorRowCondition(New Nevron.Nov.Grid.NFieldRowValue("Product Name"), Nevron.Nov.Grid.ENRowConditionOperator.Equals, "Dingtincof"))
                formattingRule.RowCondition = orCondition

                ' LightCoral background fill declaration
                Dim backgroundFillDeclaration As Nevron.Nov.Grid.NBackgroundFillDeclaration = New Nevron.Nov.Grid.NBackgroundFillDeclaration()
                backgroundFillDeclaration.Mode = Nevron.Nov.Grid.ENFillDeclarationMode.Uniform
                backgroundFillDeclaration.UniformFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.LightCoral)
                formattingRule.Declarations.Add(backgroundFillDeclaration)

                ' Bold font style declaration
                Dim fontStyleDeclaration As Nevron.Nov.Grid.NFontStyleDeclaration = New Nevron.Nov.Grid.NFontStyleDeclaration()
                fontStyleDeclaration.FontStyle = Nevron.Nov.Graphics.ENFontStyle.Bold
                formattingRule.Declarations.Add(fontStyleDeclaration)
            End If

            ' Formatting Rule 2 = applied to the Product Name column.
            ' make the Aptax and Joykix cell backgrounds LightCoral, with a bold Font
            If True Then
                ' create the formatting rule and add it in the "Product Name" column
                Dim column As Nevron.Nov.Grid.NColumn = grid.Columns.GetColumnByFieldName("Price")
                Dim formattingRule As Nevron.Nov.Grid.NFormattingRule = New Nevron.Nov.Grid.NFormattingRule()
                column.FormattingRules.Add(formattingRule)

                ' row condition
                formattingRule.RowCondition = New Nevron.Nov.Grid.NTrueRowCondition()

                ' get price field min and max
                Dim minPrice, maxPrice As Object
                Dim priceFieldIndex As Integer = grid.DataSource.GetFieldIndex("Price")
                grid.DataSource.TryGetMin(priceFieldIndex, minPrice)
                grid.DataSource.TryGetMax(priceFieldIndex, maxPrice)

                ' make a graident fill declaration 
                Dim backgroundFillDeclaration As Nevron.Nov.Grid.NBackgroundFillDeclaration = New Nevron.Nov.Grid.NBackgroundFillDeclaration()
                backgroundFillDeclaration.Mode = Nevron.Nov.Grid.ENFillDeclarationMode.TwoColorGradient
                backgroundFillDeclaration.MinimumValue = System.Convert.ToDouble(minPrice)
                backgroundFillDeclaration.MaximumValue = System.Convert.ToDouble(maxPrice)
                backgroundFillDeclaration.BeginColor = Nevron.Nov.Graphics.NColor.Green
                backgroundFillDeclaration.EndColor = Nevron.Nov.Graphics.NColor.Red
                formattingRule.Declarations.Add(backgroundFillDeclaration)
            End If

            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the conditional column formatting.
</p>
<p>
    Conditional formatting changes the default formatting of column cells, when a certain condition is met.
    NOV Grid for .NET provides strong support for authoring complex cell conditions.
</p>
<p>
    Besides static fill rules, NOV Grid for .NET also supports gradient background and text fill declarations, 
    that can be defined as a two color or three color gradient. 
</p>
<p>
    In this example <b>Price</b> background uses a two-color gradient background fill.
    The <b>Product Name</b> is has different background fill and font style applied to certain products (<b>Aptax</b>,<b>Joykix</b>,<b>Zun Zimtam</b> and <b>Dingtincof</b>).
</p>
"
        End Function

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NColumnsConditionalFormattingExample.
        ''' </summary>
        Public Shared ReadOnly NColumnsConditionalFormattingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
