using System.ComponentModel;

namespace DSSProject.Model
{
    public class TuyenSinh : INotifyPropertyChanged
    {
        private string maTruong;
        private string tenTruong;
        private string maNganh;
        private string tenNganh;
        private int chiTieu;
        private int namDaoTao;

        public string MaTruong
        {
            get => maTruong;
            set
            {
                maTruong = value;
                NotifyPropertyChanged("MaTruong");
            }
        }

        public string MaNganh
        {
            get => maNganh;
            set
            {
                maNganh = value;
                NotifyPropertyChanged("MaNganh");
            }
        }

        public int ChiTieu
        {
            get => chiTieu;
            set
            {
                chiTieu = value;
                NotifyPropertyChanged("ChiTieu");
            }
        }

        public int NamDaoTao
        {
            get => namDaoTao;
            set
            {
                namDaoTao = value;
                NotifyPropertyChanged("NamDaoTao");
            }
        }

        public string TenTruong
        {
            get => tenTruong;
            set
            {
                tenTruong = value;
                NotifyPropertyChanged("TenTruong");

            }
        }

        public string TenNganh
        {
            get => tenNganh;
            set
            {
                tenNganh = value;
                NotifyPropertyChanged("TenNganh");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
