using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public Text coinText;  // Assign in Inspector

    [Header("Coins")]
    public int coinsCollected = 0;

    [Header("PowerUps")]
    public float speedBoostDuration = 5f;
    public float shieldDuration = 5f;

    [HideInInspector] public bool isSpeedBoostActive = false;
    [HideInInspector] public bool isShieldActive = false;

    void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // ---------------- COIN COLLECTION ----------------
    public void CollectCoin(int amount = 1)
    {
        coinsCollected += amount;
        if (coinText != null)
            coinText.text = "Coins: " + coinsCollected;

        Debug.Log("Coins: " + coinsCollected);
    }

    // ---------------- POWERUPS ----------------
    public void ActivateSpeedBoost(PlaneBehaviour plane)
    {
        if (!isSpeedBoostActive)
            StartCoroutine(SpeedBoostRoutine(plane));
    }

    IEnumerator SpeedBoostRoutine(PlaneBehaviour plane)
    {
        isSpeedBoostActive = true;
        float originalSpeed = plane.speed;
        plane.speed *= 2; // Double the speed
        yield return new WaitForSeconds(speedBoostDuration);
        plane.speed = originalSpeed;
        isSpeedBoostActive = false;
    }

    public void ActivateShield(PlaneBehaviour plane)
    {
        if (!isShieldActive)
            StartCoroutine(ShieldRoutine(plane));
    }

    IEnumerator ShieldRoutine(PlaneBehaviour plane)
    {
        isShieldActive = true;
        plane.shieldActive = true; // Make sure PlaneBehaviour has this variable
        yield return new WaitForSeconds(shieldDuration);
        plane.shieldActive = false;
        isShieldActive = false;
    }
}
