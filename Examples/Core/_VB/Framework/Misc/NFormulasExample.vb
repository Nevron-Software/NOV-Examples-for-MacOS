Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Formulas
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NFormulasExample
        Inherits NExampleBase
        #Region"Constructors"

        Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.Framework.NFormulasExample.NFormulasExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NFormulasExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_FormulaEngine = New Nevron.Nov.Formulas.NFormulaEngine()
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Call Nevron.Nov.Layout.NDockLayout.SetDockArea(stack, Nevron.Nov.Layout.ENDockArea.Center)
            Me.m_InputTextBox = New Nevron.Nov.UI.NTextBox()
            stack.Add(Me.m_InputTextBox)
            Dim hstack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            hstack.Direction = Nevron.Nov.Layout.ENHVDirection.LeftToRight
            stack.Add(hstack)
            Dim evaluateButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Evaluate")
            AddHandler evaluateButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnEvaluateButtonClick)
            hstack.Add(evaluateButton)
            Dim evaluateAllButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Evaluate All")
            AddHandler evaluateAllButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnEvaluateAllButtonClick)
            hstack.Add(evaluateAllButton)
            Me.m_ResultTextBox = New Nevron.Nov.UI.NTextBox()
            stack.Add(Me.m_ResultTextBox)
            Return stack
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim testsView As Nevron.Nov.UI.NTreeView = Me.CreateTestsTreeView()
            Return New Nevron.Nov.UI.NGroupBox("Predefined Examples", testsView)
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
Demonstrates the strong support for Formulas. Formula expressions can be assigned to any DOM Element property.
</p>
"
        End Function

        #EndRegion

        #Region"Implementation"

        Private Function CreateTestsTreeView() As Nevron.Nov.UI.NTreeView
            Dim categoryItem, folderItem As Nevron.Nov.UI.NTreeViewItem
            Dim treeView As Nevron.Nov.UI.NTreeView = New Nevron.Nov.UI.NTreeView()
            Me.m_TestsTreeView = treeView

            #Region"Operators"
            
            folderItem = New Nevron.Nov.UI.NTreeViewItem("Operators")
            treeView.Items.Add(folderItem)

            #Region"Arithmetic Operators"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Arithmetic")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"+10", "-10", "-ARRAY(10, 12)", "10 ^ 2", "ARRAY(10, 12) ^ 2", "10 * 2", "ARRAY(10, 12) * 2", "10 / 2", "ARRAY(10, 12) / 2", "10 + 2", "ARRAY(10, 12) + 2", "ARRAY(10, 12) + ARRAY(12, 23)", "10 + ""Nevron""", "10 - 2", "ARRAY(10, 12) - 2", "ARRAY(10, 12) - ARRAY(12, 23)"})

            #EndRegion

            #Region"Comparision Operators"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Comparision")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"10 > 2", "10 < 2", "10 >= 2", "10 >= 10", "10 <= 2", "10 <= 10", "10 == 2", "10 != 2"})

            #EndRegion

            #Region"Logical operators"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Logical")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"true && false", "true || false", "!true"})

            #EndRegion

            #Region"Bitwise operators"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Bitwise")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"7 & 2", "5 | 3", "~1"})

            #EndRegion

            #Region"Assignment operators"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Assignment")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"a=5; b=3; a+b;", "a=5; a+=3", "a=5; a-=3"})

            #EndRegion

            #EndRegion

            #Region"Functions"

            folderItem = New Nevron.Nov.UI.NTreeViewItem("Functions")
            treeView.Items.Add(folderItem)

            #Region"Bitwise "

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Bitwise")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"BITAND(7,2)", "BITNOT(1)", "BITOR(5,3)", "BITXOR(5,3)"})
                
            #EndRegion

            #Region"Logical"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Logical")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"AND(true, false)", "AND(true, false)", "IF(true, 2, 10)", "IF(false, 2, 10)", "NOT(true)", "OR(true, false)"})

            #EndRegion

            #Region"Mathematical"

            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Mathematical")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"ABS(-2.5)", "CEILING(1.7)", "CEILING(1.7, 0.25)", "FLOOR(1.7)", "FLOOR(1.7, 0.25)", "INT(1.2)", "INT(-1.2)", "INTUP(1.2)", "INTUP(-1.2)", "LN(10)", "LOG10(10)", "MAGNITUDE(3, 4)", "MAX(1, 3, 2)", "MIN(1, 3, 2)", "MOD(5, 1.4)", "MOD(5, -1.4)", "POW(10, 2)", "ROUND(123.654,2)", "ROUND(123.654,0)", "ROUND(123.654,-1)", "SIGN(-10)", "SIGN(0)", "SQRT(4)", "SUM(1,2,3)", "TRUNC(123.654,2)", "TRUNC(123.654,0)", "TRUNC(123.654,-1)"})

            #EndRegion

            #Region"Text"
            
            categoryItem = New Nevron.Nov.UI.NTreeViewItem("Text")
            folderItem.Items.Add(categoryItem)
            Me.CreateTestItems(categoryItem, New String() {"CHAR(9)", "LEN(""Hello World"")", "LOWER(""Hello World"")", "STRSAME(""Hello"", ""hello"")", "STRSAME(""Hello"", ""hello"", true)", "TRIM("" Hello World "")", "UPPER(""Hello World"")", "INDEX(0,""Hello;World"")"})

            #EndRegion

            #Region"Trigonometrical"

            Dim trigonometrical As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem("Trigonometrical")
            folderItem.Items.Add(trigonometrical)
            Me.CreateTestItems(trigonometrical, New String() {"ACOS(0)", "ANG360(1.4 + 2 * PI())", "ASIN(1)", "ATAN2(1,1)", "ATAN2(1,SQRT(3))", "ATAN(1)", "COS(0)", "COSH(PI()/4)", "PI()", "SIN(0)", "SINH(PI()/4)", "TAN(PI()/4)", "TANH(-PI()/4)"})

            #EndRegion

            #Region"Type"

            Dim type As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem("Type")
            folderItem.Items.Add(type)
            Me.CreateTestItems(type, New String() {"EMPTY()", "ISARRAY(ARRAY(10,20))", "ISARRAY(10)", "ISBOOL(true)", "ISBOOL(false)", "ISBOOL(""true"")", "ISDATETIME(10)", "ISDATETIME(DATETIME(2008,9,15))", "ISEMPTY(EMPTY())", "ISEMPTY(true)", "ISMEASURE(10[mm])", "ISMEASURE(10)", "ISNUM(10)", "ISNUM(true)", "ISSTR(true)", "ISSTR(""hello world"")", "TOBOOL(""false"")", "TOBOOL(""true"")", "TOBOOL(""hello"")", "TODATETIME(""2008-09-15 09:30:41.770"")", "TONUM(true)", "TONUM(""10"")", "TONUM(""hello"")", "TOSTR(10)"})

            #EndRegion

            #EndRegion

            treeView.ExpandAll(True)
            AddHandler treeView.SelectedPathChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnTestsTreeViewSelectedPathChanged)
            Return treeView
        End Function

        Private Sub CreateTestItems(ByVal parentItem As Nevron.Nov.UI.NTreeViewItem, ByVal formulas As String())
            For i As Integer = 0 To formulas.Length - 1
                parentItem.Items.Add(Me.CreateTestItem(formulas(i)))
            Next
        End Sub

        Private Function CreateTestItem(ByVal formula As String) As Nevron.Nov.UI.NTreeViewItem
            Dim item As Nevron.Nov.UI.NTreeViewItem = New Nevron.Nov.UI.NTreeViewItem(formula)
            item.Tag = formula
            Return item
        End Function

        Private Sub OnTestsTreeViewSelectedPathChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim item As Nevron.Nov.UI.NTreeViewItem = Me.m_TestsTreeView.SelectedItem
            If item Is Nothing OrElse item.Tag Is Nothing Then Return
            Me.m_InputTextBox.Text = item.Tag.ToString()
            Me.EvaluateFormula()
        End Sub

        Private Sub OnEvaluateButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Me.EvaluateFormula()
        End Sub

        Private Sub OnEvaluateAllButtonClick(ByVal arg As Nevron.Nov.Dom.NEventArgs)
            Dim tests As Nevron.Nov.DataStructures.NList(Of String) = New Nevron.Nov.DataStructures.NList(Of String)()
            Dim it As Nevron.Nov.DataStructures.INIterator(Of Nevron.Nov.Dom.NNode) = Me.m_TestsTreeView.GetSubtreeIterator()

            While it.MoveNext()
                Dim item As Nevron.Nov.UI.NTreeViewItem = TryCast(it.Current, Nevron.Nov.UI.NTreeViewItem)
                If item Is Nothing OrElse item.Tag Is Nothing OrElse Not (TypeOf item.Tag Is String) Then Continue While
                tests.Add(CStr(item.Tag))
            End While

            Dim stopwatch As Nevron.Nov.NStopwatch = New Nevron.Nov.NStopwatch()
            stopwatch.Start()
            Dim itcount As Integer = 10000

            For j As Integer = 0 To itcount - 1

                For i As Integer = 0 To tests.Count - 1

                    Try
                        Me.m_FormulaEngine.Evaluate(tests(i))
                    Catch ex As System.Exception
                        Me.m_ResultTextBox.Text = "Failed on test: " & tests(i) & ". Error was: " & ex.Message
                        Me.m_InputTextBox.Text = tests(i)
                        Return
                    End Try
                Next
            Next

            stopwatch.[Stop]()
            Dim ms As Integer = stopwatch.ElapsedMilliseconds
            Me.m_ResultTextBox.Text = tests.Count & " tests performed " & itcount & " times in: " & ms & " milliseconds."
        End Sub

        Private Sub EvaluateFormula()
            Try
                Dim result As Nevron.Nov.NVariant = Me.m_FormulaEngine.Evaluate(Me.m_InputTextBox.Text)
                Me.m_ResultTextBox.Text = result.ToString()
            Catch ex As System.Exception
                Me.m_ResultTextBox.Text = ex.Message
            End Try
        End Sub

        #EndRegion

        #Region"Fields"

        Private m_InputTextBox As Nevron.Nov.UI.NTextBox
        Private m_ResultTextBox As Nevron.Nov.UI.NTextBox
        Private m_TestsTreeView As Nevron.Nov.UI.NTreeView
        Private m_FormulaEngine As Nevron.Nov.Formulas.NFormulaEngine

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NFormulasExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion
    End Class
End Namespace
