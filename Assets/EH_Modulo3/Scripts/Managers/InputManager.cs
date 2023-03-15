using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input Names")]
    [SerializeField] private string horizontalMoveName = "HorizontalMove";
    [SerializeField] private string verticalMoveName = "VerticalMove";
    [SerializeField] private string horizontalLookName = "HorizontalLook";
    [SerializeField] private string verticalLookName = "VerticalLook";
    [SerializeField] private string jumpName = "VerticalLook";
    [SerializeField] private string dashName = "VerticalLook";
    [SerializeField] private string fireName = "Fire";
    [SerializeField] private string targetLockName = "Fire";

    public static bool ENABLED = true;

    public static float HORIZONTALMOVE;
    public static float VERTICALMOVE;
    public static float HORIZONTALLOOK;
    public static float VERTICALLOOK;
    public static bool JUMP;
    public static bool DASH;
    public static bool FIRE;
    public static bool TARGETLOCK;

    private Player playerControls;


    private void Awake()
    {
        playerControls = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (ENABLED)
        {
            // Axis
            HORIZONTALMOVE = playerControls.GetAxis(horizontalMoveName);
            VERTICALMOVE = playerControls.GetAxis(verticalMoveName);
            HORIZONTALLOOK = playerControls.GetAxis(horizontalLookName);
            VERTICALLOOK = playerControls.GetAxis(verticalLookName);

            //Buttons
            JUMP = playerControls.GetButtonDown(jumpName);
            DASH = playerControls.GetButtonDown(dashName);
            TARGETLOCK = playerControls.GetButtonDown(targetLockName);

            // Buttons repeat
            FIRE = playerControls.GetButton(fireName);
        }
    }
}
