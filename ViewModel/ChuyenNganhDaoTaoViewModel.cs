using DSSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.ViewModel
{
    public class ChuyenNganhDaoTaoViewModel
    {
        public ObservableCollection<ChuyenNganhDaoTao> chuyenNganhs { get; set; }
        private ChuyenNganhDaoTaoRepository chuyenNganhRepository { get; set; }

        public ChuyenNganhDaoTaoViewModel()
        {
            chuyenNganhRepository = new ChuyenNganhDaoTaoRepository();
            chuyenNganhs = new ObservableCollection<ChuyenNganhDaoTao>(chuyenNganhRepository.chuyenNganhRepository);
        }
    }
}
