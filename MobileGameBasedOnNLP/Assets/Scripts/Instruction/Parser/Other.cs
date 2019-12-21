using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class InstructionParser
{
    private static Order Exit(string instruction)
    {
        return new Exit();
    }

    private static Order Exception(string instruction)
    {
        return new InstructionException();
    }
}
