using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScanner : MonoBehaviour
{
    public bool allCovered;
    public GameObject paint;
    public Camera myCamera;
    public GameObject background;

    public Text frames;
    private int framesCount;

    public AudioSource crash;

    void Start() {
        UpdatePaintColorAndCamera();
        SetUpScore();
    }

    void Update() {
        allCovered = true;
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).GetChild(0).GetComponent<TargetManagment>().isVisible) {
                allCovered = false;
            }          
        }
        if(allCovered) {
            crash.Play();
            UpdatePaintColorAndCamera();
            DeleteAllPaint();
            ResetScanners();
            AddScore();
        }  
    }

    void ResetScanners() {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetChild(0).GetComponent<TargetManagment>().isVisible = true;
        }
    }

    void UpdatePaintColorAndCamera() {
        myCamera.backgroundColor = paint.GetComponent<LineRenderer>().startColor;
        paint.GetComponent<LineRenderer>().startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        paint.GetComponent<LineRenderer>().endColor = paint.GetComponent<LineRenderer>().startColor;
    }

    public void DeleteAllPaint(){
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Paint")) {
            Destroy(o);
        }
    }

    void SetUpScore() {
        framesCount = 0;
        frames.text = "Frames: " + framesCount;
    }

    void AddScore() {
        framesCount++;
        frames.text = "Frames: " + framesCount;
    }

}
