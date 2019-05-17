//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tbl_User
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string UserRole { get; set; }
    }
}
