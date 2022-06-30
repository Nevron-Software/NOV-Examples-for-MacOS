Imports System
Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    ''' <summary>
    ''' The example shows how to work with the NTextBox widget
    ''' </summary>
    Public Class NTextBoxExample
        Inherits NExampleBase
		#Region"Constructors"

		Public Sub New()
        End Sub

        Shared Sub New()
            Nevron.Nov.Examples.UI.NTextBoxExample.NTextBoxExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NTextBoxExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
#Region"InternalCode"
			'string text1 = "श्रीमद् श्रीमद् श्रीमद् भगवद्गीता अध्याय अर्जुन विषाद योग धृतराष्ट्र उवाच। धर्मक्षेत्रे कुरुक्षेत्रे समवेता युयुत्सवः मामकाः पाण्डवाश्चैव किमकुर्वत संजय";
'			string text1 = "The LayoutEngine does all the work necessary to display Unicode text written in languages with complex writing systems such as Hindi (हिन्दी) Thai (ไทย) and Arabic (العربية). Here's a sample of some text written in Sanskrit: श्रीमद् श्रीमद् श्रीमद् भगवद्गीता अध्याय अर्जुन विषाद योग धृतराष्ट्र उवाच। धर्मक्षेत्रे कुरुक्षेत्रे समवेता युयुत्सवः मामकाः पाण्डवाश्चैव किमकुर्वत संजय Here's a sample of some text written in Arabic: أساسًا، تتعامل الحواسيب فقط مع الأرقام، وتقوم بتخزين الأحرف والمحارف الأخرى بعد أن تُعطي رقما معينا لكل واحد منها. وقبل اختراع here's a sample of some text written in Thai: บทที่๑พายุไซโคลนโดโรธีอาศัยอยู่ท่ามกลางทุ่งใหญ่ในแคนซัสกับลุงเฮนรีชาวไร่และป้าเอ็มภรรยาชาวไร่บ้านของพวกเขาหลังเล็กเพราะไม้สร้างบ้านต้องขนมาด้วยเกวียนเป็นระยะทางหลายไมล์";
'			string text1 = "Malyam: ഉൾപ്പെടുന്ന ഒരു ലിപിയാണ് മലയാള ലിപി. മലയാള ഭാഷ എഴുതന്നതിനാണ് മുഖ്യമായും ഈ ലിപി ഉപയോഗിക്കുന്നത്. സംസ്കൃതം, കൊങ്കണി, തുളു എന്നീ ഭാഷകൾ എഴുതുന്നതിനും വളരെക്കുറച്ച് ആളുകൾ മാത്രം സംസാരിക്കുന്ന പണിയ, കുറുമ്പ തുടങ്ങിയ ഭാഷകൾ എഴുതുന്നതിനും മലയാളലിപി ഉപയോഗിക്കാറുണ്ട്";
'			string text1 = "Once upon a time a Wolf was lapping at a spring on a hillside, when, looking up, what should he see but a Lamb just beginning to drink a little lower down. ‘There’s my supper,’ thought he, ‘if only I can find some excuse to seize it.’ Then he called out to the Lamb, ‘How dare you muddle the water from which I am drinking?’ ‘Nay, master, nay,’ said Lambikin; ‘if the water be muddy up there, I cannot be the cause of it, for it runs down from you to me.’ ‘Well, then,’ said the Wolf, ‘why did you call me bad names this time last year?’ ‘That cannot be,’ said the Lamb; ‘I am only six months old.’ ‘I don’t care,’ snarled the Wolf; ‘if it was not you it was your father;’ and with that he rushed upon the poor little Lamb and .WARRA WARRA WARRA WARRA WARRA .ate her all up. But before she died she gasped out .’Any excuse will serve a tyrant.’";
'          string text1 = "أساسًا، تتعامل الحواسيب فقط مع الأرقام، وتقوم بتخزين الأحرف والمحارف الأخرى بعد أن تُعطي رقما معينا لكل واحد منها. وقبل اختراع";
'          string text1 = "CAPITAL" + (char)'A' + (char)0x0301 + (char)'B' + (char)0x0327 + 'C' + (char)0x0308 + (char)'a' + (char)0x0301 + (char)'b' + (char)0x0327 + 'c' + (char)0x0308; ;
'          string text1 = "T" + (char)0x0327 + 'c' + (char)0x0308; 
            ' string text1 = "C" + (char)0x0308;
			'string text1 = "This is some text. This is some text. This is some text.";
#EndRegion

			Me.m_TextBox = New Nevron.Nov.UI.NTextBox()
            Me.m_TextBox.Hint = "Enter some text here."
            Return Me.m_TextBox
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
			
			' font families
			Me.m_FontFamiliesComboBox = New Nevron.Nov.UI.NComboBox()
            Dim fontFamilies As String() = Nevron.Nov.NApplication.FontService.InstalledFontsMap.FontFamilies
            Dim selectedIndex As Integer = 0

            For i As Integer = 0 To fontFamilies.Length - 1
                Me.m_FontFamiliesComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(fontFamilies(i)))

                If Equals(fontFamilies(i), Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName) Then
                    selectedIndex = i
                End If
            Next

            Me.m_FontFamiliesComboBox.SelectedIndex = selectedIndex
            AddHandler Me.m_FontFamiliesComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_FontFamiliesComboBox)

			' font sizes
			stack.Add(New Nevron.Nov.UI.NLabel("Font Size:"))
            Me.m_FontSizeComboBox = New Nevron.Nov.UI.NComboBox()

            For i As Integer = 5 To 72 - 1
                Me.m_FontSizeComboBox.Items.Add(New Nevron.Nov.UI.NComboBoxItem(i.ToString()))
            Next

            Me.m_FontSizeComboBox.SelectedIndex = 4
            AddHandler Me.m_FontSizeComboBox.SelectedIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_FontSizeComboBox)

			' add style controls
			Me.m_BoldCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_BoldCheckBox.Content = New Nevron.Nov.UI.NLabel("Bold")
            AddHandler Me.m_BoldCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_BoldCheckBox)
            Me.m_ItalicCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_ItalicCheckBox.Content = New Nevron.Nov.UI.NLabel("Italic")
            AddHandler Me.m_ItalicCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_ItalicCheckBox)
            Me.m_UnderlineCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_UnderlineCheckBox.Content = New Nevron.Nov.UI.NLabel("Underline")
            AddHandler Me.m_UnderlineCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_UnderlineCheckBox)
            Me.m_StrikeTroughCheckBox = New Nevron.Nov.UI.NCheckBox()
            Me.m_StrikeTroughCheckBox.Content = New Nevron.Nov.UI.NLabel("Strikethrough")
            AddHandler Me.m_StrikeTroughCheckBox.CheckedChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnFontStyleChanged)
            stack.Add(Me.m_StrikeTroughCheckBox)
			
			' properties
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_TextBox), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_TextBox, Nevron.Nov.UI.NTextBox.EnabledProperty, Nevron.Nov.UI.NTextBox.ReadOnlyProperty, Nevron.Nov.UI.NTextBox.MultilineProperty, Nevron.Nov.UI.NTextBox.WordWrapProperty, Nevron.Nov.UI.NTextBox.AlwaysShowSelectionProperty, Nevron.Nov.UI.NTextBox.AlwaysShowCaretProperty, Nevron.Nov.UI.NTextBox.AcceptsTabProperty, Nevron.Nov.UI.NTextBox.AcceptsEnterProperty, Nevron.Nov.UI.NTextBox.ShowCaretProperty, Nevron.Nov.UI.NTextBox.HorizontalPlacementProperty, Nevron.Nov.UI.NTextBox.VerticalPlacementProperty, Nevron.Nov.UI.NTextBox.TextAlignProperty, Nevron.Nov.UI.NTextBox.DirectionProperty, Nevron.Nov.UI.NTextBox.HScrollModeProperty, Nevron.Nov.UI.NTextBox.VScrollModeProperty, Nevron.Nov.UI.NTextBox.CharacterCasingProperty, Nevron.Nov.UI.NTextBox.PasswordCharProperty, Nevron.Nov.UI.NTextBox.HintProperty, Nevron.Nov.UI.NTextBox.HintFillProperty)
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()

            For i As Integer = 0 To editors.Count - 1
                propertiesStack.Add(editors(i))
            Next

            stack.Add(New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack)))

			' make sure font style is updated
			Me.OnFontStyleChanged(Nothing)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create and use text boxes. The controls on the right let you
	change the text box's font, alignment, placement, etc.
</p>
"
        End Function

		#EndRegion

		#Region"Event Handlers"

		Private Sub OnFontStyleChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Dim fontFamily As String = TryCast(Me.m_FontFamiliesComboBox.SelectedItem.Content, Nevron.Nov.UI.NLabel).Text
            Dim fontSize As Integer = System.Int32.Parse(TryCast(Me.m_FontSizeComboBox.SelectedItem.Content, Nevron.Nov.UI.NLabel).Text)
            Dim fontStyle As Nevron.Nov.Graphics.ENFontStyle = Nevron.Nov.Graphics.ENFontStyle.Regular

            If Me.m_BoldCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Bold
            End If

            If Me.m_ItalicCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Italic
            End If

            If Me.m_UnderlineCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Underline
            End If

            If Me.m_StrikeTroughCheckBox.Checked Then
                fontStyle = fontStyle Or Nevron.Nov.Graphics.ENFontStyle.Strikethrough
            End If

            Me.m_TextBox.Font = New Nevron.Nov.Graphics.NFont(fontFamily, fontSize, fontStyle)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_TextBox As Nevron.Nov.UI.NTextBox
        Private m_FontFamiliesComboBox As Nevron.Nov.UI.NComboBox
        Private m_FontSizeComboBox As Nevron.Nov.UI.NComboBox
        Private m_BoldCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_ItalicCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_UnderlineCheckBox As Nevron.Nov.UI.NCheckBox
        Private m_StrikeTroughCheckBox As Nevron.Nov.UI.NCheckBox

		#EndRegion

		#Region"Schema"

		Public Shared ReadOnly NTextBoxExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion
	End Class
End Namespace
