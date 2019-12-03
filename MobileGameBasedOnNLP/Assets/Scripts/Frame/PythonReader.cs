using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Text;

public static class PythonReader
{
    //调用python核心代码
    public static void RunPythonScript(string sArgName, string args = "", params string[] teps)
    {
        Process p = new Process();
        string path = @"E:\storePython\pure_re\" + sArgName;
        //path = @"C:\Users\19239\Desktop\test\" + sArgName;//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
        p.StartInfo.FileName = @"C:\Users\BianZheng\AppData\Local\Programs\Python\Python38-32\python.exe";//(注意：用的话需要换成自己的)没有配环境变量的话，可以像我这样写python.exe的绝对路径(用的话需要换成自己的)。如果配了，直接写"python.exe"即可
        string sArguments = path;
        foreach (string sigstr in teps)
        {
            sArguments += " " + sigstr;//传递参数
        }

        sArguments += " " + args;
        UnityEngine.Debug.Log(sArguments);
        p.StartInfo.Arguments = sArguments;

        p.StartInfo.UseShellExecute = false;

        p.StartInfo.RedirectStandardOutput = true;

        p.StartInfo.RedirectStandardInput = true;

        p.StartInfo.RedirectStandardError = true;

        p.StartInfo.CreateNoWindow = true;

        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
        p.WaitForExit();
    }

    private static string GB2312ToUTF8(string str)
    {
        try
        {
            Encoding uft8 = Encoding.GetEncoding(65001);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            byte[] temp = gb2312.GetBytes(str);
            byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
            string result = uft8.GetString(temp1);
            return result;
        }
        catch (Exception ex)//(UnsupportedEncodingException ex)
        {
            return null;
        }
    }

    //输出打印的信息
    private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            UnityEngine.Debug.Log(e.Data);
            Gamedata.instructions.Add(e.Data);
        }

    }

}
