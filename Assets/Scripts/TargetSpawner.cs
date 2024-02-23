using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private OVRInput.RawButton clearButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    private void RemoveAllSpawns() {
        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject spawn in allSpawns) {
            Destroy(spawn);
        }
    }

    public void Update() {
        if (OVRInput.GetDown(clearButton)) {
            RemoveAllSpawns();
        }
    }

    private IEnumerator SpawnTarget() {
        while (true) {
            yield return new WaitForSeconds(5);
            float xDisplacement = (0.5f + Random.value / 2) * Mathf.Sign(Random.value - 0.5f);
            float zDisplacement = (0.5f + Random.value / 2) * Mathf.Sign(Random.value - 0.5f);
            GameObject cloneTarget = 
                Instantiate(target, playerCamera.position + new Vector3(xDisplacement, 0, zDisplacement), Quaternion.identity);
            cloneTarget.transform.LookAt(playerCamera);
        }
    }
}
