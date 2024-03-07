using System.Collections;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material grabbableMat; 

    public void Grab(GameObject grabber) {
        Rigidbody rgbd = GetComponent<Rigidbody>();
        transform.parent = grabber.transform;
        rgbd.isKinematic = true;
        rgbd.useGravity = false;
        
    }

    public void Release(Vector3 pos, Vector3 vel, Vector3 angVel)
    {
        Rigidbody rgbd = GetComponent<Rigidbody>();
        transform.position = pos; // set the orign to match target
        rgbd.isKinematic = false;
        rgbd.useGravity = true;
        rgbd.velocity = vel;
        rgbd.angularVelocity = angVel;
    }

    public void ShowGrabbable() {
        GetComponent<Renderer>().material = grabbableMat;
    }

    public void NotGrabbable() {
        GetComponent<Renderer>().material = defaultMat;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Hoop")) {
            Destroy(collider.gameObject);
        }
    }
}
