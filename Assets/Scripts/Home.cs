using System.Collections;
using UnityEngine;

public class Home : MonoBehaviour
{
    public Transform shelfPhones;
    public Transform phoneAppearPlaceholder;
    public float scaleDifferenceFactor = 2f;

    public Transform phoneToAdd;
    private Vector3 originalPosition = new Vector3(-4.15f, 1.25f, 25);
    private Vector3 originalScale = Vector3.one * 0.35f;

    void Start()
    {
        GetComponent<Animator>().SetBool("win", GameManager.GM.DayEnd());

        for (int i = 0; i < GameManager.GM.stolenPhones.Count; i++)
        {
            Transform phone = shelfPhones.GetChild(i);
            for (int j = 0; j < GameManager.GM.numberOfTraits; j++)
            {
                Transform trait = phone.GetChild(j);
                if (trait.name == "Color")
                {
                    phone.GetComponent<MeshRenderer>().material = GameManager.GM.materials[GameManager.GM.stolenPhones[i][0]][GameManager.GM.stolenPhones[i][1]];
                }
                else
                {
                    for (int k = 0; k < trait.childCount; k++)
                    {
                        if (k == GameManager.GM.stolenPhones[i][j])
                            trait.GetChild(k).gameObject.SetActive(true);
                        else
                            trait.GetChild(k).gameObject.SetActive(false);
                    }
                }
            }

            if (i == GameManager.GM.stolenPhones.Count - 1)
            {
                phoneToAdd = phone;
                originalPosition = phone.position;
                phone.position = phoneAppearPlaceholder.position;
                originalScale = phone.localScale;
                phone.localScale = Vector3.zero;
                break;
            }

            phone.gameObject.SetActive(true);
        }
    }

    [ContextMenu("Add phone")]
    public void AddPhone()
    {
        StartCoroutine(AddPhoneCoroutine());
    }

    IEnumerator AddPhoneCoroutine()
    {
        phoneToAdd.GetChild(phoneToAdd.childCount - 1).gameObject.SetActive(true);
        phoneToAdd.position -= Vector3.forward;
        phoneToAdd.gameObject.SetActive(true);

        Vector3 startScale = phoneToAdd.localScale;
        for (float i = 0; i < 30; i++)
        {
            phoneToAdd.localScale = Vector3.Lerp(startScale, originalScale / scaleDifferenceFactor, i / 30);
            yield return new WaitForSeconds(0.0167f);
        }
        phoneToAdd.localScale = originalScale / scaleDifferenceFactor;

        yield return new WaitForSeconds(0.25f);
        phoneToAdd.GetChild(phoneToAdd.childCount - 1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);

        startScale = phoneToAdd.localScale;
        Vector3 startPosition = phoneToAdd.position;
        for (float i = 0; i < 60; i++)
        {
            phoneToAdd.localScale = Vector3.Lerp(startScale, originalScale, i / 60);
            phoneToAdd.position = Vector3.Lerp(startPosition, originalPosition, i / 60);
            yield return new WaitForSeconds(0.0167f);
        }
        phoneToAdd.localScale = originalScale;
        phoneToAdd.position = originalPosition;

        phoneToAdd.position += Vector3.forward;
    }
}
