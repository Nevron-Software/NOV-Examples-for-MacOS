Imports System
Imports System.IO
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Globalization
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.Framework
    Public Class NLocalizationExample
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
            Nevron.Nov.Examples.Framework.NLocalizationExample.NLocalizationExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.Framework.NLocalizationExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.CreateDictionaries()
            Me.m_CalculatorHost = New Nevron.Nov.UI.NContentHolder()
            Me.m_CalculatorHost.Content = Me.CreateLoanCalculator()
            Return Me.m_CalculatorHost
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(New Nevron.Nov.UI.NLabel("Language:"))
            Dim listBox As Nevron.Nov.UI.NListBox = New Nevron.Nov.UI.NListBox()
            listBox.Items.Add(CreateListBoxItem(NResources.Image_CountryFlags_us_png, Nevron.Nov.Examples.Framework.NLocalizationExample.EnglishLanguageName))
            listBox.Items.Add(CreateListBoxItem(NResources.Image_CountryFlags_bg_png, Nevron.Nov.Examples.Framework.NLocalizationExample.BulgarianLanguageName))
            listBox.Items.Add(CreateListBoxItem(NResources.Image_CountryFlags_de_png, Nevron.Nov.Examples.Framework.NLocalizationExample.GermanLanguageName))
            listBox.Selection.SingleSelect(listBox.Items(0))
            AddHandler listBox.Selection.Selected, AddressOf Me.OnListBoxItemSelected
            stack.Add(listBox)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to take advantage of the NOV localization. Localization is the process
	of translating string literals used inside an application to different language. In your application's source
	code you should write localizable strings as NLoc.Get(""My string"") instead of directly as ""My string"".
	Thus on run time the string translation will be obtained from the localization dictionary instance.
