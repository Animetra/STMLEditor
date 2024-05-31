using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


internal static class ViewModel
{
    internal static TreeViewItem GetTreeViewItem(this ItemsControl parent, object item)
    {
        TreeViewItem tvi = parent.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;

        if (tvi == null)
        {
            foreach (object child in parent.Items)
            {
                TreeViewItem childItem = parent.ItemContainerGenerator.ContainerFromItem(child) as TreeViewItem;

                if (childItem != null)
                {
                    tvi = GetTreeViewItem(childItem, item);
                }
            }
        }

        return tvi;
    }
}