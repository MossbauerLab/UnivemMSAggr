using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MossbauerLab.UnivemMsAggr.GUI.Views.Activators
{
    public static class ViewActivator
    {
        public static void Activate<T>() where T: Window, new()
        {
            T view = new T();
            view.Show();
        }
    }
}
