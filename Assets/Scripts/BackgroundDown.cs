using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDown : MonoBehaviour
{
    private float speed = 3;
    private float zPos1 = -15;
    private float zPos2 = -50;
    private Vector3 spawnPos = new Vector3(0, 0, 34.5f);
    public GameObject planePrefab;
    private bool spawnPlane;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlane = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        if (transform.position.z < zPos1 && spawnPlane)
        {
            GameObject newPlane = Instantiate(planePrefab, spawnPos, planePrefab.transform.rotation);
            newPlane.name = "BackgroundPlane";
            spawnPlane = false;
        }
        if (transform.position.z < zPos2)
        {
            Destroy(gameObject);
        }
    }
}
