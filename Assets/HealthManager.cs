using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
    public GameObject[] hearts;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
        }

        hearts[health].SetActive(true);
    }

}
