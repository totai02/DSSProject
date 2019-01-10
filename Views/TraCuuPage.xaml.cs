using DSSProject.Helper;
using DSSProject.Model;
using DSSProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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
        private ChuyenNganhVM chuyenNganhVM;
        private TraCuuVM traCuuVM;

        public TraCuuPage()
        {
            InitializeComponent();
            coSoViewModel = new CoSoVM();
            chuyenNganhVM = new ChuyenNganhVM();

            coSoViewModel.GetAllRepo();
            chuyenNganhVM.GetAllRepo();

            traCuuVM = new TraCuuVM();
            listView.ItemsSource = coSoViewModel.coSos;
            for (int i = 0; i < chuyenNganhVM.chuyenNganhs.Count; i++)
            {
                ContentControl chuyenNganh = new ContentControl();
                chuyenNganh.DataContext = chuyenNganhVM.chuyenNganhs[i].TenChuyenNganh;
                chuyenNganh.Content = FindResource("ChuyenNganhItem");
                DSChuyenNganh.Children.Add(chuyenNganh);
            }
            List<string> namDTList = traCuuVM.GetUniqueNamDaoTao();
            for (int i = 0; i < namDTList.Count; i++)
            {
                ContentControl namDT = new ContentControl();
                namDT.DataContext = namDTList[i];
                namDT.Content = FindResource("NamDaoTaoItem");
                DSNamDT.Children.Add(namDT);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CoSo item = (sender as ListViewItem).Content as CoSo;
            Tuple<CoSo, List<string>, List<List<KeyValuePair<string, string>>>> data = traCuuVM.GetDetail(item.MaTruong);
            DetailWindow detail = new DetailWindow(data.Item1, data.Item2, data.Item3);
            detail.ShowDialog();
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

        private void BtnTraCuu_Click(object sender, RoutedEventArgs e)
        {
            List<string> maNganh = new List<string>();
            List<string> namDaoTao = new List<string>();

            List<CheckBox> checkBoxList = getCheckBox();

            if (!(bool)BoChon.IsChecked)
            {
                for (int i = 1; i < checkBoxList.Count; i++)
                {
                    if ((bool)checkBoxList[i].IsChecked)
                    {
                        maNganh.Add(chuyenNganhVM.chuyenNganhs[i - 1].MaNganh);
                    }
                }
            }

            if (!(bool)CBTatCa.IsChecked)
            {
                for (int i = 1; i < DSNamDT.Children.Count; i++)
                {
                    var contentPresenter = VisualTreeHelper.GetChild(DSNamDT.Children[i], 0) as ContentPresenter;
                    var checkBox = VisualTreeHelper.GetChild(contentPresenter, 0) as CheckBox;
                    if ((bool)(checkBox).IsChecked)
                    {
                        namDaoTao.Add(checkBox.Content.ToString());
                    }
                }
            }

            int soLuongFrom;
            bool isNumeric1 = int.TryParse(tbFrom.Text, out soLuongFrom);
            if (!isNumeric1) soLuongFrom = -1;

            int soLuongTo;
            bool isNumeric2 = int.TryParse(tbTo.Text, out soLuongTo);
            if (!isNumeric2) soLuongTo = -1;

            listView.ItemsSource = traCuuVM.TraCuu(maNganh, namDaoTao, soLuongFrom, soLuongTo);
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = RecordFilter;
        }

        private List<CheckBox> getCheckBox()
        {
            List<CheckBox> list = new List<CheckBox>();
            list.Add(BoChon);
            for (int i = 1; i < DSChuyenNganh.Children.Count; i++)
            {
                var contentPresenter = VisualTreeHelper.GetChild(DSChuyenNganh.Children[i], 0) as ContentPresenter;
                list.Add(VisualTreeHelper.GetChild(contentPresenter, 0) as CheckBox);
            }
            return list;
        }

        private void BoChon_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> checkBoxList = getCheckBox();
            for (int i = 1; i < DSChuyenNganh.Children.Count; i++)
            {
                checkBoxList[i].IsChecked = false;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            BoChon.IsChecked = false;
        }

        private void CBTatCa_Click(object sender, RoutedEventArgs e)
        {
            bool selected = (bool)CBTatCa.IsChecked;
            for (int i = 1; i < DSNamDT.Children.Count; i++)
            {
                var contentPresenter = VisualTreeHelper.GetChild(DSNamDT.Children[i], 0) as ContentPresenter;
                (VisualTreeHelper.GetChild(contentPresenter, 0) as CheckBox).IsChecked = selected;
            }
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            if (!(bool)(sender as CheckBox).IsChecked)
            {
                CBTatCa.IsChecked = false;
            }
        }
    }
}
