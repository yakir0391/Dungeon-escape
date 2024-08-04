using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        this.Health = base._health;
    }

    public override void Update(){}

    public override void Movement() {}

    public void Damage()
    {
        if (_isDead)
        {
            return;
        }

        this.Health--;

        if (this.Health < 1)
        {
            this._isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base._gems;
        }
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
