using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BiznisObjects;
using DatabaseEntities;
using DataBaseExecutor;
using DataHolder;
using DirectCommunication;
using IDatabaseExecutor;

using IServices;


namespace Services
{
    /// <summary>
    ///  Služby pre správu reštauracie
    /// </summary>
    public class ServiceSprava : IServiceSprava
    {
        private IDBDatabaseExecutor aDBExecutor;
        private Sessions sessions;

        /// <summary>
        /// Vytvorí novú službu pre správu reštauracie
        /// </summary>
        /// <param name="sessions">zoznam prihlasení</param>
        public ServiceSprava(Sessions sessions)
        {
            aDBExecutor = new DBDataExecutor();
            this.sessions = sessions;
        }

        public risTabulky risContext
        {
            get { return aDBExecutor.risContext; }
        }

        /// <summary>
        ///   Detailné informácie o jedle s daným ako prenosová entita
        /// </summary>
        /// <param name="id_jedla">identifikátor jedla</param>
        /// <param name="id_jazyka">identifikátor jazyka pre prenosovú entitu</param>
        /// <returns>informácie o jedle</returns>
        public TJedlo jedlo(int id_jedla, String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BJedlo jedlo = Zoznamy.dajJedlo(id_jedla, risContext);
            TJedlo result=new TJedlo(jedlo.ID);
            result.IdTypu = jedlo.typ_jedla.id_typu;
            if (jedlo.dlzka_pripravy.HasValue) result.DlzkaPripravy = jedlo.dlzka_pripravy;
            if (jedlo.mnozstvo_kalorii.HasValue) result.MnozstvoKalorii = jedlo.mnozstvo_kalorii;
            result.Id_jazyka = id_jazyka;
            result.ZoznamSurovin = jedlo.PE_suroviny_jedla(id_jazyka);
            result.Preklady = jedlo.nazov.PrekladyToDictionary();
            return result;
        }


        /// <summary>
        ///  Všetky jedla začínajúce na zadaný reťazec v danom jazyku
        /// </summary>
        /// <param name="startingWith">zadaný reťazec</param>
        /// <param name="id_jazyka">id jazyka</param>
        /// <returns></returns>
        public ICollection<TJedlo> vsetkyJedla(String startingWith, String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BJedlo.BJedloCol result= new BJedlo.BJedloCol(risContext);
            result.GetNameStartingWith(startingWith);
            return (ICollection<TJedlo>) result.toTransferList(id_jazyka);
        }

        /// <summary>
        ///   Aktualizuje alebo prida nove jedlo na základe údajov v prenosovje entite
        /// </summary>
        /// <param name="jedlo">nové jedlo</param>
        /// <returns> <c>TRUE</c> ,ak sa podarilo
        ///         <c>FALSE</cd> , ak sa nepodarilo
        ///  </returns>
        public Boolean jedlo(String session,TJedlo jedlo)
        {
            if (sessions.jeAdmin(session))
            {
                risTabulky risContext = aDBExecutor.risContext;
                BJedlo bjedlo = new BJedlo();
                try
                {
                    bjedlo.updatefromTransferObject(jedlo, risContext);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<TSurovina> surovinyJedla(int id_jedla, String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BJedlo jedlo=Zoznamy.dajJedlo(id_jedla, risContext);
            return jedlo.PE_suroviny_jedla(id_jazyka);
        }

        public IList<TTypJedla> typyJedal(String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BTyp_jedla.BTypJedlaCol kolBTypJedlaCol = new BTyp_jedla.BTypJedlaCol(risContext);
            kolBTypJedlaCol.GetAll();
            return kolBTypJedlaCol.toList(id_jazyka);

        }



    }
}
