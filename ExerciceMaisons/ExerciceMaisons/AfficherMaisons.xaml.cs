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

namespace ExerciceMaisons
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherMaisons : Page
    {
        public AfficherMaisons()
        {
            this.InitializeComponent();
            gvMaisons.ItemsSource = SingletonMaison.getInstance().getListeMaisons();
        }

        private void rdoBtnsFiltre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sFiltre = rdoBtnsFiltre.SelectedItem as string;
            switch (sFiltre)
            {
                case "Type":
                    cmbBoxCategorie.Visibility = Visibility.Visible;
                    txtBoxRechercheVille.Visibility = Visibility.Collapsed;
                    break;
                case "Ville":
                    txtBoxRechercheVille.Visibility = Visibility.Visible;
                    cmbBoxCategorie.Visibility = Visibility.Collapsed;
                    break;
                case "Réinitialiser":
                    txtBoxRechercheVille.Visibility = Visibility.Collapsed;
                    cmbBoxCategorie.SelectedIndex = -1;
                    cmbBoxCategorie.Visibility = Visibility.Collapsed;
                    txtBoxRechercheVille.Text = "";
                    SingletonMaison.getInstance().charger();
                    break;
                default:
                    break;
            }
        }

        private void cmbBoxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SingletonMaison.getInstance().GetMaisonParCategorie((string)cmbBoxCategorie.SelectedItem);
        }

        private void txtBoxRechercheVille_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SingletonMaison.getInstance().GetMaisonParVille(txtBoxRechercheVille.Text);
        }
    }
}
