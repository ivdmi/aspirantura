using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspiranturaSqlite.Models.ViewModels
{
    
    public class AssignedAspirantData
    {
        public int Id { get; set; }
                
        [Display(Name = "Ім'я")]        
        public string Name { get; set; }

        [Display(Name = "Прізвище")]
        public string Surename { get; set; }

        [Display(Name = "По батькові")]
        public string Patronymic { get; set; }

        //[Display(Name = "Вступ")]
        //[DataType(DataType.Date)]
        //public DateTime? InputDate { get; set; }

        //[Display(Name = "Дата захисту")]
        //[DataType(DataType.Date)]
        //public DateTime? ProtectionDate { get; set; }

        //[Range(0, 5)]
        //[Display(Name = "Курс")]
        //public int Course { get; set; }

        //[Display(Name = "Статус")]
        //public String StatusName { get; set; }

        public bool Assigned { get; set; }

        [Display(Name = "Стаціонар/Заочна")]
        public bool StationaryForm { get; set; }

        [Display(Name = "Денна/Вечірня")]
        public bool DayForm { get; set; }

        [Display(Name = "Бюджет/Контракт")]
        public bool Budget { get; set; }

        [Display(Name = "Спеціальність")]
        public int SpecialityId { get; set; }

        //[Display(Name = "Захист")]
        //public bool Protection { get; set; }
    }
}
