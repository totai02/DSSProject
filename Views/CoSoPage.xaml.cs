using DSSProject.Helper;
using DSSProject.Model;
using DSSProject.ViewModel;
using System.Linq;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for CoSoDaoTaoPage.xaml
    /// </summary>
    public partial class CoSoDaoTaoPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        public CoSoVM coSoViewModel;

        public CoSoDaoTaoPage()
        {
            InitializeComponent();
            coSoViewModel = new CoSoVM();
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
            AddCoSoWindow windowDialog = new AddCoSoWindow(coSoViewModel);
            windowDialog.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCoSoWindow windowDialog = new AddCoSoWindow(coSoViewModel, (CoSo)listView.SelectedItem);
            windowDialog.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Cơ Sở", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                coSoViewModel.DelRecord(listView.SelectedItems.Cast<CoSo>().ToList());
            }
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            coSoViewModel.GetAllRepo();
            listView.ItemsSource = coSoViewModel.coSos;
        }
    }
}
