using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Anka2.ViewModel
{
    public class SheetItem : ContentControl
    {
        private static SheetItems selectedSheet;
        protected override void OnMouseDown( MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Label lb = this.Content as Label;
            selectedSheet = SheetItems.BasicInfo;

            SheetItemSelectedChanged(lb);


            SelectedSheetChanged(this);

        }

        public static SheetItems SelectedSheet { set => selectedSheet= value; get=> selectedSheet; }

        private bool SheetItemSelectedChanged(Label lb)
        {
            foreach (Label s in ((Sheetbox)lb.Parent).Items)
            {
                s.Background = ((Sheetbox)lb.Parent).Background;
            }
            lb.Background = Brushes.LightGray;

            return true;
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

            AvalonDock.Controls.LayoutDocumentPaneGroupControl layoutPanelControl = ((AvalonDock.DockingManager)parent).LayoutRootPanel.Children[2] 
                as AvalonDock.Controls.LayoutDocumentPaneGroupControl;
            AvalonDock.Controls.LayoutDocumentPaneControl layoutDocumentPane = layoutPanelControl.Children[0]
                as AvalonDock.Controls.LayoutDocumentPaneControl;
            SheetItems selectedSheet = (SheetItems)Enum.Parse(typeof(SheetItems),((Label)item.Content).Name.ToString());

            foreach(AvalonDock.Layout.LayoutDocument layoutDocument in layoutDocumentPane.Items)
            {
                if(layoutDocument.ContentId == selectedSheet.ToString())
                {
                    layoutDocument.IsSelected = true;
                }
            }              

            return true;
        }
    }
    public enum SheetItems
    {
        BasicInfo,
        Exercise,
        PHQ,
        GAD,
        IPAQ,
        OHQ,
        SPPB,
        Physique
    };
}
