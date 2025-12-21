using System.ComponentModel.DataAnnotations;

namespace Usta.Domain.Core.OrderAgg.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "لغو شده")]
        Cancelled = 0,

        [Display(Name = "انجام شده")]
        Completed = 1,

        [Display(Name = "در انتظار دریافت پیشنهاد")]
        WaitingForOffers = 2,

        [Display(Name = "در انتظار قبول کردن پیشنهاد")]
        WaitingForAcceptance = 3,

        [Display(Name = "در حال انجام")]
        InProgress = 4
    }
}