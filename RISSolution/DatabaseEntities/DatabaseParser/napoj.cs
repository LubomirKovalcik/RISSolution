//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class napoj
    {
        public napoj()
        {
            this.menu_napoj = new HashSet<menu_napoj>();
            this.napoj_surovina = new HashSet<napoj_surovina>();
            this.typ_napoja = new HashSet<typ_napoja>();
        }
    
        public int id_napoja { get; set; }
        public int nazov { get; set; }
        public int alkoholicky { get; set; }
        public Nullable<int> mnozstvo_kalorii { get; set; }
        public Nullable<int> dlzka_pripravy { get; set; }
    
        public virtual ICollection<menu_napoj> menu_napoj { get; set; }
        public virtual ICollection<napoj_surovina> napoj_surovina { get; set; }
        public virtual text text { get; set; }
        public virtual ICollection<typ_napoja> typ_napoja { get; set; }
    }
}
