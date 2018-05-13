using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspiranturaSqlite.Models
{
    public class OrderType
    {
        public int Id { get; set; }

        [Display(Name = "Вид наказу")]
        [StringLength(40)]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
