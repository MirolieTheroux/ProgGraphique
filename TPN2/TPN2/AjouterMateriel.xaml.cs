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
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TPN2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterMateriel : Page
    {
        public AjouterMateriel()
        {
            this.InitializeComponent();
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            //Enlève les messages d'erreurs
            enleverErreur();
            bool estValide = false;

            if (!SingletonVerification.getInstance().isCodeValide(txtBoxCode.Text))
            {
                txtBlErreurCode.Text = "Veuillez entrer un code valide de 3 chiffre minimum.";
                estValide = true;
            }

            if (!SingletonVerification.getInstance().isModeleValide(txtBoxModele.Text))
            {
                txtBlErreurModele.Text = "Veuillez entrer un modèle.";
                estValide = true;
            }

            if (!SingletonVerification.getInstance().isNomMeubleValide(txtBoxMeuble.Text))
            {
                txtBlErreurMeuble.Text = "Veuillez entrer un nom de meuble.";
                estValide = true;
            }

            if (!SingletonVerification.getInstance().isCategorieValide(cmbBoxCategorie.SelectedIndex))
            {
                txtBlErreurCode.Text = "Veuillez choisir une catégorie.";
                estValide = true;
            }

            if (!SingletonVerification.getInstance().isCouleurValide(txtBoxCouleur.Text))
            {
                txtBlErreurCouleur.Text = "Veuillez entrer une couleur.";
                estValide = true;
            }

            if (!SingletonVerification.getInstance().isCodeValide(txtBoxPrix.Text))
            {
                txtBlErreurPrix.Text = "Veuillez entrer un prix supérieur à 0$.";
                estValide = true;
            }

            if (estValide)
            {
                //Ajout dans la liste des matériaux
                Materiel materiel = new Materiel
                {
                    Code = int.Parse(txtBoxCode.Text),
                    Modele = txtBoxModele.Text,
                    Meuble = txtBoxMeuble.Text,
                    Categorie = (string)cmbBoxCategorie.SelectedItem,
                    Prix = double.Parse(txtBoxPrix.Text),
                    Couleur = txtBoxCouleur.Text              
                };
                SingletonMateriel.getInstance().ajouterListeMateriaux(materiel);

                //Ajout dans la BD
                SingletonMateriel.getInstance().ajouterMaterielBD(int.Parse(txtBoxCode.Text), txtBoxModele.Text, txtBoxMeuble.Text,
                    (string)cmbBoxCategorie.SelectedItem, double.Parse(txtBoxPrix.Text), txtBoxCouleur.Text);

                //Ajout dialogue "bien ajouté"
                ContentDialog diagAjoute = new ContentDialog();
                diagAjoute.XamlRoot = mainGrid.XamlRoot;
                diagAjoute.Title = "Ajout du matériel";
                diagAjoute.PrimaryButtonText = "OK";
                diagAjoute.DefaultButton = ContentDialogButton.Primary;
                diagAjoute.Content = "Le matériel a été ajouté avec succès";

                ContentDialogResult resultat = await diagAjoute.ShowAsync();

                this.Frame.Navigate(typeof(AfficherMateriel));
            }
        }
        /// <summary>
        /// Permet d'enlever les messages d'erreurs
        /// </summary>
        private void enleverErreur()
        {
            txtBlErreurCode.Text = string.Empty;
            txtBlErreurModele.Text = string.Empty;
            txtBlErreurMeuble.Text = string.Empty;
            txtBlErreurCategorie.Text = string.Empty;
            txtBlErreurCouleur.Text = string.Empty;
            txtBlErreurPrix.Text = string.Empty;
        }
    }
}
