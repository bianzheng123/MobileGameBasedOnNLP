using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class InstructionParser
{

    private static Order Move_Desk(string instruction)
    {
        try
        {
            if (!GetMoveParameter(instruction, out int x_offset, out int y_offset, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Move move = new Move(deskManager, new Vector3(x_offset, 0, y_offset), number);
            return move;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在移动桌子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Move_Chair(string instruction)
    {
        try
        {
            if (!GetMoveParameter(instruction, out int x_offset, out int y_offset, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Move move = new Move(chairManager, new Vector3(x_offset, 0, y_offset), number);
            return move;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在移动椅子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Move_Bed(string instruction)
    {
        try
        {
            if (!GetMoveParameter(instruction, out int x_offset, out int y_offset, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Move move = new Move(bedManager, new Vector3(x_offset, 0, y_offset), number);
            return move;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在移动床时发生错误" + ex.ToString());
        }
        return null;
    }

    private static bool GetMoveParameter(string instruction, out int x_offset, out int y_offset, out int number)
    {
        string[] arr = instruction.Split(' ');
        string num_str = arr[1];
        string x_offset_str = arr[2];
        string y_offset_str = arr[3];

        //离开前对参数进行赋值
        x_offset = 0;
        y_offset = 0;
        number = 0;

        if (!ToInt(x_offset_str, out x_offset) || !ToInt(y_offset_str, out y_offset) || !ToInt(num_str, out number))
        {
            return false;
        }
        return true;
    }
}
