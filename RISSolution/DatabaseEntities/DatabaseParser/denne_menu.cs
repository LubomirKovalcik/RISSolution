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
    
    public partial class denne_menu
    {
        public System.DateTime datum_platnosti { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public Nullable<int> id_obrazka { get; set; }
        public int cena { get; set; }
    
        public virtual menu menu { get; set; }
        public virtual obrazok obrazok { get; set; }
    }
}
