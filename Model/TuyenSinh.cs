using System.ComponentModel;

namespace DSSProject.Model
{
    public class TuyenSinh : INotifyPropertyChanged
    {
        private string maTruong;
        private string maNganh;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
