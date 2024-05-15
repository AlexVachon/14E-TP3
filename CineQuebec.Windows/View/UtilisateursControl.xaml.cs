using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour UtilisateursControl.xaml
    /// </summary>
    public partial class UtilisateursControl : Window
    {
        private readonly IAbonneService _abonneService;
        private readonly ITypeRecompenseService _typeRecompenseService;
        private readonly IRecompenseService _recompenseService;
        private readonly IProjectionService _projectionService;
        private readonly IFilmService _filmService;
        public Recompense recompense;
        List<Abonne> _listeDesUsers;
        
        public UtilisateursControl(IAbonneService abonneService, ITypeRecompenseService typeRecompenseService, IRecompenseService recompenseService,
            IProjectionService projectionService, IFilmService filmService)
        {
            InitializeComponent();

            _typeRecompenseService = typeRecompenseService;
            _recompenseService = recompenseService;
            _abonneService = abonneService;
            _projectionService = projectionService;
            _filmService = filmService;
            _listeDesUsers = _abonneService.ObtenirAbonnes().OrderByDescending(x=>x.Reservations.Count).ToList();

            AfficherListeUtilisateurs();
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedItems !=null)
            {
                Abonne abonne = lstUsers.SelectedItem as Abonne;             
                InformationsAbonne informationAbonne = new InformationsAbonne(abonne, _typeRecompenseService, _recompenseService);
                informationAbonne.Show();

            }
        }

        private void AfficherListeUtilisateurs()
        {
            lstUsers.Items.Clear();
            foreach (Abonne abonne in _listeDesUsers)
            {
                lstUsers.Items.Add(abonne);
            }
        }


        private void Button_Premiere_Click(object sender, RoutedEventArgs e)
        {
            RecompenseControl recompenseControl = new RecompenseControl(_abonneService, _typeRecompenseService, _recompenseService, _projectionService, _filmService);
            recompenseControl.TypeRecompense = "Assister à une avant première";
            recompenseControl.AfficherListeAbonnesPremiere();
            if (recompenseControl.ShowDialog() == true)
            {
                recompense = recompenseControl.Recompense;
                Abonne abonneSelectionne;
                if (recompenseControl.Recompense != null)
                {
                    abonneSelectionne = _listeDesUsers.Find(x => x.Id == recompense.IdAbonne);
                    abonneSelectionne.Recompenses.Add(recompense);
                }

                AfficherListeUtilisateurs();
            }
           
            
        }
        private void Button_Ticket_Click(object sender, RoutedEventArgs e)
        {
            RecompenseControl recompenseControl = new RecompenseControl(_abonneService, _typeRecompenseService, _recompenseService, _projectionService, _filmService);
            recompenseControl.TypeRecompense = "Ticket gratuit";
            recompenseControl.AfficherListeAbonnesTicket();
            if (recompenseControl.ShowDialog() == true)
            {
                recompense = recompenseControl.Recompense;
                Abonne abonneSelectionne;
                if (recompenseControl.Recompense != null)
                {
                    abonneSelectionne = _listeDesUsers.Find(x => x.Id == recompense.IdAbonne);
                    abonneSelectionne.Recompenses.Add(recompense);
                }
                AfficherListeUtilisateurs();
            }
           
            
        }

      

      
    }
}
