using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime = 1f;
    //public Player player;
    public GameObject canvasGroup;

    public static SceneManagerScript instance;

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
        StartCoroutine(LoadLevel(1));
        canvasGroup.GetComponent<CanvasGroupScript>().LoadGameScene();
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadLevel(0));
        canvasGroup.GetComponent<CanvasGroupScript>().LoadMainMenu();
    }

    IEnumerator LoadLevel(int levelIndex) {
        //transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        
        //transition.SetTrigger("End");
    }

    public void Quit() {
        Application.Quit();
    }

}
