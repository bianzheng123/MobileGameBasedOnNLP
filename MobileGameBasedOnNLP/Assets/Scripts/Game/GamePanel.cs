using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    struct InstantiationUI
    {
        public InputField x_input;
        public InputField y_input;
        public Dropdown drop;
    }
    struct DeleteUI
    {
        public InputField sequence_input;
        public Dropdown drop;
    }
    struct MoveUI
    {
        public InputField x_input;
        public InputField y_input;
        public InputField seqence_input;
        public Dropdown drop;
    }
    struct ChangeColorUI
    {
        public InputField sequence_input;
        public InputField red_input;
        public InputField green_input;
        public InputField blue_input;
        public InputField alpha_input;
        public Dropdown drop;
    }

    //规定操作的复选框
    private Toggle[] operation;
    //初始化操作UI
    private InstantiationUI instantiationUI;
    //删除操作UI
    private DeleteUI deleteUI;
    //移动操作UI
    private MoveUI moveUI;
    //改变颜色UI
    private ChangeColorUI changeColorUI;
    //输入指令的输入框
    private InputField instructionInput;
    //执行完python文件后的返回指令
    private Text returnText;
    //确定按钮
    private Button okButton;
    //退出按钮
    private Button quitButton;

    //初始化
    public override void OnInit()
    {
        skinPath = "Panel/GamePanel";
        layer = PanelManager.Layer.Panel;
    }

    //显示
    public override void OnShow(params object[] args)
    {
        //寻找组件
        //操作按钮的初始化
        operation = new Toggle[4];
        operation[0] = skin.transform.Find("ToggleManager/Instantiation").GetComponent<Toggle>();
        operation[1] = skin.transform.Find("ToggleManager/Delete").GetComponent<Toggle>();
        operation[2] = skin.transform.Find("ToggleManager/Move").GetComponent<Toggle>();
        operation[3] = skin.transform.Find("ToggleManager/ChangeColor").GetComponent<Toggle>();
        //初始化物体的UI的初始化
        instantiationUI.x_input = skin.transform.Find("Instantiation/x-input").GetComponent<InputField>();
        instantiationUI.y_input = skin.transform.Find("Instantiation/y-input").GetComponent<InputField>();
        instantiationUI.drop = skin.transform.Find("Instantiation/Dropdown").GetComponent<Dropdown>();
        //删除物体的UI的初始化
        deleteUI.sequence_input = skin.transform.Find("Delete/SequenceInput").GetComponent<InputField>();
        deleteUI.drop = skin.transform.Find("Delete/Dropdown").GetComponent<Dropdown>();
        //移动物体的UI的初始化
        moveUI.x_input = skin.transform.Find("Move/x-input").GetComponent<InputField>();
        moveUI.y_input = skin.transform.Find("Move/y-input").GetComponent<InputField>();
        moveUI.seqence_input = skin.transform.Find("Move/SequenceInput").GetComponent<InputField>();
        moveUI.drop = skin.transform.Find("Move/Dropdown").GetComponent<Dropdown>();
        //改变颜色的UI的初始化
        changeColorUI.sequence_input = skin.transform.Find("ChangeColor/SequenceInput").GetComponent<InputField>();
        changeColorUI.red_input = skin.transform.Find("ChangeColor/RedInput").GetComponent<InputField>();
        changeColorUI.green_input = skin.transform.Find("ChangeColor/GreenInput").GetComponent<InputField>();
        changeColorUI.blue_input = skin.transform.Find("ChangeColor/BlueInput").GetComponent<InputField>();
        changeColorUI.alpha_input = skin.transform.Find("ChangeColor/AlphaInput").GetComponent<InputField>();
        changeColorUI.drop = skin.transform.Find("ChangeColor/Dropdown").GetComponent<Dropdown>();
        //执行完python文件后的返回指令
        returnText = skin.transform.Find("ReturnText").GetComponent<Text>();
        //输入指令的文本框
        instructionInput = skin.transform.Find("InstructionInput").GetComponent<InputField>();
        //确定按钮初始化
        okButton = skin.transform.Find("OkButton").GetComponent<Button>();
        okButton.onClick.AddListener(OnOkButtonClick);
        //退出按钮的初始化
        quitButton = skin.transform.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(OnQuitButtonClick);


        //添加按钮事件
        //createButton.onClick.AddListener(OnCreateClick);
        //添加协议监听
        //NetManager.AddMsgListener("MsgGetAchieve", OnMsgGetAchieve);
    }

    public void OnOkButtonClick()
    {
        if(instructionInput.text == "")
        {
            Order order = ExecuteButtonInstruction();
            if(order != null)
            {
                Gamedata.broker.TakeOrder(order);
            }
        }
        else
        {
            string text = instructionInput.text;
            if(text == "退出")
            {
                OnQuitButtonClick();
            }

            string[] strArr = new string[5];//参数列表
            //string output = PythonReader.RunCmd("conda activate&&python E:\\storePython\\clean_up2\\control.py 给我一张桌子");
            string sArguments = @"control.py";//这里是python的文件名字
            strArr[0] = text;
            strArr[1] = DeskManager.Instance.BianHao;
            strArr[2] = DeskManager.Instance.ZuoBiao;
            strArr[3] = ChairManager.Instance.BianHao;
            strArr[4] = ChairManager.Instance.ZuoBiao;

            PythonReader.RunPythonScript(sArguments, "-u", strArr);
            returnText.text = "";
            for (int i = 0; i < Gamedata.instructions.Count; i++)
            {
                returnText.text += Gamedata.instructions[i];
                returnText.text += "\n";
            }
            for(int i = 0; i < Gamedata.instructions.Count; i++)
            {
                string instruction = Gamedata.instructions[i];
                Debug.Log(instruction);
                Order order = ExecuteLanguageInstruction(instruction);
                if (order != null)
                {
                    Gamedata.broker.TakeOrder(order);
                }
            }
            Gamedata.instructions.Clear();
        }
    }

    public void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //执行自然语言的指令
    private Order ExecuteLanguageInstruction(string instruction)
    {
        string[] arr = instruction.Split(' ');
        try
        {
            string type = arr[arr.Length - 1];
            switch (type)
            {
                case "desk":
                    type = "桌子";
                    break;
                case "chair":
                    type = "椅子";
                    break;
            }
            switch (arr[0])
            {
                case "Instantiation":
                    string x = arr[1];
                    string y = arr[2];
                    Instantiation instantiation = ObjectFactory.InstantiationObject(type,x,y);
                    if (instantiation == null)
                    {
                        Debug.Log("instant");
                        throw new Exception();
                    }
                    return instantiation;
                case "Delete":
                    string num = arr[1];

                    Delete delete = ObjectFactory.DeleteObject(type,num);
                    if(delete == null)
                    {
                        throw new Exception();
                    }
                    return delete;
                case "Move":
                    string x_offset = arr[1];
                    string y_offset = arr[2];
                    num = arr[3];

                    Move move = ObjectFactory.MoveObject(type,num,x_offset,y_offset);
                    if(move == null)
                    {
                        throw new Exception();
                    }
                    return move;
                case "ChangeColor":
                    num = arr[1];
                    string red = arr[2];
                    string green = arr[3];
                    string blue = arr[4];
                    string alpha = arr[5];
                    ChangeColor changeColor = ObjectFactory.ChangeColorObject(type,num,red,green,blue,alpha);
                    if(changeColor == null)
                    {
                        throw new Exception();
                    }
                    return changeColor;
                case "exception":
                    throw new Exception();
                default:
                    return null;
            }
        }
        catch
        {
            PanelManager.Open<TipPanel>("请正确输入指令");
        }
        return null;
    }

    //执行按钮的指令
    private Order ExecuteButtonInstruction()
    {
        returnText.text = "没有指令输入";
        int index = -1;
        for (int i = 0; i < operation.Length; i++)
        {
            if (operation[i].isOn)
            {
                index = i;
                break;
            }
        }

        //区分不同种类的指令
        switch ((Operation)index)
        {
            case Operation.NOP:
                PanelManager.Open<TipPanel>("没有发出任何指令");
                return null;
            case Operation.Instantiation:
                string x_input = instantiationUI.x_input.text;
                string y_input = instantiationUI.y_input.text;
                string text = instantiationUI.drop.options[instantiationUI.drop.value].text;
                //根据text内容生成不同种类的工厂模式
                Instantiation instantiation = ObjectFactory.InstantiationObject(text, x_input, y_input);
                if (instantiation == null)
                {
                    PanelManager.Open<TipPanel>("输入不合法");
                }
                return instantiation;
            case Operation.Delete:
                string num_input = deleteUI.sequence_input.text;
                text = deleteUI.drop.options[deleteUI.drop.value].text;

                Delete delete = ObjectFactory.DeleteObject(text, num_input);
                if (delete == null)
                {
                    PanelManager.Open<TipPanel>("输入不合法");
                }
                return delete;
            case Operation.Move:
                x_input = moveUI.x_input.text;
                y_input = moveUI.y_input.text;
                num_input = moveUI.seqence_input.text;
                text = moveUI.drop.options[moveUI.drop.value].text;

                Move move = ObjectFactory.MoveObject(text, num_input, x_input, y_input);
                if (move == null)
                {
                    PanelManager.Open<TipPanel>("输入不合法");
                }
                return move;
            case Operation.ChangeColor:
                num_input = changeColorUI.sequence_input.text;
                string red_input = changeColorUI.red_input.text;
                string green_input = changeColorUI.green_input.text;
                string blue_input = changeColorUI.blue_input.text;
                string alpha_input = changeColorUI.alpha_input.text;
                text = changeColorUI.drop.options[changeColorUI.drop.value].text;

                ChangeColor changeColor = ObjectFactory.ChangeColorObject(text, num_input, red_input, green_input, blue_input,alpha_input);
                if (changeColor == null)
                {
                    PanelManager.Open<TipPanel>("输入不合法");
                }
                return changeColor;
            default:
                return null;
        }

    }

    //关闭
    public override void OnClose()
    {
        //关闭协议监听协议监听
        //NetManager.RemoveMsgListener("MsgGetAchieve", OnMsgGetAchieve);
    }

}

/// <summary>
/// operation这个数组每一个位置的含义
/// </summary>
public enum Operation
{
    NOP = -1,
    Instantiation = 0,
    Delete = 1,
    Move = 2,
    ChangeColor = 3
}
