using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class Input
    {
        private static readonly Hashtable hashtable = new Hashtable();
        public static bool BasiliTus(Keys key)
        {
            if (hashtable[key] == null)
            {
                return false;
            }
            return (bool)hashtable[key];
        }

        public static void DurumDegisti(Keys key, bool state)
        {
            hashtable[key] = state;
        }
    }    
}
