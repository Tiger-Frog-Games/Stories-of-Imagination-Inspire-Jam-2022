using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class DoorManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Animator frontDoor, childDoor, grandmaDoor;

        #endregion

        #region Unity Methods
        private void Start()
        {
            childDoor.SetTrigger("Door Open");
            grandmaDoor.SetTrigger("Door Open");
        }
        #endregion

        #region Methods

        public void openFrontDoor()
        {
            frontDoor.SetTrigger("Door Open");
            frontDoor.ResetTrigger("Door Close");
        }

        public void closeFrontDoor()
        {
            frontDoor.ResetTrigger("Door Open");
            frontDoor.SetTrigger("Door Close");
        }
        public void openchildDoor()
        {
            childDoor.SetTrigger("Door Open");
            childDoor.ResetTrigger("Door Close");
        }

        public void closechildDoor()
        {
            childDoor.ResetTrigger("Door Open");
            childDoor.SetTrigger("Door Close");
        }

        public void opengrandmaDoor()
        {
            grandmaDoor.SetTrigger("Door Open");
            grandmaDoor.ResetTrigger("Door Close");
        }

        public void closegrandmaDoor()
        {
            grandmaDoor.ResetTrigger("Door Open");
            grandmaDoor.SetTrigger("Door Close");
        }


        #endregion
    }
}