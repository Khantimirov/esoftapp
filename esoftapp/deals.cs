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
    
    public partial class deals
    {
        public int id { get; set; }
        public int demand_id { get; set; }
        public int supply_id { get; set; }
    
        public virtual demands demands { get; set; }
        public virtual supplies supplies { get; set; }
    }
}
