using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CycleMaterial : MonoBehaviour
{
    private MeshRenderer mr;
    private int colorIndex = 0;
    private int patternIndex = 0;
    private CheckPhone checkPhone;

    public void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material = GameManager.GM.materials[colorIndex][patternIndex];
        checkPhone = GetComponent<CheckPhone>();
    }

    public void NextColor()
    {
        colorIndex = (colorIndex + 1) % GameManager.GM.optionsPerTrait;
        mr.material = GameManager.GM.materials[colorIndex][patternIndex];
        checkPhone.phone[0] = colorIndex;
    }

    public void PreviousColor()
    { 
        colorIndex = (colorIndex - 1 + GameManager.GM.optionsPerTrait) % GameManager.GM.optionsPerTrait;
        mr.material = GameManager.GM.materials[colorIndex][patternIndex];
        checkPhone.phone[0] = colorIndex;
    }

    public void NextPattern()
    {
        patternIndex = (patternIndex + 1) % GameManager.GM.optionsPerTrait;
        mr.material = GameManager.GM.materials[colorIndex][patternIndex];
        checkPhone.phone[1] = patternIndex;
    }

    public void PreviousPattern()
    {
        patternIndex = (patternIndex - 1 + GameManager.GM.optionsPerTrait) % GameManager.GM.optionsPerTrait;
        mr.material = GameManager.GM.materials[colorIndex][patternIndex];
        checkPhone.phone[1] = patternIndex;
    }
}
