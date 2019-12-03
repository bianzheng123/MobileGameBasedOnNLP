using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : ObjectManager
{
    private readonly static object lockObj = new object();
    private static ChairManager instance = null;
    private ChairManager()
    {
        objectCount = 0;
        prefab = ResManager.LoadPrefab("Prefabs/Chair");
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

}
