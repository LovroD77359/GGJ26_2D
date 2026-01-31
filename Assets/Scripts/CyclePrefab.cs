using Unity.VisualScripting;
using UnityEngine;

public class CyclePrefab : MonoBehaviour
{
    public int traitIndex;

    private int currentIndex = 0;
    private int childCount = 0;
    private CheckPhone checkPhone;

    void Start()
    {
        checkPhone = GetComponentInParent<CheckPhone>();
    }

    public void CycleToNextChild()
    {
        childCount = transform.childCount;
        if (childCount == 0) return;
        transform.GetChild(currentIndex).gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % childCount;
        transform.GetChild(currentIndex).gameObject.SetActive(true);
        checkPhone.phone[traitIndex] = currentIndex;
    }
    public void CycleToPreviousChild()
    {
        childCount = transform.childCount;
        if (childCount == 0) return;
        transform.GetChild(currentIndex).gameObject.SetActive(false);
        currentIndex = (currentIndex - 1 + childCount) % childCount;
        transform.GetChild(currentIndex).gameObject.SetActive(true);
        checkPhone.phone[traitIndex] = currentIndex;
    }

}
