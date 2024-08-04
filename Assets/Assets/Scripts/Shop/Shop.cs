using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject _shopPanel;
    public int _currentSelectedItem;
    public int _currentItemCost;
    Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player = collision.GetComponent<Player>();

            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player._diamondsAmount);
            }
            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(79);
                _currentItemCost = 200;
                _currentSelectedItem = 0;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-13);
                _currentItemCost = 400;
                _currentSelectedItem = 1;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-124);
                _currentItemCost = 100;
                _currentSelectedItem = 2;
                break;

        }
    }

    public void BuyItem()
    {
        if (_player._diamondsAmount < _currentItemCost)
        {
            Debug.Log("You dont have enough gems, closing shop");
            _shopPanel.SetActive(false);
        }
        else
        {
            if (_currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            _player._diamondsAmount -= _currentItemCost;
            Debug.Log("Purchesed" + _currentSelectedItem);
            Debug.Log("Remaining gems : " + _player._diamondsAmount);
            _shopPanel.SetActive(false);
        }
    }
}
