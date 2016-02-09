using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BUcet
    {
        public int id_uctu { get; set; }
        public int id { get; set; }
        public string login { get; set; }
        public string heslo { get; set; }

        public ICollection<BObjednavka> objednavka { get; set; }
        public BTyp_uctu typ_uctu { get; set; }

        public ucet entityUcet { get; set; }

        public BUcet()
        {
            this.Reset();
        }

        public BUcet(ucet u)
        {
            id_uctu = u.id_uctu;
            id = u.id;
            login = u.login;
            heslo = u.heslo;
            typ_uctu = new BTyp_uctu(u.typ_uctu);

            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in u.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
            entityUcet = u;
        }

        private void Reset()
        {
            id_uctu = 0;
            id = 0;
            login = "";
            heslo = "";
            typ_uctu = new BTyp_uctu();

            objednavka = new List<BObjednavka>();
            entityUcet = new ucet();
        }

        private void FillBObject()
        {
            id_uctu = entityUcet.id_uctu;
            id = entityUcet.id;
            login = entityUcet.login;
            heslo = entityUcet.heslo;
            typ_uctu = new BTyp_uctu(entityUcet.typ_uctu);

            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in entityUcet.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityUcet.id_uctu = entityUcet.id_uctu;
            entityUcet.id = entityUcet.id;
            entityUcet.login = entityUcet.login;
            entityUcet.heslo = entityUcet.heslo;
            entityUcet.typ_uctu = typ_uctu.entityUcet;

            foreach (var objednavka1 in objednavka)
            {
                entityUcet.objednavka.Add(objednavka1.entityObjednavka);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_uctu == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.ucet.Add(entityUcet);
                    risContext.SaveChanges();
                    id_uctu = entityUcet.id_uctu;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.ucet where a.id_uctu == id_uctu select a;
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
                var temp = risContext.ucet.First(i => i.id_uctu == id_uctu);
                risContext.ucet.Remove(temp);
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

        public bool Get(risTabulky risContext, int idU)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.ucet where a.id_uctu == idU select a;
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

        public class BUcetCol : Dictionary<int, BUcet>
        {

            public BUcetCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.ucet select a;
                    List<ucet> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_uctu, new BUcet(a));
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
