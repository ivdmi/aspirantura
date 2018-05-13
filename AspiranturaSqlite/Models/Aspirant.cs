using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspiranturaSqlite.Models
{
    public class Aspirant
    {
        public int Id { get; set; }

        [Display(Name = "Ім'я")]
        [Required]
        [RegularExpression(@"^[а-яА-ЯЇїІіЄє'a-zA-Z]+$", ErrorMessage = "Дозволяються тільки літери")]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Прізвище")]
        [Required]
        [StringLength(30, ErrorMessage = "Введіть не більше ніж 30 символів")]
        public string Surename { get; set; }

        [Display(Name = "По батькові")]
        public string Patronymic { get; set; }

        [Display(Name = "Народження")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Вступ")]
        public DateTime? InputDate { get; set; }

        [Display(Name = "Дата захисту")]
        [DataType(DataType.Date)]
        public DateTime? ProtectionDate { get; set; }

        //[Display(Name = "Стаціонар/Заочна")]
        //public bool? StationaryForm { get; set; }

        //[Display(Name = "Денна/Вечірня")]
        //public bool? DayForm { get; set; }

        //[Display(Name = "Бюджет/Контракт")]
        //public bool? Budget { get; set; }

        [Display(Name = "Докторант/Аспірант")]
        public bool Doctorant { get; set; }

        [Display(Name = "Захист")]
        public bool Protection { get; set; }
        
        [Range(0, 5)]
        [Display(Name = "Курс")]
        public int Course { get; set; }

        public int StatustypeId { get; set; }

        [Display(Name = "Спеціальність")]
        public int SpecialityId { get; set; }

        public Speciality Speciality { get; set; }
        
        [Display(Name = "Статус")]
        public StatusType StatusType { get; set; }

        public ICollection<AspirantOrder> AsppirantOrders { get; set; }

        [Display(Name = "Вступ")]
        public int? InputYear
        {
            get
            {
                if (InputDate != null)
                {
                    return InputDate.Value.Year;
                }
                else
                {
                    return null;
                }
            }
        }

        public string FIO
        {
            get
            {
                StringBuilder sb = new StringBuilder(Surename + " " + Name.Substring(0, 1) + ".");
                if (!String.IsNullOrEmpty(Patronymic))
                    sb.Append(Patronymic.Substring(0, 1)).Append(".");
                return sb.ToString();
            }
        }
    }
}