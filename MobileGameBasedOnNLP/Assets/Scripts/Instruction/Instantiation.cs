using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : Order
{
    private ObjectManager instructionObject;
    private Vector3 position;
    public Instantiation(ObjectManager instructionObject,Vector3 position)
    {
        this.instructionObject = instructionObject;
        this.position = position;
    }
    public void Execute()
    {
        instructionObject.Instantiation(position);
    }
}
