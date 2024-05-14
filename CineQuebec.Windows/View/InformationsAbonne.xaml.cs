using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
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
    /// Logique d'interaction pour InformationsAbonne.xaml
    /// </summary>
    public partial class InformationsAbonne : Window
    {
        private readonly ITypeRecompenseService _typeRecompenseService;
        private readonly IRecompenseService _recompenseService;
        private Abonne _abonne;
        List<Acteur> _acteurs = new List<Acteur>();
        public InformationsAbonne(Abonne abonne, ITypeRecompenseService pTypeRecompenseService, IRecompenseService pRecompenseService)
        {
            InitializeComponent();
            SelectUser(abonne);
            _typeRecompenseService = pTypeRecompenseService;
            _recompenseService = pRecompenseService;
            _abonne = abonne;
        }

        private void SelectUser(Abonne abonne)
        {
            if (abonne != null)
            {
                txtUserName.Text = abonne.Name;
                datePickerDateAdhesion.Text = abonne.DateAdhesion.ToString();
                txtEmail.Text = abonne.Email;
                txtNbProjection.Text=abonne.Reservations.Count.ToString();
                lstActeurs.Items.Add(abonne);
                AfficherPreferences(abonne);
                AfficherRecompenseAbonne(abonne);

            }
        }

        public void AfficherPreferences(Abonne abonne)
        {
            lstActeurs.Items.Clear();
            foreach (var preference in abonne.Preferences)
            {
                if (preference.Acteur != null && preference.Acteur.Id != ObjectId.Empty)
                {
                    lstActeurs.Items.Add(preference.Acteur);
                }
                if (preference.Realisateur != null && preference.Realisateur.Id != ObjectId.Empty)
                {
                    lstRealisateurs.Items.Add(preference.Realisateur);
                }
                if (preference.Categorie != null && preference.Categorie.Id != ObjectId.Empty)
                {
                    lstCategories.Items.Add(preference.Categorie);
                }
               
            }
        }
        public void AfficherRecompenseAbonne(Abonne abonne)
        {
            lstRecompenses.Items.Clear();
            foreach(var recompense in abonne.Recompenses)
            {
                if (recompense.IdAbonne !=null && recompense.IdTypeRecompense !=null)
                {
                    lstRecompenses.Items.Add(recompense);
                }
            }
        }
       
    }
}
