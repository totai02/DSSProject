using System.ComponentModel;

namespace DSSProject.Model
{
    public class SinhVien : INotifyPropertyChanged
    {
        private string maNganh;
        private string nhomNganh;
        private string tenChuyenNganh;
        private int soLuong;

        public string MaNganh
        {
            get => maNganh; set => maNganh = value;
        }

        public string NhomNganh
        {
            get => nhomNganh;
            set
            {
                nhomNganh = value;
                NotifyPropertyChanged("NhomNganh");
            }
        }

        public string TenChuyenNganh
        {
            get => tenChuyenNganh;
            set
            {
                tenChuyenNganh = value;
                NotifyPropertyChanged("TenChuyenNganh");
            }
        }

        public int SoLuong
        {
            get => soLuong;
            set
            {
                soLuong = value;
                NotifyPropertyChanged("SoLuong");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
