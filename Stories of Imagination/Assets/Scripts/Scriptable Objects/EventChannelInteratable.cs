using System;
using UnityEngine;

namespace StoriesofImagination
{
[CreateAssetMenu(menuName = "EventChannelBase/Interatable")]
    public class EventChannelInteratable : ScriptableObject
    {
        public event Action<Interactable> OnEvent;

        public void RaiseEvent(Interactable i)
        {
            OnEvent?.Invoke(i);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }

    }
}
