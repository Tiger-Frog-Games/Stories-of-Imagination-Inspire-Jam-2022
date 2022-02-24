using StoriesofImagination;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannelBase/StoryLine")]
public class EventChannelStoryLine : ScriptableObject
{
    // story line
    // percent complete of story
    public event Action<LINEREADERS, string, int, float> OnEvent;

    public void RaiseEvent(LINEREADERS reader, string line, int lineNumber, float percentageComplete)
    {
        OnEvent?.Invoke(reader, line, lineNumber, percentageComplete);
    }

    public int GetNumberOfLiseners()
    {
        return OnEvent.GetInvocationList().Length;
    }

}