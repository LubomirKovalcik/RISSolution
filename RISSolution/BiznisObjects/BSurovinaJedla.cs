using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;
using DataHolder;

namespace BiznisObjects
{
    public class BSurovinaJedla
    {

       
        public int id_surovina { get; set; }
        public int id_jedla { get; set; }
        public int id_typu { get; set; }
        public double mnozstvo { get; set; }

        public BJedlo jedlo { get; set; }
        public BSurovina surovina { get; set; }

        public jedlo_surovina entityJedloSurovina;


        public BSurovinaJedla(BJedlo jedlo, BSurovina surovina, double mnozstvo)
        {
            Reset();
            entityJedloSurovina.id_jedla = jedlo.ID;
            entityJedloSurovina.id_surovina = surovina.ID;
            entityJedloSurovina.mnozstvo = mnozstvo;
        }
       

        public BSurovinaJedla(jedlo_surovina js)
        {
            id_surovina = js.id_surovina;
            id_jedla = js.id_jedla;
            id_typu = js.id_typu;
            mnozstvo = js.mnozstvo;
            
            jedlo = new BJedlo(js.jedlo);
            surovina = new BSurovina(js.surovina);

            entityJedloSurovina = js;
        }

        private void Reset()
        {
            id_surovina = -1;
            id_jedla = -1;
            id_typu = 0;
            mnozstvo = 0;

            jedlo = new BJedlo();
            surovina = new BSurovina();
        }

        private void FillBObject()
        {
            id_jedla = entityJedloSurovina.id_jedla;
            id_surovina = entityJedloSurovina.id_surovina;
            id_typu = entityJedloSurovina.id_typu;

            jedlo = new BJedlo(entityJedloSurovina.jedlo);
            surovina = new BSurovina(entityJedloSurovina.surovina);

        }

        private void FillEntity()
        {
            entityJedloSurovina.id_surovina = id_surovina;
            entityJedloSurovina.id_typu = id_typu;
            entityJedloSurovina.id_jedla = id_jedla;
            entityJedloSurovina.jedlo = jedlo.entity;
            entityJedloSurovina.surovina = surovina.entitySurovina;
        }


        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_jedla == -1 && id_surovina == -1) // INSERT
                {
                    this.FillEntity();
                    risContext.jedlo_surovina.Add(entityJedloSurovina);
                    risContext.SaveChanges();
                    this.FillBObject();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.jedlo_surovina where a.id_jedla == id_jedla && a.id_surovina==id_surovina select a;
                    entityJedloSurovina = temp.Single();
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
                var temp = risContext.jedlo_surovina.First(i => i.id_jedla == id_jedla && i.id_surovina == id_surovina);
                risContext.jedlo_surovina.Remove(temp);
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
                var temp = from a in risContext.jedlo_surovina where a.id_jedla == id_jedla && a.id_surovina == id_surovina select a;
                entityJedloSurovina = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        
    }
}
