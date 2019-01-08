

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
            listView.ItemsSource = tuyenSinhVM.TuyenSinhOC;
        }

        private bool RecordFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
            {
                bool check = false;
                check = check || (item as TuyenSinh).MaTruong.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as TuyenSinh).MaNganh.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as TuyenSinh).ChiTieu.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                check = check || (item as TuyenSinh).NamDaoTao.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;

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
            MessageBoxResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa Chuyên Ngành", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                tuyenSinhVM.DelRecord(((TuyenSinh)listView.SelectedItem).MaTruong, ((TuyenSinh)listView.SelectedItem).MaNganh);
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
    }
}
