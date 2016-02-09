using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace BiznisObjects
{
    public class BPreklad
    {
        public string kod_jazyka { get; set; }
        public int text_id { get; set; }
        public string preklad1 { get; set; }

        public BJazyk jazyk { get; set; }
        public BText text { get; set; }

        public preklad entityPreklad { get; set; }

        public BPreklad()
        {
            this.Reset();
        }

        public BPreklad(preklad p)
        {
            kod_jazyka = p.kod_jazyka;
            text_id = p.text_id;
            preklad1 = p.preklad1;

            jazyk = new BJazyk(p.jazyk);
            text = new BText(p.text);

            entityPreklad = p;
        }

        public void Reset()
        {
            kod_jazyka = "";
            text_id = 0;
            preklad1 = "";

            jazyk = new BJazyk();
            text = new BText();

            entityPreklad = new preklad();
        }

        private void FillBObject()
        {
            kod_jazyka = entityPreklad.kod_jazyka;
            text_id = entityPreklad.text_id;
            preklad1 = entityPreklad.preklad1;

            jazyk = new BJazyk(entityPreklad.jazyk);
            text = new BText(entityPreklad.text);
        }

        private void FillEntity()
        {
            entityPreklad.kod_jazyka = kod_jazyka;
            entityPreklad.text_id = text_id;
            entityPreklad.preklad1 = preklad1;

            entityPreklad.jazyk = jazyk.entityJazyk;
            entityPreklad.text = text.entityText;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.preklad where a.kod_jazyka == kod_jazyka &&
                           a.text_id == text_id select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.preklad.Add(entityPreklad);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityPreklad = temp.Single();
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
                var temp = risContext.preklad.First(i => i.kod_jazyka == kod_jazyka && i.text_id == text_id);
                risContext.preklad.Remove(temp);
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

        public bool Get(risTabulky risContext, string kod, int idText)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.preklad where a.kod_jazyka == kod && a.text_id == text_id select a;
                entityPreklad = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BPrekladCol : Dictionary<string, BPreklad>
        {

            public BPrekladCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.preklad select a;
                    List<preklad> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.kod_jazyka + "," + a.text_id, new BPreklad(a));
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
