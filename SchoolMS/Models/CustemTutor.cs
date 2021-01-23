using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [MetadataType(typeof(CustemTutor))]
    public partial class Tutor
    {

    }
    public class CustemTutor
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter name")]
        [MinLength(3, ErrorMessage = "invaild name")]
        [MaxLength(20, ErrorMessage = "invaild name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(30, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You must enter salary")]
        [Range(1000, 10000, ErrorMessage = "Wrong Salary")]
        public Nullable<int> Salary { get; set; }
        [Required(ErrorMessage = "Field Cannot be empty")]
        [Range(20, 55, ErrorMessage = "age must be in range of 20 to 55")]
        public Nullable<int> Age { get; set; }
    }
}