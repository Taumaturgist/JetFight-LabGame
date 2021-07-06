using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{    
    public TextMeshProUGUI killsText;
    public float kills = 0.0f;

    private AudioSource killerSound;
    public AudioClip enemyCrash;
        
    // Start is called before the first frame update
    void Start()
    {
        killerSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) //enemy hit
    {
        if (other.CompareTag("Enemy"))
        {
            kills = kills + 0.5f;
            killsText.text = "Kills: " + kills;
            killerSound.PlayOneShot(enemyCrash);
        }
    }
    
}
