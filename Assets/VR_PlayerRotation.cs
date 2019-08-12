using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PlayerRotation : MonoBehaviour {

    public Transform player;
    public Camera vrCamera;

    private void Update()
    {
        RotatePlayer();
    }

    void RotatePlayer()
    {
        Vector3 paperResetPos = new Vector3(0f, vrCamera.transform.eulerAngles.y, 0f);
        player.rotation = Quaternion.Euler(paperResetPos);
    }
}
