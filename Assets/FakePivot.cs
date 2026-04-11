using UnityEngine;

public class FakePivot : MonoBehaviour
{
    public float offsetY = -0.5f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, offsetY);
    }
}