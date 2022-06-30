Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Diagram
Imports Nevron.Nov.Graphics

Namespace Nevron.Nov.Examples.Diagram
	''' <summary>
	''' The NTemplate abstract class serves as base class for all programmable templates
	''' </summary>
	Public MustInherit Class NTemplate
		#Region"Constructors"

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
            Me.Initialize("Template")
        End Sub

		''' <summary>
		''' Initializer contructor
		''' </summary>
		''' <paramname="name">name of the template</param>
		Public Sub New(ByVal name As String)
            Me.Initialize(name)
        End Sub

		#EndRegion

		#Region"Events"

		''' <summary>
		''' Fired when the template has been changed
		''' </summary>
		Public Event TemplateChanged As System.EventHandler

		#EndRegion

		#Region"Properties"

		''' <summary>
		''' Gets/set the template node
		''' </summary>
		Public Property Name As String
            Get
                Return Me.m_sName
            End Get
            Set(ByVal value As String)
                If Equals(value, Nothing) Then Throw New System.NullReferenceException()
                If Equals(value, Me.m_sName) Then Return
                Me.m_sName = value
                Me.OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Gets or sets the template origin
		''' </summary>
		''' <remarks>
		''' The origin defines the location at which the template will be instanciated in the document
		''' </remarks>
		Public Property Origin As Nevron.Nov.Graphics.NPoint
            Get
                Return Me.m_Origin
            End Get
            Set(ByVal value As Nevron.Nov.Graphics.NPoint)
                If value = Me.m_Origin Then Return
                Me.m_Origin = value
                Me.OnTemplateChanged()
            End Set
        End Property

		''' <summary>
		''' Specifies the history transaction description 
		''' </summary>
		Public Property TransactionDescription As String
            Get
                Return Me.m_sTransactionDescription
            End Get
            Set(ByVal value As String)
                If Equals(value, Me.m_sTransactionDescription) Then Return
                If Equals(value, Nothing) Then Throw New System.NullReferenceException()
                Me.m_sTransactionDescription = value
            End Set
        End Property

		
		#EndRegion
        
		#Region"Overridable"

		''' <summary>
		''' Creates the template in the specified document
		''' </summary>
		''' <remarks>
		''' This method will call the CreateTemplate method. 
		''' The call will be embraced in a transaction with the specified TransactionDescription
		''' </remarks>
		''' <paramname="document">document in which to create the template</param>
		''' <returns>true if the template was successfully created, otherwise false</returns> 
		Public Overridable Function Create(ByVal document As Nevron.Nov.Diagram.NDrawingDocument) As Boolean
            If document Is Nothing Then Throw New System.ArgumentNullException("document")
            document.StartHistoryTransaction(Me.m_sTransactionDescription)

            Try
                Me.CreateTemplate(document)
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteLine("Failed to create template. Exception was: " & ex.Message)
                document.RollbackHistoryTransaction()
                Return False
            End Try

            document.CommitHistoryTransaction()
            Return True
        End Function

		''' <summary>
		''' Obtains a dynamic human readable description of the template
		''' </summary>
		''' <returns>template description</returns>
		Public MustOverride Function GetDescription() As String
	
		#EndRegion

		#Region"Protected overridable"

		''' <summary>
		''' Must override to create the template
		''' </summary>
		''' <paramname="document">document in which to create the template</param>
		Protected MustOverride Sub CreateTemplate(ByVal document As Nevron.Nov.Diagram.NDrawingDocument)
		''' <summary>
		''' Called when the template has changed
		''' </summary>
		''' <remarks>
		''' This implementation will raise the TemplateChanged event
		''' </remarks>
		Protected Overridable Sub OnTemplateChanged()
            RaiseEvent TemplateChanged(Me, Nothing)
        End Sub
		
		#EndRegion

		#Region"Private"

		Private Sub Initialize(ByVal name As String)
            If Equals(name, Nothing) Then Throw New System.ArgumentNullException("name")

			' props
			Me.m_sTransactionDescription = "Create Template"
            Me.m_sName = name
            Me.m_Origin = New Nevron.Nov.Graphics.NPoint()
        End Sub


		#EndRegion

		#Region"Fields"

		Friend m_sName As String
        Friend m_sTransactionDescription As String
        Friend m_Origin As Nevron.Nov.Graphics.NPoint

		#EndRegion
	End Class
End Namespace
