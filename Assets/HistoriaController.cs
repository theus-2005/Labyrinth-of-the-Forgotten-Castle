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
    public GameObject BtnVoltar;
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
    private bool historiaComecou = false;

    private Coroutine digitacaoAtual;

    void Start()
    {
        if (BtnPular != null) BtnPular.SetActive(false);
        if (BtnContinuar != null) BtnContinuar.SetActive(false);
        if (BtnVoltar != null) BtnVoltar.SetActive(false);

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
            AvancarHistoria();
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

        historiaComecou = true;

        if (BtnPular != null) BtnPular.SetActive(true);
        if (BtnContinuar != null) BtnContinuar.SetActive(true);
        if (BtnVoltar != null) BtnVoltar.SetActive(false);

        if (frases != null && frases.Length > 0)
            IniciarDigitacao();
    }

    void IniciarDigitacao()
    {
        PararDigitacaoAtual();
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
        AtualizarBotoes();
    }

    void PararDigitacaoAtual()
    {
        if (digitacaoAtual != null)
        {
            StopCoroutine(digitacaoAtual);
            digitacaoAtual = null;
        }

        estaDigitando = false;
    }

    void CompletarFraseAtual()
    {
        PararDigitacaoAtual();

        TextoHistoria.text = frases[indiceAtual];

        VerificarEventosDaFrase();
        AtualizarBotoes();
    }

    public void AvancarHistoria()
    {
        if (!historiaComecou || frases == null || frases.Length == 0)
            return;

        if (estaDigitando)
        {
            CompletarFraseAtual();
            return;
        }

        if (indiceAtual == frases.Length - 1)
        {
            Continuar();
            return;
        }

        indiceAtual++;

        AtualizarBotoes();
        IniciarDigitacao();
    }

    public void VoltarHistoria()
    {
        if (!historiaComecou || frases == null || frases.Length == 0)
            return;

        if (indiceAtual == 0)
            return;

        PararDigitacaoAtual();

        indiceAtual--;

        TextoHistoria.text = frases[indiceAtual];

        AtualizarSombrasAoVoltar();
        AtualizarBotoes();
    }

    void AtualizarBotoes()
    {
        if (BtnVoltar != null)
            BtnVoltar.SetActive(indiceAtual > 0);

        if (BtnContinuar != null)
            BtnContinuar.SetActive(true);
    }

    void VerificarEventosDaFrase()
    {
        if (indiceAtual >= 3 && SombraBruxa != null)
            SombraBruxa.SetActive(true);

        if (indiceAtual >= 4 && SombraCavaleiro != null)
            SombraCavaleiro.SetActive(true);
    }

    void AtualizarSombrasAoVoltar()
    {
        if (SombraBruxa != null)
            SombraBruxa.SetActive(indiceAtual >= 3);

        if (SombraCavaleiro != null)
            SombraCavaleiro.SetActive(indiceAtual >= 4);
    }

    public void PularHistoria()
    {
        if (pularVaiDiretoParaCena)
        {
            SceneManager.LoadScene(cenaPular);
            return;
        }

        PararDigitacaoAtual();

        indiceAtual = frases.Length - 1;
        TextoHistoria.text = frases[indiceAtual];

        if (BtnPular != null) BtnPular.SetActive(false);
        if (BtnContinuar != null) BtnContinuar.SetActive(true);

        AtualizarSombrasAoVoltar();
        AtualizarBotoes();
    }

    public void Continuar()
    {
        if (marcarComoVeioDaHistoria)
            FluxoJogoTutorial.veioDaHistoria = true;

        SceneManager.LoadScene(cenaContinuar);
    }
}
