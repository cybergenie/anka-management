using Anka2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Anka2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SheetItem_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ((Label)sender).Background = Brushes.LightSteelBlue;

        }

        private void SheetSelectedChanged(object sender, System.EventArgs e)
        {

        }


        private bool SelectedSheetChanged(SheetItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (parent != null)
            {
                if (parent is AvalonDock.DockingManager)
                {
                    break;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            UIElement layoutPanelControl = ((AvalonDock.DockingManager)parent).LayoutRootPanel.Children[2];
            AvalonDock.Controls.LayoutDocumentPaneControl layoutDocumentPane = ((AvalonDock.Controls.LayoutDocumentPaneGroupControl)layoutPanelControl).Children[0]
                as AvalonDock.Controls.LayoutDocumentPaneControl;
            SheetItems selectedIndex = (SheetItems)Enum.Parse(typeof(SheetItems), ((Label)item.Content).Name.ToString());

            ((AvalonDock.Layout.LayoutDocument)layoutDocumentPane.Items[(int)selectedIndex]).IsSelected = true;

            return true;
        }
    }
}
