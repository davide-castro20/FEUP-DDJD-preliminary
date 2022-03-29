using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarScript : MonoBehaviour
{
    [SerializeField] private GameObject bananas;
    [SerializeField] private GameObject pencils;
        
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        bananas.GetComponent<TextMeshProUGUI>().text = playerScript.GetAmmo(AmmoScript.Ammo.Banana).ToString();
        pencils.GetComponent<TextMeshProUGUI>().text = playerScript.GetAmmo(AmmoScript.Ammo.Pencil).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePencilAmmo(String newAmmo)
    {
        pencils.GetComponent<TextMeshProUGUI>().text = newAmmo;
    }

    public void UpdateBananaCount(string p0)
    {
        bananas.GetComponent<TextMeshProUGUI>().text = p0;
    }
}
