using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SchoolMS.Models
{
    [MetadataType(typeof(CustomStudent))]
    public partial class Student
    {

    }
    public class CustomStudent
    {
        
        public int id { get; set; }
        [Required(ErrorMessage = "You must enter name")]
        [MinLength(3, ErrorMessage = "invaild name")]
        [MaxLength(20, ErrorMessage = "invaild name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [Range(20, 55, ErrorMessage = "Wrong Age")]
        public string age { get; set; }
        [Required(ErrorMessage = "Choose Department")]
        public int Department_id { get; set; }

        public virtual Department Department { get; set; }

    }
}