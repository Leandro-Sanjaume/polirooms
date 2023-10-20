using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    // Start is called before the first frame update
    public Material nightSkybox;
    public Material daySkybox;

    public Light EnviromentLight;
    private bool toggle;

    void Start(){
        EnviromentLight = GetComponent<Light>();
        toggle = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l")){
            RenderSettings.skybox = toggle ? nightSkybox : daySkybox;
            EnviromentLight.intensity = toggle ? 0.2f : 1;
            DynamicGI.UpdateEnvironment();
            toggle = !toggle;

        }

    }
}
