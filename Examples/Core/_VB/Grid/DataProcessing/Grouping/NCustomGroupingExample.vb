Imports System
Imports Nevron.Nov.Data
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Grid
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Grid
    Public Class NCustomGroupingExample
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
            Nevron.Nov.Examples.Grid.NCustomGroupingExample.NCustomGroupingExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Grid.NCustomGroupingExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' store current date time
            Me.m_Now = System.DateTime.Now

            ' create a view and get its grid
            Dim view As Nevron.Nov.Grid.NTableGridView = New Nevron.Nov.Grid.NTableGridView()
            Dim grid As Nevron.Nov.Grid.NTableGrid = view.Grid

            ' bind the grid to the data source
            grid.DataSource = Me.CreateMailDataSource()

            ' create a grouping rule with a custom row value.
            ' NOTE: The RowValue associated with each grouping rule, returns an object for each row of the data source.
            ' the rows in the data source are grouped according to that object.
            ' The NCustomRowValue provides a delegate that help you return a custom object object for each data source row.
            ' In our example the NCustomRowValue returns a member of the ENMailGroup enumeraiton for each record, depending on its Received row value.
            Dim customRowValue As Nevron.Nov.Grid.NCustomRowValue(Of Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup) = New Nevron.Nov.Grid.NCustomRowValue(Of Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup)()
            customRowValue.Description = "Received"
            customRowValue.GetRowValueDelegate = AddressOf Me.GetRowValueDelegate

            ' NOTE: The NGroupingRule provides the following events:
            ' CreateGroupRowCells - raised when the grid needs to create the cells of the group row.
            ' CreateGroupingHeaderContent - raised when the grid needs to create a grouping header content for the grouping in the groupings panel.
            Dim groupingRule As Nevron.Nov.Grid.NGroupingRule = New Nevron.Nov.Grid.NGroupingRule()
            groupingRule.RowValue = customRowValue
            groupingRule.CreateGroupRowCellsDelegate = Function(ByVal arg As Nevron.Nov.Grid.NGroupingRuleCreateGroupRowCellsArgs)
                                                           Dim groupValue As Integer = System.Convert.ToInt32(arg.GroupRow.GroupValue)
                                                           Dim text As String = Nevron.Nov.NStringHelpers.InsertSpacesBeforeUppersAndDigits(CType(groupValue, Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup).ToString())
                                                           Return New Nevron.Nov.Grid.NGroupRowCell() {New Nevron.Nov.Grid.NGroupRowCell(text)}
                                                       End Function

            groupingRule.CreateGroupingHeaderContentDelegate = Function(ByVal theGroupingRule As Nevron.Nov.Grid.NGroupingRule) New Nevron.Nov.UI.NLabel("Received")
            grid.GroupingRules.Add(groupingRule)
            Return view
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Return Nothing
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
    Demonstrates custom grouping, custom group header content and custom group row cells.
</p>
<p>
    In this example we are grouping fictional emails by the <b>Received</b> field. 
    However, since the condition for grouping is complex, we are using the <b>NCustomRowValue</b> to provide a custom row grouping condition.
</p>
<p>
    The example also demonstrates how to create custom GroupRow cells. Since the email group values are instances of the ENMailGroup enumeration, 
    the <b>CreateGroupRowCellsDelegate</b> is handled to provide a string representation for them.
</p>
<p>
    The example also demonstrates how to override the <b>CreateGroupingHeaderContentDelegate</b> to provide a custom grouping rule header content. 
    In example we have created, Received label serves as header content for the custom grouping rule.
