using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectManager
{
    protected GameObject prefab;
    //计算同种类型的物体有几个
    protected int objectCount;
    //用于存放同类物体的列表
    protected List<BaseObject> objectList = new List<BaseObject>();

    public string BianHao
    {
        get
        {
            string str = "";
            bool isfirst = true;
            for(int i = 0; i < objectList.Count; i++)
            {
                if(objectList[i] != null)
                {
                    if (isfirst)
                    {
                        str = i.ToString();
                    }
                    else
                    {
                        str += "," + i;
                    }
                    isfirst = false;
                }
            }
            if(str == "")
            {
                return "!";
            }
            return str;
        }
    }

    public string ZuoBiao
    {
        get
        {
            string str = "";
            bool isfirst = true;
            for(int i = 0; i < objectList.Count; i++)
            {
                if (objectList[i] != null)
                {
                    Vector3 position = objectList[i].gameObject.transform.position;
                    if (isfirst)
                    {
                        str = position.x + "," + position.y + "," + position.z;
                    }
                    else
                    {
                        str += "|" + position.x + "," + position.y + "," + position.z;
                    }
                    isfirst = false;
                }
            }
            if (str == "")
            {
                return "!";
            }
            return str;
        }
    }

    /// <summary>
    /// 用于物体的生成
    /// </summary>
    /// <param name="position">生成物体的位置</param>
    public virtual void Instantiation(Vector3 position)
    {
        objectCount++;
        GameObject go = Object.Instantiate(prefab, position, Quaternion.identity);
        BaseObject baseObject = go.GetComponent<BaseObject>();
        baseObject.number = objectCount;
        objectList.Add(baseObject);
    }

    /// <summary>
    /// 用于删除物体
    /// </summary>
    /// <param name="index">初始值为1，最大值为objectCount</param>
    public virtual void Delete(int index)
    {
        if (index <= 0 || index > objectCount)
        {
            PanelManager.Open<TipPanel>("编号不在场景中，请重新输入");
            return;
        }
        else if (objectList[index - 1] == null)
        {
            PanelManager.Open<TipPanel>("该物体已被删除");
            return;
        }
        index--;
        GameObject go = objectList[index].gameObject;
        objectList[index] = null;
        Object.Destroy(go);
    }
    
    public virtual void Move(Vector3 position, int index)
    {
        if (index <= 0 || index > objectCount)
        {
            PanelManager.Open<TipPanel>("编号不在场景中，请重新输入");
            return;
        }
        else if (objectList[index - 1] == null)
        {
            PanelManager.Open<TipPanel>("该物体已被删除");
            return;
        }
        index--;
        Transform transform = objectList[index].transform;
        transform.Translate(position);
    }

    public virtual void ChangeColor(int index,int red,int green,int blue,int alpha)
    {
        if (index <= 0 || index > objectCount)
        {
            PanelManager.Open<TipPanel>("编号不在场景中，请重新输入");
            return;
        }
        if (objectList[index - 1] == null)
        {
            PanelManager.Open<TipPanel>("该物体已被删除");
            return;
        }
        if(!InRange(red,0,255) || !InRange(green,0,255) || !InRange(blue, 0, 255) || !InRange(alpha, 0, 255))
        {
            PanelManager.Open<TipPanel>("颜色参数范围在0到255之间，请重新输入");
            return;
        }
        index--;
        Renderer[] renderers = objectList[index].transform.GetComponentsInChildren<Renderer>();
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = new Color((float)red / 255,(float)green / 255,(float)blue / 255,(float)alpha / 255);
        }
    }

    /// <summary>
    /// 判断是否在范围中，左闭右闭
    /// </summary>
    private bool InRange(int value,int min,int max)
    {
        return min <= value && value <= max;
    }
}
