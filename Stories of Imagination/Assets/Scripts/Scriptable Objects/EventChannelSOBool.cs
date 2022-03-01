using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannelBase/Bool")]
public class EventChannelSOBool : ScriptableObject
{
    public event Action<bool> OnEvent;

    public void RaiseEvent(bool i)
    {
        OnEvent?.Invoke(i);
    }

    public int GetNumberOfLiseners()
    {
        return OnEvent.GetInvocationList().Length;
    }

}
