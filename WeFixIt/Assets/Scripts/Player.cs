﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Properties")]
    private int id;

    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;
    public float breakSpeed;
    private Vector3 speed;

    [Header("Rotation")]
    public float maxRotationSpeed;
    private Vector3 direction;

    private void Awake()
    {
        id = 0;
        InputManager.Initialize();
        speed = Vector3.zero;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        bool up = InputManager.GetKey(id, KeyActions.Up);
        bool down = InputManager.GetKey(id, KeyActions.Down);
        bool left = InputManager.GetKey(id, KeyActions.Left);
        bool right = InputManager.GetKey(id, KeyActions.Right);

        if (up)
        {
            direction.z = 1;
            if (!left && !right)
            {
                direction.x = 0;
            }
        }
        else if (down)
        {
            direction.z = -1;
            if (!left && !right)
            {
                direction.x = 0;
            }
        }

        if (left)
        {
            direction.x = -1;
            if (!up && !down)
            {
                direction.z = 0;
            }
        }
        else if (right)
        {
            direction.x = 1;
            if (!up && !down)
            {
                direction.z = 0;
            }
        }

        // X Axis
        if (right && speed.x < maxSpeed) // Going Right
        {
            if (speed.x >= 0) // Already going right
            {
                speed.x += acceleration * Time.deltaTime;
                if (speed.x > maxSpeed)
                {
                    speed.x = maxSpeed;
                }
            }
            else // Switching direction
            {
                speed.x += (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else if (left && speed.x > -maxSpeed) // Going Left
        {
            if (speed.x <= 0) // Already going left
            {
                speed.x -= acceleration * Time.deltaTime;
                if (speed.x < -maxSpeed)
                {
                    speed.x = -maxSpeed;
                }
            }
            else // Switching direction
            {
                speed.x -= (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else
        {
            if (speed.x > 0)
            {
                speed.x -= breakSpeed * Time.deltaTime;
                if (speed.x < 0)
                {
                    speed.x = 0;
                }
            }
            else if (speed.x < 0)
            {
                speed.x += breakSpeed * Time.deltaTime;
                if (speed.x > 0)
                {
                    speed.x = 0;
                }
            }
        }

        // Z Axis
        if (up && speed.z < maxSpeed) // Going Up
        {
            if (speed.z >= 0)
            {
                speed.z += acceleration * Time.deltaTime;
                if (speed.z > maxSpeed)
                {
                    speed.z = maxSpeed;
                }
            }
            else
            {
                speed.z += (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else if (down && speed.z > -maxSpeed) // Going Back
        {
            if (speed.z <= 0)
            {
                speed.z -= acceleration * Time.deltaTime;
                if (speed.z < -maxSpeed)
                {
                    speed.z = -maxSpeed;
                }
            }
            else
            {
                speed.z -= (acceleration + breakSpeed) * Time.deltaTime;
            }
        }
        else
        {
            if (speed.z > 0)
            {
                speed.z -= breakSpeed * Time.deltaTime;
                if (speed.z < 0)
                {
                    speed.z = 0;
                }
            }
            else if (speed.z < 0)
            {
                speed.z += breakSpeed * Time.deltaTime;
                if (speed.z > 0)
                {
                    speed.z = 0;
                }
            }
        }

        transform.Translate(transform.InverseTransformDirection(speed) * Time.deltaTime);

        Vector3 newFacingDirection = Vector3.RotateTowards(transform.forward, direction, Mathf.Deg2Rad * maxRotationSpeed, Mathf.Infinity);
        transform.forward = newFacingDirection;
    }

    public int getId()
    {
        return id;
    }
}