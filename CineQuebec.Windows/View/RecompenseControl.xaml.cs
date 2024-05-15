using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
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
    /// Logique d'interaction pour RecompenseControl.xaml
    /// </summary>
    public partial class RecompenseControl : Window
    {
        private readonly IAbonneService _abonneService;
        private readonly ITypeRecompenseService _typeRecompenseService;
        private readonly IRecompenseService _recompenseService;
        private readonly IProjectionService _projectionService;
        private readonly IFilmService _filmService;
        List<Abonne> _Abonnes;
        public string TypeRecompense;
        public Recompense  Recompense;
        public RecompenseControl(IAbonneService abonneService, ITypeRecompenseService typeRecompenseService, IRecompenseService recompenseService,
            IProjectionService projectionService, IFilmService filmService)
        {
            InitializeComponent();

            _typeRecompenseService = typeRecompenseService;
            _recompenseService = recompenseService;
            _abonneService = abonneService;
            _projectionService = projectionService;
            _Abonnes = _abonneService.ObtenirAbonnes().OrderByDescending(x => x.Reservations.Count).ToList();
            _filmService = filmService;
            //AfficherListeAbonnes();
        }

        public void AfficherListeAbonnesTicket()
        {
         
                var projectionAffiches = _projectionService.GetAllProjections().Result.Where(x => x.DateProjection >= DateTime.Today);
             
                 var  Films = _filmService.GetAllFilmsAffiche(projectionAffiches.ToList()).Result;
                
                var categorieFilmAffiches = Films.Select(y => y.Categorie.Id).ToList();
               
               var Abonnes = _Abonnes.Where(x => x.Preferences.Any(preference => categorieFilmAffiches.Contains(preference.IdCategorie))).ToList();
                lstAbonnes.Items.Clear();
                foreach (Abonne abonne in Abonnes)
                {
                    lstAbonnes.Items.Add(abonne);
                }
           
           
        }
        public void AfficherListeAbonnesPremiere()
        {
        
               

                var Abonnes = _Abonnes.Where(x => x.Preferences.Any(z => z.IdCategorie == ObjectId.Empty)).OrderByDescending(y => y.Preferences.Count);
                lstAbonnes.Items.Clear();
                foreach (Abonne abonne in Abonnes)
                {
                    lstAbonnes.Items.Add(abonne);
                }
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lstAbonnes.SelectedItem !=null)
            {
                TypeRecompense typeRecompense; 
                var abonne = lstAbonnes.SelectedItem as Abonne;
                if (TypeRecompense == "Ticket gratuit")
                {
                    typeRecompense = _typeRecompenseService.ObtenirToutTypesRecompenses().FirstOrDefault(x => x.NomRecompense == "Ticket gratuit");
                }
                else
                {
                    typeRecompense = _typeRecompenseService.ObtenirToutTypesRecompenses().FirstOrDefault(x => x.NomRecompense == "Assister à une avant première");
                }
              ;
                Recompense = new Recompense { IdTypeRecompense = typeRecompense.Id, IdAbonne = abonne.Id, TypeRecompense = typeRecompense };
                _recompenseService.AjouterRecompense(Recompense);
                MessageBox.Show($"La récompense à été ajouté avec succès");
                DialogResult = true;
                

            }
            else
            {
                MessageBox.Show("Vous devez selectionner un abonné");
            }
        }
    }
}
