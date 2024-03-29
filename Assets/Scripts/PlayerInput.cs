﻿using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    [Header("Ragdoll Movement Toggle")]
    public bool isSmooth = false;

    [Header("Run Button")]
    public KeyCode acceleration = KeyCode.LeftShift;

    private Vector2 input = Vector2.zero;

    private CharacterMovement cm;

    private void Start()
    {
        cm = GetComponent<CharacterMovement>();
    }

    private void FixedUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        cm.Move(input, Input.GetKey(acceleration), isSmooth);
    }
}
