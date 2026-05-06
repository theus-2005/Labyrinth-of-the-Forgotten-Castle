using UnityEngine;
using System.Collections.Generic;

public class BotaoAlternador : MonoBehaviour
{
    public List<GameObject> portasControladas = new List<GameObject>();
    private SpriteRenderer meuRenderer;
    private bool pressionado = false;

    void Awake()
    {
        meuRenderer = GetComponent<SpriteRenderer>();
    }

    public void ConfiguracaoInicial() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressionado = !pressionado;
            meuRenderer.color = pressionado ? new Color(0.5f, 0.5f, 0.5f) : Color.white;

            foreach (GameObject porta in portasControladas)
            {
                if (porta != null) AlternarEstado(porta);
            }
        }
    }

    private void AlternarEstado(GameObject porta)
    {
        SpriteRenderer sr = porta.GetComponent<SpriteRenderer>();
        Collider2D col = porta.GetComponent<Collider2D>();

        if (sr != null) sr.enabled = !sr.enabled;
        if (col != null) col.enabled = !col.enabled;
    }
}