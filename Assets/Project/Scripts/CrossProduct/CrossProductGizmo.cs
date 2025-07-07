using UnityEngine;

public class CrossProductGizmo : MonoBehaviour
{
    public Vector3 A = new Vector3(1, 0, 0);
    public Vector3 B = new Vector3(0, 1, 0);
    Vector3 origin;
    //Vector3 

    private void OnDrawGizmos()
    {
        origin = transform.position;

        //Vector A - ROJO
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + A);
        //Vector Y - VERDE
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + B);
        //Vector A - AZUL
        Gizmos.color = Color.blue;
        // if ()
            Gizmos.DrawLine(origin, origin + Vector3.Cross(A, B));
    }
}
