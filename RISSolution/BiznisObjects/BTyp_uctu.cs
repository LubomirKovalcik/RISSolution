using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BTyp_uctu
    {
        public int id { get; set; }
        public string nazov { get; set; }

        public ICollection<BUcet> ucet { get; set; }

        public typ_uctu entityUcet { get; set; }

        public BTyp_uctu()
        {
            this.Reset();
        }

        public BTyp_uctu(typ_uctu tu)
        {
            id = tu.id;
            nazov = tu.nazov;

            ucet = new List<BUcet>();
            foreach (var ucet1 in tu.ucet)
            {
                BUcet pom = new BUcet(ucet1);
                ucet.Add(pom);
            }

            entityUcet = tu;
        }

        private void Reset()
        {
            id = 0;
            nazov = "";

            ucet = new List<BUcet>();

            entityUcet = new typ_uctu();
        }

        private void FillBObject()
        {
            id = entityUcet.id;
            nazov = entityUcet.nazov;

            ucet = new List<BUcet>();
            foreach (var ucet1 in entityUcet.ucet)
            {
                BUcet pom = new BUcet(ucet1);
                ucet.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityUcet.id = id;
            entityUcet.nazov = nazov;

            foreach (var ucet1 in ucet)
            {
                entityUcet.ucet.Add(ucet1.entityUcet);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.typ_uctu.Add(entityUcet);
                    risContext.SaveChanges();
                    id = entityUcet.id;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.typ_uctu where a.id == id select a;
                    entityUcet = temp.Single();
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
                var temp = risContext.typ_uctu.First(i => i.id == id);
                risContext.typ_uctu.Remove(temp);
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

        public bool Get(risTabulky risContext, int idT)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.typ_uctu where a.id == idT select a;
                entityUcet = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BTyp_uctuCol : Dictionary<int, BTyp_uctu>
        {

            public BTyp_uctuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.typ_uctu select a;
                    List<typ_uctu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id, new BTyp_uctu(a));
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
