using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.ViewModels;

namespace XYZLaundry.Services
{
    public interface IOrderItemService
    {
        void SetOrderItems(List<OrderItemViewModel> orderItems);

        List<OrderItemViewModel> GetOrderItems();
    }
}
