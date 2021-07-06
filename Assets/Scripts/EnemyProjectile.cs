using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 7.0f;
    private float zDestroy = -10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) //player hit
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
