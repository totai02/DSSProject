using DSSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DSSProject.ViewModel
{
    public class ChuyenNganhDaoTaoViewModel
    {
        public ObservableCollection<ChuyenNganhDaoTao> chuyenNganhs { get; set; }
        private ChuyenNganhDaoTaoRepository chuyenNganhRepository { get; set; }

        public ChuyenNganhDaoTaoViewModel()
        {
            chuyenNganhRepository = new ChuyenNganhDaoTaoRepository();
            chuyenNganhs = new ObservableCollection<ChuyenNganhDaoTao>(chuyenNganhRepository.chuyenNganhRepository);
            chuyenNganhs.CollectionChanged += Record_CollectionChanged;
        }

        public void AddRecord(ChuyenNganhDaoTao chuyenNganh)
        {
            if (chuyenNganh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            chuyenNganhs.Add(chuyenNganh);
        }

        public void UpdateRecord(ChuyenNganhDaoTao chuyenNganh)
        {
            if (chuyenNganh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            int index = 0;
            while (index < chuyenNganhs.Count)
            {
                if (chuyenNganhs[index].MaNganh == chuyenNganh.MaNganh)
                {
                    chuyenNganhs[index] = chuyenNganh;
                    break;
                }
                index++;
            }
        }

        public void DelRecord(string maNganh)
        {

            int index = 0;
            while (index < chuyenNganhs.Count)
            {
                if (chuyenNganhs[index].MaNganh == maNganh)
                {
                    chuyenNganhs.RemoveAt(index);
                    break;
                }
                index++;
            }
        }

        private void Record_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                if (!chuyenNganhRepository.addNewRecord(chuyenNganhs[newIndex]))
                {
                    MessageBox.Show("Có lỗi khi thêm dữ liệu !!!");
                } else
                {
                    MessageBox.Show("Thêm dữ liệu thành công.");
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                List<ChuyenNganhDaoTao> tempListOfRemovedItems = e.OldItems.OfType<ChuyenNganhDaoTao>().ToList();
                chuyenNganhRepository.DelRecord(tempListOfRemovedItems[0].MaNganh);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<ChuyenNganhDaoTao> tempListOfMovies = e.NewItems.OfType<ChuyenNganhDaoTao>().ToList();
                if (!chuyenNganhRepository.UpdateRecord(tempListOfMovies[0]))
                {
                    MessageBox.Show("Có lỗi khi cập nhật dữ liệu !!!");
                } 
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công !!!");
                }
            }
        }
    }
}
