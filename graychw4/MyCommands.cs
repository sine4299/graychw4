using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace graychw4
{
    public static class MyCommands
    {
        public static readonly RoutedUICommand Start = new RoutedUICommand(
            "Start",
            "Start",
            typeof(MyCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            }

            );
    }
}
