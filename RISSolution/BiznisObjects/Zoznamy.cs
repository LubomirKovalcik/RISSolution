using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects

    
{
    public class Zoznamy
    {
        public static BTyp_jedla dajTypJedla(int id, risTabulky risContext)
        {
            return new BTyp_jedla(risContext.typ_jedla.First(p => p.id_typu == id));
        }


        public static BSurovina dajSurovinu(int id, risTabulky risContext)
        {
            return new BSurovina(risContext.surovina.First(p => p.id_surovina == id));
        }

        public static BJedlo dajJedlo(int id, risTabulky risContext)
        {
            return new BJedlo(risContext.jedlo.First(p => p.id_jedla == id));
        }

        public static BUcet dajUcet(String login, risTabulky risContext)
        {
            return new BUcet(risContext.ucet.First(p => p.login==(login)));
        }
    }
}
