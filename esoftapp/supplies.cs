//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace esoftapp
{
    using System;
    using System.Collections.Generic;
    
    public partial class supplies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplies()
        {
            this.deals = new HashSet<deals>();
        }
    
        public int id { get; set; }
        public int client_id { get; set; }
        public int agent_id { get; set; }
        public int realestate_id { get; set; }
        public Nullable<int> price { get; set; }
    
        public virtual agents agents { get; set; }
        public virtual clients clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<deals> deals { get; set; }
        public virtual realestate realestate { get; set; }
    }
}