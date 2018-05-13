using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspiranturaSqlite.Models.ViewModels
{
    public class KnowledgeInfoGroup
    {
        [Display(Name = "Шифр")]
        public int Number { get; set; }

        [Display(Name = "Галузь знань")]
        public string Name { get; set; }

        [Display(Name = "Кількість спеціальностей")]
        public int SpecialityCount { get; set; }
    }
}
