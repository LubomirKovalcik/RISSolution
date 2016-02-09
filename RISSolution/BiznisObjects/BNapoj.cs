using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BNapoj
    {
        public int id_napoja { get; set; }
        public int nazov { get; set; }
        public int alkoholicky { get; set; }
        public int mnozstvo_kalorii { get; set; }
        public int dlzka_pripravy { get; set; }

        public ICollection<BMenu_napoj> menu_napoj { get; set; }
        public ICollection<BNapoj_surovina> napoj_surovina { get; set; }
        public BText text { get; set; }
        public ICollection<BTyp_napoja> typ_napoja { get; set; }

        public napoj entityNapoj { get; set; }

        public BNapoj()
        {
            this.Reset();
        }

        public BNapoj(napoj n)
        {
            id_napoja = n.id_napoja;
            nazov = n.nazov;
            alkoholicky = n.alkoholicky;
            mnozstvo_kalorii = (int) n.mnozstvo_kalorii;
            dlzka_pripravy = (int) n.dlzka_pripravy;
            text = new BText(n.text);

            menu_napoj = new List<BMenu_napoj>();
            foreach (var menuNapoj in n.menu_napoj)
            {
                BMenu_napoj pom = new BMenu_napoj(menuNapoj);
                menu_napoj.Add(pom);
            }
            napoj_surovina = new List<BNapoj_surovina>();
            foreach (var napojSurovina in n.napoj_surovina)
            {
                BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                napoj_surovina.Add(pom);
            }
            typ_napoja = new List<BTyp_napoja>();
            foreach (var typNapoja in n.typ_napoja)
            {
                BTyp_napoja pom = new BTyp_napoja(typNapoja);
                typ_napoja.Add(pom);
            }

            entityNapoj = n;

        }

        private void Reset()
        {
            id_napoja = 0;
            nazov = 0;
            alkoholicky = 0;
            mnozstvo_kalorii = 0;
            dlzka_pripravy = 0;
            text = new BText();
            menu_napoj = new List<BMenu_napoj>();
            napoj_surovina = new List<BNapoj_surovina>();
            typ_napoja = new List<BTyp_napoja>();
            entityNapoj = new napoj();
        }

        private void FillBObject()
        {
            id_napoja = entityNapoj.id_napoja;
            nazov = entityNapoj.nazov;
            alkoholicky = entityNapoj.alkoholicky;
            mnozstvo_kalorii = (int)entityNapoj.mnozstvo_kalorii;
            dlzka_pripravy = (int)entityNapoj.dlzka_pripravy;
            text = new BText(entityNapoj.text);

            menu_napoj = new List<BMenu_napoj>();
            foreach (var menuNapoj in entityNapoj.menu_napoj)
            {
                BMenu_napoj pom = new BMenu_napoj(menuNapoj);
                menu_napoj.Add(pom);
            }
            napoj_surovina = new List<BNapoj_surovina>();
            foreach (var napojSurovina in entityNapoj.napoj_surovina)
            {
                BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                napoj_surovina.Add(pom);
            }
            typ_napoja = new List<BTyp_napoja>();
            foreach (var typNapoja in entityNapoj.typ_napoja)
            {
                BTyp_napoja pom = new BTyp_napoja(typNapoja);
                typ_napoja.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityNapoj.id_napoja = id_napoja;
            entityNapoj.nazov = nazov;
            entityNapoj.alkoholicky = alkoholicky;
            entityNapoj.mnozstvo_kalorii = mnozstvo_kalorii;
            entityNapoj.dlzka_pripravy = dlzka_pripravy;
            entityNapoj.text = text.entityText;

            foreach (var menuNapoj in menu_napoj)
            {
                entityNapoj.menu_napoj.Add(menuNapoj.entityMenuNapoj);
            }
            foreach (var napojSurovina in napoj_surovina)
            {
                entityNapoj.napoj_surovina.Add(napojSurovina.entityNapojSurovina);
            }
            foreach (var typNapoja in typ_napoja)
            {
                entityNapoj.typ_napoja.Add(typNapoja.entityTypNapoja);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_napoja == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.napoj.Add(entityNapoj);
                    risContext.SaveChanges();
                    id_napoja = entityNapoj.id_napoja;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.napoj where a.id_napoja == id_napoja select a;
                    entityNapoj = temp.Single();
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
                var temp = risContext.napoj.First(i => i.id_napoja == id_napoja);
                risContext.napoj.Remove(temp);
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

        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.napoj where a.id_napoja == id select a;
                entityNapoj = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BNapojCol : Dictionary<int, BNapoj>
        {

            public BNapojCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.napoj select a;
                    List<napoj> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_napoja, new BNapoj(a));
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
