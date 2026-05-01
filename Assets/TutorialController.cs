using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [Header("Entrada")]
    public PergaminhoEntrada pergaminhoEntrada;

    [Header("Páginas")]
    public GameObject pagina1;
    public GameObject pagina2;

    [Header("Canvas Groups")]
    public CanvasGroup canvasPagina1;
    public CanvasGroup canvasPagina2;

    [Header("Titulo")]
    public CanvasGroup canvasTitulo;

    [Header("Botões")]
    public GameObject btnPular;
    public GameObject btnProximo;
    public GameObject btnVoltar;

    [Header("Config")]
    public float duracaoFade = 0.3f;

    private int paginaAtual = 1;
    private bool tutorialPronto = false;
    private bool animando = false;
    private bool veioDaHistoria;

    void Start()
    {
        veioDaHistoria = FluxoJogoTutorial.veioDaHistoria;

        if (btnPular != null) btnPular.SetActive(false);
        if (btnProximo != null) btnProximo.SetActive(false);
        if (btnVoltar != null) btnVoltar.SetActive(false);

        pagina1.SetActive(true);
        pagina2.SetActive(false);

        if (canvasPagina1 != null) canvasPagina1.alpha = 0f;
        if (canvasPagina2 != null) canvasPagina2.alpha = 0f;
        if (canvasTitulo != null)
            canvasTitulo.alpha = 0f;

        StartCoroutine(IniciarTutorial());
    }

    IEnumerator IniciarTutorial()
    {
        if (pergaminhoEntrada != null)
        {
            while (!pergaminhoEntrada.entradaIniciada)
                yield return null;

            while (!pergaminhoEntrada.terminouEntrada)
                yield return null;
        }

        tutorialPronto = true;

        if (btnProximo != null) btnProximo.SetActive(true);
        if (btnVoltar != null) btnVoltar.SetActive(false);

        if (veioDaHistoria && btnPular != null)
            btnPular.SetActive(true);

        yield return StartCoroutine(FadeIn(canvasTitulo));
        yield return StartCoroutine(FadeIn(canvasPagina1));
    }

    public void CliqueBtnProximo()
    {
        if (!tutorialPronto || animando) return;

        if (paginaAtual == 1)
        {
            StartCoroutine(TrocarParaPagina2());
        }
        else
        {
            FinalizarTutorial();
        }
    }

    public void CliqueBtnVoltar()
    {
        if (!tutorialPronto || animando || paginaAtual == 1) return;
        StartCoroutine(TrocarParaPagina1());
    }

    IEnumerator TrocarParaPagina2()
    {
        animando = true;

        yield return StartCoroutine(FadeOut(canvasPagina1));
        pagina1.SetActive(false);

        pagina2.SetActive(true);
        canvasPagina2.alpha = 0f;

        if (btnVoltar != null) btnVoltar.SetActive(true);

        yield return StartCoroutine(FadeIn(canvasPagina2));

        paginaAtual = 2;
        animando = false;
    }

    IEnumerator TrocarParaPagina1()
    {
        animando = true;

        yield return StartCoroutine(FadeOut(canvasPagina2));
        pagina2.SetActive(false);

        pagina1.SetActive(true);
        canvasPagina1.alpha = 0f;

        if (btnVoltar != null) btnVoltar.SetActive(false);

        yield return StartCoroutine(FadeIn(canvasPagina1));

        paginaAtual = 1;
        animando = false;
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float tempo = 0f;
        canvasGroup.alpha = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, tempo / duracaoFade);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float tempo = 0f;
        canvasGroup.alpha = 1f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, tempo / duracaoFade);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    public void PularTutorial()
    {
        SceneManager.LoadScene("SelecaoNiveis");
    }

    void FinalizarTutorial()
    {
        if (veioDaHistoria)
            SceneManager.LoadScene("SelecaoNiveis");
        else
            SceneManager.LoadScene("Menu");
    }
}