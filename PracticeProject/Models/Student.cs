using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PracticeProject.Models
{
    public class Student:Common
    {
        
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public int Citizenshipnumber { get; set; }

        public string Fathersname { get; set; }
        public string Mothersname { get; set; }
        public string Occupation { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string? ProfileImagePath { get; set; }


        //[Required(ErrorMessage = "Please choose profile image")]
        //public string ProfilePicture { get; set; }
        //[NotMapped]
        //[Required(ErrorMessage = "Please choose profile image")]
        //[Display(Name = "Profile Picture")]
        //public IFormFile? ProfileImage { get; set; }
    }
}
