using System.Windows;
using System.Windows.Controls;

namespace Anka2.ViewModels
{
    public class SheetBoxViewModel : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SheetItemViewModel();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is SheetItemViewModel);            
        }

        
    }

   
}
