using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatorController : MonoBehaviour
{
    private Animator animator = null;

    Vector2 firstPressPos, secondPressPos, currentSwipe, direction;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Swipe();
    }

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            if (Manager.Instance.CanPlay())
            {
                Touch t = Input.GetTouch(0);

                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = t.position;
                }

                if (t.phase == TouchPhase.Moved)
                {
                    direction = t.position - firstPressPos;
                }

                if (t.phase == TouchPhase.Ended)
                {
                    CheckTapOrSwipe();

                    secondPressPos = t.position;
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    currentSwipe.Normalize();

                    if (PlayerController.Instance.tapOnScreen)
                    {
                        MovePlayerUp();
                    }
                    else
                    {
                        //up
                        MovePlayerUp();

                        //down
                        if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                        {
                            gameObject.transform.rotation = Quaternion.Euler(270, 180, 0);

                            PlayerController.Instance.upRotation = false;
                            PlayerController.Instance.downRotation = true;
                            PlayerController.Instance.rightRotation = false;
                            PlayerController.Instance.leftRotation = false;
                        }

                        //left
                        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            gameObject.transform.rotation = Quaternion.Euler(270, -90, 0);

                            PlayerController.Instance.upRotation = false;
                            PlayerController.Instance.downRotation = false;
                            PlayerController.Instance.rightRotation = false;
                            PlayerController.Instance.leftRotation = true;
                        }

                        //right
                        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                        {
                            gameObject.transform.rotation = Quaternion.Euler(270, 90, 0);

                            PlayerController.Instance.upRotation = false;
                            PlayerController.Instance.downRotation = false;
                            PlayerController.Instance.rightRotation = true;
                            PlayerController.Instance.leftRotation = false;
                        }
                    }

                    PlayerController.Instance.CheckIfCanMove();

                    if (PlayerController.Instance.isIdle)
                        PlayerController.Instance.CanMove();

                    direction = new Vector2(0f, 0f);
                }
            }
        }
    }

    private void MovePlayerUp()
    {
        gameObject.transform.rotation = Quaternion.Euler(270, 0, 0);

        PlayerController.Instance.upRotation = true;
        PlayerController.Instance.downRotation = false;
        PlayerController.Instance.rightRotation = false;
        PlayerController.Instance.leftRotation = false;
    }

    void CheckTapOrSwipe()
    {
        if (direction.x == 0 && direction.y == 0)
            PlayerController.Instance.tapOnScreen = true;
        else
            PlayerController.Instance.tapOnScreen = false;
    }
}
