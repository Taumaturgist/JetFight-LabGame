using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Vector3 RotateAmount;
    protected PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    protected virtual void PowerUpRotate()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
    protected void FindPlayer()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
