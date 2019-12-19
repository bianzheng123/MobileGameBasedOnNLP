using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public static class NetManager
{
    private static Socket socket;
    private static byte[] sendBuffer;
    private static byte[] receiveBuffer;
    private const int DEFAULT_SIZE = 1024;

    public static void Connect(string ip,int port)
    {
        if(socket != null && socket.Connected)
        {
            Debug.Log("Connect fail, already connected!");
            return;
        }
        InitState();
        socket.NoDelay = true;
        socket.BeginConnect(ip,port,ConnectCallback,socket);
    }

    //Connect回调
    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Succ ");
            //开始接收
            socket.BeginReceive(receiveBuffer, 0, DEFAULT_SIZE, 0, ReceiveCallback, socket);

        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Connect fail " + ex.ToString());
        }
    }

    private static void InitState()
    {
        socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        sendBuffer = new byte[DEFAULT_SIZE];
        receiveBuffer = new byte[DEFAULT_SIZE];
    }

    //关闭连接
    public static void Close()
    {
        //状态判断
        if (socket == null || !socket.Connected)
        {
            return;
        }
        //没有数据在发送
        socket.Close();
    }

    //发送数据
    public static void Send(string str)
    {
        //状态判断
        if (socket == null || !socket.Connected)
        {
            return;
        }

        byte[] array = System.Text.Encoding.Default.GetBytes(str);

        Array.Copy(array,0,sendBuffer,0,str.Length);
        //Debug.Log(sendBuffer.Length);

        socket.BeginSend(sendBuffer, 0, sendBuffer.Length, 0, SendCallback, socket);

    }

    

    //Send回调
    public static void SendCallback(IAsyncResult ar)
    {

        //获取state、EndSend的处理
        Socket socket = (Socket)ar.AsyncState;
        //状态判断
        if (socket == null || !socket.Connected)
        {
            return;
        }
        Debug.Log("send successfully");
        //EndSend
        int count = socket.EndSend(ar);
    }


    private static string TrimEnd()
    {
        for(int i = 0; i < DEFAULT_SIZE; i++)
        {
            if(receiveBuffer[i] == '\0')
            {
                return System.Text.Encoding.UTF8.GetString(receiveBuffer,0,i);
            }
        }
        return null;
    }

    //Receive回调
    public static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            //获取接收数据长度
            int count = socket.EndReceive(ar);
            string str = TrimEnd();
            if(str == null)
            {
                throw new Exception("接收缓冲区长度不够，需要扩容");
            }
            Gamedata.instructions.Add(str);

            Array.Clear(receiveBuffer,0,DEFAULT_SIZE);
            socket.BeginReceive(receiveBuffer, 0, DEFAULT_SIZE, 0, ReceiveCallback, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Receive fail" + ex.ToString());
        }
    }

}
