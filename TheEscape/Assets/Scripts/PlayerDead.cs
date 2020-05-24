using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public Animator anim;
    public string playerDeadTrigger = "IsDead";

    public void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger(playerDeadTrigger);
        }
    }
}
