using UnityEngine;
using System.Collections.Generic;

public class DistribuidorDeLogica : MonoBehaviour
{
    public List<GameObject> todasAsPortas; 
    public List<BotaoAlternador> todosOsBotoes; 

    void Start()
    {
        if (todasAsPortas.Count != 5 || todosOsBotoes.Count != 5)
        {
            Debug.LogError("Certifique-se de ter 5 portas e 5 botões no Inspector!");
            return;
        }
        DistribuirPortas();
    }

    void DistribuirPortas()
{
    // Embaralhar as portas
    List<GameObject> portasEmbaralhadas = new List<GameObject>(todasAsPortas);
    for (int i = portasEmbaralhadas.Count - 1; i > 0; i--)
    {
        int rnd = Random.Range(0, i + 1);
        GameObject temp = portasEmbaralhadas[i];
        portasEmbaralhadas[i] = portasEmbaralhadas[rnd];
        portasEmbaralhadas[rnd] = temp;
    }

    // Cada botão pega 2 portas diferentes de forma circular
    for (int i = 0; i < todosOsBotoes.Count; i++)
    {
        todosOsBotoes[i].portasControladas.Clear();
        todosOsBotoes[i].portasControladas.Add(portasEmbaralhadas[i]);
        todosOsBotoes[i].portasControladas.Add(portasEmbaralhadas[(i + 1) % portasEmbaralhadas.Count]);
        todosOsBotoes[i].ConfiguracaoInicial();
    }
}
}