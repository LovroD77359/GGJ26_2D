using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public string mainScene;
    public string victoryScene;
    public int numberOfDays = 5;
    public int numberOfTraits = 5;
    public int optionsPerTrait = 5;

    [HideInInspector] public int day = 0;
    [HideInInspector] public bool firstMemory = true;
    [HideInInspector] public Dictionary<int, DialogueInfo> dialogue = new();
    [HideInInspector] public List<List<Material>> materials = new();
    [HideInInspector] public List<List<int>> phonesToSteal = new();
    [HideInInspector] public List<List<int>> phonesToStealOriginal = new();
    [HideInInspector] public List<List<int>> phonesToStealOriginal2 = new();
    [HideInInspector] public List<List<int>> stolenPhones = new();
    [HideInInspector] public List<int> stolenPhoneIndexes = new();
    [HideInInspector] public Dialogue dialogueScript;
    [HideInInspector] public Animator animator;


    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
            Destroy(gameObject);

        dialogue = JsonConvert.DeserializeObject<Dictionary<int, DialogueInfo>>(Resources.Load<TextAsset>("dialogue").text);

        Material[] unsortedMaterials = Resources.LoadAll<Material>("PhoneMaterials");
        for (int i = 0; i < optionsPerTrait; i++)
        {
            if (materials.Count <= i)
                materials.Add(new List<Material>());
            for (int j = 0; j < optionsPerTrait; j++)
            {
                materials[i].Add(unsortedMaterials[i * optionsPerTrait + j]);
            }
        }
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        day = 0;
        firstMemory = true;
        phonesToSteal.Clear();
        phonesToStealOriginal.Clear();
        phonesToStealOriginal2.Clear();
        stolenPhones.Clear();
        stolenPhoneIndexes.Clear();

        // Phone generation
        List<List<int>> traitsTaken = new();
        for (int i = 0; i < numberOfDays; i++)
        {
            List<int> phone = new();
            for (int j = 0; j < numberOfTraits; j++)
            {
                int index;
                do {
                    index = Random.Range(0, optionsPerTrait);
                } while (traitsTaken.Count > j && traitsTaken[j].Count > 0 && traitsTaken[j].Contains(index));

                phone.Add(index);
                if (traitsTaken.Count <= j)
                    traitsTaken.Add(new List<int>());
                traitsTaken[j].Add(index);
            }
            phonesToSteal.Add(phone);
        }
        phonesToStealOriginal = new ();
        phonesToStealOriginal2 = new();
        foreach (List<int> phone in phonesToSteal)
        {
            phonesToStealOriginal.Add(new List<int>(phone));
            phonesToStealOriginal2.Add(new List<int>(phone));
        }
        LogPhones();
    }

    public void LogPhones()
    {
        // Logganje
        foreach (List<int> phone in phonesToStealOriginal)
        {
            string s = "";
            foreach (int index in phone)
            {
                s += index.ToString() + " ";
            }
            Debug.Log(s);
        }
    }

    public void CheckPhone(List<int> phone)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAa");
        string s = "";
        foreach (int h in phone)
        {
            s += h.ToString() + " ";
        }
        Debug.Log(s);

        // Logganje
        foreach (List<int> a in phonesToStealOriginal)
        {
            s = "";
            foreach (int indgex in a)
            {
                s += indgex.ToString() + " ";
            }
            Debug.Log(s);
        }
        var index = phonesToStealOriginal2.FindIndex(p => p.SequenceEqual(phone));
        if (index >= 0)
        {
            stolenPhones.Add(phonesToStealOriginal2[index]);
            phonesToStealOriginal2.RemoveAt(index);
            phonesToSteal.RemoveAt(index);
            stolenPhoneIndexes.Add(phonesToStealOriginal.FindIndex(p => p.SequenceEqual(phone)));
            animator.SetTrigger("success");
        }
        else
        {
            animator.SetTrigger("loss");
        }
    }

    public void PlayDialogue(int level)
    {
        dialogueScript.PlayDialogue(level);
    }

    public void PhoneDescribeStart()
    {
        animator.SetTrigger("phoneDescribeStart");
    }

    public bool DayEnd()
    {
        day++;
        if (day >= numberOfDays)
            return true;
        else
            return false;
    }
}
