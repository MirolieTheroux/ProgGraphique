using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class ModifierMateriel : Page
    {
        int index = -1;
        public ModifierMateriel()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                index = (int)e.Parameter;
                Materiel materiel = SingletonMateriel.getInstance().getMateriel(index);
       
                txtBoxCode.Text = materiel.Code.ToString();
                txtBoxModele.Text = materiel.Modele;
                txtBoxMeuble.Text = materiel.Meuble;
                cmbBoxCategorie.SelectedItem = materiel.Categorie;
                txtBoxCouleur.Text = materiel.Couleur;
                txtBoxPrix.Text = materiel.Prix.ToString();
            }
        }

        /// <summary>
        /// Modifie dans la liste et la BD le matériel choisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnModifier_Click(object sender, RoutedEventArgs e)
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
                //Modifie dans la liste des matériaux
                Materiel materiel = new Materiel
                {
                    Code = int.Parse(txtBoxCode.Text),
                    Modele = txtBoxModele.Text,
                    Meuble = txtBoxMeuble.Text,
                    Categorie = (string)cmbBoxCategorie.SelectedItem,
                    Prix = double.Parse(txtBoxPrix.Text),
                    Couleur = txtBoxCouleur.Text
                };
                SingletonMateriel.getInstance().modifierMaterielListe(index,materiel);

                //Modifie dans la BD
                SingletonMateriel.getInstance().modiferMaterielBD(int.Parse(txtBoxCode.Text), txtBoxModele.Text, txtBoxMeuble.Text,
                    (string)cmbBoxCategorie.SelectedItem, double.Parse(txtBoxPrix.Text), txtBoxCouleur.Text);

                //Ajout dialogue "bien ajouté"
                ContentDialog diagAjoute = new ContentDialog();
                diagAjoute.XamlRoot = mainGrid.XamlRoot;
                diagAjoute.Title = "Modification du matériel";
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

        private async void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            //ouvre une boite de dialogue qui permet de confirmer la suppression
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = mainGrid.XamlRoot;
            dialog.Title = "Attention";
            dialog.PrimaryButtonText = "Oui";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "Voulez-vous supprimer cet article ?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary) //si on clique sur OUI
            {
                SingletonMateriel.getInstance().supprimerListeMateriaux(index);
                SingletonMateriel.getInstance().supprimerMaterielBD(int.Parse(txtBoxCode.Text));
                this.Frame.Navigate(typeof(AfficherMateriel));
            }
        }
    }
}
