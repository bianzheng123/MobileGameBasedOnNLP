using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于调用指令
/// </summary>
public static class Broker
{
    private static List<Order> orderList = new List<Order>();
    private static List<string> instructionList = new List<string>();

    public static void Init()
    {
        orderList = new List<Order>();
        instructionList = new List<string>();
    }

    public static void Update()
    {
        PlaceOrders();
    }

    public static void TakeInstruction(string instruction)
    {
        if (!PanelManager.ContainsPanel<GamePanel>())
        {
            return;
        }
        instructionList.Add(instruction);
    }

    private static void PlaceOrders()
    {
        GamePanel gamePanel = PanelManager.GetPanel<GamePanel>();
        foreach (string instruction in instructionList)
        {
            if(gamePanel != null)
            {
                gamePanel.ReturnContent = instruction;
            }
            Order order = InstructionManager.FireInstruction(instruction);
            if(order != null)
            {
                orderList.Add(order);
            }
        }
        foreach (Order order in orderList)
        {
            order.Execute();
        }
        if(orderList.Count > 0 && gamePanel != null)
        {
            gamePanel.StateText = "指令执行完成";
        }
        instructionList.Clear();
        orderList.Clear();
    }

   

}
