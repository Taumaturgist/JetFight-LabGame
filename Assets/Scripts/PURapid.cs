using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PURapid : PowerUp
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
    protected override void PowerUpRotate()
    {
        transform.Rotate(-RotateAmount * Time.deltaTime); //PowerUP Polymorphism
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerController.hasRapid == false)
        {
            Destroy(gameObject);
        }
    }
}
