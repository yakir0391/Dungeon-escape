using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public Text _playerGemsCountText;
    public Image _selectionImage;
    public Text _gemCountText;
    public Image[] _livesBar;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int gemsCount)
    {
        _gemCountText.text = gemsCount.ToString();
    }

    public void OpenShop(int gemsCount)
    {
        _playerGemsCountText.text = gemsCount.ToString() + "G";
    }

    public void UpdateLives(int livesRemaining)
    {
        _livesBar[livesRemaining].enabled = false;
    }
}
