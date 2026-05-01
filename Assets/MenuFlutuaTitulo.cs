using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuFlutuaTitulo : MonoBehaviour
{
    [Header("Escala")]
    public float pulseAmount = 0.03f;
    public float speed = 1f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = 1 + Mathf.Sin(Time.time * speed) * pulseAmount;

        transform.localScale = new Vector3(
            startScale.x * scaleOffset,
            startScale.y * scaleOffset,
            startScale.z
        );
    }
}
