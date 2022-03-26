using System;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    
    [SerializeField] 
    private float disguiseInvicibility;

    [SerializeField] 
    private GameObject banana;
    
    [SerializeField] 
    private GameObject pencil;
    
    [SerializeField] 
    private float bananaThrowForce;
    
    private bool _grounded = true;
    private Rigidbody2D _model;
    private Vector3 _spawnPoint;
    
    private float _hMove = 0f;
    private float _previousMove = 0f;

    private Animator _animator;
    private float _disguiseTime = 0;
    
    private SpriteRenderer _spriteRenderer;

    private int ammo = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponent<Rigidbody2D>();
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuScript.GamePaused) return; // TODO: change this?
        
        _hMove = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat("Move", Math.Abs(_hMove));

        if (_hMove < 0 && _previousMove >= 0)
        {
            TurnLeft();
        } 
        else if (_hMove > 0 && _previousMove <= 0)
        {
            TurnRight();
        }

        if (Input.GetButtonDown("Jump") && _grounded == true)
        {
            _model.AddForce(new Vector2(_model.velocity.x, jumpSpeed));
            _grounded = false;
        }

        if (_disguiseTime > 0)
        {
            if ((_disguiseTime -= Time.deltaTime) < 0)
                RemoveDisguise(false);
        }

        if (_hMove != 0)
            _previousMove = _hMove;
        
        if (Input.GetButtonDown("Fire2"))
        {
            if (GameObject.Find("Banana(Clone)") == null)
            {
                GameObject bananaInstance = Instantiate(banana, transform.position, transform.rotation);
                bananaInstance.GetComponent<BananaScript>().StartBanana(_previousMove < 0 ? -1 : 1, bananaThrowForce);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo != 0)
            {
                int direction = _previousMove < 0 ? -1 : 1;
                GameObject pencilInstance = Instantiate(pencil, transform.position, transform.rotation);
                pencilInstance.GetComponent<ProjectileScript>().setEnemyProjectile(false);
                pencilInstance.GetComponent<ProjectileScript>().StartProjectile(10 * direction,2);
                ammo--;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    void FixedUpdate()
    {
        _model.velocity = new Vector2(_hMove * speed, _model.velocity.y);
    }

    public void ResetJump()
    {
        // Debug.Log("Reset Jump");
        _grounded = true;
    }

    public void SetAirborne()
    {
        // Debug.Log("Set Airborne");
        _grounded = false;
    }

    public void Kill()
    {
        transform.position = _spawnPoint;
    }

    public void Disguise(float duration)
    {
        _animator.SetBool("Disguised", true);
        _disguiseTime = duration;
    }

    public void RemoveDisguise(bool invincibility)
    {
        _animator.SetBool("Disguised", false);
        if (invincibility)
            _disguiseTime = disguiseInvicibility;
    }

    public bool IsDisguised()
    {
        return _disguiseTime > 0;
    }
    
    private void TurnLeft()
    {
        _spriteRenderer.flipX = true;
    }

    private void TurnRight()
    {
        _spriteRenderer.flipX = false;
    }

    public void AddAmmo(int i)
    {
        this.ammo++;
    }
}
