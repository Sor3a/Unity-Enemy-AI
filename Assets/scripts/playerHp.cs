using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHp : MonoBehaviour
{
   public int hp =100;
    bool ISdead = false;

    void dead()
    {
        Debug.Log("you are dead");
    }
    private void Update()
    {
        if (hp <= 0 && !ISdead)
        {
            dead();
            ISdead = true;
        }
    }
}
