using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static Debugger instance;
    
    public TMPro.TMP_Text[] debugTextObjects;

    List<DebugMessage> debugMessage = new List<DebugMessage>();
    
    private void Awake()
    {
        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this );
        }
    }

    private void Update()
    {
        if (GameManager.instance.debugActive)
        {
            int indexer = 0;
            
            foreach (DebugMessage item in debugMessage)
            {
                debugTextObjects[indexer].text = item.prefix + item.message.ToString();
                indexer++;
            }
        }
    }

    public void Log(string prefix, object message)
    {
        if (!GameManager.instance.debugActive)
        {
            return;
        }
        
        foreach (DebugMessage item in debugMessage)
        {
            if (item.prefix == prefix)
            {
                item.message = message;
                return;
            }   
        }

        DebugMessage newMessage = new DebugMessage();
        newMessage.prefix = prefix;
        newMessage.message = message;
        
        debugMessage.Add(newMessage);
    }

    private class DebugMessage
    {
        public string prefix;
        public object message;
    }
}
