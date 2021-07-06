using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 7.0f;
    private float zDestroy = 10.0f;     
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > zDestroy)
        {
            Destroy(gameObject);
        }
    }
    
}
