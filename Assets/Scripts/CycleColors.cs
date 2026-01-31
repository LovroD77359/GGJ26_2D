using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CycleColors : MonoBehaviour
{
    public int traitIndex;
    public List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };

    private Material CurrentMaterial;
    private CheckPhone checkPhone;

    private void Start()
    {
        CurrentMaterial = GetComponent<Renderer>().material;
        CurrentMaterial.color = colors[0];
        checkPhone = GetComponentInParent<CheckPhone>();
    }

    public void NextColor()
    {
        int currentIndex = colors.IndexOf(CurrentMaterial.color);
        int nextIndex = (currentIndex + 1) % colors.Count;
        CurrentMaterial.color = colors[nextIndex];
        checkPhone.phone[traitIndex] = currentIndex;
    }

    public void PreviousColor()
    {
        int currentIndex = colors.IndexOf(CurrentMaterial.color);
        int nextIndex = (currentIndex - 1 + colors.Count) % colors.Count;
        CurrentMaterial.color = colors[nextIndex];
        checkPhone.phone[traitIndex] = currentIndex;
    }

    void OnDestroy()
    {
        CurrentMaterial.color = colors[0];
    }
}