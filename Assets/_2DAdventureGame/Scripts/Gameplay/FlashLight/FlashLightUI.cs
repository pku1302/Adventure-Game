using UnityEngine;
using UnityEngine.UI;

public class FlashLightUI : MonoBehaviour
{
    public FlashLightToggle flashLight;
    public Image Battery_0;
    public Image Battery_1;
    public Image Battery_2;
    public Image Battery_3;
    public Image Battery_4;
    public Image Dishcharge;

    private int prevLevel = -1;

    void Update()
    {
        int level = GetBatteryLevel(flashLight.battery);

        if (level != prevLevel)
        {
            UpdateUI(level);
            prevLevel = level;
        }
    }

    int GetBatteryLevel(float battery)
    {
        if (battery <= 0) return 0;
        if (battery <= 5) return 1;
        if (battery <= 25) return 2;
        if (battery <= 50) return 3;
        if (battery <= 75) return 4;
        return 5;
    }

    void UpdateUI(int level)
    {
        Battery_4.enabled = level >= 5;
        Battery_3.enabled = level >= 4;
        Battery_2.enabled = level >= 3;
        Battery_1.enabled = level >= 2;
        Battery_0.enabled = level >= 1;
        Dishcharge.enabled = level == 0;
    }
}
