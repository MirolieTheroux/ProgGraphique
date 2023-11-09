using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql.Data.MySqlClient;

namespace TPN2
{
    internal class SingletonMateriel
    {
        static SingletonMateriel instance = null;
        MySqlConnection connection;
        ObservableCollection<Materiel> listeMateriaux;


        //constructeur de la classe
        public SingletonMateriel()
        {
            connection = new MySqlConnection("Server=localhost;Database=materiel;Uid=root;Pwd=root;");
            listeMateriaux = new ObservableCollection<Materiel>();
            //charger();
        }

        //retourne l’instance du singleton
        public static SingletonMateriel getInstance()
        {
            if (instance == null)
                instance = new SingletonMateriel();

            return instance;
        }

        /// <summary>
        /// Permet d'afficher toutes les matériaux qui sont dans la BD
        /// </summary>
        /// <returns>liste des maisons qui sont dans la bd</returns>
        public ObservableCollection<Materiel> getListeMateriaux()
        {
            return listeMateriaux;
        }

        /// <summary>
        /// Permet d'ajouter un matériel dans la liste
        /// </summary>
        /// <param name="materiel">Objet matériel créer par utilisateur</param>
        public void ajouterListeMateriaux(Materiel materiel)
        {
            listeMateriaux.Add(materiel);
        }

        /// <summary>
        /// Permet de retourner la position du matériel dans la liste
        /// </summary>
        /// <param name="position">Position du matériel</param>
        /// <returns></returns>
        public Materiel getMateriel(int position)
        {
            return listeMateriaux[position];
        }

        /// <summary>
        /// Permet de supprimer du matériel dans la lsite
        /// </summary>
        /// <param name="position">position du matériel à supprimer</param>
        public void supprimerListeMateriaux(int position)
        {
            listeMateriaux.RemoveAt(position);
        }

        /// <summary>
        /// Permet de modifier du matériel
        /// </summary>
        /// <param name="position">Position du matériel</param>
        /// <param name="materiel">Objet matériel</param>
        public void modifierMaterielListe(int position, Materiel materiel)
        {
            listeMateriaux[position] = materiel;
        }
        /// <summary>
        /// Permet d'ajouter un matériel dans la BD
        /// </summary>
        /// <param name="code">Code entré par utilisateur</param>
        /// <param name="modele">Modèle entré par utilisateur</param>
        /// <param name="nomMeuble">Nom du meuble entré par utilisateur</param>
        /// <param name="cat">Catégorie choisie par l'utilisateur</param>
        /// <param name="prix">Prix entré par utilisateur</param>
        /// <param name="couleur">Couleur entré par utilisateur</param>
        public void ajouterMaterielBD(int code, string modele, string nomMeuble, string cat, double prix, string couleur)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "insert into materiel values (@code,@modele,@nomMeuble,@categorie, @prix, @couleur)";

                commande.Parameters.AddWithValue("@code", code);
                commande.Parameters.AddWithValue("@modele", modele);
                commande.Parameters.AddWithValue("@nomMeuble", nomMeuble);
                commande.Parameters.AddWithValue("@categorie", cat);
                commande.Parameters.AddWithValue("@prix", prix);
                commande.Parameters.AddWithValue("@couleur", couleur);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();

                charger();
            }
            catch (Exception ex)
            {
                connection.Close();
                //message d'erreur
            }

        }

        /// <summary>
        /// Permet de modifier le matériel dans la BD
        /// </summary>
        /// <param name="code">Code à modifier</param>
        /// <param name="modele">Modele à modifier</param>
        /// <param name="nomMeuble">Nom du meuble à modifier</param>
        /// <param name="cat">Catégorie à modifier</param>
        /// <param name="prix">Prix à modifier</param>
        /// <param name="couleur">Couleur à modifier</param>
        public void modiferMaterielBD(int code, string modele, string nomMeuble, string cat, double prix, string couleur)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "update materiel set code=@code,modele=@modele,nom_meuble=@nomMeuble,categorie=@categorie,couleur=@couleur,prix=@prix";

                commande.Parameters.AddWithValue("@code", code);
                commande.Parameters.AddWithValue("@modele", modele);
                commande.Parameters.AddWithValue("@nomMeuble", nomMeuble);
                commande.Parameters.AddWithValue("@categorie", cat);
                commande.Parameters.AddWithValue("@prix", prix);
                commande.Parameters.AddWithValue("@couleur", couleur);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();

                charger();
            }
            catch (Exception ex)
            {
                connection.Close();
                //message d'erreur
            }
        }

        /// <summary>
        /// Permet de supprimer du matériel de la BD
        /// </summary>
        /// <param name="code">Code du matériel à supprimer</param>
        public void supprimerMaterielBD(int code)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "delete from materiel where code=@code";

                commande.Parameters.AddWithValue("@code", code);
              
                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                connection.Close();

                charger();
            }
            catch (Exception ex)
            {
                connection.Close();
                //message d'erreur
            }
        }

        public void charger()
        {
            listeMateriaux.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "Select * from materiel";
                connection.Open();
                MySqlDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    int iCode = (int)reader["code"];
                    string sModele = (string)reader["modele"];
                    string sMeuble = (string)reader["nom_meuble"];
                    string sCategorie = (string)reader["categorie"];
                    string sCouleur = (string)reader["couleur"];
                    double dPrix = (double)reader["prix"];

                    Materiel materiel = new Materiel { Code = iCode, Modele = sModele, Meuble = sMeuble, Categorie = sCategorie, Couleur = sCouleur, Prix = dPrix };
                    listeMateriaux.Add(materiel);
                }

                reader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                connection.Close();
            }
        }
    }
}

