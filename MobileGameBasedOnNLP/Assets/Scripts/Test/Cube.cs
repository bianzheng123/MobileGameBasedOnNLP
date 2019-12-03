using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Renderer render;
    // Start is called before the first frame update
    void Start()
    {
       render = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            render.material.color = new Color(0.9f,0,0);

            string[] strArr = new string[2];//参数列表
            string sArguments = @"Instruction.py";//这里是python的文件名字
            strArr[0] = "2";
            strArr[1] = "3";
            PythonReader.RunPythonScript(sArguments,"-u",strArr);
        }
    }
}
