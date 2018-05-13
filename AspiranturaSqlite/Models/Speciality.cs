using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspiranturaSqlite.Models
{
    public class Speciality
    {        
        [Display(Name = "Код")]
        public int Id { get; set; }

        [Display(Name = "Спеціальність")]
        public string Name { get; set; }

        public int KnowledgeId { get; set; }

        public Knowledge Knowledge { get; set; }

        public bool IsUsed { get; set; }

        public ICollection<Aspirant> Aspirants { get; set; }
    }
}
