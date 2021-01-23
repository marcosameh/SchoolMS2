using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SchoolMS.Models
{
    [MetadataType(typeof(CustemAdmin))]

    public partial class Admin
    {

    }
    public class CustemAdmin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter salary")]
        [Range(1000, 10000, ErrorMessage = "Wrong Salary")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "You must enter name")]
        [MinLength(3, ErrorMessage = "invaild name")]
        [MaxLength(20, ErrorMessage = "invaild name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassWord { get; set; }
    }
}