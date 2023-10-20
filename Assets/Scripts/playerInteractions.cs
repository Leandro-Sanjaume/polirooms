using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    public Light flashlight;

    public AudioSource flashlightButton;
    private bool flashlightState;
    private float flashON = 2f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        flashlight = GameObject.FindGameObjectWithTag("flashlight").GetComponent<Light>();
        flashlightState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f")){
            flashlightButton.Play(); 
            flashlight.intensity  =  flashlightState ? flashON : 0;
            flashlightState = !flashlightState;
        }
    }
}
