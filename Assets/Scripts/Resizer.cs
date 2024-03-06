using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    [SerializeField]
    private new MeshRenderer renderer;
    [SerializeField]
    private float startingSize;
    // Start is called before the first frame update
    void Start()
    {
        if (renderer == null) 
        {
            renderer = GetComponent<MeshRenderer>();
        }
        Resize(startingSize);
    }

    public void Resize(float newSize) {
        float sizeMultiplier = Mathf.Pow(Mathf.Pow(newSize, 3) / VectorUtils.getVolume(renderer.bounds.size), 1f/3);
        transform.localScale *= sizeMultiplier;
    }
}
