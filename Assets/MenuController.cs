using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jogar()
    {
        // veio da história = true porque vai passar pela história antes do tutorial
        FluxoJogoTutorial.veioDaHistoria = true;

        SceneManager.LoadScene("HistoriaInicio");
    }

    public void AbrirTutorial()
    {
        // veio do menu = false
        FluxoJogoTutorial.veioDaHistoria = false;

        SceneManager.LoadScene("Tutorial");
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");

        Application.Quit();
    }
}
