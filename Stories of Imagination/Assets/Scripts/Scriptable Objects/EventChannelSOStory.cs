using StoriesofImagination;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannelBase/Story")]
public class EventChannelSOStory : ScriptableObject
{
    public event Action<STORYTYPE> OnEvent;

    public void RaiseEvent(STORYTYPE i)
    {
        OnEvent?.Invoke(i);
    }

    public int GetNumberOfLiseners()
    {
        return OnEvent.GetInvocationList().Length;
    }

}