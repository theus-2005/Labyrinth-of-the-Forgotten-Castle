using UnityEngine;
using System.Collections.Generic;

public class BotaoAlternador : MonoBehaviour
{
    public List<GameObject> portasControladas = new List<GameObject>();
    private SpriteRenderer meuRenderer;

    void Awake()
    {
        meuRenderer = GetComponent<SpriteRenderer>();
    }

    public void ConfiguracaoInicial()
    {
        AtualizarCorBotao();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject porta in portasControladas)
            {
                if (porta != null) AlternarEstado(porta);
            }
            AtualizarCorBotao();
        }
    }

    private void AlternarEstado(GameObject porta)
    {
        SpriteRenderer sr = porta.GetComponent<SpriteRenderer>();
        Collider2D col = porta.GetComponent<Collider2D>();

        if (sr != null) sr.enabled = !sr.enabled;
        if (col != null) col.enabled = !col.enabled;
    }

    public void AtualizarCorBotao()
    {
        if (meuRenderer != null && portasControladas.Count > 0 && portasControladas[0] != null)
        {
            // O botão reflete o estado da primeira porta vinculada a ele
            bool portaAtiva = portasControladas[0].GetComponent<SpriteRenderer>().enabled;
            meuRenderer.color = portaAtiva ? Color.yellow : Color.green;
        }
    }
}