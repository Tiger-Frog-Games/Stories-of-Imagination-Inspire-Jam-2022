using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class Interactable : MonoBehaviour
    {
        #region Variables
        [SerializeField] private string displayName;

        [SerializeField] public bool canOnlyActivateOnce = false;
        private bool isActivated = false;
        
        [SerializeField] public bool displayNameOnHover = true;

        [SerializeField] private bool isInteratable = true;

        [SerializeField] private EventChannelSO OnInteractEvent;
        [SerializeField] private EventChannelSO OnDeInteractEvent;

        private bool isActiveInActiveState = false;
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public string getDisplayName()
        {
            return displayName;
        }

        private float lastActivated;
        private float coolDown = .2f;
        public void Interact()
        {
            if(isInteratable == false || (canOnlyActivateOnce == true && isActivated == true)  || lastActivated + coolDown > Time.time )
            {
                return;
            }

            isActivated = true; 

            if (isActiveInActiveState == false)
            {
                ActiveInteract();
            }
            else
            {
                DeActiveInteract();
            }

            isActiveInActiveState = !isActiveInActiveState;
            lastActivated = Time.time;
        }

        protected virtual void ActiveInteract()
        {
            if (OnInteractEvent != null)
            {
                OnInteractEvent.RaiseEvent();
            }
        }

        protected virtual void DeActiveInteract()
        {
            if (OnDeInteractEvent != null)
            {
                OnDeInteractEvent.RaiseEvent();
            }
        }

        #endregion
    }
}