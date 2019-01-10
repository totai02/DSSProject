using DSSProject.Helper;
using DSSProject.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Linq;
using System;
using DSSProject.Model;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for ChuyenNganhDaoTaoView.xaml
    /// </summary>
    public partial class ChuyenNganhDaoTaoPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private ChuyenNganhVM chuyenNganhViewModel;

        public ChuyenNganhDaoTaoPage()
        {
            InitializeComponent();
            chuyenNganhViewModel = new ChuyenNganhVM();
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedIndex == -1)
            {
                EditBtn.IsEnabled = false;
                DelBtn.IsEnabled = false;
                return;
            }
            EditBtn.IsEnabled = true;
            DelBtn.IsEnabled = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddChuyenNganhWindow windowDialog = new AddChuyenNganhWindow(chuyenNganhViewModel);
            windowDialog.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddChuyenNganhWindow windowDialog = new AddChuyenNganhWindow(chuyenNganhViewModel, (ChuyenNganh)listView.SelectedItem);
            windowDialog.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Chuyên Ngành", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                chuyenNganhViewModel.DelRecord(listView.SelectedItems.Cast<ChuyenNganh>().ToList());
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                chuyenNganhViewModel.GetAllRepo();
                listView.ItemsSource = chuyenNganhViewModel.chuyenNganhs;
            }
            else
            {
                chuyenNganhViewModel.SearchRecord(txtSearch.Text);
                listView.ItemsSource = chuyenNganhViewModel.chuyenNganhs;
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
                    check = check || (item as ChuyenNganh).MaNganh.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as ChuyenNganh).TenChuyenNganh.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;

                    if (!check) return false;
                }

                return true;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = RecordFilter;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            chuyenNganhViewModel.GetAllRepo();
            listView.ItemsSource = chuyenNganhViewModel.chuyenNganhs;
        }
    }
}
