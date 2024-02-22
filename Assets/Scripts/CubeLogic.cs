using System.Collections;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{

    public void Release(Vector3 pos, Vector3 vel, Vector3 angVel)
    {
        transform.position = pos; // set the orign to match target
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = vel;
        GetComponent<Rigidbody>().angularVelocity = angVel;
    }
}
