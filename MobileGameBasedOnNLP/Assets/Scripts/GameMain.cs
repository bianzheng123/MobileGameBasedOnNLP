using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{

    void Start()
    {
        //初始化
        PanelManager.Init();
        Gamedata.Init();
        //打开登陆面板
        PanelManager.Open<GamePanel>();
        NetManager.Connect("127.0.0.1", 8888);
    }

    private void Update()
    {
        Gamedata.Update();
    }
}
