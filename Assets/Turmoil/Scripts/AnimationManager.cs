using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {

    }

    public void Attack(int type)
    {
        anim.SetBool("IsAttacking", true);
    }

    public void StopAttack(int type)
    {
        anim.SetBool("IsAttacking", false);
    }
}
