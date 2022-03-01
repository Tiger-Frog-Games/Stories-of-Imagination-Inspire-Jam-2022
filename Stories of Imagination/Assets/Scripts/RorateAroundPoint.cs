using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoriesofImagination
{
    public class RorateAroundPoint : MonoBehaviour
    {
        #region Variables
        //Assign a GameObject in the Inspector to rotate around
        [SerializeField] private GameObject target;
        [SerializeField] private float speed = 20;

        #endregion

        #region Unity Methods
        void Update()
        {
            // Spin the object around the target at 20 degrees/second.
            transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        #endregion

        
    }
}