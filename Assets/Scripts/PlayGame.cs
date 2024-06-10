using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayGame : MonoBehaviour
{
    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        SCManager.instance.LoadScene("GameScene");
    //    }

    //}

    public void LoadGameScene()
    {
        AudioManager.instance.PlaySFX("startLevel");
        AudioManager.instance.PlayMusic("MainTheme");
        SCManager.instance.LoadSceneByName("Level1");
    }

    public void RetryScene()
    {
        SCManager.instance.LoadSceneByIndex(SCManager.instance.lastScene);
    }

    //public void LoadMenuScene()
    //{
    //    SCManager.instance.LoadScene("Menu");
    //}

    public void ReloadScene()
    {
        SCManager.instance.LoadSceneByIndex(SCManager.instance.GetActualSceneIndex(SceneManager.GetActiveScene().buildIndex));
    }

    public void CloseGame()
    {
        Application.Quit();
    }


}
