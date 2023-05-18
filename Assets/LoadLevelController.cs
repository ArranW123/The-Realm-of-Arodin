using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelController : MonoBehaviour
{
    Collider2D playerCollision;
    public int levelNumber;
    public string levelName;
    public bool loadLevel = false;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerCollision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCollision.IsTouchingLayers(playerLayer))
        {
            LoadScene();
        }
    }


    void LoadScene()
    {
        print("I'm in load scene!");
        if(loadLevel)
        {
            SceneManager.LoadScene(levelNumber);
        } 
        else 
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
