using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMain()
    {
        // Debug.Log("QUit!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
