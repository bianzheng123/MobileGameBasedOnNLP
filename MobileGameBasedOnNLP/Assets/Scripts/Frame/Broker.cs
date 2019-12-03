using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 用于调用指令
/// </summary>
public class Broker
{
    private List<Order> orderList = new List<Order>();

    public void TakeOrder(Order order)
    {
        orderList.Add(order);
    }

    public void PlaceOrders()
    {
        foreach (Order order in orderList)
        {
            order.Execute();
        }
        orderList.Clear();
    }
}
