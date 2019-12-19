using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    void Start()
    {
        //初始化
        PanelManager.Init();
        //打开登陆面板
        PanelManager.Open<GamePanel>();
        
    }

    private void Update()
    {
        NetManager.Update();
    }
}
