using UnityEngine;

public class FinalDepthSort : MonoBehaviour 
{
    SpriteRenderer sr;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    
    void LateUpdate()
    {
        // CRÍTICO: Multiplicador ALTO + arredondamento
        sr.sortingOrder = -(int)(transform.position.y * 128f);
    }
}