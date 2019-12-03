﻿using UnityEngine;

public class ResManager : MonoBehaviour
{

    //加载预设
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static GameObject LoadSprite(string path, int sortingOrder)
    {
        Sprite s = Resources.Load<Sprite>(path);
        string[] name = path.Split('/');
        GameObject go = new GameObject(name[name.Length - 1]);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder;
        sr.sprite = s;
        return go;

    }


    public static Sprite LoadUISprite(string path)
    {
        Sprite s = Resources.Load<Sprite>(path);
        return s;
    }
}
