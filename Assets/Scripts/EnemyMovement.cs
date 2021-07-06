using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 100.0f; //enemy movement speed var
    private float xPos;
    private float zPos;
    private float xBound = 12.0f;
    private float zBoundHigh = 7.0f;
    private float zBoundLow = -2.0f;
    private Rigidbody enemyRb;
    private GameObject enemyFocus; //enemyFocus is an invisible spot, where all enemies are heading to
    private bool movesToNew = true; //trigger to change direction

    //here enemy shoots at us
    public GameObject shotEnemyPrefab;
    public float shotDelay = 2.0f;
    private float timeUntilNextShot;
    private AudioSource enemyAudio;
    public AudioClip enemyShoots;

    //enemy burns
    public ParticleSystem enemyBurns;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyFocus = GameObject.Find("EnemyFocus");
        enemyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenMove();
        EnemyBoundaries();
        IncomingFire();
    }
    void ScreenMove() //roam the enemies randomly on the screen
    {
        if (movesToNew == true)
        {
            movesToNew = false;
            xPos = Random.Range(-xBound, xBound); //enemy movement Xrange
            zPos = Random.Range(-zBoundLow, zBoundHigh); //enemy movement Zrange
            enemyFocus.transform.position = new Vector3(xPos, 2, zPos);
            Vector3 moveDirection = (enemyFocus.transform.position - transform.position);
            enemyRb.AddForce(moveDirection * speed);
            StartCoroutine(MovementDelay());
        }

    }
    IEnumerator MovementDelay() //timer for direction change
    {
        yield return new WaitForSeconds(5);
        movesToNew = true;

    }
    void EnemyBoundaries()
    {
        //making the boundaries for the enemy
        transform.position = new Vector3(Mathf.Min(transform.position.x, xBound), transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Max(transform.position.x, -xBound), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Min(transform.position.z, zBoundHigh));
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Max(transform.position.z, zBoundLow));
    }
    void IncomingFire()
    {
        timeUntilNextShot -= Time.deltaTime;
        // shoots us every period
        if (timeUntilNextShot <= 0.0f)
        {
            enemyAudio.PlayOneShot(enemyShoots);
            Vector3 shotSpawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Instantiate(shotEnemyPrefab, shotSpawnPos, shotEnemyPrefab.transform.rotation);
            timeUntilNextShot = shotDelay;
        }
    }
    public void OnTriggerEnter(Collider other) //enemy hit
    {
        if (other.CompareTag("Shotplayer"))
        {
            Destroy(other.gameObject);
            enemyBurns.Play();            
            StartCoroutine(EnemyDeathDelay());
        }     

    }
    IEnumerator EnemyDestroy()
    {
        yield return new WaitForSeconds(0.001f);
        Destroy(gameObject);
        Debug.Log("Enemy Down");
    }
    IEnumerator EnemyDeathDelay()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        StartCoroutine(EnemyDestroy());
    }
}
