using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private GameObject destructionEffect;

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Spawned")) {
            Vector3 forward = transform.forward;
            GameObject effect = Instantiate(destructionEffect, transform.position, Quaternion.identity);
            effect.transform.forward = forward;
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }
}
