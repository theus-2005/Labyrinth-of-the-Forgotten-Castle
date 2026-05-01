using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuFlutua : MonoBehaviour
{
    [Header("Movimento vertical")]
    public float amplitude = 10f;
    public float speed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(
            startPos.x,
            startPos.y + offsetY,
            startPos.z
        );
    }
}
