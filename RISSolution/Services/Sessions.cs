using System;
using System.Collections.Generic;
using System;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.Xml.Serialization;
using System.Xml;
using BiznisObjects;
using DatabaseEntities;

namespace DirectCommunication
{
    /// <summary>
    ///  Zoznám prihlasení
    /// </summary>
    public class Sessions
    {
        
        private readonly int ADMIN = 1;
        private readonly int STOL = 2;

        private risTabulky risContext;

        Dictionary<String,BUcet> prihlasenia =new Dictionary<String,BUcet>() ;

        /// <summary>
        /// Vytvorí nový zoznam prihlasených uživateľov
        /// </summary>
        /// <param name="risContext">kontext databázy</param>
        public Sessions(risTabulky risContext)
        {
            this.risContext = risContext;
        }

        /// <summary>
        ///  Má dané prihlasenie admin prava
        /// </summary>
        /// <param name="session">string prihlasenia</param>
        /// <returns><c>TRUE</c> ,áno ak dané prihlasenie má admin práva
        /// <c>FALSE</c> , nie ak dané prihlasenie nemá admin práva
        /// </returns>
        public Boolean jeAdmin(String session)
        {
            if (prihlasenia[session] != null)
            {
                return (prihlasenia[session].typ_uctu.id == ADMIN);
            }
            throw new SoapException("You are not logged in", SoapException.ClientFaultCode);
        }

        /// <summary>
        ///  Má dané prihlasenie práva stola
        /// </summary>
        /// <param name="session">string prihlasenia</param>
        /// <returns><c>TRUE</c> ,áno ak dané prihlasenie má práva stola
        /// <c>FALSE</c> , nie ak dané prihlasenie nemá práva stola
        /// </returns>
        public Boolean jeStol(String session)
        {
            if (prihlasenia[session] != null)
            {
                return (prihlasenia[session].typ_uctu.id == STOL);
            }
            throw new SoapException("You are not logged in", SoapException.ClientFaultCode);
        }

        /// <summary>
        ///  Prihlas učet
        /// </summary>
        /// <param name="meno">prihlasovací login</param>
        /// <param name="heslo">prihlasovacie heslo</param>
        /// <returns>
        ///    session prihlasenia
        /// </returns>
        public String logIn(String meno, String heslo)
        {
            String hash = GetCrypt(heslo);
            BUcet ucet=Zoznamy.dajUcet(meno,risContext);
            if (ucet != null)
            {
                if (ucet.heslo == hash)
                {
                    string NewID = GenerateSession();
                    prihlasenia.Add(NewID, ucet);
                    return NewID;
                }
                else
                {
                    throw new SoapException("Wrong password or login", SoapException.ClientFaultCode);
                }
            }
            else
            {
                throw new SoapException("Wrong password or login", SoapException.ClientFaultCode);
            }
        }


        private static string GenerateSession()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString + DateTime.Now.Millisecond;
            return GuidString;
        }

        private static string GetCrypt(string text)
        {
            string hash = "";
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
            hash = Encoding.UTF8.GetString(result);
            return hash;
        }

    }
}
