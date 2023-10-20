using UnityEngine;

public class Footstep : MonoBehaviour
{
    public float walkingBobbingSpeed = 12f;
    public float runningBobbingSpeed = 16f;
    public float stepbobbingAmount = 0.08f;
    public float flashlightbobbingAmountX = 0.1f;
    public float flashlightbobbingAmountY = -   0.1f;
    public Camera playerCam;
    public Light flashlight;
    private CharacterController playerController;
    private float defaultCamPosY = 0;
    private float defaultFlashlightPosY = 0;
    private float defaultFlashlightPosX = 0;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultCamPosY = playerCam.transform.localPosition.y;
        defaultFlashlightPosY = flashlight.transform.localPosition.y;
        defaultFlashlightPosX = flashlight.transform.localPosition.x;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float correctSpeed = isRunning ? runningBobbingSpeed : walkingBobbingSpeed;
        if(Mathf.Abs(playerController.velocity.x) > 0.1f || Mathf.Abs(playerController.velocity.z) > 0.1f)
        {
            //Player is moving
            timer += Time.deltaTime * correctSpeed;

            // Step Camera Boobbing
            playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x, defaultCamPosY + Mathf.Sin(timer) * stepbobbingAmount, playerCam.transform.localPosition.z);
            

            // Flashlight Bobbing
            flashlight.transform.localPosition = new Vector3(defaultFlashlightPosX + Mathf.Sin(timer) * flashlightbobbingAmountX * 2, defaultFlashlightPosY + Mathf.Sin(timer) * flashlightbobbingAmountY, flashlight.transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            // Step Camera Bobbing
            playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x, Mathf.Lerp(playerCam.transform.localPosition.y, defaultCamPosY, Time.deltaTime * correctSpeed), playerCam.transform.localPosition.z);
            // Flashlight Bobbing
            flashlight.transform.localPosition = new Vector3(Mathf.Lerp(flashlight.transform.localPosition.x, defaultFlashlightPosX, Time.deltaTime * (isRunning ? runningBobbingSpeed : walkingBobbingSpeed)), Mathf.Lerp(flashlight.transform.localPosition.y, defaultFlashlightPosY, Time.deltaTime * walkingBobbingSpeed), flashlight.transform.localPosition.z);
            flashlightbobbingAmountX = flashlightbobbingAmountX * -1;
            flashlightbobbingAmountY = flashlightbobbingAmountY * -1;

        }
    }

}