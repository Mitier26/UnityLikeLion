using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MessageTypeNotifyInCountArea
{
    public InCountArea InCountArea;
    public Collider other;
}

public class InCountArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MessageTypeNotifyInCountArea notifyInCountArea = new MessageTypeNotifyInCountArea();
        notifyInCountArea.InCountArea = this;
        notifyInCountArea.other = other;
        EventManager.Instance.PushMessage(notifyInCountArea);
    }
}