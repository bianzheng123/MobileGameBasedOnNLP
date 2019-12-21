using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : Order
{
    private ObjectManager instructionObject;
    private int index;
    public Delete(ObjectManager instructionObject,int index)
    {
        this.index = index;
        this.instructionObject = instructionObject;
    }
    public void Execute()
    {
        instructionObject.Delete(index);
    }
}
