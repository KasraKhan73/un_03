using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PeopleController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float diraction = 20f;
    [SerializeField] bool isBadGuy = false;

    private bool isRunning = false;
    private float endPoint;
    void Start()
    {
        animator.SetBool("isRunning", true);
        isRunning = true;
        Move();
    }

    void Update()
    {
        if (transform.position.x - endPoint <= 5 && isRunning)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
    }

    private void Move()
    {
        endPoint = transform.position.x + diraction;
        transform.DOMoveX(transform.position.x + diraction, 7f, false).OnComplete(() =>
        {
            animator.SetBool("isWalking", false);
            if (isBadGuy) animator.SetBool("isPunching_Left", true);
            else animator.SetTrigger("openDoor");
        }).SetEase(Ease.Linear);
    }
}
