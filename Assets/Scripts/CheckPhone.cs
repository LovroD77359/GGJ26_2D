using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CheckPhone : MonoBehaviour
{
    public List<int> phone;

    public void Check()
    {
        GameManager.GM.CheckPhone(phone);
    }
}
