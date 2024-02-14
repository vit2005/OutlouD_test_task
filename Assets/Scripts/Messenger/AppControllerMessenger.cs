using System;
using System.Collections;
using System.Collections.Generic;
using TinyMessenger;
using Unity.VisualScripting;
using UnityEngine;

public class AppControllerMessenger 
{
    protected TinyMessengerHub hub;
    public static AppControllerMessenger instance;

    public AppControllerMessenger()
    {
        instance = this;
        hub = new TinyMessengerHub();
    }

    public TinyMessageSubscriptionToken Subscribe<TMessage>(Action<TMessage> handler)
      where TMessage : class, ITinyMessage
    {
        return hub.Subscribe(handler);
    }

    public void Publish<TMessage>(TMessage message)
      where TMessage : class, ITinyMessage
    {
        hub.Publish(message);
    }

    public void Unsubscribe<TMessage>(TinyMessageSubscriptionToken token)
      where TMessage : class, ITinyMessage
    {
        hub.Unsubscribe<TMessage>(token);
    }
}
