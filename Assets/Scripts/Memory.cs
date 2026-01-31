using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public GameObject memory;
    public Transform memoryPhones;
    public float duration = 10f;

    private float timer = 0f;
    private bool isActive = false;
    private bool isFirstTime = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isActive)
        {
            if (!isFirstTime)
                RandomizePhone();
            UpdateMemory();

            memory.SetActive(true);
            isActive = true;
            isFirstTime = false;
        }
        if (isActive) timer += Time.deltaTime;
        if (timer >= duration)
        {
            memory.SetActive(false);
            isActive = false;
            timer = 0f;
        }
    }

    void RandomizePhone()
    {
        int randomPhone = Random.Range(0, GameManager.GM.numberOfDays);
        int randomTrait = Random.Range(0, GameManager.GM.numberOfTraits);

        GameManager.GM.phonesToSteal[randomPhone][randomTrait] = 
                (GameManager.GM.phonesToSteal[randomPhone][randomTrait]
                + Random.Range(1, GameManager.GM.optionsPerTrait))
                % GameManager.GM.optionsPerTrait;

        Debug.Log("Randomized phone " + randomPhone + " trait " + randomTrait);
        GameManager.GM.LogPhones();
    }

    void UpdateMemory()
    {
        for (int i = 0; i < GameManager.GM.numberOfDays; i++)
        {
            Transform phone = memoryPhones.transform.GetChild(i);
            for (int j = 0; j < GameManager.GM.numberOfTraits; j++)
            {
                Transform trait = phone.GetChild(j);
                if (trait.name == "Color")
                {
                    phone.GetComponent<MeshRenderer>().material = GameManager.GM.materials[GameManager.GM.phonesToSteal[i][0]][GameManager.GM.phonesToSteal[i][1]];
                }
                else
                {
                    for (int k = 0; k < trait.childCount; k++)
                    {
                        if (k == GameManager.GM.phonesToSteal[i][j])
                            trait.GetChild(k).gameObject.SetActive(true);
                        else
                            trait.GetChild(k).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
