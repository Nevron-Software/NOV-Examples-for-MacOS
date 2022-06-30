Imports Nevron.Nov.DataStructures
Imports Nevron.Nov.Editors
Imports Nevron.Nov.Dom
Imports Nevron.Nov.Graphics
Imports Nevron.Nov.Layout
Imports Nevron.Nov.UI

Namespace Nevron.Nov.Examples.UI
    Public Class NSingleVisiblePanelExample
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
            Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NSingleVisiblePanelExampleSchema = Nevron.Nov.Dom.NSchema.Create(GetType(Nevron.Nov.Examples.UI.NSingleVisiblePanelExample), NExampleBase.NExampleBaseSchema)
        End Sub

		#EndRegion

		#Region"Example"

		Protected Overrides Function CreateExampleContent() As Nevron.Nov.UI.NWidget
            Me.m_SingleVisiblePanel = New Nevron.Nov.UI.NSingleVisiblePanel()
            Me.m_SingleVisiblePanel.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Left
            Me.m_SingleVisiblePanel.VerticalPlacement = Nevron.Nov.Layout.ENVerticalPlacement.Top
            Me.m_SingleVisiblePanel.PreferredWidth = 400
            Me.m_SingleVisiblePanel.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Red)
            Me.m_SingleVisiblePanel.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            Dim mainStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Me.m_SingleVisiblePanel.Add(mainStack)
            mainStack.Add(Me.CreateHeaderLabel("Mobile Computers"))
            Dim i As Integer = 0, count As Integer = Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.MobileComputers.Length

            While i < count
                Dim info As Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo = Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.MobileComputers(i)

				' Create the topic's button
				Dim button As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton(info.Name)
                button.Tag = i + 1
                mainStack.Add(button)

				' Create and add the topic's content
				Me.m_SingleVisiblePanel.Add(Me.CreateComputerInfoWidget(info))
                i += 1
            End While

            AddHandler Me.m_SingleVisiblePanel.VisibleIndexChanged, New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NValueChangeEventArgs)(AddressOf Me.OnVisibleIndexValueChanged)
            Me.m_SingleVisiblePanel.AddEventHandler(Nevron.Nov.UI.NButtonBase.ClickEvent, New Nevron.Nov.Dom.NEventHandler(Of Nevron.Nov.Dom.NEventArgs)(New Nevron.Nov.[Function](Of Nevron.Nov.Dom.NEventArgs)(AddressOf Me.OnButtonClicked)))
            Return Me.m_SingleVisiblePanel
        End Function

        Protected Overrides Function CreateExampleControls() As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.FillMode = Nevron.Nov.Layout.ENStackFillMode.Last
            stack.FitMode = Nevron.Nov.Layout.ENStackFitMode.Last

			' Add the properties group box
			Dim editors As Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor) = New Nevron.Nov.DataStructures.NList(Of Nevron.Nov.Editors.NPropertyEditor)(Nevron.Nov.Editors.NDesigner.GetDesigner(CType((Me.m_SingleVisiblePanel), Nevron.Nov.Dom.NNode)).CreatePropertyEditors(Me.m_SingleVisiblePanel, Nevron.Nov.UI.NSingleVisiblePanel.EnabledProperty, Nevron.Nov.UI.NSingleVisiblePanel.SizeToVisibleProperty, Nevron.Nov.UI.NSingleVisiblePanel.VisibleIndexProperty))
            Dim propertiesStack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            Dim i As Integer = 0, count As Integer = editors.Count

            While i < count
                stack.Add(editors(i))
                i += 1
            End While

            Dim propertiesGroupBox As Nevron.Nov.UI.NGroupBox = New Nevron.Nov.UI.NGroupBox("Properties", New Nevron.Nov.UI.NUniSizeBoxGroup(propertiesStack))
            stack.Add(propertiesGroupBox)

			' Add an events log
			Me.m_EventsLog = New NExampleEventsLog()
            stack.Add(Me.m_EventsLog)
            Return stack
        End Function

        Protected Overrides Function GetExampleDescription() As String
            Return "
<p>
	This example demonstrates how to create a single visible panel and add some widgets to it.
	The single visible panel is a panel in which only a single child element can be visible.
	The currently visible child element can be controlled through the <b>VisibleIndex</b> or the
	<b>VisibleElement</b> property. The <b>SizeToVisible</b> property determines whether the panel
	should be sized to the visible element or to all contained elements.  
