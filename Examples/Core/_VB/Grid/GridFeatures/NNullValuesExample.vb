Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI
Imports System

Namespace Nevron.Nov.Examples.Grid
    Public Class NNullValuesExample
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
            Nevron.Nov.Examples.Grid.NNullValuesExample.NNullValuesExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NNullValuesExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
			' // create a memory data table that supports null values
' 						NMemoryDataTable table = new NMemoryDataTable();
' 						table.AddField(new NFieldInfo("Name", typeof(string), true));
' 						table.AddField(new NFieldInfo("Birthday", typeof(DateTime), true));
' 						table.AddField(new NFieldInfo("Country", typeof(ENCountry), true));
' 						table.AddField(new NFieldInfo("Email", typeof(string), true));
' 
' 						Random rnd = new Random();
' 						for (int i = 0; i < NDummyDataSource.PersonInfos.Length; i++)
' 						{
' 							NDummyDataSource.NPersonInfo personInfo = NDummyDataSource.PersonInfos[i];
' 
' 							bool nullName = (rnd.Next(8) == 1);
' 							bool nullBirthday = (rnd.Next(8) == 2);
' 							bool nullCountry = (rnd.Next(8) == 3);
' 							bool nullEmail = (rnd.Next(8) == 4);
' 
' 							table.AddRow(
' 								(nullName? null: personInfo.Name),                  // name
' 								(nullBirthday? null: (object)personInfo.Birthday),  // birthday
' 								(nullCountry ? null : (object)personInfo.Country),  // country
' 								(nullEmail? null: personInfo.Email));               // email
' 						}

			' create a memory data table that supports null values
			Dim table As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable()
            table.AddField(New Nevron.Nov.Data.NFieldInfo("Name", GetType(String), True))
            Dim rnd As System.Random = New System.Random()

            For i As Integer = 0 To 1 - 1
                Dim personInfo As NDummyDataSource.NPersonInfo = NDummyDataSource.PersonInfos(i)
                Dim nullName As Boolean = (rnd.[Next](8) = 1)
                Dim nullBirthday As Boolean = (rnd.[Next](8) = 2)
                Dim nullCountry As Boolean = (rnd.[Next](8) = 3)
                Dim nullEmail As Boolean = (rnd.[Next](8) = 4)
                table.AddRow((If(nullName, Nothing, personInfo.Name)))
            Next

            Me.m_TableView = New Nevron.Nov.Grid.NTableGridView()
            Me.m_TableView.Grid.DataSource = New Nevron.Nov.Data.NDataSource(table)
            Me.m_TableView.Grid.AllowEdit = True
            Return Me.m_TableView
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates the grid support for null values. Note that the grid also supports editing of null values. 
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TableView As Nevron.Nov.Grid.NTableGridView

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NNullValuesExample.
        ''' </summary>
        Public Shared ReadOnly NNullValuesExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
