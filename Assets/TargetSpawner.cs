using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget() {
        while (true) {
            float xDisplacement = (1f + Random.value / 2) * Mathf.Sign(Random.value - 0.5f);
            float zDisplacement = (1f + Random.value / 2) * Mathf.Sign(Random.value - 0.5f);
            GameObject cloneTarget = 
                Instantiate(target, playerCamera.position + new Vector3(xDisplacement, 0, zDisplacement), Quaternion.identity);
            cloneTarget.transform.LookAt(playerCamera);
            yield return new WaitForSeconds(5);
        }
    }
}
