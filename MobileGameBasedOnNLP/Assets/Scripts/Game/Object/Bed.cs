using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : BaseObject
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        offset[0] = 0;
        offset[1] = 0;
    }

    private void OnGUI()
    {
        PaintLabel();
    }
}
