using DSSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DSSProject.ViewModel
{
    public class CoSoVM
    {
        public ObservableCollection<CoSo> coSos { get; set; }
        private CoSoRepository CoSoRepository { get; set; }

        public CoSoVM()
        {
            CoSoRepository = new CoSoRepository();
            string[] maTruongList = new string[CoSoRepository.coSoRepository.Count];
            for (int i = 0; i < CoSoRepository.coSoRepository.Count; i++)
            {
                maTruongList[i] = CoSoRepository.coSoRepository[i].MaTruong;
            }
            Application.Current.Resources["MaTruongList"] = maTruongList;
        }

        public void GetAllRepo()
        {
            coSos = new ObservableCollection<CoSo>(CoSoRepository.GetCoSoRepo());
            coSos.CollectionChanged += Record_CollectionChanged;
        }

        public void SearchRecord(string queryString)
        {
            string[] arrQuery = queryString.Split(' ');
            coSos = new ObservableCollection<CoSo>(CoSoRepository.SearchRecord(arrQuery));
            coSos.CollectionChanged += Record_CollectionChanged;
        }

        public void AddRecord(CoSo chuyenNganh)
        {
            if (chuyenNganh == null)
                throw new ArgumentNullException("Error: The argument is Null");
            coSos.Add(chuyenNganh);
        }

        public void UpdateRecord(CoSo coSo)
        {
            if (coSo == null)
                throw new ArgumentNullException("Error: The argument is Null");
            int index = 0;
            while (index < coSos.Count)
            {
                if (coSos[index].MaTruong == coSo.MaTruong)
                {
                    coSos[index] = coSo;
                    break;
                }
                index++;
            }
        }

        public void DelRecord(List<CoSo> rmList)
        {
            for (int i = 0; i < rmList.Count; i++)
            {
                string maTruong = rmList[i].MaTruong;
                int index = 0;
                while (index < coSos.Count)
                {
                    if (coSos[index].MaTruong == maTruong)
                    {
                        coSos.RemoveAt(index);
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
                if (!CoSoRepository.addNewRecord(coSos[newIndex]))
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
                List<CoSo> tempListOfRemovedItems = e.OldItems.OfType<CoSo>().ToList();
                CoSoRepository.DelRecord(tempListOfRemovedItems[0].MaTruong);
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                List<CoSo> tempListOfMovies = e.NewItems.OfType<CoSo>().ToList();
                if (!CoSoRepository.UpdateRecord(tempListOfMovies[0]))
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
