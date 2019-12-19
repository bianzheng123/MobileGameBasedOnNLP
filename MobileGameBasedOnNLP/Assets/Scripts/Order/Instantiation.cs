using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : Order
{
    private ObjectManager instructionObject;
    private Vector3 position;
    private int prefabIndex;
    private Vector3 rotation;
    public Instantiation(ObjectManager instructionObject,Vector3 position,int prefabIndex,Vector3 rotation)
    {
        this.instructionObject = instructionObject;
        this.position = position;
        this.prefabIndex = prefabIndex;
        this.rotation = rotation;
    }
    public void Execute()
    {
        instructionObject.Instantiation(position,prefabIndex,rotation);
    }
}
