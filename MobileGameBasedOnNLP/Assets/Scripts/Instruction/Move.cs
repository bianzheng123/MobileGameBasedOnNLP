using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Order
{
    private ObjectManager instructionObject;
    private Vector3 position;
    private int index;
    public Move(ObjectManager instructionObject, Vector3 position,int index)
    {
        this.instructionObject = instructionObject;
        this.position = position;
        this.index = index;
    }
    public void Execute()
    {
        instructionObject.Move(position,index);
    }
}
