//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zero_Hunger.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public request()
        {
            this.foods = new HashSet<food>();
        }
    
        public int id { get; set; }
        public string status { get; set; }
        public System.DateTime order_datetime { get; set; }
        public System.DateTime expire_datetime { get; set; }
        public int total_quantity { get; set; }
        public Nullable<int> restaurant_id { get; set; }
        public Nullable<int> employee_id { get; set; }
        public Nullable<int> admin_id { get; set; }
    
        public virtual admin admin { get; set; }
        public virtual employee employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<food> foods { get; set; }
        public virtual restaurant restaurant { get; set; }
    }
}
