using DSSProject.Model;
using DSSProject.ViewModel;
using System.Windows;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for AddCoSoWindow.xaml
    /// </summary>
    public partial class AddCoSoWindow : Window
    {
        private CoSoVM coSoVM;
        private bool isAddRecord = true;

        public AddCoSoWindow(CoSoVM coSoDaoTaoViewModel, CoSo oldData = null)
        {
            InitializeComponent();
            coSoVM = coSoDaoTaoViewModel;

            if (oldData != null)
            {
                isAddRecord = false;
                Title = "Cập nhật Cơ sở Đào tạo";
                txtMaTruong.IsEnabled = false;

                txtMaTruong.Text = oldData.MaTruong;
                txtTenTruong.Text = oldData.TenTruong;
                txtDiaChi.Text = oldData.DiaChi;
                txtWebsite.Text = oldData.Website;
                txtTinhThanh.Text = oldData.TinhThanh;
                txtDVChuQuan.Text = oldData.DVChuQuan;
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            CoSo coSo = new CoSo
            {
                MaTruong = txtMaTruong.Text,
                TenTruong = txtTenTruong.Text,
                DiaChi = txtDiaChi.Text,
                Website = txtWebsite.Text,
                TinhThanh = txtTinhThanh.Text,
                DVChuQuan = txtDVChuQuan.Text
            };

            if (isAddRecord)
            {
                coSoVM.AddRecord(coSo);
            }
            else
            {
                coSoVM.UpdateRecord(coSo);
            }
            Close();
        }

        private void Retype_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Bạn chắc chắn không?", "Nhập lại các trường", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                txtTenTruong.Text = "";
                txtDiaChi.Text = "";
                txtWebsite.Text = "";
                txtTinhThanh.Text = "";
                txtDVChuQuan.Text = "";
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
