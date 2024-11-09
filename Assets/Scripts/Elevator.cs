using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform elevatorTop;
    [SerializeField] private Transform elevatorBottom;
    [SerializeField] private float elevatorSpeed = 0.3f;
    [SerializeField] private float waitTime = 1.5f;

    private float elevatorWaitCountdown;
    private bool movingUp = true;

    private void Start()
    {
        elevatorWaitCountdown = waitTime;
    }

    private void FixedUpdate()
    {
        if (elevatorWaitCountdown > 0)
        {
            if (platform.position.y > elevatorTop.position.y)
            {
                movingUp = false;
                elevatorWaitCountdown -= Time.deltaTime;
                return;
            }

            if (platform.position.y < elevatorBottom.position.y)
            {
                movingUp = true;
                elevatorWaitCountdown -= Time.deltaTime;
                return;
            }
        }
        else
        {
            elevatorWaitCountdown = waitTime;
        }

        platform.transform.position = movingUp
            ? new Vector2(transform.position.x,
                platform.transform.position.y + elevatorSpeed)
            : new Vector2(transform.position.x,
                platform.transform.position.y - elevatorSpeed);
    }
}