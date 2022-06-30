Imports System
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NTransformContentExample
        Inherits NExampleBase
        #Region"Constructors"

        Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NTransformContentExample.NTransformContentExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTransformContentExample), NExampleBase.NExampleBaseSchema)
        End Sub

        #EndRegion

        #Region"Example"

        Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            ' create a dummy button
            Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("I can be transformed")
            Me.m_TransformContent = New Nevron.Nov.UI.NTransformContent(button)
            Me.m_TransformContent.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Me.m_TransformContent.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Return Me.m_TransformContent
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            
            ' Transform Properties
            If True Then
                Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TransformContent), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TransformContent, Nevron.Nov.UI.NTransformContent.ScaleXProperty, Nevron.Nov.UI.NTransformContent.ScaleYProperty, Nevron.Nov.UI.NTransformContent.AngleProperty, Nevron.Nov.UI.NTransformContent.StretchAtRightAnglesProperty, Nevron.Nov.UI.NTransformContent.HorizontalPlacementProperty, Nevron.Nov.UI.NTransformContent.VerticalPlacementProperty)
                Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

                For i As Integer = 0 To editors.Count - 1
                    propertiesStack.Add(editors(i))
                Next

                stack.Add(New Nevron.Nov.UI.NGroupBox("Transform Content Properties", propertiesStack))
            End If

            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use the NTransformContent widget. 
    This widget allows you to aggregate another widget and apply arbitrary rotation and scaling on the content.
</p>
"
        End Function

        #EndRegion

        #Region"Fields"

        Private m_TransformContent As Nevron.Nov.UI.NTransformContent

        #EndRegion

        #Region"Schema"

        Public Shared ReadOnly NTransformContentExampleSchema As Nevron.Nov.Dom.NSchema

        #EndRegion

        #Region"Nested Types - NDynamicContentTooltip"

        ''' <summary>
        ''' A tooltip that shows as content the current date and time
        ''' </summary>
        Public Class NDynamicContentTooltip
            Inherits Nevron.Nov.UI.NTooltip
            #Region"Constructors"

            Public Sub New()
            End Sub

            Shared Sub New()
                Nevron.Nov.Examples.UI.NTransformContentExample.NDynamicContentTooltip.NDynamicContentTooltipSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTransformContentExample.NDynamicContentTooltip), Nevron.Nov.UI.NTooltip.NTooltipSchema)
            End Sub

            #EndRegion

            #Region"Overrides - GetContent()"

            Public Overrides Function GetContent() As Nevron.Nov.UI.NWidget
                Dim now As System.DateTime = System.DateTime.Now
                Return New Nevron.Nov.UI.NLabel("I was shown at: " & now.ToString("T"))
            End Function

            #EndRegion

            #Region"Schema"

            Public Shared ReadOnly NDynamicContentTooltipSchema As Nevron.Nov.Dom.NSchema

            #EndRegion
        End Class

        #EndRegion
    End Class
End Namespace
