using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Extension;

public class PlayerAnimationController : MonoBehaviour
{
    private bool IsLeft;
    private bool IsWalking;
    public bool IsJumping;
    public bool Attack;
    public bool Jump;

    private int PlayerType;
    [SerializeField] private RuntimeAnimatorController[] m_acs;

    private Animator m_anim;
    private Rigidbody m_rigid;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rigid = GetComponent<Rigidbody>();
        IsLeft = transform.GetFacing2D() == Facing2D.Left;
        IsWalking = false;
        IsJumping = false;
        Attack = false;

        PlayerType = 0;
    }

    public void ChangeForm(int index)
    {
        PlayerType = index;
        m_anim.runtimeAnimatorController = m_acs[index];
    }

    private void Update()
    {
        if (m_rigid.velocity.x == 0) IsWalking = false;
        else IsWalking = true;


        IsLeft = transform.GetFacing2D() == Facing2D.Left;

        if (Attack)
        {
            m_anim.SetTrigger("Attack");
            Attack = false;
        }

        if (Jump)
        {
            IsJumping = true;
            m_anim.SetTrigger("Jump");
            Jump = false;
        }

        m_anim.SetBool("IsWalking", IsWalking);
        m_anim.SetBool("IsLeft", IsLeft);
        m_anim.SetBool("IsJumping", IsJumping);

        if (m_rigid.velocity.y == 0) IsJumping = false;
        else IsJumping = true;
    }
}
