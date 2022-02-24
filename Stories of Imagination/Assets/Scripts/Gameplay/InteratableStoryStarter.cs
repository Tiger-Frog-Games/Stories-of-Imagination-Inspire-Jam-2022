using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class InteratableStoryStarter : Interactable
    {
        #region Variables
        [SerializeField] private StorySO storyToTell;
        [SerializeField] private EventChannelSOStory OnStoryStart;
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        protected override void ActiveInteract()
        {
            base.ActiveInteract();
            if (OnStoryStart != null)
            {
                OnStoryStart.RaiseEvent(storyToTell);
            }
        }

        protected override void DeActiveInteract()
        {
            base.DeActiveInteract();
        }

        #endregion
    }
}