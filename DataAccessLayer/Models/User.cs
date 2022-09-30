//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Messages = new HashSet<Message>();
            this.Requests = new HashSet<Request>();
        }
    
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Surname { get; set; }
        public string User_Password { get; set; }
        public string User_Mail { get; set; }
        public string User_Phone { get; set; }
        public Nullable<int> Department_Id { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public int User_Group_ID { get; set; }
        public string Attached_Person { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string User_Photo { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
