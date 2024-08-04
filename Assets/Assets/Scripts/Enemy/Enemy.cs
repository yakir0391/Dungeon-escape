using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField]
    protected int _health;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected int _gems;
    [SerializeField]
    protected Transform _pointA, _pointB;
    protected Vector3 _currentTarget;
    protected Animator _anim;
    protected SpriteRenderer _sprite;
    protected bool _isHit = false;
    protected Player _player;
    protected bool _isDead = false;

    public virtual void Init()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !_anim.GetBool("InCombat"))
        {
            return;
        }

        if (!_isDead)
        {
            Movement();
        }
    }

    public virtual void Movement()
    {
        if (_currentTarget == _pointA.position)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }

        if (transform.position == _pointA.position)
        {
            _anim.SetTrigger("toIdle");
            _currentTarget = _pointB.position;
        }
        else if (transform.position == _pointB.position)
        {
            _anim.SetTrigger("toIdle");
            _currentTarget = _pointA.position;
        }

        if (!_isHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > 2.0f)
        {
            _isHit = false;
            _anim.SetBool("InCombat", false);
        }

        Vector3 direction = _player.transform.localPosition - this.transform.localPosition;

        if (direction.x < 0 && _anim.GetBool("InCombat"))
        {
            this._sprite.flipX = true;
        }
        else if (direction.x > 0 && _anim.GetBool("InCombat"))
        {
            this._sprite.flipX = false;
        }
    }
}