using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HistoriaController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI TextoHistoria;
    public GameObject BtnPular;
    public GameObject BtnContinuar;
    public GameObject SombraBruxa;
    public GameObject SombraCavaleiro;

    [Header("Entrada")]
    public PergaminhoEntrada pergaminhoEntrada;

    [Header("Texto")]
    [TextArea(2, 5)]
    public string[] frases;
    public float velocidadeTexto = 0.03f;

    [Header("Cenas")]
    public string cenaContinuar = "Tutorial";
    public string cenaPular = "Tutorial";
    public bool pularVaiDiretoParaCena = false;
    public bool marcarComoVeioDaHistoria = false;

    private int indiceAtual = 0;
    private bool estaDigitando = false;
    private bool historiaTerminou = false;
    private bool historiaComecou = false;

    private Coroutine digitacaoAtual;

    void Start()
    {
        if (BtnPular != null) BtnPular.SetActive(false);
        if (BtnContinuar != null) BtnContinuar.SetActive(false);
        if (SombraBruxa != null) SombraBruxa.SetActive(false);
        if (SombraCavaleiro != null) SombraCavaleiro.SetActive(false);
        if (TextoHistoria != null) TextoHistoria.text = "";

        StartCoroutine(IniciarHistoria());
    }

    void Update()
    {
        if (!historiaComecou) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (historiaTerminou) return;

            if (estaDigitando)
            {
                CompletarFraseAtual();
            }
            else
            {
                ProximaFrase();
            }
        }
    }

    IEnumerator IniciarHistoria()
    {
        if (pergaminhoEntrada != null)
        {
            while (!pergaminhoEntrada.entradaIniciada)
                yield return null;

            while (!pergaminhoEntrada.terminouEntrada)
                yield return null;
        }

        if (BtnPular != null) BtnPular.SetActive(true);

        historiaComecou = true;

        if (frases != null && frases.Length > 0)
            IniciarDigitacao();
    }

    void IniciarDigitacao()
    {
        if (digitacaoAtual != null)
            StopCoroutine(digitacaoAtual);

        digitacaoAtual = StartCoroutine(DigitarFrase());
    }

    IEnumerator DigitarFrase()
    {
        estaDigitando = true;
        TextoHistoria.text = "";

        string fraseAtual = frases[indiceAtual];

        foreach (char letra in fraseAtual)
        {
            TextoHistoria.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }

        estaDigitando = false;
        digitacaoAtual = null;

        VerificarEventosDaFrase();
        VerificarFimDaHistoria();
    }

    void CompletarFraseAtual()
    {
        if (digitacaoAtual != null)
        {
            StopCoroutine(digitacaoAtual);
            digitacaoAtual = null;
        }

        TextoHistoria.text = frases[indiceAtual];
        estaDigitando = false;

        VerificarEventosDaFrase();
        VerificarFimDaHistoria();
    }

    void ProximaFrase()
    {
        indiceAtual++;

        if (indiceAtual < frases.Length)
        {
            IniciarDigitacao();
        }
        else
        {
            FinalizarHistoria();
        }
    }

    void VerificarEventosDaFrase()
    {
        if (indiceAtual == 3 && SombraBruxa != null)
            SombraBruxa.SetActive(true);

        if (indiceAtual == 4 && SombraCavaleiro != null)
            SombraCavaleiro.SetActive(true);
    }

    void VerificarFimDaHistoria()
    {
        if (indiceAtual == frases.Length - 1)
            FinalizarHistoria();
    }

    void FinalizarHistoria()
    {
        historiaTerminou = true;
        estaDigitando = false;

        if (digitacaoAtual != null)
        {
            StopCoroutine(digitacaoAtual);
            digitacaoAtual = null;
        }

        if (BtnContinuar != null) BtnContinuar.SetActive(true);
        if (BtnPular != null) BtnPular.SetActive(false);
    }

    public void PularHistoria()
    {
        if (pularVaiDiretoParaCena)
        {
            SceneManager.LoadScene(cenaPular);
            return;
        }

        if (digitacaoAtual != null)
        {
            StopCoroutine(digitacaoAtual);
            digitacaoAtual = null;
        }

        StopAllCoroutines();

        indiceAtual = frases.Length - 1;
        TextoHistoria.text = frases[indiceAtual];

        estaDigitando = false;
        historiaTerminou = true;
        historiaComecou = true;

        if (BtnContinuar != null) BtnContinuar.SetActive(true);
        if (BtnPular != null) BtnPular.SetActive(false);

        if (SombraBruxa != null) SombraBruxa.SetActive(true);
        if (SombraCavaleiro != null) SombraCavaleiro.SetActive(true);
    }

    public void Continuar()
    {
        if (marcarComoVeioDaHistoria)
            FluxoJogoTutorial.veioDaHistoria = true;

        SceneManager.LoadScene(cenaContinuar);
    }
}
