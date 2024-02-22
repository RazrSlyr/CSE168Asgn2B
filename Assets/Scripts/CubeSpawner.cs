using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform trackingspace;
    [SerializeField] private GameObject rightControllerPivot;
    [SerializeField] private OVRInput.RawButton actionBtn;
    [SerializeField] private GameObject cubePrefab;
    private GameObject currentCube = null;
    private bool cubeGrabbed = false;

    private void Update()
    {
        if (!cubeGrabbed && OVRInput.GetDown(actionBtn))
        {
            currentCube = Instantiate(cubePrefab, rightControllerPivot.transform.position, Quaternion.identity);
            currentCube.transform.parent = rightControllerPivot.transform;
            Rigidbody cubeRigidbody = currentCube.GetComponent<Rigidbody>();
            cubeRigidbody.isKinematic = true;
            cubeRigidbody.useGravity = false;
            cubeGrabbed = true;
        }

        if (cubeGrabbed && OVRInput.GetUp(actionBtn))
        {
            currentCube.transform.parent = null;
            var ballPos = currentCube.transform.position;
            var vel = trackingspace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            var angVel = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
            currentCube.GetComponent<CubeLogic>().Release(ballPos, vel, angVel);
            cubeGrabbed = false;
        }
    }
}
