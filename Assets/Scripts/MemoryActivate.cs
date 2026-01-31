using UnityEngine;

public class MemoryActivate : MonoBehaviour
{
    public GameObject Memory;
    public float duration = 10f;

    private float timer = 0f;
    private bool isActive = false;
    private bool isFirstTime = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isActive)
        {
            Memory.SetActive(true);
            isActive = true;
            if(!isFirstTime)            
                RandomizePhone();
            isFirstTime = false;
        }
        if (isActive) timer += Time.deltaTime;
        if (timer >= duration)
        {
            Memory.SetActive(false);
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
}
