using RG.Systems.Tests.Player;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Pickable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something trigger");
        if(other.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }
}
