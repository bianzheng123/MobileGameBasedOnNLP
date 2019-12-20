using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : ObjectManager
{
    public static int prefabsLen;
    public static new string[] prefabImagePaths;//用于显示切换模型时，加载图片的路径
    private readonly static object lockObj = new object();
    private static BedManager instance = null;
    private BedManager()
    {
        prefabsLen = 5;
        objectCount = 0;
        prefabs = new GameObject[prefabsLen];
        prefabImagePaths = new string[prefabsLen];
        for (int i = 0; i < prefabsLen; i++)
        {
            prefabs[i] = ResManager.LoadPrefab("Prefabs/Bed/Bed_" + (i + 1));
            prefabImagePaths[i] = "Image/Bed/Bed_" + (i + 1);
        }
    }

    public static BedManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new BedManager();
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
