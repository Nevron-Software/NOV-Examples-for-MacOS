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
    Public Class NAutomaticDataColumnsExamle
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
            Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NAutomaticDataColumnsExamleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_GridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = Me.m_GridView.Grid
            Dim items As Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem() = New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem() {New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem(Nevron.Nov.Grid.NResources.Image_CountryFlags_ad_png, Nevron.Nov.Graphics.NColor.Navy, New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Moccasin), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.AntiqueWhite)), New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem(Nevron.Nov.Grid.NResources.Image_CountryFlags_ae_png, Nevron.Nov.Graphics.NColor.Olive, New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.NColor.Violet, Nevron.Nov.Graphics.NColor.WhiteSmoke), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.Bisque)), New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem(Nevron.Nov.Grid.NResources.Image_CountryFlags_af_png, Nevron.Nov.Graphics.NColor.OldLace, New Nevron.Nov.Graphics.NHatchFill(Nevron.Nov.Graphics.ENHatchStyle.DiagonalBrick, Nevron.Nov.Graphics.NColor.Yellow, Nevron.Nov.Graphics.NColor.Red), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DarkCyan)), New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem(Nevron.Nov.Grid.NResources.Image_CountryFlags_ag_png, Nevron.Nov.Graphics.NColor.Plum, New Nevron.Nov.Graphics.NImageFill(Nevron.Nov.Grid.NResources.Image__16x16_Birthday_png), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.DimGray)), New Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem(Nevron.Nov.Grid.NResources.Image_CountryFlags_ai_png, Nevron.Nov.Graphics.NColor.Peru, New Nevron.Nov.Graphics.NStockGradientFill(Nevron.Nov.Graphics.ENGradientStyle.FromCenter, Nevron.Nov.Graphics.ENGradientVariant.Variant1, Nevron.Nov.Graphics.NColor.Wheat, Nevron.Nov.Graphics.NColor.DarkGoldenrod), New Nevron.Nov.Graphics.NStroke(Nevron.Nov.Graphics.NColor.CadetBlue))}

            ' bind the grid to the data source
            grid.DataSource = New Nevron.Nov.Data.NDataSource(New Nevron.Nov.Data.NGenericIListDataTable(Of Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.NItem)(items))
            AddHandler grid.AutoCreateColumn, AddressOf Me.grid_AutoCreateColumn
            Return Me.m_GridView
        End Function

        Private Sub grid_AutoCreateColumn(ByVal arg As Nevron.Nov.Grid.NAutoCreateColumnEventArgs)
            ' get the data column which was automatically created
            Dim dataColumn As Nevron.Nov.Grid.NDataColumn = arg.DataColumn
        End Sub

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates automatically created data columns.
</p>
<p>
    Data columns are columns, which obtain and edit data from the data source.
</p>
<p>
    When the grid is bound to a data source, it will automatically create data columns for all data source fields, if the grid <b>AutoCreateColumns</b> property is true.</br>
    During this process it will also raise the <b>AutoCreateColumn</b> column event.
</p>
<p>
</p>"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_GridView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NAutomaticDataColumnsExamle.
        ''' </summary>
        Public Shared ReadOnly NAutomaticDataColumnsExamleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        Public Enum ENDummyEnum
            EnumValue1
            EnumValue2
            EnumValue3
        End Enum

        Public Class NItem
            Public Sub New(ByVal image As Nevron.Nov.Graphics.NImage, ByVal color As Nevron.Nov.Graphics.NColor, ByVal fill As Nevron.Nov.Graphics.NFill, ByVal stroke As Nevron.Nov.Graphics.NStroke)
                Me.Image = image
                Me.Color = color
                Me.Fill = fill
                Me.Stroke = stroke
            End Sub

            Public Property BooleanValue As Boolean
            Public Property ByteValue As Byte
            Public Property UInt16Value As UShort
            Public Property UInt32Value As UInteger
            Public Property UInt64Value As ULong
            Public Property Int16Value As Short
            Public Property Int32Value As Integer
            Public Property Int64Value As Long
            Public Property SingleValue As Single
            Public Property DoubleValue As Double
            Public Property DecimalValue As Decimal
            Public Property DateTimeValue As System.DateTime
            Public Property StringValue As String
            Public Property EnumValue As Nevron.Nov.Examples.Grid.NAutomaticDataColumnsExamle.ENDummyEnum
            Public Property Image As Nevron.Nov.Graphics.NImage
            Public Property Color As Nevron.Nov.Graphics.NColor
            Public Property Fill As Nevron.Nov.Graphics.NFill
            Public Property Stroke As Nevron.Nov.Graphics.NStroke
        End Class
    End Class
End Namespace
