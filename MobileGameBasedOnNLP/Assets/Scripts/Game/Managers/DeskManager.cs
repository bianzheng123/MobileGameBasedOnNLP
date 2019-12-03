using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : ObjectManager
{
    private readonly static object lockObj = new object();
    private static DeskManager instance = null;
    private DeskManager()
    {
        prefab = ResManager.LoadPrefab("Prefabs/Desk");
        objectCount = 0;
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

}
