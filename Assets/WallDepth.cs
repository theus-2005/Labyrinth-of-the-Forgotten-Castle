using UnityEngine;
using UnityEngine.Tilemaps;

public class WallDepth : MonoBehaviour 
{
    TilemapRenderer tr;
    
    void Awake()
    {
        tr = GetComponent<TilemapRenderer>();
    }
    
    void LateUpdate()
    {
        // Mesmo multiplicador, mas -1 para parede sempre "perder" em empate
        float avgY = tr.bounds.center.y;
        tr.sortingOrder = -(int)(avgY * 128f) - 1;
    }
}