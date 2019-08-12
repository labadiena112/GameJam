using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PlayerMovement : MonoBehaviour {

    public Transform target;
    CharacterController charControler;
    public static float walkSpeed = 4f;
    public static float runSpeed = 8f;

    float playerspeedGlobal = 5f;

    public Vector3 moveDirSide;
    public Vector3 moveDirForward;

    void Awake()
    {
        charControler = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            MovePlayer();
        }
        else
        {
            IdlePlayer();
        }
    }

    void IdlePlayer()
    {
        moveDirForward = transform.forward * 0;
        moveDirSide = transform.right * 0;
        charControler.SimpleMove(moveDirSide);
        charControler.SimpleMove(moveDirForward);
    }

    void MovePlayer()
    {
        // horizontalus inputas
        float h = Input.GetAxis("Horizontal");
        // vertikalus inputas
        float v = Input.GetAxis("Vertical");

        // judeti pagal raudona playerio rodykle - visada i sona
        moveDirSide = transform.right * h * playerspeedGlobal;
        // judeti pagal melyna playerio rodykle - visada i tiesiai
        moveDirForward = transform.forward * v * playerspeedGlobal;

        // movinamas zaidejas sono vektoriumi pagal greiti
        charControler.SimpleMove(moveDirSide);
        // movinamas zaidejas tiesiai vektoriumi pagal greiti
        charControler.SimpleMove(moveDirForward);

        // ne/begti
        //if (Input.GetButtonDown("Sprint"))
        //    playerspeedGlobal = VR_PlayerMovement.runSpeed;
        //else
        //    playerspeedGlobal = VR_PlayerMovement.walkSpeed;
    }
}
