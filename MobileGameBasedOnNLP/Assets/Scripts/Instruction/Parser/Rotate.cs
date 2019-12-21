using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class InstructionParser
{
    private static Order Rotate_Desk(string instruction)
    {
        try
        {
            if (!GetRotationParameter(instruction, out int gameobjectIndex,out int rotateNum))
            {
                throw new Exception("参数类型转换错误");
            }
            Rotate rotate = new Rotate(deskManager, gameobjectIndex,rotateNum);
            return rotate;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在旋转桌子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Rotate_Chair(string instruction)
    {
        try
        {
            if (!GetRotationParameter(instruction, out int gameobjectIndex, out int rotateNum))
            {
                throw new Exception("参数类型转换错误");
            }
            Rotate rotate = new Rotate(chairManager, gameobjectIndex, rotateNum);
            return rotate;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在旋转椅子时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order Rotate_Bed(string instruction)
    {
        try
        {
            if (!GetRotationParameter(instruction, out int gameobjectIndex, out int rotateNum))
            {
                throw new Exception("参数类型转换错误");
            }
            Rotate rotate = new Rotate(bedManager, gameobjectIndex, rotateNum);
            return rotate;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在旋转床时发生错误" + ex.ToString());
        }
        return null;
    }

    /// <summary>
    /// 省略提取参数的过程
    /// </summary>
    /// <param name="instruction">指令</param>
    /// <returns>true代表转换成功，false代表转换失败</returns>
    private static bool GetRotationParameter(string instruction, out int gameobjectIndex, out int rotateNum)
    {
        string[] arr = instruction.Split(' ');
        string gameobjectIndex_str = arr[1];
        string rotateNum_str = arr[2];

        //离开前对参数进行赋值
        gameobjectIndex = 0;
        rotateNum = 0;

        if (!ToInt(gameobjectIndex_str, out gameobjectIndex) || !ToInt(rotateNum_str, out rotateNum))
        {
            return false;
        }
        return true;
    }
}
