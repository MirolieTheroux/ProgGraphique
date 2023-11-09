using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciceMaisons
{
    internal class SingletonProprietaire
    {
        static SingletonProprietaire instance = null;
        MySqlConnection connection;
        ObservableCollection<Proprietaire> listeProprietaires;

        public SingletonProprietaire()
        {
            connection = new MySqlConnection("Server=cours.cegep3r.info;Database=1468780-mirolie-théroux;Uid=1468780;Pwd=1468780;");
            listeProprietaires = new ObservableCollection<Proprietaire>();
            charger();
        }

        //retourne l’instance du singleton
        public static SingletonProprietaire getInstance()
        {
            if (instance == null)
                instance = new SingletonProprietaire();

            return instance;
        }

        /// <summary>
        /// Permet d'afficher toutes les maisons qui sont dans la BD
        /// </summary>
        /// <returns>liste des maisons qui sont dans la bd</returns>
        public ObservableCollection<Proprietaire> getListeProprietaires()
        {
            return listeProprietaires;
        }

        public void ajouterProprietaire(string nom, string prenom)
        {
            try
            {
                //appel de la procédure stockées (plus de requête SQL)
                MySqlCommand commande = new MySqlCommand("p_ajout_proprietaire");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("nom", nom);
                commande.Parameters.AddWithValue("prenom", prenom);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                connection.Close();
            }
        }
         
        public void afficherMaisonsDesProprio(int idP)
        {
            try
            {
                //appel de la procédure stockées (plus de requête SQL)
                MySqlCommand commande = new MySqlCommand("p_ajout_maisons_proprietaire");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("idP", idP);

                connection.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Permet d'avoir la liste à jour à partir des données de la BD
        /// </summary>
        public void charger()
        {
            listeProprietaires.Clear();
            try
            {
                //appel de la procédure stockées (plus de requête SQL)
                MySqlCommand commande = new MySqlCommand("p_get_proprietaires");
                commande.Connection = connection;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                MySqlDataReader reader = commande.ExecuteReader();
                while (reader.Read())
                {
                    string sNom = (string)reader["nom_proprio"];
                    string sPrenom = (string)reader["prenom_proprio"];
                    int iId = (int)reader["id_proprio"];
                    
                    //Pourquoi ne retourne pas les propriétés
                    Proprietaire proprietaire = new Proprietaire { id = iId, Nom = sNom, Prenom = sPrenom };
                    listeProprietaires.Add(proprietaire);
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
