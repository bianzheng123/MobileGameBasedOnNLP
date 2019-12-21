using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModelAfter : Order
{
    private ObjectManager instructionObject;
    private int prefabIndex;
    private int gameobjectIndex;
    /// <summary>
    /// 生成改变模型的指令
    /// </summary>
    /// <param name="gameobjectIndex">代表是对几号物体切换模型</param>
    /// <param name="prefabIndex">代表要换成第几个物体</param>
    public ChangeModelAfter(ObjectManager instructionObject, int gameobjectIndex,int prefabIndex)
    {
        this.prefabIndex = prefabIndex;
        this.instructionObject = instructionObject;
        this.gameobjectIndex = gameobjectIndex;
    }
    public void Execute()
    {
        instructionObject.ChangeModelAfter(gameobjectIndex,prefabIndex);
    }
}

public class ChangeModelBefore : Order
{
    private ObjectManager instructionObject;
    private int gameobjectIndex;
    /// <summary>
    /// 生成改变模型的指令
    /// </summary>
    /// <param name="gameobjectIndex">代表是对几号物体切换模型</param>
    public ChangeModelBefore(ObjectManager instructionObject, int gameobjectIndex)
    {
        this.instructionObject = instructionObject;
        this.gameobjectIndex = gameobjectIndex;
    }
    public void Execute()
    {
        instructionObject.ChangeModelBefore(gameobjectIndex);
    }
}