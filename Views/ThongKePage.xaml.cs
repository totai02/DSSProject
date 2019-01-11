using DSSProject.Helper;
using DSSProject.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DSSProject.Views
{
    /// <summary>
    /// Interaction logic for ThongKePage.xaml
    /// </summary>
    public partial class ThongKePage : Page
    {
        private ThongKeModel thongKe;

        public ThongKePage()
        {
            InitializeComponent();
            thongKe = new ThongKeModel();
            LoadComponent();
        }

        public void LoadComponent()
        {
            var namList = Application.Current.Resources["NamDTList"] as List<string>;
            for (int i = 0; i < namList.Count; i++)
            {
                GridViewColumnHeader gvch = new GridViewColumnHeader();
                GridViewColumn gvc = new GridViewColumn();
                gvch.Content = "Năm " + namList[i];
                gvch.FontSize = 16;
                gvch.Width = 100;
                gvc.Header = gvch;
                Binding binding = new Binding();
                binding.Converter = new DictionaryItemConverter();
                binding.ConverterParameter = namList[i];
                gvc.DisplayMemberBinding = binding;

                (listView.View as GridView).Columns.Add(gvc);
            }

            var chuyenNganhList = (string[])Application.Current.TryFindResource("MaNganhList");
            var tenNganhList = (string[])Application.Current.TryFindResource("TenNganhList");

            var Data = new List<Dictionary<string, string>>();

            var count = 0;

            for (int i = 0; i < chuyenNganhList.Length; i++)
            {
                var soLuong = thongKe.SoLuongTheoNganh(chuyenNganhList[i]);

                var row = new Dictionary<string, string>();
                row.Add("MaNganh", chuyenNganhList[i]);
                row.Add("TenNganh", tenNganhList[i]);
                for (int j = 0; j < namList.Count; j++)
                {
                    if (soLuong != null)
                    {
                        if (soLuong.Count > j)
                        {
                            row.Add(namList[j], soLuong[j]);
                            count += int.Parse(soLuong[j]);
                        }
                        else
                        {
                            row.Add(namList[j], "0");
                        }
                    }
                    else
                    {
                        row.Add(namList[j], "0");
                    }
                }

                Data.Add(row);
            }

            SoSV.Content = count.ToString();
            listView.ItemsSource = Data;
        }
    }
}
