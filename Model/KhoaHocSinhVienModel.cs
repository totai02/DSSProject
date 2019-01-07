using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class KhoaHocSinhVienModel
    {
    }

    public class KhoaHocSinhVien : INotifyPropertyChanged
    {
        private string maKhoaHoc;
        private int namVao;
        private int namRa;

        public string MaKhoaHoc
        {
            get => maKhoaHoc;
            set
            {
                maKhoaHoc = value;
                NotifyPropertyChanged("MaKhoaHoc");
            }
        }
        public int NamVao
        {
            get => namVao;
            set
            {
                namVao = value;
                NotifyPropertyChanged("NamVao");
            }
        }
        public int NamRa
        {
            get => namRa;
            set
            {
                namRa = value;
                NotifyPropertyChanged("NamRa");
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
