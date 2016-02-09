using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;


namespace BiznisObjects
{

    public class BAkcia
    {
        public int id_akcie { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int text_id { get; set; }
        public DateTime platnost_od { get; set; }
        public DateTime platnost_do { get; set; }
        public int akciova_cena { get; set; }

        public BText text { get; set; }
        public BMenu menu { get; set; }
        public BObrazok obrazok { get; set; }

        public akcia entityAkcia { get; set; }

        public BAkcia()
        {
            this.Reset();
        }

        public BAkcia(akcia akcia)
        {
            id_akcie = akcia.id_akcie;
            id_menu = akcia.id_menu;
            id_podniku = akcia.id_podniku;
            if (akcia.id_obrazka != null) id_obrazka = (int) akcia.id_obrazka;
            if (akcia.text_id != null) text_id = (int) akcia.text_id;
            platnost_od = akcia.platnost_od;
            platnost_do = akcia.platnost_do;
            if (akcia.akciova_cena != null) akciova_cena = (int) akcia.akciova_cena;
            
            text = new BText(akcia.text);
            menu = new BMenu(akcia.menu);
            obrazok = new BObrazok(akcia.obrazok);
            
            entityAkcia = akcia;
        }

        private void Reset()
        {
            id_akcie = 0;
            id_menu = 0;
            id_podniku = 0;
            id_obrazka = 0;
            text_id = 0;
            platnost_od = DateTime.MinValue;
            platnost_do = DateTime.MinValue;
            akciova_cena = 0;
            text = new BText();
            menu = new BMenu();
            obrazok = new BObrazok();

            entityAkcia = new akcia();
        }

        private void FillBObject()
        {
            id_akcie = entityAkcia.id_akcie;
            id_menu = entityAkcia.id_menu;
            id_podniku = entityAkcia.id_podniku;
            if (entityAkcia.id_obrazka != null) id_obrazka = (int)entityAkcia.id_obrazka;
            if (entityAkcia.text_id != null) text_id = (int)entityAkcia.text_id;
            platnost_od = entityAkcia.platnost_od;
            platnost_do = entityAkcia.platnost_do;
            if (entityAkcia.akciova_cena != null) akciova_cena = (int)entityAkcia.akciova_cena;

            text = new BText(entityAkcia.text);
            menu = new BMenu(entityAkcia.menu);
            obrazok = new BObrazok(entityAkcia.obrazok);
        }

        private void FillEntity()
        {
            entityAkcia.id_akcie = id_akcie;
            entityAkcia.id_menu = id_menu;
            entityAkcia.id_podniku = id_podniku;
            entityAkcia.id_obrazka = id_obrazka;
            entityAkcia.text_id = text_id;
            entityAkcia.platnost_od = platnost_od;
            entityAkcia.platnost_do = platnost_do;
            entityAkcia.akciova_cena = akciova_cena;
            entityAkcia.text = text.entityText;
            entityAkcia.menu = menu.entityMenu;
            entityAkcia.obrazok = obrazok.entityObrazok;
        }

        public bool Save(risTabulky risContext)
		{
			bool success = false;

			try
			{
                if (id_akcie == 0) // INSERT
				{
                    this.FillEntity();
                    risContext.akcia.Add(entityAkcia);
                    risContext.SaveChanges();
				    id_akcie = entityAkcia.id_akcie; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;	
				}
				else // UPDATE
				{
                    var temp = from a in risContext.akcia where a.id_akcie == id_akcie select a;
                    entityAkcia = temp.Single();
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
                var temp = risContext.akcia.First(i => i.id_akcie == id_akcie);
                risContext.akcia.Remove(temp);
                risContext.SaveChanges();
                Reset();
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
                var temp = from a in risContext.akcia where a.id_akcie == id select a;
                entityAkcia = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BAkciaCol : Dictionary<int, BAkcia>
        {

            public BAkciaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.akcia select a;
                    List<akcia> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_akcie, new BAkcia(a));
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
