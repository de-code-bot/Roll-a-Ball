using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX, movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextobject;
    public GameObject replay;
    public AudioSource paudio;
    public GameObject waudio;
    public GameObject laudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //paudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextobject.SetActive(false);
        replay.SetActive(false);
        waudio.SetActive(false);
        laudio.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            paudio.PlayOneShot(paudio.clip);
            SetCountText();
        }


    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 40)
        {
            winTextobject.SetActive(true);
            // Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            replay.SetActive(true);
            waudio.SetActive(true);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy(gameObject);
            winTextobject.SetActive(true);
            winTextobject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            replay.SetActive(true);
            laudio.SetActive(true);
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        winTextobject.SetActive(false);
    }

}
