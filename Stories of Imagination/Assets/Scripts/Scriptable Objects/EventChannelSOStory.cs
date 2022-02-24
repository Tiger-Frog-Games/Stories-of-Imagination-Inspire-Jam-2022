using StoriesofImagination;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannelBase/Story")]
public class EventChannelSOStory : ScriptableObject
{
    public event Action<StorySO> OnEvent;

    public void RaiseEvent(StorySO i)
    {
        OnEvent?.Invoke(i);
    }

    public int GetNumberOfLiseners()
    {
        return OnEvent.GetInvocationList().Length;
    }

}