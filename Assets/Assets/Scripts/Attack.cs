using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && _canAttack)
        {
           hit.Damage();
           _canAttack = false;
           StartCoroutine(CanAttack());
        }
    }

    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
}
