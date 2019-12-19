using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据不同的指令返回不同的指令对象
/// </summary>
public static class ObjectFactory
{
    private static ChairManager chairManager = ChairManager.Instance;

    private static DeskManager deskManager = DeskManager.Instance;

    public static Instantiation InstantiationObject(string type,string x_input,string y_input,string prefabIndex_input)
    {
        int x = 0;
        int y = 0;
        int prefabIndex = 0;
        if (!ToInt(x_input, out x) || !ToInt(y_input, out y) || !ToInt(prefabIndex_input,out prefabIndex)) return null;

        Instantiation instantiation = null;
        switch (type.Trim())
        {
            case "desk":
                instantiation = new Instantiation(deskManager, new Vector3(x, 0, y), prefabIndex,new Vector3(0,0,0));
                break;
            case "chair":
                instantiation = new Instantiation(chairManager, new Vector3(x, 0, y), prefabIndex,new Vector3(0,180,0));
                break;
        }
        return instantiation;
    }

    public static Delete DeleteObject(string type, string num_input)
    {
        int num = 0;
        if (!ToInt(num_input, out num)) return null;

        Delete delete = null;
        switch (type)
        {
            case "desk":
                delete = new Delete(deskManager, num);
                break;
            case "chair":
                delete = new Delete(chairManager, num);
                break;
        }
        return delete;
    }

    public static Move MoveObject(string type, string num_input, string x_input, string y_input)
    {
        int num = 0, x = 0, y = 0;
        if (!ToInt(x_input, out x) || !ToInt(y_input, out y) || !ToInt(num_input,out num)) return null;
        Move move = null;
        switch (type)
        {
            case "desk":
                move = new Move(deskManager,new Vector3(x,0,y),num);
                break;
            case "chair":
                move = new Move(chairManager,new Vector3(x,0,y),num);
                break;
        }
        return move;
    }

    public static ChangeModel ChangeModelObject(string type,string num_input,int prefabIndex)
    {
        int num = 0;
        if (!ToInt(num_input, out num)) return null;
        ChangeModel changeModel = null;
        switch (type)
        {
            case "desk":
                changeModel = new ChangeModel(deskManager,num,prefabIndex);
                break;
            case "chair":
                changeModel = new ChangeModel(chairManager,num,prefabIndex);
                break;
        }
        return changeModel;
    }

    public static Exit Exit()
    {
        return new Exit();
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