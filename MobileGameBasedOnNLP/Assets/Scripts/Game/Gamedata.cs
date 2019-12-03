using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gamedata
{
    public static List<string> instructions;

    public static Broker broker;

    public static void Init()
    {
        broker = new Broker();
        instructions = new List<string>();
    }

    public static void Update()
    {
        broker.PlaceOrders();
    }
}
