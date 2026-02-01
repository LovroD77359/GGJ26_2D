using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject memory;
    public Transform memoryPhones;
    public float duration = 10f;

    private float timer = 0f;
    private bool isActive = false;

    void Update()
    {
        if (isActive) timer += Time.deltaTime;
        if (timer >= duration)
        {
            GameManager.GM.animator.SetTrigger("memoryPanelDisappear");
            GameManager.GM.PlayDialogue(4);
            isActive = false;
            timer = 0f;
        }
    }

    public void Remember()
    {
        if (isActive) return;  

        if (!GameManager.GM.firstMemory)
            RandomizePhone();
        UpdateMemory();

        GameManager.GM.animator.SetTrigger("memoryPanelAppear");
        isActive = true;
        GameManager.GM.firstMemory = false;
    }

    void RandomizePhone()
    {
        int randomPhone = Random.Range(0, GameManager.GM.phonesToSteal.Count);
        int randomTrait = Random.Range(0, GameManager.GM.numberOfTraits);

        GameManager.GM.phonesToSteal[randomPhone][randomTrait] = 
                (GameManager.GM.phonesToSteal[randomPhone][randomTrait]
                + Random.Range(1, GameManager.GM.optionsPerTrait))
                % GameManager.GM.optionsPerTrait;

        Debug.Log("Randomized phone " + randomPhone + " trait " + randomTrait);
        GameManager.GM.LogPhones();
    }

    public void UpdateMemory()
    {
        int ptsIndex = 0;
        for (int i = 0; i < GameManager.GM.numberOfDays; i++)
        {
            Transform phone = memoryPhones.transform.GetChild(i);

            if (GameManager.GM.stolenPhoneIndexes.Contains(i))
            {
                phone.gameObject.SetActive(false);
                continue;
            }

            for (int j = 0; j < GameManager.GM.numberOfTraits; j++)
            {
                Transform trait = phone.GetChild(j);
                if (trait.name == "Color")
                {
                    phone.GetComponent<MeshRenderer>().material = GameManager.GM.materials[GameManager.GM.phonesToSteal[ptsIndex][0]][GameManager.GM.phonesToSteal[ptsIndex][1]];
                }
                else
                {
                    for (int k = 0; k < trait.childCount; k++)
                    {
                        if (k == GameManager.GM.phonesToSteal[ptsIndex][j])
                            trait.GetChild(k).gameObject.SetActive(true);
                        else
                            trait.GetChild(k).gameObject.SetActive(false);
                    }
                }
            }
            ptsIndex++;
        }
    }
}
