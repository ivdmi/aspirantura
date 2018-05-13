using System.ComponentModel.DataAnnotations;

// таблица соединения многие ко многим для табл. Aspirant и Order
namespace AspiranturaSqlite.Models
{
    public class AspirantOrder
    {
        public int AspirantId { get; set; }
        public int OrderId { get; set; }
        public int PrevOrderId { get; set; }

        [Display(Name = "Стаціонар/Заочна")]
        public bool StationaryForm { get; set; }

        [Display(Name = "Денна/Вечірня")]
        public bool DayForm { get; set; }

        [Display(Name = "Бюджет/Контракт")]
        public bool Budget { get; set; }

        [Range(0, 5)]
        [Display(Name = "Курс")]
        public int Course { get; set; }

        public virtual Aspirant Aspirant { get; set; }
        public virtual Order Order { get; set; }
    }
}
