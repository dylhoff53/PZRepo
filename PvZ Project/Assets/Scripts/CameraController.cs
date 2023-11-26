using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Vector2 movement;
    public float lastInput;
    public float camMoveSpeed;
    public float acceleration;
    public float accelerationInterval;
    public bool isAccel;
    public float scrollInput;


    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }


    public void OnScroll(InputAction.CallbackContext context)
    {
        scrollInput = context.ReadValue<float>();
        Debug.Log(scrollInput);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    public void CameraMovement()
    {

        float currentInput = movement.x;

        if (lastInput == 0 && currentInput != 0)
        {
            isAccel = true;
        } 


        if(isAccel == true)
        {
            acceleration += accelerationInterval;
            if (acceleration > camMoveSpeed)
            {
                acceleration = camMoveSpeed;
            }
        }

        if (currentInput > 0f && transform.position.x < 5f)
        {
            currentInput = 1f;
        }
        else if (currentInput < 0 && transform.position.x > 0)
        {
            currentInput = -1f;
        }else
        {
            currentInput = 0f;
        }

        if (currentInput == 0)
        {
            acceleration = 0;
            isAccel = false;
        }

        Vector3 move = new Vector3(currentInput, 0, 0);
        transform.Translate(move * (camMoveSpeed + acceleration) * Time.deltaTime, Space.World);
        lastInput = currentInput;

    }
}
