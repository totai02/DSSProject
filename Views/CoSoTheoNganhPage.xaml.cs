using DSSProject.Helper;
using DSSProject.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for CoSoTheoNganhPage.xaml
    /// </summary>
    public partial class CoSoTheoNganhPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private CoSoTheoNganhViewModel cSTheoNganhViewModel;

        public CoSoTheoNganhPage()
        {
            InitializeComponent();
            cSTheoNganhViewModel = new CoSoTheoNganhViewModel();
            listView.ItemsSource = cSTheoNganhViewModel.coSoTheoNganhs;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Refresh();
        }

        private void GridViewHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                listView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            listView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}
