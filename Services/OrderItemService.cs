using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.ViewModels;

namespace XYZLaundry.Services
{
    public class OrderItemService : IOrderItemService
    {
        private List<OrderItemViewModel> _orderItemViewModels;

        public OrderItemService()
        {
            _orderItemViewModels = new List<OrderItemViewModel>();
        }

        public List<OrderItemViewModel> GetOrderItems()
        {
            return _orderItemViewModels;
        }

        public void SetOrderItems(List<OrderItemViewModel> orderItems)
        {
            _orderItemViewModels = orderItems;
        }
    }
}
