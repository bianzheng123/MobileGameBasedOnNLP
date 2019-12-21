using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输入的编号从1开始，但是存储的编号从0开始
/// </summary>
public abstract class ObjectManager
{
    public int prefabsLen;
    public string[] prefabImagePaths;//用于显示切换模型时，加载图片的路径
    //代表家居的种类
    public string type;
    protected GameObject[] prefabs;
    //计算同种类型的物体有几个
    protected int objectCount;
    //用于存放同类物体的列表
    protected List<BaseObject> objectList = new List<BaseObject>();

    /// <summary>
    /// 通过索引得到里面的元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public BaseObject GetBaseObjectByIndex(int index)
    {
        if(!(0 <= index && index < objectList.Count))
        {
            PanelManager.Open<TipPanel>("数组越界");
            return null;
        }else if(objectList[index] == null)
        {
            PanelManager.Open<TipPanel>("访问的物体已被删除");
            return null;
        }
        return objectList[index];
    }

    /// <summary>
    /// 用于物体的生成
    /// </summary>
    /// <param name="position">生成物体的位置</param>
    /// /// <param name="prefabIndex">预制体的索引值</param>
    public virtual void Instantiation(Vector3 position,int prefabIndex,Vector3 rotation)
    {
        objectCount++;
        GameObject go = Object.Instantiate(prefabs[prefabIndex], position, Quaternion.Euler(rotation));
        BaseObject baseObject = go.GetComponent<BaseObject>();
        baseObject.number = objectCount;
        baseObject.prefabIndex = prefabIndex;
        objectList.Add(baseObject);
    }

    /// <summary>
    /// 用于删除物体
    /// </summary>
    /// <param name="gameobjectIndex">初始值为1，最大值为objectCount</param>
    public virtual void Delete(int gameobjectIndex)
    {
        if (CheckGameobjectIndex(gameobjectIndex) == false)
        {
            return;
        }
        gameobjectIndex--;
        GameObject go = objectList[gameobjectIndex].gameObject;
        objectList[gameobjectIndex] = null;
        Object.Destroy(go);
    }
    
    public virtual void Move(Vector3 position, int gameobjectIndex)
    {
        if (CheckGameobjectIndex(gameobjectIndex) == false)
        {
            return;
        }
        gameobjectIndex--;
        Transform transform = objectList[gameobjectIndex].transform;
        transform.Translate(position,Space.World);
    }

    public virtual void ChangeModelBefore(int gameobjectIndex)
    {
        if (CheckGameobjectIndex(gameobjectIndex) == false)
        {
            return;
        }

        PanelManager.Open<ObjectTipPanel>(gameobjectIndex,this);

    }

    /// <summary>
    /// 更换模型的方法
    /// </summary>
    /// <param name="gameobjectIndex">游戏物体的编号</param>
    /// <param name="prefabIndex">新模型的索引</param>
    public virtual void ChangeModelAfter(int gameobjectIndex,int prefabIndex)
    {
        if(CheckGameobjectIndex(gameobjectIndex) == false)
        {
            return;
        }

        gameobjectIndex--;
        //修改下面的方法即可
        GameObject go = objectList[gameobjectIndex].gameObject;

        int number = go.GetComponent<BaseObject>().number;
        int oldPrefabIndex = go.GetComponent<BaseObject>().prefabIndex;
        if(oldPrefabIndex == prefabIndex)
        {
            Debug.Log("调用原先的模型，程序出错！");
        }
        Vector3 position = go.transform.position;
        Quaternion rotation = go.transform.rotation;
        Vector3 scale = go.transform.localScale;

        objectList[gameobjectIndex] = null;
        Object.Destroy(go);

        go = Object.Instantiate(prefabs[prefabIndex], position, rotation);
        go.transform.localScale = scale;

        BaseObject baseObject = go.GetComponent<BaseObject>();
        baseObject.number = number;
        baseObject.prefabIndex = prefabIndex;
        objectList[gameobjectIndex] = baseObject;
    }

    public virtual void Rotate(int gameobjectIndex, int rotateNum)
    {
        if (CheckGameobjectIndex(gameobjectIndex) == false)
        {
            return;
        }
        gameobjectIndex--;
        Transform transform = objectList[gameobjectIndex].transform;
        transform.Rotate(new Vector3(0,-rotateNum,0),Space.World);
    }

    private bool CheckGameobjectIndex(int gameobjectIndex)
    {
        if (gameobjectIndex <= 0 || gameobjectIndex > objectCount)
        {
            PanelManager.Open<TipPanel>("编号不在场景中，请重新输入");
            return false;
        }
        if (objectList[gameobjectIndex - 1] == null)
        {
            PanelManager.Open<TipPanel>("该物体已被删除");
            return false;
        }
        return true;
    }

}
