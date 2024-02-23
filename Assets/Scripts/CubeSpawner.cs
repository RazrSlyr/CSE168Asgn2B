using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform trackingspace;
    [SerializeField] private GameObject rightControllerPivot;
    [SerializeField] private GameObject leftControllerPivot;
    [SerializeField] private OVRInput.RawButton spawnButton;
    [SerializeField] private OVRInput.RawButton grabButton;
    [SerializeField] private OVRInput.RawButton despawnButton;
    [SerializeField] private GameObject cubePrefab;
    private GameObject currentCube = null;
    private bool rightHandFull = false;
    private bool leftHandFull = false;
    private GameObject grabbableCube = null;


    private GameObject FindGrabbableCube() {
        GameObject closestCube = null;
        Collider[] cubes = Physics.OverlapSphere(leftControllerPivot.transform.position, 0.1f);
        foreach (Collider item in cubes)
        {
            if (item.GetComponent<Grabbable>() && closestCube == null || 
                Vector3.Distance(leftControllerPivot.transform.position, item.transform.position) <
                Vector3.Distance(leftControllerPivot.transform.position, closestCube.transform.position)) 
            {
                closestCube = item.gameObject;
            }
        }
        return closestCube;
    }

    private void RemoveAllSpawns() {
        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("Spawned");
        foreach (GameObject spawn in allSpawns) {
            if (spawn != currentCube && (!leftHandFull || spawn != grabbableCube)){
                Destroy(spawn);
            }
        }
    }

    private void Update()
    {
        if (!leftHandFull)
        {
            GameObject newGrabbableCube = FindGrabbableCube();
            if (newGrabbableCube != grabbableCube) {
                if (grabbableCube != null) {
                    grabbableCube.GetComponent<Grabbable>().NotGrabbable();
                }
                if (newGrabbableCube != null) {
                    newGrabbableCube.GetComponent<Grabbable>().ShowGrabbable();
                }
                grabbableCube = newGrabbableCube;
            }
        }

        if (grabbableCube != null && !leftHandFull && OVRInput.GetDown(grabButton)) {
            grabbableCube.GetComponent<Grabbable>().Grab(leftControllerPivot);
            leftHandFull = true;
        }

        if (leftHandFull && OVRInput.GetUp(grabButton))
        {
            grabbableCube.transform.parent = null;
            grabbableCube.GetComponent<Grabbable>().NotGrabbable();
            var ballPos = grabbableCube.transform.position;
            var vel = trackingspace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            var angVel = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch);
            grabbableCube.GetComponent<Grabbable>().Release(ballPos, vel, angVel);
            leftHandFull = false;
            grabbableCube = null;
        }

        if (!rightHandFull && OVRInput.GetDown(spawnButton))
        {
            currentCube = Instantiate(cubePrefab, rightControllerPivot.transform.position, Quaternion.identity);
            currentCube.GetComponent<Grabbable>().Grab(rightControllerPivot);
            rightHandFull = true;
        }

        if (rightHandFull && OVRInput.GetUp(spawnButton))
        {
            currentCube.transform.parent = null;
            var ballPos = currentCube.transform.position;
            var vel = trackingspace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            var angVel = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
            currentCube.GetComponent<Grabbable>().Release(ballPos, vel, angVel);
            rightHandFull = false;
            currentCube = null;
        }

        if (OVRInput.GetDown(despawnButton)) {
            RemoveAllSpawns();
        }
    }
}
