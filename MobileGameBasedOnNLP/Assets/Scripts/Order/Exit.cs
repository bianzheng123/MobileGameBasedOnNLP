using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Order
{
    public Exit()
    {

    }
    public void Execute()
    {
        SystemOperation.Quit();
    }
}
