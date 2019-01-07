using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class ChuyenNganhDaoTaoModel
    {
    }

    public class ChuyenNganhDaoTao : INotifyPropertyChanged
    {
        private string maNganh;
        private string nhomNganh;
        private string tenChuyenNganh;

        public string MaNganh
        {
            get => maNganh;
            set
            {
                maNganh = value;
                NotifyPropertyChanged("MaNganh");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
