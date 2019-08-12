using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForScience.StickyCamera
{
    public class VR_StickyCamera : MonoBehaviour
    {

        public Transform target;

        void Update()
        {
            transform.position = target.position + new Vector3(0f, 0.8f, 0f);
        }
    }
}
