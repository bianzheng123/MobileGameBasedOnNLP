using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : Order
{
    private ObjectManager instructionObject;
    private int index;
    private int red;
    private int green;
    private int blue;
    private int alpha;
    public ChangeColor(ObjectManager instructionObject, int index,int red,int green,int blue,int alpha)
    {
        this.red = red;
        this.green = green;
        this.blue = blue;
        this.index = index;
        this.alpha = alpha;
        this.instructionObject = instructionObject;
    }
    public void Execute()
    {
        instructionObject.ChangeColor(index,red,green,blue,alpha);
    }
}
