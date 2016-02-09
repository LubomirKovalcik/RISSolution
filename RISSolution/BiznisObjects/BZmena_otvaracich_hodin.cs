using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BZmena_otvaracich_hodin
    {
        public DateTime zaciatok_platnosti { get; set; }
        public int id_podniku { get; set; }
        public int cislo_dna { get; set; }
        public DateTime koniec_platnosti { get; set; }
        public DateTime cas_otvorenia { get; set; }
        public int doba_otvorenia { get; set; }

        public BDen_v_tyzdni den_v_tyzdni { get; set; }
        public BPodnik podnik { get; set; }

        public zmena_otvaracich_hodin entityZmenaOtvaracichHodin;

        public BZmena_otvaracich_hodin(zmena_otvaracich_hodin zoh)
        {
            zaciatok_platnosti = zoh.zaciatok_platnosti;
            id_podniku = zoh.id_podniku;
            cislo_dna = zoh.cislo_dna;
            koniec_platnosti = zoh.koniec_platnosti;
            cas_otvorenia = zoh.cas_otvorenia;
            doba_otvorenia = zoh.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(zoh.den_v_tyzdni);
            podnik = new BPodnik(zoh.podnik);
            entityZmenaOtvaracichHodin = zoh;
        }



        public BZmena_otvaracich_hodin()
        {
            this.Reset();
        }


        private void Reset()
        {
            zaciatok_platnosti = DateTime.MinValue;
            id_podniku = 0;
            cislo_dna = 0;
            koniec_platnosti = DateTime.MinValue;
            cas_otvorenia = DateTime.MinValue;
            doba_otvorenia = 0;
            den_v_tyzdni = new BDen_v_tyzdni(null);
            podnik = new BPodnik(null);
            entityZmenaOtvaracichHodin = new zmena_otvaracich_hodin();
        }


        private void FillBObject()
        {
            zaciatok_platnosti = entityZmenaOtvaracichHodin.zaciatok_platnosti;
            id_podniku = entityZmenaOtvaracichHodin.id_podniku;
            cislo_dna = entityZmenaOtvaracichHodin.cislo_dna;
            koniec_platnosti = entityZmenaOtvaracichHodin.koniec_platnosti;
            cas_otvorenia = entityZmenaOtvaracichHodin.zaciatok_platnosti;
            doba_otvorenia = entityZmenaOtvaracichHodin.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(entityZmenaOtvaracichHodin.den_v_tyzdni);
            podnik = new BPodnik(entityZmenaOtvaracichHodin.podnik);
            entityZmenaOtvaracichHodin = new zmena_otvaracich_hodin();
        }


        private void FillEntity()
        {
            entityZmenaOtvaracichHodin.zaciatok_platnosti = zaciatok_platnosti;
            entityZmenaOtvaracichHodin.id_podniku = id_podniku;
            entityZmenaOtvaracichHodin.cislo_dna = cislo_dna;
            entityZmenaOtvaracichHodin.koniec_platnosti = koniec_platnosti;
            entityZmenaOtvaracichHodin.cas_otvorenia = cas_otvorenia;
            entityZmenaOtvaracichHodin.doba_otvorenia = doba_otvorenia;
            entityZmenaOtvaracichHodin.den_v_tyzdni = den_v_tyzdni.entityDenVTyzdni;
            entityZmenaOtvaracichHodin.podnik = podnik.entityPodnik;
            entityZmenaOtvaracichHodin.id_podniku = id_podniku;

        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (zaciatok_platnosti == DateTime.MinValue) // INSERT
                {
                    this.FillEntity();
                    risContext.zmena_otvaracich_hodin.Add(entityZmenaOtvaracichHodin);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.zmena_otvaracich_hodin where a.zaciatok_platnosti == zaciatok_platnosti select a;
                    entityZmenaOtvaracichHodin = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    this.FillBObject();
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
                var temp = risContext.zmena_otvaracich_hodin.First(i => i.zaciatok_platnosti == zaciatok_platnosti);
                risContext.zmena_otvaracich_hodin.Remove(temp);
                risContext.SaveChanges();
                success = true;
                this.Reset();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }



        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.zmena_otvaracich_hodin where a.zaciatok_platnosti == zaciatok_platnosti select a;
                entityZmenaOtvaracichHodin = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }


        public class BZmena_otvaracich_hodinCol : Dictionary<DateTime, BZmena_otvaracich_hodin>
        {

            public BZmena_otvaracich_hodinCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.zmena_otvaracich_hodin select a;
                    List<zmena_otvaracich_hodin> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.zaciatok_platnosti, new BZmena_otvaracich_hodin(a));
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
