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

    public static void TakeChangeModelOrder(ChangeModel order)
    {
        orderList.Add(order);
    }

    public static void TakeInstruction(string instruction)
    {
        if (!PanelManager.ContainsPanel<GamePanel>())
        {
            return;
        }
        instructionList.Add(instruction);
    }

    public static void PlaceOrders()
    {
        GamePanel gamePanel = PanelManager.GetPanel<GamePanel>();
        foreach (string instruction in instructionList)
        {
            gamePanel.ReturnContent = instruction;
            Order order = ParseInstruction(instruction);
            if(order != null)
            {
                orderList.Add(order);
            }
        }
        foreach (Order order in orderList)
        {
            order.Execute();
        }
        instructionList.Clear();
        orderList.Clear();
    }

    /// <summary>
    /// 执行指令
    /// 对于切换模型的指令，直接跳出选择界面的窗口
    /// </summary>
    /// <param name="instruction"></param>
    /// <returns></returns>
    private static Order ParseInstruction(string instruction)
    {
        string[] arr = instruction.Split(' ');
        try
        {
            string type = arr[arr.Length - 1];
            switch (arr[0])
            {
                case "Instantiation":
                    string x = arr[1];
                    string y = arr[2];
                    string prefabIndex = arr[3];
                    Instantiation instantiation = ObjectFactory.InstantiationObject(type, x, y, prefabIndex);
                    if (instantiation == null)
                    {
                        throw new Exception("Exception on create instantiation instruciton");
                    }
                    return instantiation;
                case "Delete":
                    string num = arr[1];

                    Delete delete = ObjectFactory.DeleteObject(type, num);
                    if (delete == null)
                    {
                        throw new Exception("Exception on create delete instruciton");
                    }
                    return delete;
                case "Move":
                    num = arr[1];
                    string x_offset = arr[2];
                    string y_offset = arr[3];

                    Move move = ObjectFactory.MoveObject(type, num, x_offset, y_offset);
                    if (move == null)
                    {
                        throw new Exception("Exception on create move instruciton");
                    }
                    return move;
                case "ChangeModel":
                    num = arr[1];
                    int num_int = int.Parse(num);

                    PanelManager.Open<ObjectTipPanel>(num_int, type);
                    return null;
                case "Exit":
                    Exit exit = ObjectFactory.Exit();
                    if (exit == null)
                    {
                        throw new Exception("Exception on create exit instruciton");
                    }
                    break;
                case "Exception":
                    throw new Exception("Exception on return instruction");
                default:
                    PanelManager.Open<TipPanel>("输入指令格式不正确");
                    return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            PanelManager.Open<TipPanel>("返回指令格式错误，出现bug");
        }
        return null;
    }

}
