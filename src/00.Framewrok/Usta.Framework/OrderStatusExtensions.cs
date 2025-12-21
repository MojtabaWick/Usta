using Usta.Domain.Core.OrderAgg.Enums;

namespace Usta.Framework
{
    public static class OrderStatusExtensions
    {
        public static string ToDisplayString(this OrderStatus status)
        {
            if (status == OrderStatus.Cancelled)
                return "لغو شده";
            else if (status == OrderStatus.Completed)
                return "انجام شده";
            else if (status == OrderStatus.WaitingForOffers)
                return "در انتظار دریافت پیشنهاد";
            else if (status == OrderStatus.WaitingForAcceptance)
                return "در انتظار قبول کردن پیشنهاد";
            else if (status == OrderStatus.InProgress)
                return "در حال انجام";
            else
                return status.ToString(); // fallback
        }
    }
}