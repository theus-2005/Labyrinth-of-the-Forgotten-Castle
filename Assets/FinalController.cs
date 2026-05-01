using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalController : MonoBehaviour
{
    public RectTransform bruxa;
    public Vector2 posicaoInicial = new Vector2(0, 80);
    public Vector2 posicaoFinal = new Vector2(1200, 180);
    public float duracao = 3f;
    public string cenaMenu = "Menu";

    void Start()
    {
        StartCoroutine(AnimarBruxa());
    }

    IEnumerator AnimarBruxa()
    {
        bruxa.anchoredPosition = posicaoInicial;

        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracao;

            bruxa.anchoredPosition = Vector2.Lerp(posicaoInicial, posicaoFinal, t);

            yield return null;
        }

        bruxa.anchoredPosition = posicaoFinal;

        SceneManager.LoadScene(cenaMenu);
    }
}
