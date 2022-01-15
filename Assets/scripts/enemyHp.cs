using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHp : MonoBehaviour
{
    public int hp = 100;

    void Enemydead()
    {
        Debug.Log("enemy dead");
    }
    private void Update()
    {
        if (hp <= 0)
            Enemydead();
    }
}
