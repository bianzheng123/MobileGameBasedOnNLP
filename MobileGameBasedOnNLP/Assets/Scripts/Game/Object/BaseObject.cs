using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有物体的基类，这里实现了头上编号
/// </summary>
public class BaseObject : MonoBehaviour
{
    protected Camera mainCamera;
    public int number = 0;

    //编号显示的偏移量
    protected Vector2 offset;

    /// <summary>
    /// 记得设置标签偏移量
    /// </summary>
    protected virtual void Init()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// 绘制标签
    /// </summary>
    protected void PaintLabel()
    {
        //得到游戏物体在3D世界中的坐标
        Vector3 worldPosition = new Vector3(transform.position.x + offset[0], transform.position.y + offset[1], transform.position.z);
        //根据游戏物体的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = mainCamera.WorldToScreenPoint(worldPosition);
        //得到游戏物体的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);
        //计算游戏物体的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(number.ToString()));
        //设置显示颜色为黄色
        GUI.color = Color.yellow;
        //绘制游戏物体标签
        GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y, nameSize.x, nameSize.y), number.ToString());
    }

}
