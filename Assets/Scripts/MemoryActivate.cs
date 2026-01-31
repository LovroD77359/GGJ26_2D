using UnityEngine;

public class MemoryActivate : MonoBehaviour
{
    public Canvas MemoryCanvas;
    public float duration = 10f;
    
    private float timer = 0f;
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MemoryCanvas.enabled = true;
            isActive = true;
        }
        if (isActive) timer += Time.deltaTime;
        if (timer >= duration)
        {
            MemoryCanvas.enabled = false;
            isActive = false;
            timer = 0f;
        }
    }
}
