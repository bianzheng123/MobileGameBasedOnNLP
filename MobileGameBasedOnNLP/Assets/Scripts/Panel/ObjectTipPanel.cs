using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于发动切换模型指令，点击确定时发送切换模型的指令
/// </summary>
public class ObjectTipPanel : BasePanel
{
    //单选框
    private Toggle[] chooseToggle;
    //图像面板
    private GameObject item;
    //content
    private Transform content;
    //确定按钮
    private Button okButton;
    //目前的模型编号
    private int modelIndex;
    //这个游戏物体的名称
    private int gameobjectIndex;
    private ObjectManager om;

    //初始化
    public override void OnInit()
    {
        skinPath = "Panel/ObjectTipPanel";
        layer = PanelManager.Layer.Panel;
    }
    //显示
    public override void OnShow(params object[] args)
    {
        //物体的编号
        gameobjectIndex = (int)args[0];//上文保证了一定可以转换成int
        om = (ObjectManager)args[1];
        int prefablen = om.prefabsLen;
        BaseObject bo = om.GetBaseObjectByIndex(gameobjectIndex - 1);
        if (bo == null)
        {
            Close();
        }
        modelIndex = bo.prefabIndex;

        //寻找组件
        item = skin.transform.Find("Item").gameObject;
        content = skin.transform.Find("Scroll View/Viewport/Content");
        okButton = skin.transform.Find("OkButton").GetComponent<Button>();
        chooseToggle = new Toggle[prefablen];
        //对面板进行初始化
        InitPanel(prefablen,om);
        //监听
        okButton.onClick.AddListener(OnOkClick);

    }

    private void InitPanel(int prefablen,ObjectManager om)
    {
        //使用toggle，由于是在同一个panel中，不需要自己重新设置内容
        for(int i = 0; i < prefablen; i++)
        {
            GameObject go = Instantiate(item);
            chooseToggle[i] = go.transform.Find("Toggle").GetComponent<Toggle>();
            go.GetComponent<Image>().sprite = ResManager.LoadUISprite(om.prefabImagePaths[i]);
            if(i == modelIndex)
            {
                chooseToggle[i].gameObject.SetActive(false);
            }
            go.transform.SetParent(content);
        }
    }

    //关闭
    public override void OnClose()
    {

    }

    //当按下确定按钮
    public void OnOkClick()
    {
        int selectedIndex = -1;
        for(int i = 0; i < chooseToggle.Length; i++)
        {
            if (chooseToggle[i].isOn)
            {
                selectedIndex = i;
                break;
            }
        }
        if(selectedIndex == -1)
        {
            PanelManager.Open<TipPanel>("请选择要游戏物体");
            return;
        }else if(selectedIndex == modelIndex)
        {
            PanelManager.Open<TipPanel>("出现bug，选择的索引和模型的索引相同");
            return;
        }
        //生成指令
        string instruction = "ChangeModelAfter " + gameobjectIndex + " " + selectedIndex + " " + om.type;
        Broker.TakeInstruction(instruction);
        Close();
        
    }
}
