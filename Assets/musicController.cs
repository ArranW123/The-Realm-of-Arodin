using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicController : MonoBehaviour
{
    AudioSource BackgroundMusic;
    AudioSource BossLevelMusic;
    AudioSource EndingMusic;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    bool reachedBoss;
    bool reachedEnding;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        reachedBoss = false;   
        reachedEnding = false;
        BackgroundMusic = gameObject.AddComponent<AudioSource>();
        BossLevelMusic = gameObject.AddComponent<AudioSource>();
        EndingMusic = gameObject.AddComponent<AudioSource>();

        //Volume settings
        BackgroundMusic.volume = 0.2f;
        BossLevelMusic.volume = 0.2f;
        EndingMusic.volume = 0.3f;

        //audio declaration       
        BackgroundMusic.clip = clip1;
        BossLevelMusic.clip = clip2;
        EndingMusic.clip = clip3;

        BackgroundMusic.Play();
    }

    void Update()
    {             
        if(SceneManager.GetActiveScene().buildIndex == 2) //if it reaches boss level, then it changes music
        {
            if(!reachedBoss)
            {
                BossMusic();
                reachedBoss = true;
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 3) //if it reaches ending level, then it changes music
        {
            if(!reachedEnding)
            {
                Ending();
                reachedEnding = true;
            }
        }
    }

    void BossMusic()
    {
        print("I am playing boss music!");               
        if(BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Stop();
        }
        
        BossLevelMusic.Play();
    }

    void Ending()
    {
        BossLevelMusic.Stop();
        EndingMusic.Play();
    }


}
