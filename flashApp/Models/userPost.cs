//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace flashApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public userPost()
        {
            this.postResponses = new HashSet<postResponse>();
        }
    
        public long postId { get; set; }
        public string userName { get; set; }
        public string caption { get; set; }
        public string contentIMG { get; set; }
        public Nullable<System.DateTime> postTime { get; set; }
    
        public virtual appUser appUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<postResponse> postResponses { get; set; }
    }
}
