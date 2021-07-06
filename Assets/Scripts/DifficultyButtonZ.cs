using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class DifficultyButtonZ : MonoBehaviour
{
    private Button button;
    private SpawnManagerZ spawnManager;
    public float difficulty;

    private AudioSource buttonClick;
    public AudioClip click;
    public AudioClip onPointerClick;
       
        
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        spawnManager = GameObject.Find("SpawnManagerZ").GetComponent<SpawnManagerZ>();
        buttonClick = GetComponent<AudioSource>();
                
    }

    // Update is called once per frame
    void Update()
    {
        //button.OnPointerEnter.OnPointerClick();
    }
    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        buttonClick.PlayOneShot(click);
        spawnManager.StartSpawn(difficulty);               
    }
    void OnPointerClick()
    {
        buttonClick.PlayOneShot(onPointerClick);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
