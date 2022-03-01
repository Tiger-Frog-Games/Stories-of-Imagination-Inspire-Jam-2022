using Micosmo.SensorToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StoriesofImagination
{
    public class PlayerInteractor : MonoBehaviour
    {
        #region Variables

        [SerializeField] private InputActionAsset actions;
        private InputAction interactButton;

        [SerializeField] private RaySensor sensor;

        [SerializeField] private EventChannelInteratable OnHover;
        [SerializeField] private EventChannelInteratable OnHoverLost;

        private Interactable currentHoveredInteract;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            sensor.OnDetected.AddListener(OnDetectedInteratable);
            sensor.OnLostDetection.AddListener(OnLostInteratable);

            interactButton = actions.FindAction("InteractWithObject");
            if (interactButton != null)
            { 
                interactButton.started += interactButtonPress;
            }

            interactButton.Enable();
        }

        private void OnDestroy()
        {
            sensor.OnDetected.RemoveListener(OnDetectedInteratable);
            sensor.OnLostDetection.RemoveListener(OnLostInteratable);

            if (interactButton != null)
            {
                interactButton.started -= interactButtonPress;
            }
        }

        #endregion

        #region Methods

        private void interactButtonPress(InputAction.CallbackContext obj)
        {
            if ( currentHoveredInteract != null && GameStateManager.Instance.CurrentGameState == GameState.Gameplay )
            {

                currentHoveredInteract.Interact();
            }
        }

        private void OnDetectedInteratable(GameObject detectedObject, Micosmo.SensorToolkit.Sensor sensor)
        {
            foreach(Interactable interact in detectedObject.GetComponentsInChildren<Interactable>())
            {
                //find the first interatable that is enabled
                if (interact.enabled == true)
                {
                    currentHoveredInteract = interact;
                    OnHover.RaiseEvent(currentHoveredInteract);
                    break;
                }
            }

            //if(detectedObject.TryGetComponent<Interactable>(out Interactable interact))
            //{
            //    currentHoveredInteract = interact;
            //    OnHover.RaiseEvent(currentHoveredInteract);
            //}
                
        }

        private void OnLostInteratable(GameObject detectedObject, Micosmo.SensorToolkit.Sensor sensor)
        {
            if (currentHoveredInteract == null)
            {
                return;
            }

            foreach (Interactable interact in detectedObject.GetComponentsInChildren<Interactable>())
            {
                if (interact == currentHoveredInteract)
                {
                    OnHoverLost.RaiseEvent(currentHoveredInteract);

                    currentHoveredInteract = null;
                    return;
                }
            }
            
        }

        



        #endregion
    }
}