// Developer Express Code Central Example:
// How to remove standard bar items and add custom ones to the DocumentPreview toolbar
// 
// This example demonstrates how to customize the DocumentPreview control's
// toolbar.
// 
// In particular, it shows how you can remove a standard button from
// it, and add custom ones.
// 
// You can tie your custom logic to a button either in
// the BarManager.ItemClick event handler, or using the View Model commands.
// 
// See
// Also:
// http://www.devexpress.com/scid=E4482
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2974

using System.Globalization;
using System.Windows;
using DevExpress.Xpf.Printing;
// ...
using System.Windows.Data;
using System;
using System.Windows.Markup;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Docking.VisualElements;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;

namespace ToolbarWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        string[] data;

        MainWindowViewModel ViewModel {
            get { return DataContext as MainWindowViewModel; }
        }

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            // Create a document to display.
            data = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;

            SimpleLink link = new SimpleLink();
            link.DetailTemplate = (DataTemplate)Resources["dayNameTemplate"];
            link.DetailCount = data.Length;
            link.CreateDetail += link_CreateDetail;
            
            ((LinkPreviewModel)ViewModel.PreviewModel).Link = link;
            documentPreview1.Model = ViewModel.PreviewModel;

            ViewModel.CreateDocumentCommand.Execute(null);
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = data[e.DetailIndex];
        }

        private static void HidePanels(object sender) {
            DockBarContainerControl control = LayoutHelper.FindElementByName(sender as FrameworkElement, "PART_BarContainerControl") as DockBarContainerControl;
            control.Visibility = System.Windows.Visibility.Collapsed;

            ClosedItemsPanel panel = LayoutHelper.FindElementByName(sender as FrameworkElement, "PART_ClosedItemsPanel") as ClosedItemsPanel;
            panel.Visibility = System.Windows.Visibility.Collapsed;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            HidePanels(sender);
        }

       
    }
}