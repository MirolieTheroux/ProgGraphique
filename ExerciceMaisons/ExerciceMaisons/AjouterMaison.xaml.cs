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
    public sealed partial class AjouterMaison : Page
    {
        public AjouterMaison()
        {
            this.InitializeComponent();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {

            bool bErreur = false;

            if (cmbBoxCategorie.SelectedItem == null)
            {
                txtBlErreurCategorie.Text = "Vous devez choisir un type de maison.";
                bErreur = true;
            }
            else
                txtBlErreurCategorie.Text = "";

            if (txtBoxPrix.Text == "" || txtBoxPrix.Text == null)
            {
                txtBlErreurPrix.Text = "Vous devez entrer un prix.";
                bErreur = true;
            }
            else if (!double.TryParse(txtBoxPrix.Text, out _) && txtBoxPrix.Text != null && txtBoxPrix.Text != "")
            {
                txtBlErreurPrix.Text = "Le prix doit seulement être des chiffres.";
                bErreur = true;
            }
            else
                txtBlErreurPrix.Text = "";

            if (txtBoxVille.Text == "" || txtBoxVille.Text == null)
            {
                txtBlErreurVille.Text = "Vous devez entrer une ville.";
                bErreur = true;
            }
            else
                txtBlErreurVille.Text = "";

            if (txtBoxNomP.Text == "" || txtBoxNomP.Text == null)
            {
                txtBlErreurNomP.Text = "Vous devez entrer un nom.";
                bErreur = true;
            }
            else
                txtBlErreurNomP.Text = "";

            if (txtBoxPrenomP.Text == "" || txtBoxPrenomP.Text == null)
            {
                txtBlErreurPrenomP.Text = "Vous devez entrer un prenom.";
                bErreur = true;
            }
            else
                txtBlErreurPrenomP.Text = "";


            if (!bErreur)
            {
                SingletonMaison.getInstance().ajouterMaisonBD((string)cmbBoxCategorie.SelectedItem, Convert.ToDouble(txtBoxPrix.Text), txtBoxVille.Text, txtBoxNomP.Text, txtBoxPrenomP.Text);

                cmbBoxCategorie.SelectedIndex = -1;
                txtBoxPrix.Text = "";
                txtBoxVille.Text = "";
                txtBoxNomP.Text = "";
                txtBoxPrenomP.Text = "";
            }
        }
    }
}
