using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject hoop;
    [SerializeField]
    private new Collider collider;
    
    public void SpawnHoop() {
        GameObject newHoop = Instantiate(hoop, collider.bounds.center, Quaternion.identity);
        newHoop.transform.forward = transform.forward;
    }
}
