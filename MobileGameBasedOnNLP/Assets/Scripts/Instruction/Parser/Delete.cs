using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class InstructionParser
{
    private static Order Delete_Desk(string instruction)
    {
        try
        {
            if (!GetDeleteParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Delete delete = new Delete(deskManager, number);
            return delete;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在删除桌子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Delete_Chair(string instruction)
    {
        try
        {
            if (!GetDeleteParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Delete delete = new Delete(chairManager, number);
            return delete;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在删除椅子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Delete_Bed(string instruction)
    {
        try
        {
            if (!GetDeleteParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            Delete delete = new Delete(bedManager, number);
            return delete;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在删除床时发生错误" + ex.ToString());
        }
        return null;
    }

    private static bool GetDeleteParameter(string instruction, out int number)
    {
        string[] arr = instruction.Split(' ');
        string num_str = arr[1];

        //离开前对参数进行赋值
        number = 0;

        if (!ToInt(num_str, out number))
        {
            return false;
        }
        return true;
    }
}