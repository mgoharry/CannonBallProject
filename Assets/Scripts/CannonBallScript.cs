using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject impactEffect;
    Renderer rend;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(impactEffect, transform.position, transform.rotation), 2);

        if (!collision.collider.CompareTag("Floor"))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = rend.material.color;
        }
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = Random.ColorHSV();
    }
}
