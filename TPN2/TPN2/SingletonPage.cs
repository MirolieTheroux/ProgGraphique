using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPN2
{
    internal class SingletonPage
    {
        static SingletonPage instance = null;
        Window fenetre;
        public static SingletonPage getInstance()
        {
            if (instance == null)
                instance = new SingletonPage();

            return instance;
        }

        //Propriété fenetre 
        public Window Fenetre
        {
            get { return fenetre; }
            set { fenetre = value; }
        }
    }
}
