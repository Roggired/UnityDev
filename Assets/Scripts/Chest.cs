using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject coinPrefab;
    public RectTransform coinOnScorePlate;
    public Text coinScore;
    public int coinsNumber = 10;

    public void Play()
    {
        StartCoroutine(SpawnCoins());
    }
    private IEnumerator SpawnCoins()
    {
        for (int i = 0; i < coinsNumber; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = transform.position;

            coin.GetComponent<TakeCoin>().coinScore = coinScore;
            coin.GetComponent<TakeCoin>().coinOnScorePlate = coinOnScorePlate;
            coin.GetComponent<TakeCoin>().Play();

            yield return new WaitForSeconds(0.1f);
        }
    }
}
