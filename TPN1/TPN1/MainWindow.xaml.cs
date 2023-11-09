using ABI.System;
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
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TPN1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        List<Event> eventsCrees = new List<Event>();

        public MainWindow()
        {
            this.InitializeComponent();
            eventsCrees = new List<Event>();
        }
        /// <summary>
        /// Ajoute un �v�nement lors du clic et permet
        /// de v�rifier si tous les champs sont
        /// bien remplis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            //Variable si erreur
            bool bErreur = false;

            //Message erreur si url vide ou si mauvais format
            string lienRegex = @"\b(?:(?:https?|ftp)://|www\.)[-a-z0-9+&@#\/%?=~_|!:,.;]*[-a-z0-9+&@#\/%=~_|]";
            Regex reg2 = new Regex(lienRegex, RegexOptions.IgnoreCase);
            if (txtBoxUrl.Text == "" || txtBoxUrl.Text == null)
            {
                txtBlErreurImage.Text = "Veuillez entrer un lien pour l'image de l'�v�nement";
                bErreur = true;
            }
            else if (!reg2.IsMatch(txtBoxUrl.Text))
            {
                txtBlErreurImage.Text = "Veuillez entrer un lien valide";
                bErreur = true;
            }
            else
            {
                txtBlErreurImage.Text = "";
            }

            //Message erreur nom event vide
            if (txtBoxNomEvent.Text == null || txtBoxNomEvent.Text == "")
            {
                txtBlErreurNom.Text = "Veuillez entrer un nom d'�v�nement";
                bErreur = true;
            }
            else
            {
                txtBlErreurNom.Text = "";
            }

            //Message erreur num civique vide ou pas de chiffre
            if (txtBoxNumCivique.Text == null || txtBoxNumCivique.Text == "")
            {
                txtBlErreurNumCiv.Text = "Veuillez entrer un num�ro civique";
                bErreur = true;
            }
            else if (!int.TryParse(txtBoxNumCivique.Text, out _) && txtBoxNumCivique.Text != null && txtBoxNumCivique.Text != "")
            {
                txtBlErreurNumCiv.Text = "Le num�ro civique doit contenir seulement des chiffres";
                bErreur = true;
            }
            else
            {
                txtBlErreurNumCiv.Text = "";
            }

            //Message erreur si pas bon format de code postal et si vide
            string codePostalRegex = "^[A-Za-z]\\d[A-Za-z] \\d[A-Za-z]\\d$";
            Regex reg = new Regex(codePostalRegex, RegexOptions.IgnoreCase);
            if (txtBoxCp.Text == "" || txtBoxCp.Text == null)
            {
                txtBlErreurCp.Text = "Veuillez entrer un code postal";
                bErreur = true;
            }
            else if (!reg.IsMatch(txtBoxCp.Text))
            {
                txtBlErreurCp.Text = "Le format du code postal doit �tre : G0X 2M0";
                bErreur = true;
            }
            else
            {
                txtBlErreurCp.Text = "";
            }
            //Message adresse vide
            if (txtBoxAdresse.Text == "" || txtBoxAdresse.Text == null)
            {
                txtBlErreurAdresse.Text = "Veuillez entrer une adresse";
                bErreur = true;
            }
            else
            {
                txtBlErreurAdresse.Text = "";
            }
            //Message erreur date non choisie
            if (!calDateP.Date.HasValue)
            {
                txtBlErreurDate.Text = "Vous devez choisir une date pour l'�v�nement";
                bErreur = true;
            }
            else
            {
                txtBlErreurDate.Text = "";
            }
            //Message erreur heure non choisie
            if (timeP.SelectedTime == null)
            {
                txtBlErreurHeure.Text = "Veuillez choisir une heure";
                bErreur = true;
            }
            else
            {
                txtBlErreurHeure.Text = "";

            }
            //Message erreur si aucune cat�gorie choisie
            if (cmBoxCat.SelectedItem == null)
            {
                txtBlErreurCat.Text = "Veuillez choisir une cat�gorie";
                bErreur = true;
            }
            else
            {
                txtBlErreurCat.Text = "";

            }

            if (!bErreur)
            {
                // Cr�ation de l'event
                string t = timeP.SelectedTime.ToString();
                Event ajoutEvent = new Event(txtBoxNomEvent.Text, calDateP.Date.Value.ToString("dddd dd MMMM"),
                                            t.Substring(0, t.Length - 3), txtBoxNumCivique.Text + " " + txtBoxAdresse.Text + " " + txtBoxCp.Text,
                                            cmBoxCat.SelectedItem as string, txtBoxUrl.Text, chkBoxPublic.IsChecked ?? false);
                //Ajout de l'�vent dans la liste d'�v�venements
                eventsCrees.Add(ajoutEvent);

                // Met � jour la source de donn�es de la ListView
                lvEvent.ItemsSource = null;
                lvEvent.ItemsSource = eventsCrees;

                // R�initialise les champs de saisie
                viderAjouter();
            }
        }
        /// <summary>
        /// Permet d'afficher un message d'erreur si la date n'a pas �t� chang�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            calDateP.MinDate = DateTime.Now.Date;
            if (calDateP.Date.Value.Date < calDateP.MinDate)
                txtBlErreurDate.Text = "Vous devez choisir une date sup�rieure ou �gale � aujourd'hui";
            else
                txtBlErreurDate.Text = "";
        }
        /// <summary>
        /// Permet de vider les informations dans ajouter lorsque l'�v�nement a �t� ajout�
        /// </summary>
        public void viderAjouter()
        {
            txtBoxNomEvent.Text = "";
            txtBoxNumCivique.Text = "";
            txtBoxCp.Text = "";
            txtBlErreurCp.Text = "";
            txtBoxAdresse.Text = "";
            //comment remettre la date a null sans avoir d'erreur
            //calDateP.Date = null;
            cmBoxCat.SelectedIndex = -1;
            timeP.SelectedTime = null;
            chkBoxPublic.IsChecked = false;
            txtBoxUrl.Text = "";
        }

        /// <summary>
        /// Permet d'afficher les d�tails selon l'�v�nement choisi par l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvEvent.SelectedIndex >= 0)
            {
                Event eventSelect = lvEvent.SelectedItem as Event;

                //Met les bon d�tails selon l'�v�nement cliqu�
                txtBlNom.Text = eventSelect.NomEvent;
                System.Uri uri = new System.Uri(eventSelect.UrlImage);
                img.Source = new BitmapImage(uri);
                txtBlDate.Text = eventSelect.DateEvent;
                txtBlHeure.Text = eventSelect.HeureEvent;
                txtBlAdresse.Text = eventSelect.Adresse.ToString();
                txtBlCategorie.Text = eventSelect.Categorie;
                if (eventSelect.EstPublic == true)
                {
                    txtBlPublic.Text = "Public";
                }
                else
                {
                    txtBlPublic.Text = "Non public";
                }
            }
        }
        /// <summary>
        /// Permet de supprimer l'�v�nement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupp_Click(object sender, RoutedEventArgs e)
        {
            if (lvEvent.SelectedIndex >= 0)
            {
                int eventSupp = lvEvent.SelectedIndex;
                eventsCrees.RemoveAt(eventSupp);
                // vide les d�tails
                viderDetails();
                // Met � jour la source de donn�es de la ListView
                lvEvent.ItemsSource = null;
                lvEvent.ItemsSource = eventsCrees;
            }
        }

        /// <summary>
        /// Permet de vider les d�tails afficher lorsque l'�v�nement est supprim�
        /// </summary>
        public void viderDetails()
        {
            txtBlNom.Text = "";
            txtBlDate.Text = "";
            txtBlHeure.Text = "";
            txtBlAdresse.Text = "";
            txtBlPublic.Text = "";
            txtBlCategorie.Text = "";
            img.Source = null;
        }
    }
}
