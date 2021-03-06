﻿using DSSProject.Model;
using DSSProject.ViewModel;
using System.Windows;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for AddChuyenNganhWindow.xaml
    /// </summary>
    public partial class AddChuyenNganhWindow : Window
    {
        private ChuyenNganhVM chuyenNganhViewModel;
        private bool isAddRecord = true;

        public AddChuyenNganhWindow(ChuyenNganhVM chuyenNganhVM, ChuyenNganh oldData = null)
        {
            InitializeComponent();
            chuyenNganhViewModel = chuyenNganhVM;

            if (oldData != null)
            {
                isAddRecord = false;
                Title = "Cập nhật Chuyên Ngành";
                txtMaNganh.IsEnabled = false;

                txtMaNganh.Text = oldData.MaNganh;
                txtTenNganh.Text = oldData.TenChuyenNganh;
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ChuyenNganh chuyenNganh = new ChuyenNganh
            {
                MaNganh = txtMaNganh.Text,
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
