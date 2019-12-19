using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : Order
{
    private ObjectManager instructionObject;
    private int prefabIndex;
    private int gameobjectIndex;
    /// <summary>
    /// 生成改变模型的指令
    /// </summary>
    /// <param name="gameobjectIndex">代表是对几号物体切换模型</param>
    /// <param name="prefabIndex">代表要换成第几个物体</param>
    public ChangeModel(ObjectManager instructionObject, int gameobjectIndex,int prefabIndex)
    {
        this.prefabIndex = prefabIndex;
        this.instructionObject = instructionObject;
        this.gameobjectIndex = gameobjectIndex;
    }
    public void Execute()
    {
        instructionObject.ChangeModel(gameobjectIndex,prefabIndex);
    }
}
