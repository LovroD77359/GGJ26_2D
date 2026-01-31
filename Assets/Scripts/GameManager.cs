using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [HideInInspector] public int day = 0;
    [HideInInspector] public Dictionary<int, DialogueInfo> dialogue;

    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dialogue = JsonConvert.DeserializeObject<Dictionary<int, DialogueInfo>>(Resources.Load<TextAsset>("dialogue").text);
    }
}
