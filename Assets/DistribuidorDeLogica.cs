using UnityEngine;
using System.Collections.Generic;

public class DistribuidorDeLogica : MonoBehaviour
{
    public List<GameObject> todasAsPortas; 
    public List<BotaoAlternador> todosOsBotoes; 
    public int portasPorBotao = 2; 

    void Start()
    {
        DistribuirPortas();
    }

    void DistribuirPortas()
    {
        if (todasAsPortas.Count == 0 || todosOsBotoes.Count == 0) return;

        // Criamos uma lista gigante repetindo as portas (se cada uma deve estar em 2 botões)
        // Isso garante que cada porta seja sorteada a quantidade de vezes necessária
        List<GameObject> poolDePortas = new List<GameObject>();
        
        // Adiciona cada porta na lista 2 vezes (ou quantas vezes você quiser que ela apareça)
        for (int i = 0; i < 2; i++) 
        {
            poolDePortas.AddRange(todasAsPortas);
        }

        // Embaralha essa lista gigante
        for (int i = 0; i < poolDePortas.Count; i++)
        {
            GameObject temp = poolDePortas[i];
            int randomIndex = Random.Range(i, poolDePortas.Count);
            poolDePortas[i] = poolDePortas[randomIndex];
            poolDePortas[randomIndex] = temp;
        }

        int portaIndex = 0;

        foreach (BotaoAlternador botao in todosOsBotoes)
        {
            botao.portasControladas.Clear();
            
            for (int i = 0; i < portasPorBotao; i++)
            {
                if (portaIndex < poolDePortas.Count)
                {
                    botao.portasControladas.Add(poolDePortas[portaIndex]);
                    portaIndex++;
                }
            }
            botao.ConfiguracaoInicial();
        }
    }
}