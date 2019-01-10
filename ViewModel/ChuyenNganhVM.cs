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
        private ChuyenNganhRepository chuyenNganhRepo { get; set; }

        public ChuyenNganhVM()
        {
            chuyenNganhRepo = new ChuyenNganhRepository();
            string[] maNganhList = new string[chuyenNganhRepo.chuyenNganhRepository.Count];
            for (int i = 0; i < chuyenNganhRepo.chuyenNganhRepository.Count; i++)
            {
                maNganhList[i] = chuyenNganhRepo.chuyenNganhRepository[i].MaNganh;
            }
            Application.Current.Resources["MaNganhList"] = maNganhList;
        }

        public void GetAllRepo()
        {
            chuyenNganhs = new ObservableCollection<ChuyenNganh>(chuyenNganhRepo.GetChuyenNganhRepo());
            chuyenNganhs.CollectionChanged += Record_CollectionChanged;
        }

        public void SearchRecord(string queryString)
        {
            string[] arrQuery = queryString.Split(' ');
            chuyenNganhs = new ObservableCollection<ChuyenNganh>(chuyenNganhRepo.SearchRecord(arrQuery));
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

        public void DelRecord(List<ChuyenNganh> removeList)
        {
            for (int i = 0; i < removeList.Count; i++)
            {
                string maNganh = removeList[i].MaNganh;
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
        }

        private void Record_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int newIndex = e.NewStartingIndex;
                if (!chuyenNganhRepo.addNewRecord(chuyenNganhs[newIndex]))
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
                chuyenNganhRepo.DelRecord(tempListOfRemovedItems[0].MaNganh);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<ChuyenNganh> tempListOfMovies = e.NewItems.OfType<ChuyenNganh>().ToList();
                if (!chuyenNganhRepo.UpdateRecord(tempListOfMovies[0]))
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
