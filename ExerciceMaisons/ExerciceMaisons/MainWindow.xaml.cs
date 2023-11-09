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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(AfficherMaisons));
        }

        private void btnAfficher_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AfficherMaisons));
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AjouterMaison));
        }

        private void btnAjouterP_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AjouterProprietaire));
        }

        private void btnAfficherP_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AfficherProprietaires));
        }

        private void btnAfficherMaisonsEtProprios_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AfficherMetP));
        }
    }
}
