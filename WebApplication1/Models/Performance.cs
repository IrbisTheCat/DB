//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Performance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Performance()
        {
            this.ShowLists = new HashSet<ShowList>();
            this.PerformanceCommentThreads = new HashSet<PerformanceCommentThread>();
            this.SetLists = new HashSet<SetList>();
        }
    
        public int Id { get; set; }
        public long BandId { get; set; }
        public long LocationId { get; set; }
        public Nullable<long> SetListId { get; set; }
        public Nullable<System.TimeSpan> Duriation { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
    
        public virtual Band Band { get; set; }
        public virtual Location Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShowList> ShowLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerformanceCommentThread> PerformanceCommentThreads { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SetList> SetLists { get; set; }
    }
}
