using UnityEngine;
using UnityEngine.SceneManagement;

public class Saida : MonoBehaviour
{
    public string cenaFinal = "HistoriaFinal";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(cenaFinal);
    }
}