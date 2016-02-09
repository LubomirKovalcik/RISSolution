using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BText
    {
        public int text_id { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDen_v_tyzdni> den_v_tyzdni { get; set; }
        public ICollection<BJedlo> jedlo { get; set; }
        public ICollection<BMenu> menu { get; set; }
        public ICollection<BMenu> menu1 { get; set; }
        public ICollection<BNapoj> napoj { get; set; }
        public ICollection<BPreklad> preklad { get; set; }
        public ICollection<BSurovina> surovina { get; set; }
        public ICollection<BTyp_jedla> typ_jedla { get; set; }
        public ICollection<BTyp_napoja> typ_napoja { get; set; }

        public text entityText { get; set; }

        public String getPreklad(String kodJazyka)
        {
            IEnumerable<string> preklad=from a in entityText.preklad.OfType<preklad>() where a.kod_jazyka == kodJazyka select a.preklad1;
            return preklad.FirstOrDefault();
        }

        public BText()
        {
            this.Reset();
        }
   
   
        public BText(text t)
        {
            text_id = t.text_id;
           
            entityText = t;
        }

        private void Reset()
        {
            text_id = entityText.text_id;
            akcia = new List<BAkcia>();
            den_v_tyzdni = new List<BDen_v_tyzdni>();
            jedlo = new List<BJedlo>();
            menu = new List<BMenu>();
            menu1 = new List<BMenu>();
            napoj = new List<BNapoj>();
            preklad = new List<BPreklad>();
            surovina = new List<BSurovina>();
            typ_jedla = new List<BTyp_jedla>();
            typ_napoja = new List<BTyp_napoja>();

            entityText = new text();
        }

        private void FillBObject()
        {
            text_id = entityText.text_id;
            akcia = new List<BAkcia>();
            foreach (var akcia1 in entityText.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            den_v_tyzdni = new List<BDen_v_tyzdni>();
            foreach (var denVTyzdni in entityText.den_v_tyzdni)
            {
                BDen_v_tyzdni pom = new BDen_v_tyzdni(denVTyzdni);
                den_v_tyzdni.Add(pom);
            }
            jedlo = new List<BJedlo>();
            foreach (var jedlo1 in entityText.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }
            menu = new List<BMenu>();
            foreach (var menu2 in entityText.menu)
            {
                BMenu pom = new BMenu(menu2);
                menu.Add(pom);
            }
            menu1 = new List<BMenu>();
            foreach (var menu2 in entityText.menu1)
            {
                BMenu pom = new BMenu(menu2);
                menu1.Add(pom);
            }
            napoj = new List<BNapoj>();
            foreach (var napoj1 in entityText.napoj)
            {
                BNapoj pom = new BNapoj(napoj1);
                napoj.Add(pom);
            }
            preklad = new List<BPreklad>();
            foreach (var preklad1 in entityText.preklad)
            {
                BPreklad pom = new BPreklad(preklad1);
                preklad.Add(pom);
            }
            surovina = new List<BSurovina>();
            foreach (var surovina1 in entityText.surovina)
            {
                BSurovina pom = new BSurovina(surovina1);
                surovina.Add(pom);
            }
            typ_jedla = new List<BTyp_jedla>();
            foreach (var typJedla in entityText.typ_jedla)
            {
                BTyp_jedla pom = new BTyp_jedla(typJedla);
                typ_jedla.Add(pom);
            }
            typ_napoja = new List<BTyp_napoja>();
            foreach (var typNapoja in entityText.typ_napoja)
            {
                BTyp_napoja pom = new BTyp_napoja(typNapoja);
                typ_napoja.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityText.text_id = entityText.text_id;
            foreach (var akcia1 in akcia)
            {
                entityText.akcia.Add(akcia1.entityAkcia);
            }
            foreach (var denVTyzdni in den_v_tyzdni)
            {
                entityText.den_v_tyzdni.Add(denVTyzdni.entityDenVTyzdni);
            }
            foreach (var jedlo1 in jedlo)
            {
                entityText.jedlo.Add(jedlo1.entity);
            }
            foreach (var menu2 in menu)
            {
                entityText.menu.Add(menu2.entityMenu);
            }
            foreach (var menu2 in menu1)
            {
                entityText.menu1.Add(menu2.entityMenu);
            }
            foreach (var napoj1 in napoj)
            {
                entityText.napoj.Add(napoj1.entityNapoj);
            }
            foreach (var preklad1 in preklad)
            {
                entityText.preklad.Add(preklad1.entityPreklad);
            }
            foreach (var surovina1 in surovina)
            {
                entityText.surovina.Add(surovina1.entitySurovina);
            }
            foreach (var typJedla in typ_jedla)
            {
                entityText.typ_jedla.Add(typJedla.entityTypJedla);
            }
            foreach (var typNapoja in typ_napoja)
            {
                entityText.typ_napoja.Add(typNapoja.entityTypNapoja);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (text_id == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.text.Add(entityText);
                    risContext.SaveChanges();
                    text_id = entityText.text_id; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.text where a.text_id == text_id select a;
                    entityText = temp.Single();
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
                var temp = risContext.text.First(i => i.text_id == text_id);
                risContext.text.Remove(temp);
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
                var temp = from a in risContext.text where a.text_id == id select a;
                entityText = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }


        public Dictionary<String, String> PrekladyToDictionary()
        {
            Dictionary<String, String> result=new Dictionary<string, string>();
            foreach (var preklad1 in entityText.preklad)
            {
                result.Add(preklad1.kod_jazyka, preklad1.preklad1);
            }
            return result;
        }

        public class BTextCol : Dictionary<int, BText>
        {

            public BTextCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.text select a;
                    List<text> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.text_id, new BText(a));
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
