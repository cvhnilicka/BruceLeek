using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


    public void StartGameButton()
    {
        print("START GAMEEEEEEEE");
        SceneManager.LoadScene(1);
    }

    public void QuitGameButton()
    {
        print("QUITTTTTTT GAMEEEEEEEE");
        Application.Quit();
    }

    public void ReloadGame()
    {
        print("Reload!!!!!!!!!");
        SceneManager.LoadScene(0);
    }

}
