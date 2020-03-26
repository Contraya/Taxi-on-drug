using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float staticSpeed;
    public Text text;
    private bool playedAcc;
    private bool playedDec;
    private bool playedEngineLow;
    private bool playedEngineMed;
    private bool playedEngineHigh;
    private AudioManager audioManager;
    void Start()
    {
        playedAcc = false;
        playedDec = false;
        playedEngineLow =  true;
        playedEngineMed = false;
        playedEngineHigh = false;
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        audioManager.Play("engineLow");
    }

    void FixedUpdate()
    {
        rb.drag = 1;
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movementSides = new Vector3(moveHorizontal, 0, 0);
        Vector3 movementFrontStatic = new Vector3(0, 0, staticSpeed);
        Vector3 movementFront = new Vector3(0, 0, moveVertical*(speed-staticSpeed));
        rb.AddForce(movementFrontStatic);
        if (rb.velocity.z <= movementFrontStatic.z && moveVertical < 0){
            movementFront.Set(0, 0, 0);
        }
        rb.AddForce(movementFront);
        rb.AddForce(movementSides * 100);
        if (moveVertical < 0){
            rb.rotation = Quaternion.Euler(moveVertical*2, 180+moveHorizontal*3, moveHorizontal*-10);
        } else if (moveVertical > 0) {
            rb.rotation = Quaternion.Euler(moveVertical*4, 180+moveHorizontal*3, moveHorizontal*-10);
        } else {
            rb.rotation = Quaternion.Euler(0, 180+moveHorizontal*3, moveHorizontal*-10);
        }
        Sound();
        text.text = "Horizontal: " + moveHorizontal.ToString() + "\nVertical: " + moveVertical.ToString() +"\n"+ movementFront.ToString() +"\n"+ rb.velocity.ToString();
    }

    void Sound(){
        if (rb.velocity.z <= 2*((speed - staticSpeed)/4) && !playedEngineLow){
            audioManager.Stop("engineMed");
            audioManager.Play("engineLow");
            playedEngineLow = true;
            playedEngineMed = false;
            playedEngineHigh = false;
        } else if (rb.velocity.z <= (3*(speed - staticSpeed)/4) && rb.velocity.z > 2*((speed - staticSpeed)/4) && !playedEngineMed) {
            if(playedEngineLow){
                audioManager.Stop("engineLow");
            } else {
                audioManager.Stop("engineHigh");
            }
            audioManager.Play("engineMed");
            playedEngineLow = false;
            playedEngineMed = true;
            playedEngineHigh = false;
        }else if (rb.velocity.z > (3*(speed - staticSpeed)/4) && !playedEngineHigh){
            audioManager.Stop("engineMed");
            audioManager.Play("engineHigh");
            playedEngineLow = false;
            playedEngineMed = false;
            playedEngineHigh = true;
        }
    }

}
