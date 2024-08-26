using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moneyUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI text;
    public static int money;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Money :" + money;
    }


    public static void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }
}
