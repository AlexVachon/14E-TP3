using CineQuebec.Windows.BLL.Interfaces;
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
    /// Logique d'interaction pour PreferencesAbonne.xaml
    /// </summary>
    public partial class PreferencesAbonne : Window
    {
        private readonly IAbonneService _abonneService;
        private readonly IActeurService _acteurService;
        private readonly ICategorieService _categorieService;
        private readonly IRealisateurService _realisateurService;
        private readonly IPreferenceService _preferenceService;
        
        List<Abonne> _listeDesUsers;
        List<Acteur>? _listeDesActeurs;
        List<Categorie> _listeCategories;
        List<Realisateur> _listeRealisateurs;
        List<Preference> _preferences;
        public PreferencesAbonne(IActeurService acteurService, ICategorieService categorieService, IRealisateurService realisateurService, IPreferenceService preferenceService)
        {
            InitializeComponent();
            //_abonneService = abonneService;
            _acteurService = acteurService;
            _categorieService = categorieService;
            _realisateurService = realisateurService;
            _preferenceService = preferenceService;
            //_listeDesUsers = _abonneService.ObtenirAbonnes();
            _listeDesActeurs = _acteurService.ObtenirActeurs();
            _listeCategories = _categorieService.GetAllCategories().Result;
            _listeRealisateurs = _realisateurService.ObtenirRealisateurs();
            var user = App.Current.Properties["UserConnect"] as User;
            _preferences = _preferenceService.ObtenirPreferencesAbonne(user.Id);
            AfficherListeRealisateur();
            AfficherListeActeurs();
            AfficherListeCategories();
            AfficherListePreferences();
        }

        private void AfficherListeRealisateur()
        {
            lstRealisateurs.Items.Clear();
            foreach (Realisateur realisateur in _listeRealisateurs)
            {
                lstRealisateurs.Items.Add(realisateur);
            }
        }

        private void AfficherListeActeurs()
        {
            lstActeurs.Items.Clear();
            foreach (Acteur acteur in _listeDesActeurs)
            {
                lstActeurs.Items.Add(acteur);
            }
        }
        private void AfficherListeCategories()
        {
            lstCategories.Items.Clear();
            foreach (Categorie categorie in _listeCategories)
            {
                lstCategories.Items.Add(categorie);
            }
        }
        private void AfficherListePreferences()
        {
            lstPrefUser.Items.Clear();
            foreach (Preference preference in _preferences)
            {
                lstPrefUser.Items.Add(preference);
            }
        }


        private void Button_Ajouter_Realisateur_Click(object sender, RoutedEventArgs e)
        {
            Abonne abonne = (App.Current.Properties["UserConnect"]) as Abonne;
            List<Preference> preferencesRealisateur = _preferenceService.ObtenirPreferencesAbonne(abonne.Id);
            if (preferencesRealisateur.Where(x => x.IdRealisateur != ObjectId.Empty).Count() >= 5)
            {
                MessageBox.Show("Vous ne pouvez pas selectionner plus de cinq réalisateurs");
            }
            else
            {
                if (lstRealisateurs.SelectedItem != null)
                {
                    Preference preference = new Preference { IdRealisateur = (lstRealisateurs.SelectedItem as Realisateur).Id, IdAbonne = abonne.Id };
                    _preferenceService.AjouterPreference(preference);
                    preference.Realisateur = lstRealisateurs.SelectedItem as Realisateur;
                    lstPrefUser.Items.Add(preference);
                }
                else
                {
                    MessageBox.Show($"Vous devez selectionner un réalisateur");
                }
            }
          
        }

        private void lstRealisateurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Bouton_Ajouter_Acteur(object sender, RoutedEventArgs e)
        {
            Abonne abonne = (App.Current.Properties["UserConnect"]) as Abonne;
            List<Preference> preferencesActeurs = _preferenceService.ObtenirPreferencesAbonne(abonne.Id);
            if (preferencesActeurs.Where(x => x.IdActeur != ObjectId.Empty).Count() >= 5)
            {
                MessageBox.Show("Vous ne pouvez pas selectionner plus de cinq acteurs");
            }
            else
            {
                if (lstActeurs.SelectedItem != null)
                {
                    Preference preference = new Preference { IdActeur = (lstActeurs.SelectedItem as Acteur).Id, IdAbonne = abonne.Id };
                    _preferenceService.AjouterPreference(preference);
                    preference.Acteur = lstActeurs.SelectedItem as Acteur;
                    lstPrefUser.Items.Add(preference);
                }
                else
                {
                    MessageBox.Show($"Vous devez selectionner un film");
                }
            }
            
        }

        private void Button_Ajouter_Categorie(object sender, RoutedEventArgs e)
        {
            Abonne abonne = (App.Current.Properties["UserConnect"]) as Abonne;
            List<Preference> preferencesCategorie = _preferenceService.ObtenirPreferencesAbonne(abonne.Id);
            if (preferencesCategorie.Where(x =>x.IdCategorie != ObjectId.Empty).Count() >=3)
            {
                MessageBox.Show("Vous ne pouvez pas selectionner plus de trois catégories");
            }
            else
            {
                if (lstCategories.SelectedItem != null)
                {
                    Preference preference = new Preference { IdCategorie = (lstCategories.SelectedItem as Categorie).Id, IdAbonne = abonne.Id };
                    _preferenceService.AjouterPreference(preference);
                    preference.Categorie = lstCategories.SelectedItem as Categorie;
                    lstPrefUser.Items.Add(preference);
                }
                else
                {
                    MessageBox.Show($"Vous devez selectionner une catégorie");
                }
            }
          
        }
    }
}
