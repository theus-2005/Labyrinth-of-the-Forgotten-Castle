using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public float Speed;

    [Header("Dificuldade")]
    public int dificuldade = 0;

    [Header("Controles")]
    public float shuffleInterval = 5f;

    [Header("Luz")]
    public Light2D globalLight;
    public Light2D spotLight;
    public float luzEscuro = 0.05f;
    public float luzNormal = 1f;

    private Dictionary<string, KeyCode> keyMap;
    private Dictionary<string, KeyCode> arrowMap;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        dificuldade = PlayerPrefs.GetInt("dificuldade", 0);

        ResetControls();
        AplicarDificuldade();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) { dificuldade = 0; AplicarDificuldade(); }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { dificuldade = 1; AplicarDificuldade(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { dificuldade = 2; AplicarDificuldade(); }
    }

    public void AplicarDificuldade()
{
    StopAllCoroutines();
    ResetControls();

    if (dificuldade >= 2)
        StartCoroutine(ShuffleLoop());

    if (globalLight != null)
        globalLight.intensity = dificuldade >= 1 ? luzEscuro : luzNormal;

    if (spotLight != null)
        spotLight.gameObject.SetActive(dificuldade >= 1);
}

    private void FixedUpdate()
    {
        float h = 0, v = 0;

        if (Input.GetKey(keyMap["right"]) || Input.GetKey(arrowMap["right"])) h += 1;
        if (Input.GetKey(keyMap["left"])  || Input.GetKey(arrowMap["left"]))  h -= 1;
        if (Input.GetKey(keyMap["up"])    || Input.GetKey(arrowMap["up"]))    v += 1;
        if (Input.GetKey(keyMap["down"])  || Input.GetKey(arrowMap["down"]))  v -= 1;

        Vector2 moveDir = new Vector2(h, v);
        rb.linearVelocity = moveDir.normalized * Speed;

        if (moveDir.x != 0)
        {
            ResetLayers();
            anim.SetLayerWeight(2, 1);
            sprite.flipX = moveDir.x > 0;
        }

        if (moveDir.y > 0 && moveDir.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(1, 1);
        }

        if (moveDir.y < 0 && moveDir.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(0, 1);
        }

        anim.SetBool("walking", moveDir != Vector2.zero);

        if (spotLight != null && moveDir != Vector2.zero)
        {
            float angulo = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            spotLight.transform.rotation = Quaternion.Euler(0, 0, angulo - 90f);
        }
    }

    private void ResetLayers()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
    }

    private void ResetControls()
    {
        keyMap = new Dictionary<string, KeyCode> {
            { "up",    KeyCode.W },
            { "down",  KeyCode.S },
            { "left",  KeyCode.A },
            { "right", KeyCode.D }
        };
        arrowMap = new Dictionary<string, KeyCode> {
            { "up",    KeyCode.UpArrow    },
            { "down",  KeyCode.DownArrow  },
            { "left",  KeyCode.LeftArrow  },
            { "right", KeyCode.RightArrow }
        };
    }

    private void ShuffleDict(Dictionary<string, KeyCode> dict)
    {
        var keys   = new List<string>(dict.Keys);
        var values = new List<KeyCode>(dict.Values);

        for (int i = values.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (values[i], values[j]) = (values[j], values[i]);
        }

        for (int i = 0; i < keys.Count; i++)
            dict[keys[i]] = values[i];
    }

    public void ShuffleNow()
    {
        ShuffleDict(keyMap);
        ShuffleDict(arrowMap);
    }

    private IEnumerator ShuffleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(shuffleInterval);
            ShuffleNow();
        }
    }
}