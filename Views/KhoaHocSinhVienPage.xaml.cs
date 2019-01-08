using DSSProject.Helper;
using DSSProject.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for KhoaHocSinhVienPage.xaml
    /// </summary>
    public partial class KhoaHocSinhVienPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private KhoaHocSinhVienViewModel khoaHocViewModel;

        public KhoaHocSinhVienPage()
        {
            InitializeComponent();
            khoaHocViewModel = new KhoaHocSinhVienViewModel();
            listView.ItemsSource = khoaHocViewModel.khoaHocs;
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
