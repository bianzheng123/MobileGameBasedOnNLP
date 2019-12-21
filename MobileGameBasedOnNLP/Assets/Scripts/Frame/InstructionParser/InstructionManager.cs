using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class InstructionManager
{
    //消息委托类型
    public delegate Order InstructionListener(string instruction);
    //消息监听列表
    private static Dictionary<InstructionHead, InstructionListener> instructionListeners = new Dictionary<InstructionHead, InstructionListener>();
    //添加消息监听
    public static void AddInstructionListener(InstructionHead instructionHead, InstructionListener listener)
    {
        //添加
        if (instructionListeners.ContainsKey(instructionHead))
        {
            instructionListeners[instructionHead] += listener;
        }
        //新增
        else
        {
            instructionListeners[instructionHead] = listener;
        }
    }
    //删除消息监听
    public static void RemoveInstructionListener(InstructionHead instructionHead, InstructionListener listener)
    {
        if (instructionListeners.ContainsKey(instructionHead))
        {
            instructionListeners[instructionHead] -= listener;
            if (instructionListeners[instructionHead] == null)
            {
                instructionListeners.Remove(instructionHead);
            }

        }
    }
    //分发消息
    public static Order FireInstruction(string instruction)
    {
        string[] arr = instruction.Split(' ');
        string type = arr[arr.Length - 1];
        if(arr.Length == 1)//是退出指令或者报错指令
        {
            type = "";
        }
        string opcode = arr[0];
        InstructionHead instructionHead = new InstructionHead(opcode,type);
        if (instructionListeners.ContainsKey(instructionHead) && instructionListeners[instructionHead] != null)
        {
            return instructionListeners[instructionHead](instruction);
        }
        else
        {
            PanelManager.Open<TipPanel>("接收到的指令不在内容中");
        }
        return null;
    }
}