using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BMenu_jedlo
    {
        public int id_jedla { get; set; }
        public int id_menu { get; set; }
        public double cena { get; set; }
        public int id_typu { get; set; }
        public int id_podniku { get; set; }

        public BJedlo jedlo { get; set; }
        public BMenu menu { get; set; }

        public menu_jedlo entityMenuJedlo;

        public BMenu_jedlo(menu_jedlo mj)
        {
            id_jedla = mj.id_jedla;
            id_menu = mj.id_menu;
            cena = mj.cena;
            id_typu = mj.id_typu;
            id_podniku = mj.id_podniku;

            jedlo = new BJedlo(mj.jedlo);
            menu = new BMenu(mj.menu);

            entityMenuJedlo = mj;
        }
    }
}
