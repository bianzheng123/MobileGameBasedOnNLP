using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InstructionException : Order
{
    public InstructionException()
    {

    }
    public void Execute()
    {
        PanelManager.Open<TipPanel>("指令出现错误");
    }
}
