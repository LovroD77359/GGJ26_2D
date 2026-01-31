using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Cyclepatterns : MonoBehaviour
{
    public int traitIndex;
    public List<Texture> patterns = new List<Texture>();

    private Material CurrentMaterial;
    private int currentIndex = 0;
    private CheckPhone checkPhone;

    private void Start()
    {
        CurrentMaterial = GetComponent<Renderer>().material;
        CurrentMaterial.mainTexture = patterns[0];
        checkPhone = GetComponentInParent<CheckPhone>();
    }

    public void Nexttexture()
    {
        if (patterns.Count == 0) return;
        currentIndex = (currentIndex + 1) % patterns.Count;
        CurrentMaterial.mainTexture = patterns[currentIndex];
        checkPhone.phone[traitIndex] = currentIndex;
    }

    public void Previoustexture()
    {
        if (patterns.Count == 0) return;
        currentIndex = (currentIndex - 1 + patterns.Count) % patterns.Count;
        CurrentMaterial.mainTexture = patterns[currentIndex];
        checkPhone.phone[traitIndex] = currentIndex;
    }

    void OnDestroy()
    {
            CurrentMaterial.mainTexture = patterns[0];
    }
}