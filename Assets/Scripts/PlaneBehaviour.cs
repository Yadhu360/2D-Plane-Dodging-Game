using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaneBehaviour : MonoBehaviour
{
    public Camera cam;
    public Transform plane;
    public GameObject brokenPlane, canvas;
    public Transform leftPoint, rightPoint, forwardPoint;
    private Rigidbody2D rb;
    public float speed = 5f, rotateSpeed = 50f;
    public static bool isGameOver;

    [Header("Shield")]
    [HideInInspector] public bool shieldActive = false; // Needed for GameManager
    public GameObject shieldVisual; // optional: assign a sprite/particle in inspector

    [Header("Audio")]
    public AudioSource movementAudio; // looping engine
    public AudioSource effectsAudio; // for coin & crash
    public AudioClip coinClip;
    public AudioClip crashClip;

    public Text bestScoreText;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        isGameOver = false;

        if (shieldVisual != null)
            shieldVisual.SetActive(false); // Hide shield at start

        if (movementAudio != null)
            movementAudio.Play(); // start engine sound
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            MovePlane();
            float horizontalInput = Input.GetAxis("Horizontal");
            RotatePlane(horizontalInput);
        }
    }

    void MovePlane()
    {
        rb.linearVelocity = transform.up * speed; // fixed linearVelocity
    }

    void RotatePlane(float x)
    {
        float angle;
        Vector2 direction = Vector2.zero;
        if (x < 0) direction = (Vector2)leftPoint.position - rb.position;
        if (x > 0) direction = (Vector2)rightPoint.position - rb.position;

        direction.Normalize();
        angle = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = (x != 0) ? -rotateSpeed * angle : 0;

        angle = Mathf.Atan2(forwardPoint.position.y - plane.position.y,
                            forwardPoint.position.x - plane.position.x) * Mathf.Rad2Deg;
        plane.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // ---------------- SHIELD CONTROL ----------------
    public void EnableShield()
    {
        shieldActive = true;
        if (shieldVisual != null) shieldVisual.SetActive(true);
    }

    public void DisableShield()
    {
        shieldActive = false;
        if (shieldVisual != null) shieldVisual.SetActive(false);
    }
    // -------------------------------------------------

    public void GameOver(Transform missile)
    {
        if (shieldActive) return; // Ignore missiles if shield is active

        // Stop engine
        if (movementAudio != null)
            movementAudio.Stop();

        // Play crash sound
        if (effectsAudio != null && crashClip != null)
            effectsAudio.PlayOneShot(crashClip);

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        isGameOver = true;
        rb.linearVelocity = Vector2.zero;
        cam.GetComponent<CameraBehaviour>().gameOver = true;

        GameObject planeTemp = Instantiate(brokenPlane, transform.position, transform.rotation);
        for (int i = 0; i < planeTemp.transform.childCount; i++)
        {
            Rigidbody2D rbTemp = planeTemp.transform.GetChild(i).GetComponent<Rigidbody2D>();
            rbTemp.AddForce(((Vector2)missile.position - rbTemp.position) * -5f, ForceMode2D.Impulse);
        }

        StartCoroutine(CanvasStuff());
    }

    IEnumerator CanvasStuff()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
        bestScoreText.text = "Score\n" + Mathf.FloorToInt(timeManger.timeToDisplay).ToString() + " Seconds";

        for (int i = 0; i <= 10; i++)
        {
            float k = (float)i / 10;
            canvas.transform.localScale = new Vector3(k, k, k);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(gameObject);
    }

    // ---------------- COIN SOUND HELPER ----------------
    public void PlayCoinSound()
    {
        if (effectsAudio != null && coinClip != null)
            effectsAudio.PlayOneShot(coinClip);
    }
}
