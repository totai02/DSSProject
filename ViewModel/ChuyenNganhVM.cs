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
    public class ChuyenNganhVM
    {
        public ObservableCollection<ChuyenNganh> chuyenNganhs { get; set; }
        private ChuyenNganhRepository chuyenNganhRepository { get; set; }

        public ChuyenNganhVM()
        {
            chuyenNganhRepository = new ChuyenNganhRepository();
            GetAllRepo();
        }

        public void GetAllRepo()
        {
            chuyenNganhs = new ObservableCollection<ChuyenNganh>(chuyenNganhRepository.GetChuyenNganhRepo());
            chuyenNganhs.CollectionChanged += Record_CollectionChanged;
        }

        public void SearchRecord(string queryString)
        {
            string[] arrQuery = queryString.Split(' ');
            chuyenNganhs = new ObservableCollection<ChuyenNganh>(chuyenNganhRepository.SearchRecord(arrQuery));
            chuyenNganhs.CollectionChanged += Record_CollectionChanged;
        }

        public void AddRecord(ChuyenNganh chuyenNganh)
        {
            if (chuyenNganh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            chuyenNganhs.Add(chuyenNganh);
        }

        public void UpdateRecord(ChuyenNganh chuyenNganh)
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
                List<ChuyenNganh> tempListOfRemovedItems = e.OldItems.OfType<ChuyenNganh>().ToList();
                chuyenNganhRepository.DelRecord(tempListOfRemovedItems[0].MaNganh);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<ChuyenNganh> tempListOfMovies = e.NewItems.OfType<ChuyenNganh>().ToList();
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
