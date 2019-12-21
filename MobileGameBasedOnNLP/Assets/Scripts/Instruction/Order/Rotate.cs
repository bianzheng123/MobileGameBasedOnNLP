using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Order
{
    private ObjectManager instructionObject;
    private int gameobjectIndex;
    private int rotateNum;
    public Rotate(ObjectManager instructionObject, int gameobjectIndex,int rotateNum)
    {
        this.instructionObject = instructionObject;
        this.gameobjectIndex = gameobjectIndex;
        this.rotateNum = rotateNum;
    }
    public void Execute()
    {
        instructionObject.Rotate(gameobjectIndex, rotateNum);
    }
}
