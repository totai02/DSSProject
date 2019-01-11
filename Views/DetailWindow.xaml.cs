using DSSProject.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private List<StackPanel> listStack;
        public DetailWindow(CoSo coSo, List<string> namDaoTao, List<List<KeyValuePair<string, string>>> chuyenNganh)
        {
            InitializeComponent();
            listStack = new List<StackPanel>();
            DisplayData(coSo, namDaoTao, chuyenNganh);
        }

        private void DisplayData(CoSo coSo, List<string> namDaoTao, List<List<KeyValuePair<string, string>>> chuyenNganh)
        {
            txtMaTruong.Text = coSo.MaTruong;
            txtTenTruong.Text = coSo.TenTruong;
            txtDiaChi.Text = coSo.DiaChi;
            txtWebsite.Text = coSo.Website;
            txtTinhThanh.Text = coSo.TinhThanh;
            txtDVChuQuan.Text = coSo.DVChuQuan;

            if (namDaoTao != null)
            {
                CBox.ItemsSource = namDaoTao;
                CBox.SelectedIndex = 0;
            }

            if (chuyenNganh != null)
            {
                for (int k = 0; k < chuyenNganh.Count; k++)
                {
                    StackPanel stack = new StackPanel();
                    stack.Visibility = Visibility.Collapsed;
                    for (int i = 0; i < chuyenNganh[k].Count; i++)
                    {
                        ContentControl label = new ContentControl();
                        ContentControl textBox = new ContentControl();

                        label.Content = FindResource("ChuyenNganhLabel");
                        textBox.Content = FindResource("ChuyenNganhTB");

                        label.DataContext = chuyenNganh[k][i].Key;
                        textBox.DataContext = chuyenNganh[k][i].Value;

                        stack.Children.Add(label);
                        stack.Children.Add(textBox);
                    }
                    listStack.Add(stack);
                    TuyenSinhContainer.Children.Add(stack);
                }
            }
            else
            {
                StackPanel stack = new StackPanel();
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 24;
                textBlock.Foreground = Brushes.Gray;
                textBlock.Text = "Không có dữ liệu!";
                stack.Children.Add(textBlock);
                listStack.Add(stack);
                TuyenSinhContainer.Children.Add(stack);
            }

            listStack[0].Visibility = Visibility.Visible;

        }

        private void CBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listStack.Count != 0)
            {
                for (int i = 0; i < listStack.Count; i++)
                {
                    listStack[i].Visibility = Visibility.Collapsed;
                }
                listStack[CBox.SelectedIndex].Visibility = Visibility.Visible;
            }
        }
    }
}
