using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Manager.Instance.UpdateCoinCount(coinValue);
            AudioManager.Instance.GetAudio(gameObject.tag).Play();
            gameObject.SetActive(false);
        }
    }
}
