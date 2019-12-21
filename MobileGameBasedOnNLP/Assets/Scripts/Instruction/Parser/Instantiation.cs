using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class InstructionParser
{
    private static Order Instantiation_Desk(string instruction)
    {
        try
        {
            if (!GetInstantiationParameter(instruction, out int x, out int y, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }

            Instantiation instantiation = new Instantiation(deskManager, new Vector3(x, 0, y), prefabIndex, new Vector3(0, 0, 0));

            return instantiation;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在生成桌子时发生错误" + ex.ToString());
        }
        return null;

    }

    private static Order Instantiation_Chair(string instruction)
    {
        try
        {
            if (!GetInstantiationParameter(instruction, out int x, out int y, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }
            Instantiation instantiation = new Instantiation(chairManager, new Vector3(x, 0, y), prefabIndex, new Vector3(0, 180, 0));
            return instantiation;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在生成椅子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Instantiation_Bed(string instruction)
    {
        try
        {
            if (!GetInstantiationParameter(instruction, out int x, out int y, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }
            Instantiation instantiation = new Instantiation(bedManager, new Vector3(x, 0, y), prefabIndex, new Vector3(0, 180, 0));
            return instantiation;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在生成床时发生错误" + ex.ToString());
        }
        return null;
    }

    /// <summary>
    /// 省略提取参数的过程
    /// </summary>
    /// <param name="instruction">指令</param>
    /// <returns>true代表转换成功，false代表转换失败</returns>
    private static bool GetInstantiationParameter(string instruction, out int x, out int y, out int prefabIndex)
    {
        string[] arr = instruction.Split(' ');
        string x_str = arr[1];
        string y_str = arr[2];
        string prefabIndex_str = arr[3];

        //离开前对参数进行赋值
        x = 0;
        y = 0;
        prefabIndex = 0;

        if (!ToInt(x_str, out x) || !ToInt(y_str, out y) || !ToInt(prefabIndex_str, out prefabIndex))
        {
            return false;
        }
        return true;
    }
}