using System;
using UnityEngine;

public class EventManager
{

    public Action<Vector3, float> LineSimulateAction;
    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    private EventManager()
    {
    }
}