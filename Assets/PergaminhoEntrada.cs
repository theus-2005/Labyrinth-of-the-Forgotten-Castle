using System.Collections;
using UnityEngine;

public class PergaminhoEntrada : MonoBehaviour
{
    public RectTransform pergaminho;
    public Vector2 posicaoInicial = new Vector2(0, -1200);
    public Vector2 posicaoFinal = new Vector2(0, 0);
    public float duracao = 1f;

    [HideInInspector] public bool entradaIniciada = false;
    [HideInInspector] public bool terminouEntrada = false;

    void Start()
    {
        if (pergaminho == null)
            pergaminho = GetComponent<RectTransform>();

        StartCoroutine(AnimarEntrada());
    }

    IEnumerator AnimarEntrada()
    {
        entradaIniciada = true;
        terminouEntrada = false;

        pergaminho.anchoredPosition = posicaoInicial;

        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;
            t = Mathf.SmoothStep(0f, 1f, t);

            pergaminho.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinal, t);
            yield return null;
        }

        pergaminho.anchoredPosition = posicaoFinal;
        terminouEntrada = true;
    }
}
