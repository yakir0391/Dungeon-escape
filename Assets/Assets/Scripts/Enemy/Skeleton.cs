using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy , IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        this.Health = base._health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        if (_isDead)
        {
            return;
        }

        this.Health--;
        _anim.SetTrigger("Hit");
        _isHit = true;
        _anim.SetBool("InCombat", true);

        if (this.Health < 1)
        {
            this._isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base._gems;
        }
    }
}
