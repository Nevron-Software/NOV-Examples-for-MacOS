Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Layout
Imports Nevron.Nov.Serialization
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
	''' <summary>
	''' The example demonstrates how to serialize / deserialize .NET (CLR) objects
	''' </summary>
	Public Class NCLRSerializationExample
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
            Nevron.Nov.Examples.Framework.NCLRSerializationExample.NCLRSerializationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NCLRSerializationExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Overrides"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            stack.MinWidth = 400
            Me.m_NameTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_AddressTextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_MarriedCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_GenderComboBox = New Nevron.Nov.UI.NComboBox()
            Me.m_GenderComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Male"))
            Me.m_GenderComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem("Female"))
            Me.m_GenderComboBox.SelectedIndex = 0
            Me.m_OtherTextBox = New Nevron.Nov.UI.NTextBox()
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Name (string):"), Me.m_NameTextBox, True))
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Address (string):"), Me.m_AddressTextBox, True))
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Married (boolean):"), Me.m_MarriedCheckBox, True))
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Gender (singleton):"), Me.m_GenderComboBox, True))
            stack.Add(New Nevron.Nov.UI.NPairBox(New Nevron.Nov.UI.NLabel("Other (string, non serialized):"), Me.m_OtherTextBox, True))
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim saveStateButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Save")
            AddHandler saveStateButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnSaveStateButtonClick)
            stack.Add(saveStateButton)
            Dim loadStateButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Load")
            AddHandler loadStateButton.Click, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnLoadStateButtonClick)
            stack.Add(loadStateButton)
            Return stack
        End Function
		''' <summary>
		''' 
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function GetExampleDescription() As String
            Return "
<p>This example demonstrates how to use CLR serialization in order to serialize .NET objects.</p>
<p>Press the ""Save"" button to save the current state of the form.</p>
<p>Press the ""Load"" button to load a previously loaded form state.</p>
<p><b>Note:</b> The value of ""Other"" is not persisted, because this field is marked as non serialized.</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnSaveStateButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            Try
                Me.m_MemoryStream = New System.IO.MemoryStream()
                Dim serializer As Nevron.Nov.Serialization.NSerializer = New Nevron.Nov.Serialization.NSerializer()
                Dim serializationObject As Nevron.Nov.Examples.Framework.NCLRSerializationExample.PersonInfo = New Nevron.Nov.Examples.Framework.NCLRSerializationExample.PersonInfo(Me.m_NameTextBox.Text, Me.m_AddressTextBox.Text, Me.m_MarriedCheckBox.Checked, If(Me.m_GenderComboBox.SelectedIndex = 0, Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Male, Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Female), Me.m_OtherTextBox.Text)
                serializer.SaveToStream(serializationObject, Me.m_MemoryStream, Nevron.Nov.Serialization.ENPersistencyFormat.Binary)
            Catch ex As System.Exception
                Call Nevron.Nov.NTrace.WriteLine(ex.Message)
            End Try
        End Sub

        Private Sub OnLoadStateButtonClick(ByVal arg1 As Nevron.Nov.Dom.NEventArgs)
            If Me.m_MemoryStream Is Nothing Then Return
            Me.m_MemoryStream.Seek(0, System.IO.SeekOrigin.Begin)
            Dim deserializer As Nevron.Nov.Serialization.NDeserializer = New Nevron.Nov.Serialization.NDeserializer()
            Dim serializationObject As Nevron.Nov.Examples.Framework.NCLRSerializationExample.PersonInfo = CType(deserializer.LoadFromStream(Me.m_MemoryStream, Nevron.Nov.Serialization.ENPersistencyFormat.Binary), Nevron.Nov.Examples.Framework.NCLRSerializationExample.PersonInfo)
            Me.m_NameTextBox.Text = serializationObject.Name
            Me.m_AddressTextBox.Text = serializationObject.Address
            Me.m_MarriedCheckBox.Checked = serializationObject.Married
            Me.m_GenderComboBox.SelectedIndex = If(serializationObject.Gender Is Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Male, 0, 1)
            Me.m_OtherTextBox.Text = serializationObject.Other
        End Sub

		#EndRegion

		#Region"Nested Types"

		''' <summary>
		''' Represents a singleton object
		''' </summary>
		Public Class GenderSingleton
			#Region"Constructors"

			''' <summary>
			''' Initializer constructor
			''' </summary>
			''' <paramname="genderName"></param>
			Private Sub New(ByVal genderName As String)
                Me.m_GenderName = genderName
            End Sub

			#EndRegion

			#Region"Static Methods"

			Public Shared Function GetSurrogateSerializer_NoObf(ByVal singleton As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton) As Object
                Return New Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSurrogate(singleton)
            End Function

			#EndRegion

			#Region"Fields"

			Private m_GenderName As String

			#EndRegion

			#Region"Static Fields"

			Public Shared Male As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton = New Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton("Male")
            Public Shared Female As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton = New Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton("Female")

			#EndRegion
		End Class
		''' <summary>
		''' Represents a class used to perform the actual serialization of the NGenderSingleton class
		''' </summary>
		Public Class GenderSurrogate
            Implements Nevron.Nov.Serialization.INSurrogateSerializer
			#Region"Constructors"

			''' <summary>
			''' Default constructor
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Initializer constructor
			''' </summary>
			''' <paramname="instance"></param>
			Public Sub New(ByVal instance As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton)
                Me.IsMale = instance Is Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Male
            End Sub

			#EndRegion

			#Region"INSurrogateSerializer"

			Public Function GetRealObject() As Object Implements Global.Nevron.Nov.Serialization.INSurrogateSerializer.GetRealObject
                If Me.IsMale Then
                    Return Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Male
                Else
                    Return Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton.Female
                End If
            End Function

            Public Sub ApplyToRealObject(ByVal obj As Object) Implements Global.Nevron.Nov.Serialization.INSurrogateSerializer.ApplyToRealObject
				' not implemented
			End Sub

			#EndRegion

			#Region"Fields"

			Public IsMale As Boolean

			#EndRegion
		End Class
		''' <summary>
		''' Represents a class showing some 
		''' </summary>
		Public Class PersonInfo
			#Region"Constructors"

			''' <summary>
			''' Default constructor
			''' </summary>
			Public Sub New()
            End Sub
			''' <summary>
			''' Initializer constructor
			''' </summary>
			''' <paramname="a"></param>
			''' <paramname="b"></param>
			''' <paramname="c"></param>
			Public Sub New(ByVal name As String, ByVal address As String, ByVal married As Boolean, ByVal gender As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton, ByVal other As String)
                Me.Name = name
                Me.Address = address
                Me.Married = married
                Me.Gender = gender
                Me.Other = other
            End Sub

			#EndRegion

			#Region"Fields"

			Public Name As String
            Public Address As String
            Public Married As Boolean
            Public Gender As Nevron.Nov.Examples.Framework.NCLRSerializationExample.GenderSingleton
            <Nevron.Nov.Serialization.NNonSerializedAttribute>
            Public Other As String

			#EndRegion
		End Class

		#EndRegion

		#Region"Fields"

		Private m_NameTextBox As Nevron.Nov.UI.NTextBox
        Private m_AddressTextBox As Nevron.Nov.UI.NTextBox
        Private m_MarriedCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_GenderComboBox As Nevron.Nov.UI.NComboBox
        Private m_OtherTextBox As Nevron.Nov.UI.NTextBox
        Private m_MemoryStream As System.IO.MemoryStream

		#EndRegion

		#Region"Static"

		Public Shared ReadOnly NCLRSerializationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
