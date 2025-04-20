using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    [SerializeField] private GameObject asteroidExplosion;
    [SerializeField] private GameObject scorePopup;
    public void AsteroidDestoryed()
    {
        Instantiate(asteroidExplosion, transform.position, transform.rotation);

        float distanceFromPlayer = Vector3.Distance(transform.position, Vector3.zero);
        int asteroidScore = 10 * (int)distanceFromPlayer;

        scorePopup.transform.GetComponentInChildren<TextMeshProUGUI>().text = asteroidScore.ToString();

        var scorePopupGO = Instantiate(scorePopup, transform.position, Quaternion.identity);
        scorePopupGO.transform.localScale = transform.localScale * (distanceFromPlayer / 5);

        GameController.Instance.UpdateGameScore(asteroidScore);

        Destroy(this.gameObject);
    }
}
