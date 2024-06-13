using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBouncingBall
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Share = null;
        public virtual void Awake()
        {
            Share = this;

        }


        public Action CreatePanelAction;
    }
}