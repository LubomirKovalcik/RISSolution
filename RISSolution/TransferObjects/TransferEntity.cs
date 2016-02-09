using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHolder
{
    public abstract class TransferEntity
    {
        String id_jazyka;

        public string Id_jazyka
        {
            get
            {
                return id_jazyka;
            }

            set
            {
                id_jazyka = value;
            }
        }
    }
}
