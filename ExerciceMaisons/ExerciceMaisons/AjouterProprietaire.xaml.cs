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
    public sealed partial class AjouterProprietaire : Page
    {
        public AjouterProprietaire()
        {
            this.InitializeComponent();
        }

        private void btnAjoutProprio_Click(object sender, RoutedEventArgs e)
        {
            bool bErreur = false;

            if (txtBoxNom.Text == "" || txtBoxNom.Text == null)
            {
                txtBlErreurNom.Text = "Vous devez entrer un nom.";
                bErreur = true;
            }
            else
                txtBlErreurNom.Text = "";

            if (txtBoxPrenom.Text == "" || txtBoxPrenom.Text == null)
            {
                txtBlErreurPrenom.Text = "Vous devez entrer un prenom.";
                bErreur = true;
            }
            else
                txtBlErreurPrenom.Text = "";

            if (!bErreur)
            {
                SingletonProprietaire.getInstance().ajouterProprietaire(txtBoxPrenom.Text,txtBoxNom.Text);
                txtBoxNom.Text = "";
                txtBoxPrenom.Text = "";
            }
        }
    }
}
