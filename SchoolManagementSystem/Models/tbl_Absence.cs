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

    public partial class tbl_Absence
    {
        [Display(Name = "Attendance ID")]
        public int AttendanceID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public Nullable<int> StudID { get; set; }

        [Required]
        [Display(Name = "Grade ID")]
        public Nullable<int> GradeID { get; set; }

        [Display(Name = "Date of Absence")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> AbsenceDate { get; set; }
    
        public virtual tbl_Grade tbl_Grade { get; set; }
        public virtual tbl_Student tbl_Student { get; set; }
    }
}