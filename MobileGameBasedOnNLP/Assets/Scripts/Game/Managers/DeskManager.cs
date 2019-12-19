using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : ObjectManager
{
    public static int prefabsLen;
    public static new string[] prefabImagePaths;//用于显示切换模型时，加载图片的路径
    private readonly static object lockObj = new object();
    private static DeskManager instance = null;
    private DeskManager()
    {
        prefabsLen = 5;
        objectCount = 0;
        prefabs = new GameObject[prefabsLen];
        prefabImagePaths = new string[prefabsLen];
        for (int i = 0; i < prefabsLen; i++)
        {
            prefabs[i] = ResManager.LoadPrefab("Prefabs/Desk_" + (i + 1));
            prefabImagePaths[i] = "Image/Desk_" + (i+1);
        }
    }

    public static DeskManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new DeskManager();
                    }
                }
            }
            return instance;
        }
    }
    //单例

    public override void ChangeModel(int gameobjectIndex, int prefabIndex)
    {
        if (!(0 <= prefabIndex && prefabIndex < prefabsLen))
        {
            PanelManager.Open<TipPanel>("选择了索引值不同");
            return;
        }
        base.ChangeModel(gameobjectIndex, prefabIndex);
    }
}
