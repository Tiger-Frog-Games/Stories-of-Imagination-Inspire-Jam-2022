using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class InteratableStoryStarter : Interactable
    {
        #region Variables
        [SerializeField] private STORYTYPE storyToTell;
        [SerializeField] private EventChannelSOStory OnStoryStart;
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        protected override void ActiveInteract()
        {
            print("meow mix!");
            base.ActiveInteract();
            if (OnStoryStart != null)
            {
                OnStoryStart.RaiseEvent(storyToTell);
            }
        }

        protected override void DeActiveInteract()
        {
            print("meow mix?");
            base.DeActiveInteract();
        }

        #endregion
    }
}