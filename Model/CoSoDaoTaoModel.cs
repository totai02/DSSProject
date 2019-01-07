using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class CoSoDaoTaoModel
    {
    }

    public class CoSoDaoTao : INotifyPropertyChanged
    {
        private string maTruong;
        private string tenTruong;
        private string diaChi;
        private string website;
        private string tinhThanh;
        private string dVChuQuan;

        public string MaTruong
        {
            get => maTruong;
            set
            {
                maTruong = value;
                NotifyPropertyChanged("MaTruong");
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

        public string DiaChi
        {
            get => diaChi;
            set
            {
                diaChi = value;
                NotifyPropertyChanged("DiaChi");
            }
        }

        public string Website
        {
            get => website;
            set
            {
                website = value;
                NotifyPropertyChanged("Website");
            }
        }

        public string TinhThanh
        {
            get => tinhThanh;
            set
            {
                tinhThanh = value;
                NotifyPropertyChanged("TinhThanh");
            }
        }

        public string DVChuQuan
        {
            get => dVChuQuan;
            set
            {
                dVChuQuan = value;
                NotifyPropertyChanged("DVChuQuan");
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
