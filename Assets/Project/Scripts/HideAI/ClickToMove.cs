using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour {
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]
    InputActionReference LMB;
    void Start()
    {
        LMB.action.performed += OnPointerPressed;
    }

    private void OnPointerPressed(InputAction.CallbackContext context)
    {
        
    }

    private void Update()
    {
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, 100, FloorLayers, QueryTriggerInteraction.Ignore))
            {
                GetComponent<NavMeshAgent>().SetDestination(Hit.point);
            }
        }
    }
}