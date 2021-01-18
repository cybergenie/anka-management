using System.Windows;
using System.Windows.Controls;

namespace Anka2.ViewModel
{
    public class Sheetbox : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SheetItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is SheetItem);            
        }

        
    }

   
}
