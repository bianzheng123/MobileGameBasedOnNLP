using System;
using System.Text;
using UnityEngine;

public static class InstructionParser
{

    /// <summary>
    /// 执行指令
    /// 对于切换模型的指令，直接跳出选择界面的窗口
    /// </summary>
    /// <param name="instruction"></param>
    /// <returns></returns>
    public static Order ExecuteLanguageInstruction(string instruction)
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
                    Instantiation instantiation = ObjectFactory.InstantiationObject(type,x,y,prefabIndex);
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
                    if(exit == null)
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