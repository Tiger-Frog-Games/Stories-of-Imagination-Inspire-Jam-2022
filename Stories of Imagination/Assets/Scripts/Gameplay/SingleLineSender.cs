using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class SingleLineSender : Interactable
    {
        #region Variables

        [SerializeField] private StoryLine lineToTell;
        [SerializeField] private EventChannelStoryLine OnSendSingleLine;

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        protected override void ActiveInteract()
        {
            base.ActiveInteract();
            sendLine();
        }

        protected override void DeActiveInteract()
        {
            base.DeActiveInteract();
            sendLine();
        }

        private void sendLine()
        {
            if (OnSendSingleLine != null)
            {
                OnSendSingleLine.RaiseEvent(lineToTell, 0, 1);
            }
        }

        public void changeLine(string newLineIn)
        {
            lineToTell.changeLine(newLineIn);
        }

        #endregion
    }
}