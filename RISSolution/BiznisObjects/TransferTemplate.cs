using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;
using DataHolder;

namespace BiznisObjects
{
    /// <summary>
    /// Rozhranie pre prenosovú entitu
    /// </summary>
    public interface TransferTemplate
    {
        /// <summary>
        ///   Skonvertuje biznis objekt do odpovedajúceho prenosového objektu s čo najmenším počtom dát
        /// </summary>
        /// <param name="id_jazyka">id jazyka v ktorom mmajú byť texty prenosovej entity</param>
        /// <returns></returns>
         TransferEntity toTransferObject(String id_jazyka);

        /// <summary>
        ///   Aktualizuje biznis objekt a príslušný záznam v databáze na základe dát z prenosovej entity
        /// </summary>
        /// <param name="transferEntity">prenosová entita odpovedajúceho typu</param>
        /// <param name="risContext">kontext databázy</param>
        void updatefromTransferObject(TransferEntity transferEntity,risTabulky risContext);
    }

    /// <summary>
    /// Rozhranie pre zoznam prenosových entít
    /// </summary>
    public interface TransferTemplateList
    {
        /// <summary>
        ///   Skonvertuje kolekciu biznis objektov do odpovedajúcich prenosových objektov s čo najmenším množstvom dát
        /// </summary>
        /// <param name="id_jazyka">id jazyka v ktorom je prenosový objekt</param>
        /// <returns>kolekcia prenosových objektov</returns>
        IList<TransferEntity> toTransferList(String id_jazyka);
    }

}
