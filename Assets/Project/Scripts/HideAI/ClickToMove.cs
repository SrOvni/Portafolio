using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, 100, FloorLayers, QueryTriggerInteraction.Ignore))
            {
                GetComponent<NavMeshAgent>().SetDestination(Hit.point);
            }
        }
    }
}