</p>
"
        End Function

        #EndRegion

        #Region"Custom Row Value Provider Delegates"

        ''' <summary>
        ''' Delegate for getting a row value.
        ''' </summary>
        ''' <paramname="grid"></param>
        ''' <paramname="row"></param>
        ''' <returns></returns>
        Private Function GetRowValueDelegate(ByVal args As Nevron.Nov.Grid.NCustomRowValueGetRowValueArgs(Of Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup)) As Nevron.Nov.NNullable(Of Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup)
            Dim received As System.DateTime = CDate(args.DataSource.GetValue(args.Row, "Received"))
            Dim dayOfWeek As System.DayOfWeek = Me.m_Now.DayOfWeek
            Dim todayStart As System.DateTime = New System.DateTime(Me.m_Now.Year, Me.m_Now.Month, Me.m_Now.Day, 0, 0, 0)
            Dim yesterdayStart As System.DateTime = todayStart - New System.TimeSpan(1, 0, 0, 0)

            ' today
            If received >= todayStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Today

            ' yesterday
            If received >= yesterdayStart AndAlso received < todayStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Yesterday

            ' check weeks
            If True Then
                Dim lastWeekEnd As System.DateTime = todayStart - New System.TimeSpan((CInt(dayOfWeek) - 1), 0, 0, 0)

                If received < lastWeekEnd Then
                    Dim lastWeekStart As System.DateTime = lastWeekEnd - New System.TimeSpan(7, 0, 0, 0)
                    If received >= lastWeekStart AndAlso received < lastWeekEnd Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.LastWeek
                    Dim twoWeekAgoStart As System.DateTime = lastWeekStart - New System.TimeSpan(7, 0, 0, 0)
                    If received >= twoWeekAgoStart AndAlso received < lastWeekStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.TwoWeeksAgo
                    Dim threeWeeksAgoStart As System.DateTime = twoWeekAgoStart - New System.TimeSpan(7, 0, 0, 0)
                    If received >= threeWeeksAgoStart AndAlso received < twoWeekAgoStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.ThreeWeeksAgo
                End If
            End If

            ' check days of week
            If True Then
                Dim dayOfWeekStart As System.DateTime = todayStart
                Dim dayOfWeekEnd As System.DateTime = todayStart + New System.TimeSpan(24, 0, 0)

                While True

                    If received >= dayOfWeekStart AndAlso received < dayOfWeekEnd Then
                        Select Case dayOfWeek
                            Case System.DayOfWeek.Friday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Friday
                            Case System.DayOfWeek.Monday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Monday
                            Case System.DayOfWeek.Saturday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Saturday
                            Case System.DayOfWeek.Sunday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Sunday
                            Case System.DayOfWeek.Thursday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Thursday
                            Case System.DayOfWeek.Tuesday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Tuesday
                            Case System.DayOfWeek.Wednesday
                                Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Wednesday
                            Case Else
                                Throw New System.Exception("New DayOfWeek?")
                        End Select
                    End If

                    dayOfWeek = dayOfWeek - 1
                    dayOfWeekStart = dayOfWeekStart - New System.TimeSpan(24, 0, 0)
                    dayOfWeekEnd = dayOfWeekEnd - New System.TimeSpan(24, 0, 0)
                    If dayOfWeek = System.DayOfWeek.Sunday Then Exit While
                End While
            End If

            ' check months
            Dim lastMonthEnd As System.DateTime = New System.DateTime(Me.m_Now.Year, Me.m_Now.Month, 1, 0, 0, 0)
            Dim lastMonthStart As System.DateTime = lastMonthEnd.AddMonths(-1)
            If received >= lastMonthStart AndAlso received < lastMonthEnd Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.LastMonth
            Dim twoMonthsAgoStart As System.DateTime = lastMonthStart - New System.TimeSpan(7, 0, 0, 0)
            If received >= twoMonthsAgoStart AndAlso received < lastMonthStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.TwoMonthsAgo
            Dim threeMonthsAgoStart As System.DateTime = twoMonthsAgoStart - New System.TimeSpan(7, 0, 0, 0)
            If received >= threeMonthsAgoStart AndAlso received < twoMonthsAgoStart Then Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.ThreeMonthsAgo
            Return Nevron.Nov.Examples.Grid.NCustomGroupingExample.ENMailGroup.Older
        End Function

        #EndRegion
     
        #Region"Mail Data Source"

        ''' <summary>
        ''' Creates a fictional data source that represents received e-mails.
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateMailDataSource() As Nevron.Nov.Data.NDataSource
            ' create a a dummy data table that represents a simple organization.
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable(New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("From", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Subject", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Received", GetType(System.DateTime)), New Nevron.Nov.Data.NFieldInfo("Size", GetType(System.[String]))})
            Dim subjects As String() = New String() {"VIVACOM BILL", "SharePoint Users", "USB Sticks", "Garden Conference", ".NET Core and .NET Native", "Hackers Attack", "Week in Review", "Big Data Analytics", "Encryption Compromise", "Grid Issues", "DSC SOT BILL", "Data Security Bulletin", "How Cybercriminals use Facebook", "Empowering Users Success", "Boost your Income", "The AMISH way to motivate", "Daily news"}
            Dim rnd As System.Random = New System.Random()

            For i As Integer = 0 To 600 - 1
                Dim name As String = NDummyDataSource.RandomPersonInfo().Name
                Dim subject As String = subjects(rnd.[Next](subjects.Length))
                Dim received As System.DateTime = Me.m_Now - New System.TimeSpan(rnd.[Next](60), rnd.[Next](24), rnd.[Next](60), 0)
                Dim size As String = (10 + rnd.[Next](CInt((100)))).ToString() & " KB"
                dataTable.AddRow(name, subject, received, size)
            Next

            Return New Nevron.Nov.Data.NDataSource(dataTable)
        End Function

        #EndRegion

        #Region"Fields"

        Private m_Now As System.DateTime

        #EndRegion

        #Region"Schema"

        ''' <summary>
        ''' Schema associated with NCustomGroupingExample.
        ''' </summary>
        Public Shared ReadOnly NCustomGroupingExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"MailGroup"

        Public Enum ENMailGroup
            Today
            Yesterday
            Sunday
            Saturday
            Friday
            Thursday
            Wednesday
            Tuesday
            Monday
            LastWeek
            TwoWeeksAgo
            ThreeWeeksAgo
            LastMonth
            TwoMonthsAgo
            ThreeMonthsAgo
            Older
        End Enum


        #EndRegion
    End Class
End Namespace
