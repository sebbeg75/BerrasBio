using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerrasBio.Models
{
    /// <summary>
    /// Movie class
    /// </summary>
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Price")]
        public decimal MoviePrice { get; set; }
    }
}
