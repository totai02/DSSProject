using DSSProject.Helper;
using DSSProject.Model;
using DSSProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for TraCuuPage.xaml
    /// </summary>
    public partial class TraCuuPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private CoSoVM coSoViewModel;
        private TraCuuVM traCuuVM;

        public TraCuuPage()
        {
            InitializeComponent();
            coSoViewModel = new CoSoVM();
            listView.ItemsSource = coSoViewModel.coSos;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                coSoViewModel.GetAllRepo();
                listView.ItemsSource = coSoViewModel.coSos;
            }
            else
            {
                coSoViewModel.SearchRecord(txtSearch.Text);
                listView.ItemsSource = coSoViewModel.coSos;
            }

            CollectionViewSource.GetDefaultView(listView.ItemsSource).Refresh();
        }

        private bool RecordFilter(object item)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                return true;
            else
            {
                string[] arrFilter = txtSearch.Text.Split(';');
                foreach (string filter in arrFilter)
                {
                    if (filter == "") continue;
                    string str = filter.Trim();

                    bool check = false;
                    check = check || (item as CoSo).MaTruong.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as CoSo).TenTruong.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as CoSo).DiaChi.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as CoSo).Website.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as CoSo).TinhThanh.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as CoSo).DVChuQuan.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (!check) return false;
                }

                return true;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = RecordFilter;
        }
    }
}
