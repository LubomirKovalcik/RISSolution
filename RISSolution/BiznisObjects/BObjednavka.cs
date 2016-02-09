using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BObjednavka
    {
        public int id_objednavky { get; set; }
        public int id_stola { get; set; }
        public int id_uctu { get; set; }
        public DateTime datum_objednania { get; set; }
        public int potvrdena { get; set; }
        public DateTime datum_zaplatenia { get; set; }
        public double suma { get; set; }

        public BStol stol { get; set; }
        public ICollection<BObjednavka_menu> objednavka_menu { get; set; }
        public BUcet ucet { get; set; }

        public objednavka entityObjednavka;

        public BObjednavka(objednavka o)
        {
            id_objednavky = o.id_objednavky;
            id_stola = o.id_stola;
            id_uctu = o.id_uctu;
            datum_objednania = o.datum_objednania;
            datum_zaplatenia = (DateTime) o.datum_zaplatenia;
            potvrdena = (int) o.potvrdena;
            suma = o.suma;

            stol = new BStol(o.stol);
            ucet = new BUcet(o.ucet);

            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in o.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }
        }

        public BObjednavka()
        {
        }
    }
}
