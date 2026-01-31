using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CycleColors : MonoBehaviour
{
    List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };

    private Material CurrentMaterial;

    private void Start()
    {
        CurrentMaterial = GetComponent<Renderer>().material;
        CurrentMaterial.color = colors[0];
    }

    public void NextColor()
    {
        int currentIndex = colors.IndexOf(CurrentMaterial.color);
        int nextIndex = (currentIndex + 1) % colors.Count;
        CurrentMaterial.color = colors[nextIndex];
    }

    public void PreviousColor()
    {
        int currentIndex = colors.IndexOf(CurrentMaterial.color);
        int nextIndex = (currentIndex - 1 + colors.Count) % colors.Count;
        CurrentMaterial.color = colors[nextIndex];
    }

    void OnDestroy()
    {
        CurrentMaterial.color = colors[0];
    }
}