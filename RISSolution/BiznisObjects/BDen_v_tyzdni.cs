using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;


namespace BiznisObjects
{
    public class BDen_v_tyzdni
    {
        public int cislo_dna { get; set; }
        public int text_id { get; set; }

        public ICollection<BOtvaracie_hodiny> otvaracie_hodiny { get; set; }
        public ICollection<BZmena_otvaracich_hodin> zmena_otvaracich_hodin { get; set; }

        public BText text { get; set; }

        public den_v_tyzdni entityDenVTyzdni;

        public BDen_v_tyzdni(den_v_tyzdni denVTyzdni)
        {
            cislo_dna = denVTyzdni.cislo_dna;
            text_id = denVTyzdni.text_id;

            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in denVTyzdni.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }
            
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in denVTyzdni.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }

            text = new BText(denVTyzdni.text);

            entityDenVTyzdni = denVTyzdni;
        }

        public BDen_v_tyzdni()
        {
        }
    }
}
