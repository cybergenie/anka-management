using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Anka2.ViewModel
{
    class BasicInfoSheet: ContentControl
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(e.Key==Key.Enter)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;

                if(focusedElement.GetType().Name == "CheckBox"|| focusedElement.GetType().Name == "RadioButton")
                {                    
                    ((ToggleButton)focusedElement).IsChecked = !((ToggleButton)focusedElement).IsChecked;
                }               

            }           
           
        }      


    }
}
