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

    public static Instantiation InstantiationObject(string type,string x_input,string y_input)
    {
        int x = 0;
        int y = 0;
        if (!ToInt(x_input, out x) || !ToInt(y_input, out y)) return null;

        Instantiation instantiation = null;
        switch (type)
        {
            case "桌子":
                instantiation = new Instantiation(deskManager, new Vector3(x, 1f, y));
                break;
            case "椅子":
                instantiation = new Instantiation(chairManager, new Vector3(x, 1f, y));
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
            case "桌子":
                delete = new Delete(deskManager, num);
                break;
            case "椅子":
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
            case "桌子":
                move = new Move(deskManager,new Vector3(x,0,y),num);
                break;
            case "椅子":
                move = new Move(chairManager,new Vector3(x,0,y),num);
                break;
        }
        return move;
    }

    public static ChangeColor ChangeColorObject(string type,string num_input,string red_input,string green_input,string blue_input,string alpha_input)
    {
        int num = 0, red = 0, green = 0, blue = 0,alpha = 0;
        if (!ToInt(num_input, out num) || !ToInt(red_input, out red) || !ToInt(green_input, out green) || !ToInt(blue_input,out blue) || !ToInt(alpha_input, out alpha)) return null;
        ChangeColor changeColor = null;
        switch (type)
        {
            case "桌子":
                changeColor = new ChangeColor(deskManager,num,red,green,blue,alpha);
                break;
            case "椅子":
                changeColor = new ChangeColor(chairManager,num,red,green,blue,alpha);
                break;
        }
        return changeColor;
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