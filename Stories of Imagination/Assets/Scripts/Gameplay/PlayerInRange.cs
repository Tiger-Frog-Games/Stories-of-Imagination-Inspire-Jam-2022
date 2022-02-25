using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class PlayerInRange : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform playerLookTransform;
        [SerializeField] private Transform PlayerAncor;
        private Vector3 startPosition;
        #endregion

        #region Unity Methods

        private void Start()
        {
            startPosition = new Vector3();
            startPosition = playerLookTransform.position;
        }

        #endregion

        #region Methods

        public void setLookTargetToPlayer()
        {
            playerLookTransform.parent = PlayerAncor;
            playerLookTransform.localPosition = Vector3.zero;
        }

        public void removeLookTarget()
        {
            playerLookTransform.parent = null;
            playerLookTransform.position = startPosition;
        }

        #endregion
    }
}