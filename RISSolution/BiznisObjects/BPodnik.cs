using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BPodnik
    {
        public int id_podniku { get; set; }
        public string nazov { get; set; }
        public int telefon_cislo { get; set; }
        public int ico { get; set; }
        public string adresa { get; set; }

        public virtual List<BMenu> menu { get; set; }
        public virtual List<BOtvaracie_hodiny> otvaracie_hodiny { get; set; }
        public virtual List<BZmena_otvaracich_hodin> zmena_otvaracich_hodin { get; set; }

        public podnik entityPodnik;

        public BPodnik(podnik p)
        {
            id_podniku = p.id_podniku;
            nazov = p.nazov;
            telefon_cislo = p.telefon_cislo;
            ico = p.ico;
            adresa = p.adresa;
            
            menu = new List<BMenu>();
            foreach (var menu1 in p.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in p.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in p.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }
            entityPodnik = p;
        }
    }
}
