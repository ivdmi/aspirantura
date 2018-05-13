using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspiranturaSqlite.Models
{
    public class Knowledge
    {
        [Display(Name = "Шифр")]
        public int Id { get; set; }

        [Display(Name = "Галузь знань")]
        public string Name { get; set; }
        
        public ICollection<Speciality> Specialities { get; set; }
    }
}
