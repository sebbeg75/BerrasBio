using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models
{
    /// <summary>
    /// Display class
    /// </summary>
    public class Display
    {
        public int ID { get; set; }

        [DisplayName("Start at")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH\\:mm}")]
        public DateTime StartingTime { get; set; }
        public int MovieID { get; set; }
        public int SalonID { get; set; }

        [DisplayName("Seats left")]
        public int SeatsLeft { get; set; }
        public Movie Movie { get; set; }
        public Salon Salon { get; set; }
    }
}
 