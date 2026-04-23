using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLightToggle : MonoBehaviour
{
    public Light2D flashlight;
    public Collider2D hitbox;

    public float maxBattery = 100f;
    public float battery;
    public float drainSpeed = 0.1f;

    private void Start()
    {
        battery = maxBattery;
        flashlight.enabled = false;
        hitbox.enabled = false;
    }

    private void Update()
    {
        if (flashlight.enabled)
        {
            battery -= drainSpeed * Time.deltaTime;

            if (battery <= 0)
            {
                battery = 0;
                flashlight.enabled = false;
            }
        }
    }

    public void ToggleFlashlight()
    {
        if (battery > 0)
        {
            flashlight.enabled = !flashlight.enabled;
            hitbox.enabled = !hitbox.enabled;
        }
    }


    
}
