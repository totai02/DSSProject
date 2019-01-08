using DSSProject.Model;
using DSSProject.ViewModel;
using System;
using System.Windows;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for AddChuyenNganhWindow.xaml
    /// </summary>
    public partial class AddChuyenNganhWindow : Window
    {
        private ChuyenNganhDaoTaoViewModel chuyenNganhViewModel;
        private bool isAddRecord = true;

        public AddChuyenNganhWindow(ChuyenNganhDaoTaoViewModel chuyenNganhVM, ChuyenNganhDaoTao oldData = null)
        {
            InitializeComponent();
            this.chuyenNganhViewModel = chuyenNganhVM;

            if (oldData != null)
            {
                isAddRecord = false;
                this.Title = "Cập nhật Chuyên Ngành";
                txtMaNganh.IsEnabled = false;

                txtMaNganh.Text = oldData.MaNganh;
                txtNhomNganh.Text = oldData.NhomNganh;
                txtTenNganh.Text = oldData.TenChuyenNganh;
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ChuyenNganhDaoTao chuyenNganh = new ChuyenNganhDaoTao
            {
                MaNganh = txtMaNganh.Text,
                NhomNganh = txtNhomNganh.Text,
                TenChuyenNganh = txtTenNganh.Text
            };

            if (isAddRecord)
            {
                chuyenNganhViewModel.AddRecord(chuyenNganh);
            }
            else
            {
                chuyenNganhViewModel.UpdateRecord(chuyenNganh);
            }
            Close();
        }

        private void Retype_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn chắc chắn không?", "Nhập lại các trường", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                txtMaNganh.Text = "";
                txtNhomNganh.Text = "";
                txtTenNganh.Text = "";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Hủy bỏ thao tác?", "Hủy bỏ", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
