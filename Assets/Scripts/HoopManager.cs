using System;
using System.Collections;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    private IEnumerator CheckHoops() {
        while (true)
        {
            Debug.Log("Checking for Hoops");
            yield return new WaitForSeconds(1);
            GameObject[] currentHoops = GameObject.FindGameObjectsWithTag("Hoop");
            if (currentHoops.Length > 0) continue;
            Debug.Log("No Hoops Found");
            HoopSpawner[] allWalls = FindObjectsOfType<HoopSpawner>();
            if (allWalls.Length == 0) continue;
            Debug.Log("Wall Found");
            HoopSpawner randomWall = allWalls[UnityEngine.Random.Range(0, allWalls.Length - 1)];
            randomWall.SpawnHoop();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckHoops());
    }

    
}
