using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;
using DataHolder;

namespace BiznisObjects
{
    public class BTyp_jedla
    {
        public int id_typu { get; set; }

        public int typ { get; set; }

        public ICollection<BJedlo> jedlo
        {
            get
            {
                List<BJedlo>  jedlo_temp = new List<BJedlo>();
                foreach (var jedlo1 in entityTypJedla.jedlo)
                {
                    BJedlo pom = new BJedlo(jedlo1);
                    jedlo_temp.Add(pom);
                }
                return jedlo_temp;
            }
            set
            {
                this.jedlo = value;   
            }
        }

        public List<TJedlo> toListJedlo(String id_jazyka)
        {
            List<TJedlo> jedlo_temp = new List<TJedlo>();
            foreach (var jedlo1 in entityTypJedla.jedlo)
            {
                BJedlo jedlo_pom=new BJedlo(jedlo1);
                
                jedlo_temp.Add((TJedlo)jedlo_pom.toTransferObject(id_jazyka));
            }
            return jedlo_temp;
        }

        

        public BText text { get; set; }

        public typ_jedla entityTypJedla;

        public BTyp_jedla(typ_jedla tj)
        {
            id_typu = tj.id_typu;
            typ = tj.typ;
            text = new BText(tj.text);

          
            entityTypJedla = tj;
        }

        public BTyp_jedla()
        {
            this.Reset();
        }




        public void Reset()
        {
            id_typu = -1;
            typ = 0;
            text = new BText();

            jedlo = new List<BJedlo>();
            
            entityTypJedla = new typ_jedla();
        }


        private void FillBObject()
        {
            id_typu = entityTypJedla.id_typu;
            typ = entityTypJedla.typ;
            text = new BText(entityTypJedla.text);

            jedlo = new List<BJedlo>();
          /*  foreach (var jedlo1 in entityTypJedla.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }*/
            

        }


        private void FillEntity()
        {
            entityTypJedla.id_typu = id_typu;
            entityTypJedla.typ = typ;
            entityTypJedla.text = text.entityText;

          /*  foreach (var jedlo1 in jedlo)
            {
                entityTypJedla.jedlo.Add(jedlo1.entityJedlo);
            }*/
       }



        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_typu == -1) // INSERT
                {
                    this.FillEntity();
                    risContext.typ_jedla.Add(entityTypJedla);
                    risContext.SaveChanges();
                    this.FillBObject();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.typ_jedla where a.id_typu == id_typu select a;
                    entityTypJedla = temp.Single();
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
                var temp = risContext.typ_jedla.First(i => i.id_typu == id_typu);
                risContext.typ_jedla.Remove(temp);
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
                var temp = from a in risContext.typ_jedla where a.id_typu == id select a;
                entityTypJedla = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTypJedlaCol : Dictionary<int, BTyp_jedla>
        {
            private risTabulky risContext;

            public BTypJedlaCol(risTabulky risContext)
            {
                this.risContext = risContext;
            }

            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.typ_jedla select a;
                    List<typ_jedla> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_typu, new BTyp_jedla(a));
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }


            public IList<TTypJedla> toList(String id_jazyka)
            {
                IList<TTypJedla> result = new List<TTypJedla>();
                foreach (var typJedla in this)
                {
                    result.Add(new TTypJedla(typJedla.Value.id_typu, typJedla.Value.text.getPreklad(id_jazyka)));
                }
                return result;
            }

        }



    }
}
