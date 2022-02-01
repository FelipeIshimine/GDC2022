using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelUI : BaseMonoSingleton<InfoPanelUI>
{
    private GameObject _activeInfo;
    public CoinInfoUI coinInfoUI;
    
    public static void SetInfo(Coin coin)
    {
        Instance.SetActiveInfo(Instance.coinInfoUI.gameObject);
        Instance.coinInfoUI.Initialize(coin);
    }

    private void SetActiveInfo(GameObject nGameObject)
    {
        if(Instance._activeInfo)
            Instance._activeInfo.gameObject.SetActive(false);
        _activeInfo = nGameObject;
        if(Instance._activeInfo)
            Instance._activeInfo.gameObject.SetActive(true);
    }

    public static void Clear()
    {
        //Instance.SetActiveInfo(null);
    }
}
