using System.ComponentModel;


namespace DSSProject.Model
{
    public class ChuyenNganh : INotifyPropertyChanged
    {
        private string maNganh;
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
