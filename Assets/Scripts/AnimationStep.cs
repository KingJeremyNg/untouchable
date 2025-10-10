using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationStep : MonoBehaviour
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 5f)
        {
            animator.SetTrigger("ChangeAnimation");
        }
    }
}
