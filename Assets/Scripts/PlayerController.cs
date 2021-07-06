using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player movement speed and boundaries
    [SerializeField] private float speed = 10.0f; //INCAPSULATION
    [SerializeField] private float xBound = 12.0f; //INCAPSULATION
    [SerializeField] private float zBound = 7.0f; //INCAPSULATION...

    //here we shoot at the enemies
    [SerializeField] private GameObject shotPlayerPrefab;
    private float shotDelay = 1.0f;
    private float timeUntilNextShot;

    //shield powerup
    public bool hasShield = false;
    [SerializeField] private GameObject shield;
    private int shieldDuration = 7;
    private int shieldExpiration = 1;
    private int shieldReset = 7;

    //rapidfire powerup
    public bool hasRapid = false;
    [SerializeField] private GameObject rapid;
    private int rapidDuration = 7;
    private int rapidExpiration = 1;
    private int rapidReset = 7;

    //user interface here      
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI rapidText;

    //game timer
    private bool gameIsActive;
    [SerializeField] private TextMeshProUGUI timerText;
    private int timer = 0;
    private int timerAdd = 1;

    //restart button and text    
    [SerializeField] private GameObject restartTitle;

    //sound and action music effects here
    private AudioSource playerAudio;
    [SerializeField] private AudioClip playerCrash;
    [SerializeField] private AudioClip shieldActive;
    [SerializeField] private AudioClip playerShoots;

    //explosion on death
    [SerializeField] private ParticleSystem explosionParticle;

    //stop enemy spawn upon death
    [SerializeField] private GameObject enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = true;        
        StartCoroutine(GameTimer());
        playerAudio = GetComponent<AudioSource>();        
    }
    

    // Update is called once per frame
    void Update()
    {        
        MovePlayer(); //ABSTRACTION
        PlayerBoundaries(); //ABSTRACTION
        FireAtWill(); //ABSTRACTION       
        shield.transform.position = transform.position;
        rapid.transform.position = transform.position;

    }
    private void MovePlayer()
    {
            //assigning movement vars
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //making the player to move x and z axis
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);          
    }
    private void PlayerBoundaries()
    {
        //making the boundaries
        transform.position = new Vector3(Mathf.Min(transform.position.x, xBound), transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Max(transform.position.x, -xBound), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Min(transform.position.z, zBound));
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Max(transform.position.z, -zBound));
    }
    private void FireAtWill()
    {
            timeUntilNextShot -= Time.deltaTime;
            // On spacebar press, fire the projectile
            if (Input.GetKeyDown(KeyCode.Space) && timeUntilNextShot <= 0.0f)
            {
            playerAudio.PlayOneShot(playerShoots);    
            Vector3 shotSpawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                Instantiate(shotPlayerPrefab, shotSpawnPos, shotPlayerPrefab.transform.rotation);
                timeUntilNextShot = shotDelay;
            }             
    }
    private void OnTriggerEnter(Collider other) //player pickup & player hit
    {
        if (other.CompareTag("PUShield") && hasShield == false)
        {
            hasShield = true;
            playerAudio.PlayOneShot(shieldActive);
            shield.gameObject.SetActive(true);
            shieldText.gameObject.SetActive(true);
            shieldDuration = shieldReset;
            shieldText.text = "Shield:" + shieldDuration;
            StartCoroutine(ShieldCountdownRoutine()); //actual player invulnerability on/off
            StartCoroutine(ShieldTimer()); //shield expiration timer in UI
        }
        if (other.CompareTag("Shotenemy") && hasShield == false)
        {
            playerAudio.PlayOneShot(playerCrash);
            explosionParticle.Play();
            Destroy(enemySpawn.gameObject);
            StartCoroutine(PlayerDied()); //we need to wait a bit to play all the death sfx and particles            
        }
        if (other.CompareTag("PURapid") && hasRapid == false)
        {
            hasRapid = true;
            playerAudio.PlayOneShot(shieldActive);
            rapid.gameObject.SetActive(true);
            rapidText.gameObject.SetActive(true);
            rapidDuration = rapidReset;
            rapidText.text = "Rapid:" + rapidDuration;
            shotDelay /= 2;
            StartCoroutine(RapidCountdownRoutine()); //actual player rapid on/off
            StartCoroutine(RapidTimer()); //shield expiration timer in UI
        }
    }
    IEnumerator ShieldCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasShield = false;
        shield.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
    }
    IEnumerator RapidCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        shotDelay *= 2;
        hasRapid = false;
        rapid.gameObject.SetActive(false);
        rapidText.gameObject.SetActive(false);
    }
    IEnumerator ShieldTimer()
    {      
        while (hasShield)
        {
            yield return new WaitForSeconds(1);
            if (hasShield)
            {               
                shieldDuration -= shieldExpiration;
                shieldText.text = "Shield:" + shieldDuration;
            }
        }          
    }
    IEnumerator RapidTimer()
    {
        while (hasRapid)
        {
            yield return new WaitForSeconds(1);
            if (hasRapid)
            {
                rapidDuration -= rapidExpiration;
                rapidText.text = "Rapid: " + rapidDuration;
            }
        }
    }
    IEnumerator GameTimer()
    {
        Debug.Log("Timer Started!");
        while (gameIsActive)
        {
            yield return new WaitForSeconds(1);
            if (gameIsActive)
            {       
                    Debug.Log("TimeClick+1");
                    timer += timerAdd;
                    timerText.text = "Time: " + timer;                                    

            }
        }
        
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    
    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(1.5f);
        gameIsActive = false;
        gameObject.SetActive(false);  
        rapid.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
        restartTitle.gameObject.SetActive(true);
    }
}
