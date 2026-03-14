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
        if (portasControladas.Count == 0) return;

        for (int i = 0; i < portasControladas.Count; i++)
        {
            // Define o estado inicial (ex: todas visíveis)
            SetPortaEstado(portasControladas[i], true);
        }
        AtualizarCorBotao();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Certifique-se que o Player tem a Tag "Player" no Unity
        if (other.CompareTag("Player"))
        {
            foreach (GameObject porta in portasControladas)
            {
                AlternarEstado(porta);
            }
            // Pequeno atraso para garantir que a cor mude após a física processar
            Invoke("AtualizarCorBotao", 0.05f);
        }
    }

    private void AlternarEstado(GameObject porta)
    {
        if (porta == null) return;
        
        SpriteRenderer sr = porta.GetComponent<SpriteRenderer>();
        Collider2D col = porta.GetComponent<Collider2D>();

        if (sr != null && col != null)
        {
            sr.enabled = !sr.enabled;
            col.enabled = !col.enabled;
        }
    }

    private void SetPortaEstado(GameObject porta, bool estado)
    {
        if (porta == null) return;
        
        SpriteRenderer sr = porta.GetComponent<SpriteRenderer>();
        Collider2D col = porta.GetComponent<Collider2D>();

        if (sr != null) sr.enabled = estado;
        if (col != null) col.enabled = estado;
    }

    private void AtualizarCorBotao()
    {
        if (meuRenderer != null && portasControladas.Count > 0)
        {
            // Se a porta estiver visível (ativa), botão fica amarelo, senão verde
            bool portaAtiva = portasControladas[0].GetComponent<SpriteRenderer>().enabled;
            meuRenderer.color = portaAtiva ? Color.yellow : Color.green;
        }
    }
}