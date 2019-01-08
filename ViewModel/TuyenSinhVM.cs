using DSSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DSSProject.ViewModel
{
    public class TuyenSinhVM
    {
        public ObservableCollection<TuyenSinh> TuyenSinhOC{ get; set; }
        private TuyenSinhRepository tuyenSinhRepo { get; set; }

        public TuyenSinhVM()
        {
            tuyenSinhRepo = new TuyenSinhRepository();
            GetAllRepo();
        }

        public void GetAllRepo()
        {
            TuyenSinhOC = new ObservableCollection<TuyenSinh>(tuyenSinhRepo.GetTuyenSinhRepo());
            TuyenSinhOC.CollectionChanged += Record_CollectionChanged;
        }

        public void SearchRecord(string queryString)
        {
            string[] arrQuery = queryString.Split(' ');
            TuyenSinhOC = new ObservableCollection<TuyenSinh>(tuyenSinhRepo.SearchRecord(arrQuery));
            TuyenSinhOC.CollectionChanged += Record_CollectionChanged;
        }

        public void AddRecord(TuyenSinh chuyenNganh)
        {
            if (chuyenNganh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            TuyenSinhOC.Add(chuyenNganh);
        }

        public void UpdateRecord(TuyenSinh tuyenSinh)
        {
            if (tuyenSinh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            int index = 0;
            while (index < TuyenSinhOC.Count)
            {
                if (TuyenSinhOC[index].MaTruong == tuyenSinh.MaTruong && TuyenSinhOC[index].MaNganh == tuyenSinh.MaNganh)
                {
                    TuyenSinhOC[index] = tuyenSinh;
                    break;
                }
                index++;
            }
        }

        public void DelRecord(string maTruong, string maNganh)
        {
            int index = 0;
            while (index < TuyenSinhOC.Count)
            {
                if (TuyenSinhOC[index].MaTruong == maTruong && TuyenSinhOC[index].MaNganh == maNganh)
                {
                    TuyenSinhOC.RemoveAt(index);
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
                if (!tuyenSinhRepo.addNewRecord(TuyenSinhOC[newIndex]))
                {
                    MessageBox.Show("Có lỗi khi thêm dữ liệu !!!");
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu thành công.");
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                List<TuyenSinh> tempListOfRemovedItems = e.OldItems.OfType<TuyenSinh>().ToList();
                tuyenSinhRepo.DelRecord(tempListOfRemovedItems[0].MaTruong, tempListOfRemovedItems[0].MaNganh);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<TuyenSinh> tempListOfMovies = e.NewItems.OfType<TuyenSinh>().ToList();
                if (!tuyenSinhRepo.UpdateRecord(tempListOfMovies[0]))
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
