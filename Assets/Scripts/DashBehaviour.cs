using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DashBehaviour : MonoBehaviour
{
    private playerMovementBehaviour player;
    public float dashForce = 10f;

    void Start()
    {
        player = playerMovementBehaviour.instance;
        Vector2 playerInput = player.MovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
