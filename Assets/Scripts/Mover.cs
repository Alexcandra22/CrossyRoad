using DG.Tweening;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    [HideInInspector]
    public bool parentOnTrigger = true;
    [HideInInspector]
    public bool hitBoxOnTrigger = false;
    [HideInInspector]
    public bool goToLeftSide = false;
    [HideInInspector]
    public bool goToRightSide = false;

    public GameObject moverObject = null;

    private Renderer renderer = null;
    private bool isVisible = false;

    void Start()
    {
        renderer = moverObject.GetComponent<Renderer>();
    }

    void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                other.transform.parent = transform;
            }

            if (hitBoxOnTrigger)
            {
                other.GetComponent<PlayerController>().GetHit();
            }
        }

        if (goToRightSide)
            if (other.tag == "rightWall")
            {
                goToRightSide = false;
                gameObject.SetActive(false);
            }

        if (goToLeftSide)
            if (other.tag == "leftWall")
            {
                goToLeftSide = false;
                gameObject.SetActive(false);
            }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentOnTrigger)
            {
                other.transform.parent = null;
            }
        }
    }
}
