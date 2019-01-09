using DSSProject.Model;
using DSSProject.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for AddTuyenSinhWindow.xaml
    /// </summary>
    public partial class AddTuyenSinhWindow : Window
    {
        private TuyenSinhVM tuyenSinhVM;
        private bool isAddRecord = true;

        public AddTuyenSinhWindow(TuyenSinhVM tuyenSinhVM, TuyenSinh oldData = null)
        {
            InitializeComponent();
            this.tuyenSinhVM = tuyenSinhVM;

            if (oldData != null)
            {
                isAddRecord = false;
                Title = "Cập nhật Thông tin Tuyển sinh";
                txtMaTruong.IsEnabled = false;
                txtMaNganh.IsEnabled = false;

                txtMaTruong.Text = oldData.MaTruong;
                txtMaNganh.Text = oldData.MaNganh;
                txtSoLuong.Text = oldData.SoLuong.ToString();
                txtNamDaoTao.Text = oldData.NamDaoTao.ToString();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            TuyenSinh tuyenSinh = new TuyenSinh
            {
                MaTruong = txtMaTruong.Text,
                MaNganh = txtMaNganh.Text,
                SoLuong = int.Parse(txtSoLuong.Text),
                NamDaoTao = int.Parse(txtNamDaoTao.Text)
            };

            if (isAddRecord)
            {
                tuyenSinhVM.AddRecord(tuyenSinh);
            }
            else
            {
                tuyenSinhVM.UpdateRecord(tuyenSinh);
            }
            Close();
        }

        private void Retype_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn chắc chắn không?", "Nhập lại các trường", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                txtMaTruong.Text = "";
                txtMaNganh.Text = "";
                txtSoLuong.Text = "";
                txtNamDaoTao.Text = "";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Hủy bỏ thao tác?", "Hủy bỏ", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
