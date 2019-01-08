
using DSSProject.Helper;
using DSSProject.Model;
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
    /// Interaction logic for CoSoDaoTaoPage.xaml
    /// </summary>
    public partial class CoSoDaoTaoPage : Page
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private CoSoVM coSoViewModel;

        public CoSoDaoTaoPage()
        {
            InitializeComponent();
            coSoViewModel = new CoSoVM();
            listView.ItemsSource = coSoViewModel.coSos;
        }

        private bool RecordFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
            {
                bool check = false;
                check = check || (item as CoSo).MaTruong.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as CoSo).TenTruong.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as CoSo).DiaChi.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as CoSo).DVChuQuan.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as CoSo).TinhThanh.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;

                return check;
            }
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

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = RecordFilter;
            expander.IsExpanded = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = RecordFilter;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            expander.Height = 30;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            expander.Height = 120;
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
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Chuyên Ngành", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                coSoViewModel.DelRecord(((CoSo)listView.SelectedItem).MaTruong);
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
    }
}
