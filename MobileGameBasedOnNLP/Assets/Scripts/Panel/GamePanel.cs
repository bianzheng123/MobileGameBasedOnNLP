using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于处理指令，生成指令
/// </summary>
public class GamePanel : BasePanel
{
    //输入指令的输入框
    private InputField instructionInput;
    //运行指令的按钮
    private Button executeInstructionButton;
    //获取原指令的命令
    private Text instructionContent;
    //解释后的指令命令
    private Text returnContent;
    //退出按钮
    private Button quitButton;
    //显示状态的按钮
    private Text stateText;

    public string ReturnContent
    {
        set
        {
            if (returnContent.text == "")
            {
                returnContent.text = value;
            }
            else
            {
                returnContent.text += "\n" + value;
            }
        }
    }

    public string StateText
    {
        set { stateText.text = value; }
    }

    //初始化
    public override void OnInit()
    {
        skinPath = "Panel/GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        NetManager.Connect("127.0.0.1", 8888);
        InstructionParser.AddInstructionListener();
        //寻找组件
        instructionInput = skin.transform.Find("InstructionInput/Input").GetComponent<InputField>();
        executeInstructionButton = skin.transform.Find("ExecuteInstructionButton").GetComponent<Button>();
        instructionContent = skin.transform.Find("InstructionContent/Content").GetComponent<Text>();
        returnContent = skin.transform.Find("ReturnContent/Content").GetComponent<Text>();
        quitButton = skin.transform.Find("QuitButton").GetComponent<Button>();
        stateText = skin.transform.Find("StateText").GetComponent<Text>();
        //添加按钮事件
        quitButton.onClick.AddListener(OnQuitButtonClick);
        
        executeInstructionButton.onClick.AddListener(OnExecuteInstructionButtonClick);
    }

    private void ExecuteInstruction(string text)
    {
        AddInstructionContentText(text);
        //发送给网络，让他们处理
        NetManager.Send(text);
        stateText.text = "指令处理中，请稍后";
    }

    private void OnExecuteInstructionButtonClick()
    {
        string text = instructionInput.text;
        if(text == "")
        {
            PanelManager.Open<TipPanel>("请输入指令");
            return;
        }

        ExecuteInstruction(text);
        
    }

    private void OnQuitButtonClick()
    {
        SystemOperation.Quit();
    }

    private void AddInstructionContentText(string text)
    {
        if (instructionContent.text == "")
        {
            instructionContent.text = text;

        }
        else
        {
            instructionContent.text += "\n" + text;
        }
    }

}