</p>
<p>
	To see the localization in action, simply select a language from the list box on the right.
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Sub CreateDictionaries()
            Dim dictionary As Nevron.Nov.Globalization.NLocalizationDictionary = Nevron.Nov.Globalization.NLocalizationDictionary.Instance

			' Create the stream for the Bulgarian dictionary
			dictionary.SetTranslation("Loan Calculator", "Кредитен калкулатор")
            dictionary.SetTranslation("Amount:", "Количество:")
            dictionary.SetTranslation("Term in years:", "Срок в години:")
            dictionary.SetTranslation("Interest rate per year (%):", "Годишна лихва (%):")
            dictionary.SetTranslation("Repayment Summary", "Информация за погасяване")
            dictionary.SetTranslation("Monthly Payment:", "Месечна вноска:")
            dictionary.SetTranslation("Total Payments:", "Сума за връщане:")
            dictionary.SetTranslation("Total Interest:", "Общо лихви:")
            Me.m_BulgarianStream = New System.IO.MemoryStream()
            dictionary.SaveToStream(Me.m_BulgarianStream)

			' Create the stream for the German Dictionary
			dictionary.SetTranslation("Loan Calculator", "Kreditrechner")
            dictionary.SetTranslation("Amount:", "Kreditbetrag:")
            dictionary.SetTranslation("Term in years:", "Laufzeit in Jahren:")
            dictionary.SetTranslation("Interest rate per year (%):", "Zinssatz pro Jahr (%):")
            dictionary.SetTranslation("Repayment Summary", "Rückzahlung Zusammenfassung")
            dictionary.SetTranslation("Monthly Payment:", "Monatliche Bezahlung:")
            dictionary.SetTranslation("Total Payments:", "Gesamtbetrag:")
            dictionary.SetTranslation("Total Interest:", "Gesamtzins:")
            Me.m_GermanStream = New System.IO.MemoryStream()
            dictionary.SaveToStream(Me.m_GermanStream)

			' Create the stream for the English dictionary
			dictionary.SetTranslation("Loan Calculator", "Loan Calculator")
            dictionary.SetTranslation("Amount:", "Amount:")
            dictionary.SetTranslation("Term in years:", "Term in years:")
            dictionary.SetTranslation("Interest rate per year (%):", "Interest rate per year (%):")
            dictionary.SetTranslation("Repayment Summary", "Repayment Summary")
            dictionary.SetTranslation("Monthly Payment:", "Monthly Payment:")
            dictionary.SetTranslation("Total Payments:", "Total Payments:")
            dictionary.SetTranslation("Total Interest:", "Total Interest:")
            Me.m_EnglishStream = New System.IO.MemoryStream()
            dictionary.SaveToStream(Me.m_EnglishStream)
        End Sub

        Private Function CreateLoanCalculator() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 10)
            stack.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            stack.VerticalSpacing = Nevron.Nov.NDesign.VerticalSpacing * 2
            Dim titleLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(Nevron.Nov.NLoc.[Get]("Loan Calculator"))
            titleLabel.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 16)
            titleLabel.Margins = New Nevron.Nov.Graphics.NMargins(0, 0, 0, Nevron.Nov.NDesign.VerticalSpacing)
            titleLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            stack.Add(titleLabel)
            Me.m_AmountUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_AmountUpDown.Value = 10000
            Me.m_AmountUpDown.[Step] = 500
            AddHandler Me.m_AmountUpDown.ValueChanged, AddressOf Me.OnUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Amount:"), Me.m_AmountUpDown))
            Me.m_TermUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_TermUpDown.Value = 8
            AddHandler Me.m_TermUpDown.ValueChanged, AddressOf Me.OnUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Term in years:"), Me.m_TermUpDown))
            Me.m_RateUpDown = New Nevron.Nov.UI.NNumericUpDown()
            Me.m_RateUpDown.Value = 5
            Me.m_RateUpDown.[Step] = 0.1
            Me.m_RateUpDown.DecimalPlaces = 2
            AddHandler Me.m_RateUpDown.ValueChanged, AddressOf Me.OnUpDownValueChanged
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Interest rate per year (%):"), Me.m_RateUpDown))

			' Create the results labels
			Dim repaymentLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(Nevron.Nov.NLoc.[Get]("Repayment Summary"))
            repaymentLabel.Margins = New Nevron.Nov.Graphics.NMargins(0, Nevron.Nov.NDesign.VerticalSpacing * 5, 0, 0)
            repaymentLabel.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 12, Nevron.Nov.Graphics.ENFontStyle.Underline)
            repaymentLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleCenter
            stack.Add(repaymentLabel)
            Me.m_MonthlyPaymentLabel = New Nevron.Nov.UI.NLabel()
            Me.m_MonthlyPaymentLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleRight
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Monthly Payment:"), Me.m_MonthlyPaymentLabel))
            Me.m_TotalPaymentsLabel = New Nevron.Nov.UI.NLabel()
            Me.m_TotalPaymentsLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleRight
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Total Payments:"), Me.m_TotalPaymentsLabel))
            Me.m_TotalInterestLabel = New Nevron.Nov.UI.NLabel()
            Me.m_TotalInterestLabel.TextAlignment = Nevron.Nov.ENContentAlignment.MiddleRight
            stack.Add(Nevron.Nov.UI.NPairBox.Create(Nevron.Nov.NLoc.[Get]("Total Interest:"), Me.m_TotalInterestLabel))
            Me.CalculateResult()
            Return New Nevron.Nov.UI.NUniSizeBoxGroup(stack)
        End Function

        Private Sub CalculateResult()
            Dim amount As Double = Me.m_AmountUpDown.Value
            Dim payments As Double = Me.m_TermUpDown.Value * 12
            Dim monthlyRate As Double = Me.m_RateUpDown.Value / 100 / 12

			' Calculate the repayment values
			Dim x As Double = System.Math.Pow(1 + monthlyRate, payments)
            Dim monthly As Double = (amount * x * monthlyRate) / (x - 1)
            Dim total As Double = monthly * payments
            Dim interest As Double = total - amount

			' Display the result
			Me.m_MonthlyPaymentLabel.Text = monthly.ToString("C")
            Me.m_TotalPaymentsLabel.Text = total.ToString("C")
            Me.m_TotalInterestLabel.Text = interest.ToString("C")
        End Sub

        Private Function CreateListBoxItem(ByVal icon As Nevron.Nov.Graphics.NImage, ByVal languageName As String) As Nevron.Nov.UI.NListBoxItem
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(icon, languageName, Nevron.Nov.UI.ENPairBoxRelation.Box1BeforeBox2)
            pairBox.Spacing = Nevron.Nov.NDesign.VerticalSpacing
            Dim item As Nevron.Nov.UI.NListBoxItem = New Nevron.Nov.UI.NListBoxItem(pairBox)
            item.Text = languageName
            Return item
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnUpDownValueChanged(ByVal arg As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.CalculateResult()
        End Sub

        Private Sub OnListBoxItemSelected(ByVal arg As Nevron.Nov.UI.NSelectEventArgs(Of Nevron.Nov.UI.NListBoxItem))
            Dim selectedItem As Nevron.Nov.UI.NListBoxItem = arg.Item

            Select Case selectedItem.Text
                Case Nevron.Nov.Examples.Framework.NLocalizationExample.EnglishLanguageName
					' Load the English dictionary
					Me.m_EnglishStream.Position = 0
                    Call Nevron.Nov.Globalization.NLocalizationDictionary.Instance.LoadFromStream(Me.m_EnglishStream)
                Case Nevron.Nov.Examples.Framework.NLocalizationExample.BulgarianLanguageName
					' Load the Bulgarian dictionary
					Me.m_BulgarianStream.Position = 0
                    Call Nevron.Nov.Globalization.NLocalizationDictionary.Instance.LoadFromStream(Me.m_BulgarianStream)
                Case Nevron.Nov.Examples.Framework.NLocalizationExample.GermanLanguageName
					' Load the German dictionary
					Me.m_GermanStream.Position = 0
                    Call Nevron.Nov.Globalization.NLocalizationDictionary.Instance.LoadFromStream(Me.m_GermanStream)
            End Select

			' Recreate the Loan Calculator
			Me.m_CalculatorHost.Content = Me.CreateLoanCalculator()
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_CalculatorHost As Nevron.Nov.UI.NContentHolder
        Private m_AmountUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_TermUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_RateUpDown As Nevron.Nov.UI.NNumericUpDown
        Private m_MonthlyPaymentLabel As Nevron.Nov.UI.NLabel
        Private m_TotalPaymentsLabel As Nevron.Nov.UI.NLabel
        Private m_TotalInterestLabel As Nevron.Nov.UI.NLabel
        Private m_EnglishStream As System.IO.Stream
        Private m_BulgarianStream As System.IO.Stream
        Private m_GermanStream As System.IO.Stream

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NLocalizationExample.
		''' </summary>
		Public Shared ReadOnly NLocalizationExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Const EnglishLanguageName As String = "English (US)"
        Private Const BulgarianLanguageName As String = "Bulgarian"
        Private Const GermanLanguageName As String = "German"

		#EndRegion
	End Class
End Namespace
