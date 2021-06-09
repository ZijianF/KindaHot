using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSceneLoader : MonoBehaviour
{
    [SerializeField] public PlayerMovement Player;
    [SerializeField] public GameObject DeathMenu;



    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Cursor.lockState = CursorLockMode.locked;
        if(this.Player.IsDead())
        {
            Debug.Log("He's dead!");
            // Cursor.lockState = CursorLockMode.unlocked;
            Cursor.visible = true;
            this.DeathMenu.SetActive(true);
            return;
        }
        Cursor.visible = false;
        this.DeathMenu.SetActive(false);
    }
}
