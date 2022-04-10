using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerrasBio.Models
{
    /// <summary>
    /// Salon class
    /// </summary>
    public class Salon
    {
        public int ID { get; set; }

        [DisplayName("Seats")]
        public int TotalSeats { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
