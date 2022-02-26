using ECM2.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class HeadBob : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CharacterMovement cm;

        [SerializeField] private float headBobRate;
        [SerializeField] private float headBobDistance;

        private float startPos;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;

        }

        private void OnDestroy()
        {

            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        #endregion

        #region Methods
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay);
        }
        private void Start()
        {
            startPos = this.transform.localPosition.y;
            helper = new Vector3(0,startPos,0);
        }

        private void FixedUpdate()
        {
            if (cm.velocity.magnitude > .1f)
            {
                float vel = cm.velocity.magnitude;
                helper.y = startPos + (Mathf.Sin(Time.time * headBobRate * vel) * headBobDistance);
            }
            else
            {
                helper.y = startPos;
            }
            
        }

        private void Update()
        {
             UpdateHeadPosition();
        }

        #endregion

        #region Methods

        Vector3 helper;
        private void UpdateHeadPosition()
        {
            

            //Vector3 target;

            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, helper, Time.deltaTime) ;
        }

        #endregion
    }
}