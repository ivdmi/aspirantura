using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspiranturaSqlite.Models
{
    public class StatusType
    {
        public int Id { get; set; }

        [Display(Name = "Статус")]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<Aspirant> Aspirants { get; set; }
    }
}
