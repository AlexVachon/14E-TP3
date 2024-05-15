using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.ViewModel
{
    public class RecompensesViewModel:ViewModelBase
    {
        public Recompense? Recompense { get; set; }
         private IAbonneService _AbonneService { get; set;}
        public RecompensesViewModel( IAbonneService abonneService)
        {           
            _AbonneService = abonneService;
        }
    }
}
