using UnityEngine;

public class CameraAjuste : MonoBehaviour
{
    public float alturaBase = 5f;

    void Start()
    {
        GetComponent<Camera>().orthographicSize = alturaBase * Screen.height / Screen.width;
    }
}