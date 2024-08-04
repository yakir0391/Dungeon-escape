using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour , IDamageable
{
    public int _diamondsAmount;
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _speed = 2.7f;
    bool _onGround = true;
    float _jumpForce = 7f;
    private Player_Animation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Player_Animation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        this.Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if (horizontalInput > 0)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && _onGround == true)
        {
            _rigid.velocity = new Vector2(horizontalInput * _speed, _jumpForce);
        }

        _rigid.velocity = new Vector2(horizontalInput * _speed, _rigid.velocity.y);
        _playerAnim.Move(horizontalInput);
    }

    void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && _onGround == true)
        {
            _playerAnim.Attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _onGround = true;
            _playerAnim.Jump(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _onGround = false;
            _playerAnim.Jump(true);
        }
    }

    public void Damage()
    {
        if ( this.Health < 1)
        {
            return;
        }

        this.Health--;
        UIManager.Instance.UpdateLives(this.Health);

        if (this.Health < 1)
        {
            _playerAnim.Death();
        }
    }

    public void AddGems(int amount)
    {
        _diamondsAmount += amount;
        UIManager.Instance.UpdateGemCount(_diamondsAmount);
    }
}
