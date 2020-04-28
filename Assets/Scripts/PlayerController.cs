using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1.1f;
    public bool isIdle = true;

    public ParticleSystem particle = null;
    public GameObject chick = null;
    private Renderer renderer = null;
    AudioSource[] playerSounds;
    bool isVisible = false;
    AudioSource playerJumpSound;
    AudioSource playerDeathSound;

    [HideInInspector]
    public bool downRotation, rightRotation, leftRotation, tapOnScreen, upRotation;
    [HideInInspector]
    public bool isDead = false;

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        renderer = chick.GetComponent<Renderer>();
        playerSounds = gameObject.GetComponents<AudioSource>();
        playerDeathSound = playerSounds[0];
        playerJumpSound = playerSounds[1];
    }

    void Update()
    {
        if (!Manager.Instance.CanPlay())
            return;

        if (isDead)
            return;

        CheckIfCanMove();
        IsVisible();
    }

    private void CheckIfCanMove()
    {
        Physics.Raycast(this.transform.position, -chick.transform.up, out RaycastHit hit, colliderDistCheck);
        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);

        if (hit.collider == null || (hit.collider != null && hit.collider.tag != "collider") || hit.collider.tag == "coin")
            AllowedMove();
        else
            NotAllowedMove();
    }

    void AllowedMove()
    {
        isIdle = true;
    }

    void NotAllowedMove()
    {
        isIdle = false;
    }

    public void CanMove()
    {
        if (upRotation)
        {
            Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
            SetMoveForwardState();
        }
        else if (downRotation)
            Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
        else if (leftRotation)
            Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
        else if (rightRotation)
            Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));

    }

    void Moving(Vector3 pos)
    {
        isIdle = false;
        AudioManager.Instance.GetAudio(gameObject.tag).Play();
        gameObject.transform.DOMove(pos, moveTime);
    }

    void SetMoveForwardState()
    {
        Manager.Instance.UpdateDistanceCount();
    }

    void IsVisible()
    {
        if (renderer.isVisible)
            isVisible = true;

        if (!renderer.isVisible && isVisible == true)
            GetHit();
    }

    public void GetHit()
    {
        playerDeathSound.Play();
        Manager.Instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }
}
