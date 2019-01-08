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
        private TuyenSinhPage tuyenSinhPage;

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

        private void SinhVien_Click(object sender, RoutedEventArgs e)
        {
            if (tuyenSinhPage == null)
            {
                tuyenSinhPage = new TuyenSinhPage();
            }
            LoadPage(tuyenSinhPage);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Menu1.Visibility = Visibility.Visible;
            Menu2.Visibility = Visibility.Hidden;
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            Menu1.Visibility = Visibility.Hidden;
            Menu2.Visibility = Visibility.Visible;
        }
    }
}
