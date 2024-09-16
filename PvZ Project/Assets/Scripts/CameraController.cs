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


    public Node currentNode;
    public Node SelectedNode;
    public BuildManager buildManager;
    public LayerMask nodeLayerMask;


    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }


    public void OnScroll(InputAction.CallbackContext context)
    {
        scrollInput = context.ReadValue<float>();
        Debug.Log(scrollInput);
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (currentNode != null)
        {
            currentNode.MouseDownCheck();
            currentNode = null;
        }
    }

    public void OnRightMouseClick(InputAction.CallbackContext context)
    {
        if( BuildManager.selectedAbility != null || BuildManager.turretToBuild != null)
        {
            BuildManager.selectedAbility = null;
            BuildManager.turretToBuild = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(buildManager.CanBuild || BuildManager.selectedAbility != null)
        {
            NodeCheck();
        }
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

        if (currentInput > 0f && transform.position.x < 20f)
        {
            currentInput = 1f;
        }
        else if (currentInput < 0 && transform.position.x > 6.5f)
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

    public void NodeCheck()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500f, nodeLayerMask)) {
            SelectedNode = hit.collider.GetComponent<Node>();
            if (SelectedNode != null) {
                if (SelectedNode == currentNode)
                {
                    return;
                }
                else if (SelectedNode != currentNode && currentNode != null)
                {
                    currentNode.ResetColors();
                    currentNode = SelectedNode;
                    currentNode.MouseEnterCheck();
                }
                else if (SelectedNode != currentNode && currentNode == null)
                {
                    currentNode = SelectedNode;
                    currentNode.MouseEnterCheck();
                }

            } else
            {
                if(currentNode != null)
                {
                currentNode.ResetColors(); 
                }
                currentNode = null;
            }
        }
    }
}
