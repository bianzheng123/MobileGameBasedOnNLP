using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : ObjectManager
{
    private readonly static object lockObj = new object();
    private static ChairManager instance = null;
    private ChairManager()
    {
        prefabsLen = 6;
        objectCount = 0;
        type = "chair";
        prefabs = new GameObject[prefabsLen];
        prefabImagePaths = new string[prefabsLen];
        for (int i = 0; i < prefabsLen; i++)
        {
            prefabs[i] = ResManager.LoadPrefab("Prefabs/Chair/Chair_" + (i+1));
            prefabImagePaths[i] = "Image/Chair/Chair_" + (i+1);
        }
    }

    public static ChairManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new ChairManager();
                    }
                }
            }
            return instance;
        }
    }
    //单例

    public override void ChangeModelAfter(int gameobjectIndex, int prefabIndex)
    {
        if (!(0 <= prefabIndex && prefabIndex < prefabsLen))
        {
            PanelManager.Open<TipPanel>("索引值越界");
            return;
        }
        base.ChangeModelAfter(gameobjectIndex, prefabIndex);
    }
}
