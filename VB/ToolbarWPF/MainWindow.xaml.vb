' Developer Express Code Central Example:
' How to remove standard bar items and add custom ones to the DocumentPreview toolbar
' 
' This example demonstrates how to customize the DocumentPreview control's
' toolbar.
' 
' In particular, it shows how you can remove a standard button from
' it, and add custom ones.
' 
' You can tie your custom logic to a button either in
' the BarManager.ItemClick event handler, or using the View Model commands.
' 
' See
' Also:
' http://www.devexpress.com/scid=E4482
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2974


Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Windows
Imports DevExpress.Xpf.Printing
' ...
Imports System.Windows.Data
Imports System
Imports System.Windows.Markup
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Docking.VisualElements
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Docking

Namespace ToolbarWPF
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Private data() As String

		Private ReadOnly Property ViewModel() As MainWindowViewModel
			Get
				Return TryCast(DataContext, MainWindowViewModel)
			End Get
		End Property

		Public Sub New()
			InitializeComponent()
			AddHandler Loaded, AddressOf MainWindow_Loaded
			' Create a document to display.
			data = CultureInfo.CurrentCulture.DateTimeFormat.DayNames

			Dim link As New SimpleLink()
			link.DetailTemplate = CType(Resources("dayNameTemplate"), DataTemplate)
			link.DetailCount = data.Length
			AddHandler link.CreateDetail, AddressOf link_CreateDetail

			CType(ViewModel.PreviewModel, LinkPreviewModel).Link = link
			documentPreview1.Model = ViewModel.PreviewModel

			ViewModel.CreateDocumentCommand.Execute(Nothing)
		End Sub

		Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
			e.Data = data(e.DetailIndex)
		End Sub

		Private Shared Sub HidePanels(ByVal sender As Object)
			Dim control As DockBarContainerControl = TryCast(LayoutHelper.FindElementByName(TryCast(sender, FrameworkElement), "PART_BarContainerControl"), DockBarContainerControl)
			control.Visibility = System.Windows.Visibility.Collapsed

			Dim panel As ClosedItemsPanel = TryCast(LayoutHelper.FindElementByName(TryCast(sender, FrameworkElement), "PART_ClosedItemsPanel"), ClosedItemsPanel)
			panel.Visibility = System.Windows.Visibility.Collapsed
		End Sub

		Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			HidePanels(sender)
		End Sub


	End Class
End Namespace