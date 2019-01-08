using DSSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSSProject.ViewModel
{
    class CoSoTheoNganhViewModel
    {
        public ObservableCollection<CoSoTheoNganh> coSoTheoNganhs { get; set; }
        private CoSoTheoNganhRepository coSoTheoNganhRepository { get; set; }

        public CoSoTheoNganhViewModel()
        {
            coSoTheoNganhRepository = new CoSoTheoNganhRepository();
            coSoTheoNganhs = new ObservableCollection<CoSoTheoNganh>(coSoTheoNganhRepository.coSoRepository);
        }
    }
}
