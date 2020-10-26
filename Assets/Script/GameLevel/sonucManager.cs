using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sonucManager : MonoBehaviour

{
    public void anaMenuyeDon()
    {
        SceneManager.LoadScene("MenuScene");
    }


    public void yenidenBasla()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void cıkıs()
    {
        Application.Quit();
    }



}


