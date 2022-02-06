using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockUI : MonoBehaviour
{
    public AnimatedContainer animContainer;
    public TextMeshProUGUI text;

    public void SetValue(int count)
    {
        text.text = $"+{count.ToString()}";
        if (count > 0)
        {
            gameObject.SetActive(true);
            Open();
        }
        else
            Close();
    }
    public void Open() => animContainer.Open();
    public void Close() => animContainer.Close();
}
