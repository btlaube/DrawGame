using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagment : MonoBehaviour
{
    public GameObject target;
    public bool isVisible;

    void Awake() {
        isVisible = true;
    }

    void Update() {
        if(isVisible){
            if(IsVisible()) {
                //Debug.Log("visible");
                isVisible = true;
                //target.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else {
                //Debug.Log("not visible");
                isVisible = false;
                //target.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        
    }

    private bool IsVisible() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), 10f);
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * 10f, Color.red);

        if(hit) {
            if(hit.collider.tag == "Front") {
                //Debug.Log("Front");
                return true;
            }
            else if(hit.collider.tag == "Paint") {
                //Debug.Log("Paint");
                return false;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
        
    }
}
