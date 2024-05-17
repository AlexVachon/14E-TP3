using CineQuebec.Windows.BLL.Interfaces;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineQuebec.Windows.View
{
	/// <summary>
	/// Logique d'interaction pour AbonneHomeControl.xaml
	/// </summary>
	public partial class AbonneHomeControl : UserControl
	{
		private readonly IFilmService _filmService;
		private readonly ICategorieService _categorieService;
		private readonly IRealisateurService _realisateurService;
		private readonly IPreferenceService _preferenceService;
		private readonly IProjectionService _projectionService;
		private readonly IReservationService _reservationService;
		private readonly IActeurService _acteurService;
		private readonly INoteService _noteService;

		public AbonneHomeControl(IFilmService pFilmService, ICategorieService pCategorieService, IRealisateurService pRealisateurService, IPreferenceService pPreferenceService, IProjectionService projectionService, IReservationService reservationService, INoteService noteService, IActeurService acteurService)
		{
			InitializeComponent();

			_filmService = pFilmService;
			_categorieService = pCategorieService;
			_realisateurService = pRealisateurService;
			_preferenceService = pPreferenceService;
			_projectionService = projectionService;
			_reservationService = reservationService;
			_acteurService = acteurService;
			_noteService = noteService;
		}

 		private void Button_Profile_Click(object sender, RoutedEventArgs e)
		{
			var utilisateursPreference = new PreferencesAbonne(_acteurService, _categorieService, _realisateurService, _preferenceService);
			utilisateursPreference.Show();
		}

		private void Button_Projections_Click(object sender, RoutedEventArgs e)
		{
			new ProjectionsControl(_projectionService, _filmService, _reservationService, _noteService).Show();
		}
	}
}
