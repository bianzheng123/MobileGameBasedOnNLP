using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InstructionHead
{
    /// <summary>
    /// 不同的指令类别，例如：生成，删除，换模型等等
    /// </summary>
    private string opcode;
    /// <summary>
    /// 不同的家具，例如：桌子，椅子等等
    /// </summary>
    private string type;

    public InstructionHead(string opcode,string type)
    {
        this.opcode = opcode;
        this.type = type;
    }

    public override bool Equals(object obj)
    {
        var head = obj as InstructionHead;
        return head != null &&
               opcode == head.opcode &&
               type == head.type;
    }

    public override int GetHashCode()
    {
        var hashCode = -1051948660;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(opcode);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(type);
        return hashCode;
    }
}

