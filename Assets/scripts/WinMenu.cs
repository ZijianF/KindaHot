using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void BackToMain()
    {
        // Debug.Log("QUit!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
