using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
                VoltarAoJogo();
            else
                AbrirPause();
        }
    }

    public void AbrirPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void VoltarAoJogo()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SairParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}