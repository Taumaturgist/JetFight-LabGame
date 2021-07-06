using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUShield : PowerUp //INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        FindPlayer(); //PowerUp INHERITANCE
    }

    // Update is called once per frame
    void Update()
    {
        PowerUpRotate(); //PowerUp INHERITANCE
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerController.hasShield == false)
        {
            Destroy(gameObject);
        }
    }
}
