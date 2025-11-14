using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType { SpeedBoost, Shield }
    public PowerType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlaneBehaviour plane = other.GetComponent<PlaneBehaviour>();

            if (type == PowerType.SpeedBoost)
                GameManager.instance.ActivateSpeedBoost(plane);

            if (type == PowerType.Shield)
                GameManager.instance.ActivateShield(plane);

            Destroy(gameObject);
        }
    }
}
