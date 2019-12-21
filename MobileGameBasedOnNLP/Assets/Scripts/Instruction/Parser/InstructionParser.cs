using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 这里已经是确定了对哪一种物体进行操作，以及是哪一个物体进行操作
/// </summary>
public static partial class InstructionParser
{
    private static ChairManager chairManager = ChairManager.Instance;

    private static DeskManager deskManager = DeskManager.Instance;

    private static BedManager bedManager = BedManager.Instance;

    public static void AddInstructionListener()
    {
        InstructionHead head = new InstructionHead("ChangeModelAfter", "desk");
        InstructionManager.AddInstructionListener(head, ChangeModelAfter_Desk);

        head = new InstructionHead("ChangeModelAfter", "chair");
        InstructionManager.AddInstructionListener(head, ChangeModelAfter_Chair);

        head = new InstructionHead("ChangeModelAfter", "bed");
        InstructionManager.AddInstructionListener(head, ChangeModelAfter_Bed);

        head = new InstructionHead("ChangeModel", "desk");
        InstructionManager.AddInstructionListener(head, ChangeModelBefore_Desk);

        head = new InstructionHead("ChangeModel", "chair");
        InstructionManager.AddInstructionListener(head, ChangeModelBefore_Chair);

        head = new InstructionHead("ChangeModel", "bed");
        InstructionManager.AddInstructionListener(head, ChangeModelBefore_Bed);

        head = new InstructionHead("Delete", "desk");
        InstructionManager.AddInstructionListener(head, Delete_Desk);

        head = new InstructionHead("Delete", "chair");
        InstructionManager.AddInstructionListener(head, Delete_Chair);

        head = new InstructionHead("Delete", "bed");
        InstructionManager.AddInstructionListener(head, Delete_Bed);

        head = new InstructionHead("Exit", "");
        InstructionManager.AddInstructionListener(head, Exit);

        head = new InstructionHead("Instantiation", "desk");
        InstructionManager.AddInstructionListener(head, Instantiation_Desk);

        head = new InstructionHead("Instantiation", "chair");
        InstructionManager.AddInstructionListener(head, Instantiation_Chair);

        head = new InstructionHead("Instantiation", "bed");
        InstructionManager.AddInstructionListener(head, Instantiation_Bed);

        head = new InstructionHead("InstructionException", "");
        InstructionManager.AddInstructionListener(head, Exception);

        head = new InstructionHead("Move", "desk");
        InstructionManager.AddInstructionListener(head, Move_Desk);

        head = new InstructionHead("Move", "chair");
        InstructionManager.AddInstructionListener(head, Move_Chair);

        head = new InstructionHead("Move", "bed");
        InstructionManager.AddInstructionListener(head, Move_Bed);

        head = new InstructionHead("Rotate","desk");
        InstructionManager.AddInstructionListener(head,Rotate_Desk);

        head = new InstructionHead("Rotate", "chair");
        InstructionManager.AddInstructionListener(head, Rotate_Chair);

        head = new InstructionHead("Rotate", "bed");
        InstructionManager.AddInstructionListener(head, Rotate_Bed);

    }

    /// <summary>
    /// 查看字符串是否合法
    /// 必须要是数字，不能为空
    /// </summary>
    private static bool ToInt(string str, out int result)
    {
        result = 0;
        try
        {
            result = int.Parse(str);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

