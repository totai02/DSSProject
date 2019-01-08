using DSSProject.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DSSProject.ViewModel
{
    public class SinhVienViewModel
    {
        public ObservableCollection<SinhVien> SinhViens{ get; set; }
        private SinhVienRepository SinhVienRepository { get; set; }

        public SinhVienViewModel()
        {
            SinhVienRepository = new SinhVienRepository();
            SinhViens = new ObservableCollection<SinhVien>(SinhVienRepository.sinhVienRepository);
        }
    }
}
