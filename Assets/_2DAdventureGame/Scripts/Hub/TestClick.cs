using UnityEngine;

public class TestClick : MonoBehaviour
{
    public void OnClickEnterDungeon()
    {
        GameManager.Instance.EnterDungeon();
    }
}