</p>
"
        End Function

		#EndRegion

		#Region"Implementation"

		Private Function CreateHeaderLabel(ByVal text As String) As Nevron.Nov.UI.NLabel
            Dim label As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(text)
            label.TextFill = New Nevron.Nov.Graphics.NColorFill(Nevron.Nov.Graphics.NColor.Blue)
            label.Font = New Nevron.Nov.Graphics.NFont(Nevron.Nov.Graphics.NFontDescriptor.DefaultSansFamilyName, 16)
            label.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            Return label
        End Function

        Private Function CreateComputerInfoWidget(ByVal info As Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo) As Nevron.Nov.UI.NWidget
            Dim stack As Nevron.Nov.UI.NStackPanel = New Nevron.Nov.UI.NStackPanel()
            stack.Add(Me.CreateHeaderLabel(info.Name))

			' Create a pair box with the image and the description
			Dim descriptionLabel As Nevron.Nov.UI.NLabel = New Nevron.Nov.UI.NLabel(info.Description)
            descriptionLabel.TextWrapMode = Nevron.Nov.Graphics.ENTextWrapMode.WordWrap
            Dim pairBox As Nevron.Nov.UI.NPairBox = New Nevron.Nov.UI.NPairBox(info.Image, descriptionLabel)
            pairBox.Box1.Border = Nevron.Nov.UI.NBorder.CreateFilledBorder(Nevron.Nov.Graphics.NColor.Black)
            pairBox.Box1.BorderThickness = New Nevron.Nov.Graphics.NMargins(1)
            pairBox.Spacing = 5
            stack.Add(pairBox)
            Dim backButton As Nevron.Nov.UI.NButton = New Nevron.Nov.UI.NButton("Back")
            backButton.Content.HorizontalPlacement = Nevron.Nov.Layout.ENHorizontalPlacement.Center
            backButton.Tag = 0
            stack.Add(backButton)
            Return stack
        End Function

		#EndRegion

		#Region"Event Handlers"

		''' <summary>
		''' 
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnVisibleIndexValueChanged(ByVal args As Nevron.Nov.Dom.NValueChangeEventArgs)
            Me.m_EventsLog.LogEvent("Visible child index: " & args.NewValue.ToString())
        End Sub
		''' <summary>
		''' Handler for NButtonBase.Click event.
		''' </summary>
		''' <paramname="args"></param>
		Private Sub OnButtonClicked(ByVal args As Nevron.Nov.Dom.NEventArgs)
            If args.Cancel OrElse Not (TypeOf args.TargetNode Is Nevron.Nov.UI.NButton) OrElse Not (TypeOf args.TargetNode.Tag Is Integer) Then Return
            Dim singleVisiblePanel As Nevron.Nov.UI.NSingleVisiblePanel = CType(args.CurrentTargetNode, Nevron.Nov.UI.NSingleVisiblePanel)
            Dim button As Nevron.Nov.UI.NButton = CType(args.TargetNode, Nevron.Nov.UI.NButton)
            singleVisiblePanel.VisibleIndex = CInt(button.Tag)
        End Sub

		#EndRegion

		#Region"Fields"

		Private m_SingleVisiblePanel As Nevron.Nov.UI.NSingleVisiblePanel
        Private m_EventsLog As NExampleEventsLog

		#EndRegion

		#Region"Schema"

		''' <summary>
		''' Schema associated with NSingleVisiblePanelExample.
		''' </summary>
		Public Shared ReadOnly NSingleVisiblePanelExampleSchema As Nevron.Nov.Dom.NSchema

		#EndRegion

		#Region"Constants"

		Private Shared ReadOnly MobileComputers As Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo() = New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo() {New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Laptop", NResources.Image_MobileComputers_Laptop_jpg, "A laptop, also called a notebook, is a personal computer for mobile use. A laptop integrates most of the typical components of a desktop computer, including a display, a keyboard, a pointing device (a touchpad, also known as a trackpad, and/or a pointing stick) and speakers into a single unit."), New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Netbook", NResources.Image_MobileComputers_Netbook_jpg, "Netbooks are a category of small, lightweight, legacy-free, and inexpensive laptop computers. At their inception in late 2007 as smaller notebooks optimized for low weight and low cost — netbooks omitted certain features (e.g., the optical drive), featured smaller screens and keyboards, and offered reduced computing power when compared to a full-sized laptop."), New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Smartbook", NResources.Image_MobileComputers_Smartbook_jpg, "A smartbook is a class of mobile device that combine certain features of both a smartphone and netbook computer. Smartbooks feature always on, all-day battery life, 3G, or Wi-Fi connectivity and GPS (all typically found in smartphones) in a laptop or tablet-style body with a screen size of 5 to 10 inches and a physical or soft touchscreen keyboard."), New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Tablet", NResources.Image_MobileComputers_Tablet_jpg, "A tablet computer, or simply tablet, is a complete mobile computer, larger than a mobile phone or personal digital assistant, integrated into a flat touch screen and primarily operated by touching the screen. It often uses an onscreen virtual keyboard, a passive stylus pen, or a digital pen, rather than a physical keyboard."), New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Ultra-mobile PC", NResources.Image_MobileComputers_UMPC_jpg, "An ultra-mobile PC (ultra-mobile personal computer or UMPC) is a small form factor version of a pen computer which is smaller than a netbook, has a TFT display measuring about 12.7 to 17.8 cm and is operated using a touch screen or a stylus. Lately ultra-mobile PCs have largely been supplanted by tablets."), New Nevron.Nov.Examples.UI.NSingleVisiblePanelExample.NMobileCopmuterInfo("Ultrabook", NResources.Image_MobileComputers_Ultrabook_jpg, "An Ultrabook is a computer in a category of thin and lightweight ultraportable laptops, defined by a specification from Intel corporation. Ultrabooks combine the power of ordinary laptops and the portability and battery life of netbooks but this comes at a higher price.")}

		#EndRegion

		#Region"Nested Types"

		Private Structure NMobileCopmuterInfo
            Public Sub New(ByVal name As String, ByVal image As Nevron.Nov.Graphics.NImage, ByVal description As String)
                Me.Name = name
                Me.Image = image
                Me.Description = description
            End Sub

            Public Name As String
            Public Image As Nevron.Nov.Graphics.NImage
            Public Description As String
        End Structure

		#EndRegion
	End Class
End Namespace
