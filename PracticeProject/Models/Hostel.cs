using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class Hostel:Common
    {
        public int ID { get; set; }
        public int Emptyseats { get; set; }
        
        public int Totalseats { get; set; }
        [Required]
        public int PAN { get; set; }

        public int StudentID { get; set; }
        public Student? Student { get; set; }

      
    }
}
