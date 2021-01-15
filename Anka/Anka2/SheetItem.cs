using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Anka2
{
    public class SheetItem : ContentControl
    {
        private Point? dragStartPoint = null;

        static SheetItem()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SheetItem), new FrameworkPropertyMetadata(typeof(SheetItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            if (this.dragStartPoint.HasValue)
            {
                // XamlWriter.Save() has limitations in exactly what is serialized,
                // see SDK documentation; short term solution only;
                string xamlString = XamlWriter.Save(this.Content);
                DragObject dataObject = new DragObject();
                dataObject.Xaml = xamlString;

                StackPanel panel = VisualTreeHelper.GetParent(this) as StackPanel;
                if (panel != null)
                {
                    // desired size for DesignerCanvas is the stretched Toolbox item size                    
                    dataObject.DesiredSize = new Size(50,50);
                }

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);

                e.Handled = true;
            }
        }
    }

    public class DragObject
    {
        // 序列化组件内容
        public String Xaml { get; set; }

        // 拖拽组件的尺寸
        public Size? DesiredSize { get; set; }
    }
}
