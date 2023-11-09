using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ExerciceMaisons
{
    internal class SingletonMaison
    {
        static SingletonMaison instance = null;
        MySqlConnection connection;
        ObservableCollection<Maison> listeMaisons;

        //constructeur de la classe
        public SingletonMaison()
        {
            connection = new MySqlConnection("Server=cours.cegep3r.info;Database=1468780-mirolie-théroux;Uid=1468780;Pwd=1468780;");
            listeMaisons = new ObservableCollection<Maison>();
            charger();
        }

        //retourne l’instance du singleton
        public static SingletonMaison getInstance()
        {
            if (instance == null)
                instance = new SingletonMaison();

            return instance;
        }

        /// <summary>
        /// Permet d'afficher toutes les maisons qui sont dans la BD
        /// </summary>
        /// <returns>liste des maisons qui sont dans la bd</returns>
        public ObservableCollection<Maison> getListeMaisons()
        {
            return listeMaisons;
        }

        /// <summary>
        /// Permet d'afficher les maisons selon le type recherché
        /// </summary>
        /// <param name="categorieMaison">Type de maison</param>
        /// <returns>les maisons selon la categorie</returns>
        public ObservableCollection<Maison> GetMaisonParCategorie(string categorieMaison)
        {
            listeMaisons.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "Select * from maison where categorie = @categorieMaison";
                connection.Open();

                commande.Parameters.AddWithValue("@categorieMaison", categorieMaison);

                commande.Prepare();

                MySqlDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    string categorie = (string)reader["categorie"];
                    double prix = (double)reader["prix"];
                    string ville = (string)reader["ville"];
                    int iId = (int)reader["id"];


                    Maison maison = new Maison { id = iId, Categorie = categorie, Prix = prix, Ville = ville };
                    listeMaisons.Add(maison);
                }

                reader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                connection.Close();
            }

            return listeMaisons;
        }

        /// <summary>
        /// Permet d'afficher les maisons selon la ville
        /// </summary>
        /// <param name="villeMaison">Nom de la ville</param>
        /// <returns></returns>
        public ObservableCollection<Maison> GetMaisonParVille(string villeMaison)
        {
            listeMaisons.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = connection;
                commande.CommandText = "Select * from maison where ville like @villeMaison";
                connection.Open();

                commande.Parameters.AddWithValue("@villeMaison", "%" + villeMaison + "%");

                commande.Prepare();

                MySqlDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    string categorie = (string)reader["categorie"];
                    double prix = (double)reader["prix"];
                    string ville = (string)reader["ville"];
                    int iId = (int)reader["id"];


                    Maison maison = new Maison { id = iId, Categorie = categorie, Prix = prix, Ville = ville };
                    listeMaisons.Add(maison);
                }

                reader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                connection.Close();
            }

            return listeMaisons;
        }

        /// <summary>
        /// Permet d'ajouter dans la BD
        /// </summary>
        /// <param name="cat">Type de la maison</param>
        /// <param name="prix">Prix de la maison</param>
        /// <param name="ville">Nom de la ville</param>
        public void ajouterMaisonBD(string cat, double prix, string ville, string nom, string prenom)
        {
            try
            {
                //appel de la procédure stockées (plus de requête SQL)
                MySqlCommand commande = new MySqlCommand("p_ajout_maison");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("categorieM", cat);
                commande.Parameters.AddWithValue("prixM", prix);
                commande.Parameters.AddWithValue("villeM", ville);
                commande.Parameters.AddWithValue("nom", nom);
                commande.Parameters.AddWithValue("prenom", prenom);

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
        /// Permet d'avoir la liste à jour à partir des données de la BD
        /// </summary>
        public void charger()
        {
            listeMaisons.Clear();
            try
            {
                //appel de la procédure stockées (plus de requête SQL)
                MySqlCommand commande = new MySqlCommand("p_get_maison");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                MySqlDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    string categorie = (string)reader["categorie"];
                    double prix = (double)reader["prix"];
                    string ville = (string)reader["ville"];
                    int iId = (int)reader["id"];

                    Maison maison = new Maison { id = iId, Categorie = categorie, Prix = prix, Ville = ville };
                    listeMaisons.Add(maison);
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
