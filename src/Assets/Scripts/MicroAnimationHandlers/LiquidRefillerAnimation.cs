using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidRefillerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Refill = Animator.StringToHash("Refill");

    public void PlayRefillAnimation()
    {
        animator.SetTrigger(Refill);
    }
}
