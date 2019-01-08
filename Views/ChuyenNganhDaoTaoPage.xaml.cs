using DSSProject.Helper;
using DSSProject.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        private ChuyenNganhDaoTaoViewModel chuyenNganhViewModel;

        public ChuyenNganhDaoTaoPage()
        {
            InitializeComponent();
            chuyenNganhViewModel = new ChuyenNganhDaoTaoViewModel();
            listView.ItemsSource = chuyenNganhViewModel.chuyenNganhs;
        }

        private bool RecordFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as ChuyenNganhDaoTao).TenChuyenNganh.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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
            expander.Height = 80;
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
            AddChuyenNganhWindow windowDialog = new AddChuyenNganhWindow(chuyenNganhViewModel, (ChuyenNganhDaoTao)listView.SelectedItem);
            windowDialog.ShowDialog();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Chuyên Ngành", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                chuyenNganhViewModel.DelRecord(((ChuyenNganhDaoTao)listView.SelectedItem).MaNganh);
            }
        }
    }
}
