using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TPN2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherMateriel : Page
    {
        public AfficherMateriel()
        {
            this.InitializeComponent();
            gvMateriaux.ItemsSource = SingletonMateriel.getInstance().getListeMateriaux();
        }

        private async void btnLectureCSV_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletonPage.getInstance().Fenetre);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

            /*boucle permettant de lire chacune des lignes du fichier
            * et de remplir une liste d'objets de type CLient*/
            foreach (var ligne in lignes)
            {
                var vDonnee = ligne.Split(";"); 
         
                SingletonMateriel.getInstance().ajouterMaterielBD
                (Convert.ToInt32(vDonnee[0]), vDonnee[1], vDonnee[2], vDonnee[3], Convert.ToDouble(vDonnee[5]), vDonnee[4]);

                //Ajout dans la liste des matériaux
                Materiel materiel = new Materiel
                {
                    Code = Convert.ToInt32(vDonnee[0]),
                    Modele = vDonnee[1],
                    Meuble = vDonnee[2],
                    Categorie = vDonnee[3],
                    Prix = Convert.ToDouble(vDonnee[5]),
                    Couleur = vDonnee[4]                   
                };
                SingletonMateriel.getInstance().ajouterListeMateriaux(materiel);
             
            }
        }

        private void gvMateriaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvMateriaux.SelectedIndex >= 0)
                this.Frame.Navigate(typeof(ModifierMateriel), gvMateriaux.SelectedIndex);
        }
    }
}
