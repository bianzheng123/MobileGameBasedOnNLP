using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class InstructionParser
{
    private static Order ChangeModelBefore_Desk(string instruction)
    {
        try
        {
            if (!GetChangeModelBeforeParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelBefore changeModel = new ChangeModelBefore(deskManager, number);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在加载切换桌子模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order ChangeModelBefore_Chair(string instruction)
    {
        try
        {
            if (!GetChangeModelBeforeParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelBefore changeModel = new ChangeModelBefore(chairManager, number);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在加载切换椅子模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order ChangeModelBefore_Bed(string instruction)
    {
        try
        {
            if (!GetChangeModelBeforeParameter(instruction, out int number))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelBefore changeModel = new ChangeModelBefore(bedManager, number);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在加载切换床模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order ChangeModelAfter_Desk(string instruction)
    {
        try
        {
            if (!GetChangeModelAfterParameter(instruction, out int gameobjectIndex, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelAfter changeModel = new ChangeModelAfter(deskManager, gameobjectIndex, prefabIndex);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在切换桌子模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order ChangeModelAfter_Chair(string instruction)
    {
        try
        {
            if (!GetChangeModelAfterParameter(instruction, out int gameobjectIndex, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelAfter changeModel = new ChangeModelAfter(chairManager, gameobjectIndex, prefabIndex);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在切换椅子模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static Order ChangeModelAfter_Bed(string instruction)
    {
        try
        {
            if (!GetChangeModelAfterParameter(instruction, out int gameobjectIndex, out int prefabIndex))
            {
                throw new Exception("参数类型转换错误");
            }
            ChangeModelAfter changeModel = new ChangeModelAfter(bedManager, gameobjectIndex, prefabIndex);
            return changeModel;
        }
        catch (Exception ex)
        {
            PanelManager.Open<TipPanel>("在切换床模型时发生错误" + ex.ToString());
        }
        return null;
    }

    private static bool GetChangeModelAfterParameter(string instruction, out int gameobjectIndex, out int modelIndex)
    {
        string[] arr = instruction.Split(' ');
        string gameobjectIndex_str = arr[1];
        string modelIndex_str = arr[2];

        gameobjectIndex = 0;
        modelIndex = 0;

        if (!ToInt(gameobjectIndex_str, out gameobjectIndex) || !ToInt(modelIndex_str, out modelIndex))
        {
            return false;
        }
        return true;
    }

    private static bool GetChangeModelBeforeParameter(string instruction, out int number)
    {
        string[] arr = instruction.Split(' ');
        string number_str = arr[1];

        number = 0;

        if (!ToInt(number_str, out number))
        {
            return false;
        }
        return true;
    }
}
