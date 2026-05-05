using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletorDificuldade : MonoBehaviour
{
    public void EscolherNormal()
    {
        PlayerPrefs.SetInt("dificuldade", 0);
        SceneManager.LoadScene("SampleScene"); // nome da sua cena do jogo
    }

    public void EscolherMedio()
    {
        PlayerPrefs.SetInt("dificuldade", 1);
        SceneManager.LoadScene("SampleScene");
    }

    public void EscolherDificil()
    {
        PlayerPrefs.SetInt("dificuldade", 2);
        SceneManager.LoadScene("SampleScene");
    }
}