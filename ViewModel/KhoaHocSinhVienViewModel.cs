using DSSProject.Model;
using System.Collections.ObjectModel;

namespace DSSProject.ViewModel
{
    class KhoaHocSinhVienViewModel
    {
        public ObservableCollection<KhoaHocSinhVien> khoaHocs { get; set; }
        private KhoaHocSinhVienRepository khoaHocRepository { get; set; }

        public KhoaHocSinhVienViewModel()
        {
            khoaHocRepository = new KhoaHocSinhVienRepository();
            khoaHocs = new ObservableCollection<KhoaHocSinhVien>(khoaHocRepository.khoaHocRepository);
        }
    }
}
