using StoriesofImagination;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannelBase/StoryLine")]
public class EventChannelStoryLine : ScriptableObject
{
    // story line
    // percent complete of story
    public event Action<StoryLine,int,float> OnEvent;

    public void RaiseEvent(StoryLine storyLine, int lineNumber, float percentageComplete)
    {
        OnEvent?.Invoke(storyLine, lineNumber, percentageComplete);
    }

    public int GetNumberOfLiseners()
    {
        return OnEvent.GetInvocationList().Length;
    }

}