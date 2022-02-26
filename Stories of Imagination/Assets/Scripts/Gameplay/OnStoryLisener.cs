using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoriesofImagination
{
    public class OnStoryLisener : MonoBehaviour
    {
        #region Variables

        [SerializeField] private StorySO storyToLisenTo;
        [SerializeField] private EventChannelSOStory OnPartOfStoryToRespondTo;
        [SerializeField] private UnityEvent eventOnLisen;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            OnPartOfStoryToRespondTo.OnEvent += OnPartOfStoryToRespondTo_OnEvent;
        }

        private void OnDisable()
        {
            OnPartOfStoryToRespondTo.OnEvent -= OnPartOfStoryToRespondTo_OnEvent;
        }
        #endregion

        #region Methods

        private void OnPartOfStoryToRespondTo_OnEvent(StorySO obj)
        {
            
            if (storyToLisenTo == obj)
            {
                eventOnLisen.Invoke();
            }
        }

        #endregion
    }
}