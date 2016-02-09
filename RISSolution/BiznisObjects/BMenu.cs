using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BMenu
    {
        public int text_id { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int nazov { get; set; }
        public int typ_platnosti { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDenne_menu> denne_menu { get; set; }
        public ICollection<BMenu_jedlo> menu_jedlo { get; set; }
        public ICollection<BMenu_napoj> menu_napoj { get; set; }
        public ICollection<BObjednavka_menu> objednavka_menu { get; set; }
        
        public BPodnik podnik { get; set; }
        public BObrazok obrazok { get; set; }
        public BText text { get; set; }
        public BText text1 { get; set; }
        public BPlatnost_zaznamu platnost_zaznamu { get; set; }

        public menu entityMenu { get; set; }

        public BMenu()
        {
            this.Reset();
        }

        public BMenu(menu m)
        {
            text_id = (int) m.text_id;
            id_menu = m.id_menu;
            id_podniku = m.id_podniku;
            id_obrazka = (int) m.id_obrazka;
            nazov = (int) m.nazov;
            typ_platnosti = (int) m.typ_platnosti;
            entityMenu = m;
            naplnListy();

            podnik = new BPodnik(m.podnik);
            obrazok = new BObrazok(m.obrazok);
            text = new BText(m.text);
            text1 = new BText(m.text1);
            platnost_zaznamu = new BPlatnost_zaznamu(m.platnost_zaznamu);
        }

        private void naplnListy()
        {
            akcia = new List<BAkcia>();
            foreach (var akcia1 in entityMenu.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            denne_menu = new List<BDenne_menu>();
            foreach (var denneMenu in entityMenu.denne_menu)
            {
                BDenne_menu pom = new BDenne_menu(denneMenu);
                denne_menu.Add(pom);
            }
            menu_jedlo = new List<BMenu_jedlo>();
            foreach (var menuJedlo in entityMenu.menu_jedlo)
            {
                BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                menu_jedlo.Add(pom);
            }
            menu_napoj = new List<BMenu_napoj>();
            foreach (var menuNapoj in entityMenu.menu_napoj)
            {
                BMenu_napoj pom = new BMenu_napoj(menuNapoj);
                menu_napoj.Add(pom);
            }
            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in entityMenu.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }
        }

        private void Reset()
        {
            text_id = 0;
            id_menu = 0;
            id_podniku = 0;
            id_obrazka = 0;
            nazov = 0;
            typ_platnosti = 0;
            entityMenu = new menu();
            naplnListy();

            podnik = new BPodnik(new podnik());
            obrazok = new BObrazok();
            text = new BText();
            text1 = new BText();
            platnost_zaznamu = new BPlatnost_zaznamu(new platnost_zaznamu());
        }

        private void FillBObject()
        {
            text_id = (int)entityMenu.text_id;
            id_menu = entityMenu.id_menu;
            id_podniku = entityMenu.id_podniku;
            id_obrazka = (int)entityMenu.id_obrazka;
            nazov = (int)entityMenu.nazov;
            typ_platnosti = entityMenu.typ_platnosti.Value;

            podnik = new BPodnik(entityMenu.podnik);
            obrazok = new BObrazok(entityMenu.obrazok);
            text = new BText(entityMenu.text);
            text1 = new BText(entityMenu.text1);
            platnost_zaznamu = new BPlatnost_zaznamu(entityMenu.platnost_zaznamu);
        }

        private void FillEntity()
        {
            entityMenu.text_id = text_id;
            entityMenu.id_menu = id_menu;
            entityMenu.id_podniku = id_podniku;
            entityMenu.id_obrazka = id_obrazka;
            entityMenu.nazov = nazov;
            entityMenu.typ_platnosti = typ_platnosti;

            entityMenu.podnik = podnik.entityPodnik;
            entityMenu.obrazok = obrazok.entityObrazok;
            entityMenu.text = text.entityText;
            entityMenu.text1 = text1.entityText;
            entityMenu.platnost_zaznamu = platnost_zaznamu.entityPlatnostZaznamu;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_menu == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.menu.Add(entityMenu);
                    risContext.SaveChanges();
                    id_menu = entityMenu.id_menu;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.menu where a.id_menu == id_menu 
                                   && a.id_podniku == id_podniku select a;
                    entityMenu = temp.Single();
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
                var temp = risContext.menu.First(i => i.id_menu == id_menu);
                risContext.menu.Remove(temp);
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

        public bool Get(risTabulky risContext, int idPodniku, int idMenu)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.menu where a.id_menu == idMenu &&
                           id_podniku == idPodniku select a;
                entityMenu = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BMenuCol : Dictionary<string, BMenu>
        {

            public BMenuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.menu select a;
                    List<menu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu + "," + a.id_podniku, new BMenu(a));
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
