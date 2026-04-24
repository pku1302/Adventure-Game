using UnityEngine;

public class TestReturn : MonoBehaviour
{
    public void OnClickReturnHub()
    {
        GameManager.Instance.ReturnToHub();
    }
}
