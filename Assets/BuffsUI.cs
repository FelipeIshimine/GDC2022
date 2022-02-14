using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffsUI : MonoBehaviour
{

    public AnimatedContainer animatedContainer;

    [Header("Block")]
    public TextMeshProUGUI blockText;
    public GameObject block;

    [Header("Dodge")]
    public GameObject dodge;
    public TextMeshProUGUI dodgeText;

    [ShowInInspector]private readonly HashSet<GameObject> _activeBuffs = new HashSet<GameObject>();

    public void SetBlock(int nValue)=> SetBuff(blockText, $"{(nValue > 0 ? "+" : "-")}{nValue}", nValue, block); 
    public void SetDodge(int nValue)=> SetBuff(dodgeText, $"{(nValue > 0 ? "+" : "-")}{nValue}%",nValue, dodge);

    private void SetBuff(TextMeshProUGUI textComponent,string text, int value, GameObject container)
    {
        textComponent.text = text;
        if (value == 0)
        {
            _activeBuffs.Remove(container);
            container.gameObject.SetActive(false);
        }
        else
        {
            _activeBuffs.Add(container);
            container.gameObject.SetActive(true);
        }

        if (_activeBuffs.Count > 0)
            animatedContainer.Open();
        else
            animatedContainer.Close();
    }
}
