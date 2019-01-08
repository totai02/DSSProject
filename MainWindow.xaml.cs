using System.Windows;
using System.Windows.Controls;
using DSSProject.Views;

namespace DSSProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChuyenNganhDaoTaoPage chuyenNganhDaoTaoPage;
        private CoSoDaoTaoPage coSoPage;
        private CoSoTheoNganhPage coSoNganhPage;
        private KhoaHocSinhVienPage khoaHocPage;
        private SinhVienPage sinhVienPage;

        public MainWindow()
        {
            InitializeComponent();
            ChuyenNganh_Click(this, null);
        }

        public void LoadPage(Page page)
        {
            Frame.Navigate(page);
        }

        private void ChuyenNganh_Click(object sender, RoutedEventArgs e)
        {
            if (chuyenNganhDaoTaoPage == null)
            {
                chuyenNganhDaoTaoPage = new ChuyenNganhDaoTaoPage();
            }
            LoadPage(chuyenNganhDaoTaoPage);
        }

        private void CoSo_Click(object sender, RoutedEventArgs e)
        {
            if (coSoPage == null)
            {
                coSoPage = new CoSoDaoTaoPage();
            }
            LoadPage(coSoPage);
        }

        private void CoSoNganh_Click(object sender, RoutedEventArgs e)
        {
            if (coSoNganhPage == null)
            {
                coSoNganhPage = new CoSoTheoNganhPage();
            }
            LoadPage(coSoNganhPage);
        }

        private void KhoaHoc_Click(object sender, RoutedEventArgs e)
        {
            if (khoaHocPage == null)
            {
                khoaHocPage = new KhoaHocSinhVienPage();
            }
            LoadPage(khoaHocPage);
        }

        private void SinhVien_Click(object sender, RoutedEventArgs e)
        {
            if (sinhVienPage == null)
            {
                sinhVienPage = new SinhVienPage();
            }
            LoadPage(sinhVienPage);
        }
    }
}
