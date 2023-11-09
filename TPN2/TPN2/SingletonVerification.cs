using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPN2
{
    internal class SingletonVerification
    {
        static SingletonVerification instance;

        public SingletonVerification() { }

        public static SingletonVerification getInstance()
        {
            if (instance == null)
                instance = new SingletonVerification();

            return instance;
        }

        /// <summary>
        /// Permet de vérifier si le code est valide et au moins 3 chiffres
        /// </summary>
        /// <param name="code">Code que l'utilisateur entre</param>
        /// <returns></returns>
        public bool isCodeValide(string code)
        {
            int iCode = 0;
            if (int.TryParse(code, out iCode))
            {
                if (code.Length >= 3)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Permet de vérifier si le modèle n'est pas vide
        /// </summary>
        /// <param name="modele">Nom du modèle entré par l'utilisateur</param>
        /// <returns></returns>
        public bool isModeleValide(string modele)
        {
            if (!string.IsNullOrEmpty(modele.Trim()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Permet de vérifier si le nom du meuble n'est pas vide
        /// </summary>
        /// <param name="nomMeuble">Nom du meuble entré par l'utilisateur</param>
        /// <returns></returns>
        public bool isNomMeubleValide(string nomMeuble)
        {
            if (!string.IsNullOrEmpty(nomMeuble.Trim()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Permet de vérifier si une catégorie a été choisi
        /// </summary>
        /// <param name="catIndex">Index de la catégorie choisie</param>
        /// <returns></returns>
        public bool isCategorieValide(int catIndex)
        {
            if (catIndex>=0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Permet de vérifier si une couleur a été entré
        /// </summary>
        /// <param name="couleur">Couleur entrée par l'utilisateur</param>
        /// <returns></returns>
        public bool isCouleurValide(string couleur)
        {
            if (!string.IsNullOrEmpty(couleur.Trim()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Permet de vérifier si un prix a été entré
        /// </summary>
        /// <param name="prix">Prix entré par l'utilisateur</param>
        /// <returns></returns>
        public bool isPrixValide(string prix)
        {
            double dPrix = 0;
            if (double.TryParse(prix, out dPrix))
            {
                if (dPrix > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
