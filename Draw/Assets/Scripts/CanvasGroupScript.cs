using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGroupScript : MonoBehaviour
{
    string currentScene;
    public static CanvasGroupScript instance;
    //public Player player;
    //public GameObject accuseButton;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene() {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LoadMainMenu() {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
