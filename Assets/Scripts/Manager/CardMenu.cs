using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerScripts;
    [SerializeField]
    private GameObject _cardMenu;

    [SerializeField]
    private string _cardID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            _cardMenu.SetActive(true);
        }
    }

    public void FirstCard()
    {

    }

    public void SecondCard()
    {

    }

    public void ThirdCard()
    {

    }
}
