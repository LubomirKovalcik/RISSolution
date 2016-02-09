using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BOtvaracie_hodiny
    {
        private otvaracie_hodiny otvaracieHodiny;

        public int id_podniku { get; set; }
        public int cislo_dna { get; set; }
        public DateTime cas_otvorenia { get; set; }
        public int doba_otvorenia { get; set; }

        public BDen_v_tyzdni den_v_tyzdni { get; set; }
        public BPodnik podnik { get; set; }

        public otvaracie_hodiny entityOtvaracieHodiny { get; set; }

        public BOtvaracie_hodiny()
        {
            this.Reset();
        }

        public BOtvaracie_hodiny(otvaracie_hodiny oh)
        {
            id_podniku = oh.id_podniku;
            cislo_dna = oh.cislo_dna;
            cas_otvorenia = oh.cas_otvorenia;
            doba_otvorenia = oh.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(oh.den_v_tyzdni);
            this.podnik = new BPodnik(oh.podnik);
            entityOtvaracieHodiny = oh;
        }

       

        private void Reset()
        {
            id_podniku = 0;
            cislo_dna = 0;
            cas_otvorenia = DateTime.MinValue;
            doba_otvorenia = 0;
            den_v_tyzdni = new BDen_v_tyzdni();
            this.podnik = new BPodnik(new podnik());
            entityOtvaracieHodiny = new otvaracie_hodiny();
        }

        private void FillBObject()
        {
            id_podniku = entityOtvaracieHodiny.id_podniku;
            cislo_dna = entityOtvaracieHodiny.cislo_dna;
            cas_otvorenia = entityOtvaracieHodiny.cas_otvorenia;
            doba_otvorenia = entityOtvaracieHodiny.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(entityOtvaracieHodiny.den_v_tyzdni);
            this.podnik = new BPodnik(entityOtvaracieHodiny.podnik);
        }

        private void FillEntity()
        {
            entityOtvaracieHodiny.id_podniku = id_podniku;
            entityOtvaracieHodiny.cislo_dna = cislo_dna;
            entityOtvaracieHodiny.cas_otvorenia = cas_otvorenia;
            entityOtvaracieHodiny.doba_otvorenia = doba_otvorenia;
            entityOtvaracieHodiny.den_v_tyzdni = den_v_tyzdni.entityDenVTyzdni;
            entityOtvaracieHodiny.podnik = podnik.entityPodnik;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.otvaracie_hodiny where a.id_podniku == id_podniku &&
                               a.cislo_dna == cislo_dna select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.otvaracie_hodiny.Add(entityOtvaracieHodiny);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityOtvaracieHodiny = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.otvaracie_hodiny.First(i => i.id_podniku == id_podniku && i.cislo_dna == cislo_dna);
                risContext.otvaracie_hodiny.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, int idPodniku, int cisloDna)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.otvaracie_hodiny where a.id_podniku == idPodniku &&
                           a.cislo_dna == cisloDna select a;
                entityOtvaracieHodiny = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BOtvaracie_hodinyCol : Dictionary<string, BOtvaracie_hodiny>
        {

            public BOtvaracie_hodinyCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.otvaracie_hodiny select a;
                    List<otvaracie_hodiny> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_podniku + "," + a.cislo_dna, new BOtvaracie_hodiny(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
