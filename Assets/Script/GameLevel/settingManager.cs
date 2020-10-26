using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class settingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
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

    


   
    void Update()
    {
        
    }
}
