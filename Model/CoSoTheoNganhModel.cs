using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.Model
{
    public class CoSoTheoNganhModel
    {
    }

    public class CoSoTheoNganh : INotifyPropertyChanged
    {
        private string maTruong;
        private string maNganh;
        private int namDT;
        private int soCB;
        private float diemChuan;
        private int chiTieu;
        private int slDaTuyen;

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

        public int NamDT
        {
            get => namDT;
            set
            {
                namDT = value;
                NotifyPropertyChanged("NamDT");
            }
        }

        public int SoCB
        {
            get => soCB;
            set
            {
                soCB = value;
                NotifyPropertyChanged("SoCB");
            }
        }

        public float DiemChuan
        {
            get => diemChuan;
            set
            {
                diemChuan = value;
                NotifyPropertyChanged("DiemChuan");
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

        public int SlDaTuyen
        {
            get => slDaTuyen;
            set
            {
                slDaTuyen = value;
                NotifyPropertyChanged("SlDaTuyen");
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
