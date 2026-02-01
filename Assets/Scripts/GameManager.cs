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
    [HideInInspector] public Dictionary<int, DialogueInfo> dialogue = new();
    [HideInInspector] public List<List<Material>> materials = new();
    [HideInInspector] public List<List<int>> phonesToSteal = new();
    [HideInInspector] public List<List<int>> stolenPhones = new();
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
        phonesToSteal.Clear();
        stolenPhones.Clear();

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
        LogPhones();
    }

    public void LogPhones()
    {
        // Logganje
        foreach (List<int> phone in phonesToSteal)
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
        var index = phonesToSteal.FindIndex(p => p.SequenceEqual(phone));
        if (index >= 0)
        {
            stolenPhones.Add(phonesToSteal[index]);
            phonesToSteal.RemoveAt(index);
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

    public void DayEnd()
    {
        day++;
        if (day >= numberOfDays)
        {
            // Remove from memory ?
            SceneManager.LoadSceneAsync(mainScene);
        }
        else
        {
            SceneManager.LoadScene(victoryScene);
        }
    }
}
