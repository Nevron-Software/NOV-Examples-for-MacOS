Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace Nevron.Nov.Examples.Diagram
    ''' <summary>
    ''' Enumerates the predefined tree layout schemes
    ''' </summary>
    Public Enum ENTreeLayoutType
        ''' <summary>
        ''' Do not use schemes
        ''' </summary>
        None
        ''' <summary>
        ''' Layered tree layout
        ''' </summary>
        Layered
        ''' <summary>
        ''' Layered tree layout with layers aligned to the left
        ''' </summary>
        LayeredLeftAligned
        ''' <summary>
        ''' Layered tree layout with layers aligned to the right
        ''' </summary>
        LayeredRightAligned
    End Enum
End Namespace
