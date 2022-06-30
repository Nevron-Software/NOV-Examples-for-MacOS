Imports Nevron.Nov.Graphics
Imports System

Namespace Nevron.Nov.Examples.Chart
	''' <summary>
	''' Helper class that generates sample data for stock/financial series
	''' </summary>
	Friend Class NStockDataGenerator
		#Region"Constructors"

		''' <summary>
		''' Initializer constructor
		''' </summary>
		''' <paramname="range"></param>
		''' <paramname="reversalFactor"></param>
		''' <paramname="valueScale"></param>
		Friend Sub New(ByVal range As Nevron.Nov.Graphics.NRange, ByVal reversalFactor As Double, ByVal valueScale As Double)
            Me.m_Rand = New System.Random()
            Me.m_Range = range
            Me.m_Direction = 1
            Me.m_StepsInCurrentTrend = 0
            Me.m_Value = 0
            Me.m_ReversalPorbability = 0
            Me.m_ReversalFactor = reversalFactor
            Me.m_ValueScale = valueScale
        End Sub

		#EndRegion

		#Region"Methods"

		''' <summary>
		''' Resets the enumerator
		''' </summary>
		Friend Sub Reset()
            Me.m_Direction = 1
            Me.m_StepsInCurrentTrend = 0
            Me.m_Value = (Me.m_Range.Begin + Me.m_Range.[End]) / 2
            Me.m_ReversalPorbability = 0
        End Sub
		''' <summary>
		''' Gets the next price value
		''' </summary>
		''' <returns></returns>
		Friend Function GetNextValue() As Double
            Dim nNewValueDirection As Integer = 0

            If Me.m_Value <= Me.m_Range.Begin Then
                If Me.m_Direction = -1 Then
                    Me.m_ReversalPorbability = 1.0
                Else
                    Me.m_ReversalPorbability = 0.0
                End If

                nNewValueDirection = 1
            ElseIf Me.m_Value >= Me.m_Range.[End] Then

                If Me.m_Direction = 1 Then
                    Me.m_ReversalPorbability = 1.0
                Else
                    Me.m_ReversalPorbability = 0.0
                End If

                nNewValueDirection = -1
            Else

                If Me.m_Rand.NextDouble() < 0.80 Then
                    nNewValueDirection = Me.m_Direction
                Else
                    nNewValueDirection = -Me.m_Direction
                End If

                Me.m_ReversalPorbability += Me.m_StepsInCurrentTrend * Me.m_ReversalFactor
            End If

            If Me.m_Rand.NextDouble() < Me.m_ReversalPorbability Then
                Me.m_Direction = -Me.m_Direction
                Me.m_ReversalPorbability = 0
                Me.m_StepsInCurrentTrend = 0
            Else
                Me.m_StepsInCurrentTrend += 1
            End If

            Me.m_Value += nNewValueDirection * Me.m_Rand.NextDouble() * Me.m_ValueScale
            Return Me.m_Value
        End Function

		#EndRegion

		#Region"Fields"

		Private m_Rand As System.Random
        Private m_Range As Nevron.Nov.Graphics.NRange
        Private m_Direction As Integer
        Private m_StepsInCurrentTrend As Integer
        Private m_Value As Double
        Private m_ReversalPorbability As Double
        Private m_ReversalFactor As Double
        Private m_ValueScale As Double

		#EndRegion
	End Class
End Namespace
