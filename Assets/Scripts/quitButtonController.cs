using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitButtonController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        print("Quit!");
    }

}
