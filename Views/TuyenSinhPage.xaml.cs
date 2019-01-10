

using DSSProject.Helper;
using DSSProject.Model;
using DSSProject.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for SinhVienPage.xaml
    /// </summary>
    public partial class TuyenSinhPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private TuyenSinhVM tuyenSinhVM;

        public TuyenSinhPage()
        {
            InitializeComponent();
            tuyenSinhVM = new TuyenSinhVM();
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
            AddTuyenSinhWindow windowDialog = new AddTuyenSinhWindow(tuyenSinhVM);
            windowDialog.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTuyenSinhWindow windowDialog = new AddTuyenSinhWindow(tuyenSinhVM, (TuyenSinh)listView.SelectedItem);
            windowDialog.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Thông tin Tuyển sinh", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                tuyenSinhVM.DelRecord(listView.SelectedItems.Cast<TuyenSinh>().ToList());
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                tuyenSinhVM.GetAllRepo();
                listView.ItemsSource = tuyenSinhVM.TuyenSinhOC;
            }
            else
            {
                tuyenSinhVM.SearchRecord(txtSearch.Text);
                listView.ItemsSource = tuyenSinhVM.TuyenSinhOC;
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
                    check = check || (item as TuyenSinh).MaTruong.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as TuyenSinh).TenTruong.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as TuyenSinh).MaNganh.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as TuyenSinh).TenNganh.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as TuyenSinh).ChiTieu.ToString().IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;
                    check = check || (item as TuyenSinh).NamDaoTao.ToString().IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0;

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
            tuyenSinhVM.GetAllRepo();
            listView.ItemsSource = tuyenSinhVM.TuyenSinhOC;
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).Name == "showDH")
            {
                if ((bool)(sender as CheckBox).IsChecked) (listView.View as GridView).Columns[2].Width = Double.NaN;
                else
                {
                    (listView.View as GridView).Columns[2].Width = 0;
                }
            }

            if ((sender as CheckBox).Name == "showCN")
            {
                if ((bool)(sender as CheckBox).IsChecked) (listView.View as GridView).Columns[4].Width = Double.NaN;
                else
                {
                    (listView.View as GridView).Columns[4].Width = 0;
                }
            }
        }
    }
}
