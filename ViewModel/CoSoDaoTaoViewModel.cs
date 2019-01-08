using DSSProject.Model;
using System.Collections.ObjectModel;

namespace DSSProject.ViewModel
{
    class CoSoDaoTaoViewModel
    {
        public ObservableCollection<CoSoDaoTao> coSos { get; set; }
        private CoSoDaoTaoRepository CoSoRepository { get; set; }

        public CoSoDaoTaoViewModel()
        {
            CoSoRepository = new CoSoDaoTaoRepository();
            coSos = new ObservableCollection<CoSoDaoTao>(CoSoRepository.coSoRepository);
        }
    }
}
