using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    
    [FormerlySerializedAs("disguiseInvicibility")] [SerializeField] 
    private float disguiseInvincibility;

    [SerializeField] private float blinkTime = 0.5f; // time, in seconds, per blink
    private float _blinkTimer;
    private float _currentInvicible;

    private DisguiseBar _disguiseBar;

    [SerializeField] 
    private GameObject banana;
    
    [SerializeField] 
    private GameObject pencil;
    
    [SerializeField] 
    private float bananaThrowForce;
    
    private bool _grounded = true;
    private Rigidbody2D _model;
    private Vector3 _spawnPoint;
    
    private float _hMove;
    private bool _jump = false;
    private float _previousMove;

    private Animator _animator;
    private float _disguiseTime;
    
    private SpriteRenderer _spriteRenderer;

    private HotbarScript _hotbarScript;

    [SerializeField]
    private int bananaAmmo = 0;
    [SerializeField]
    private int pencilAmmo = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        _model = GetComponent<Rigidbody2D>();
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hotbarScript = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<HotbarScript>();
        _hotbarScript.UpdatePencilAmmo(pencilAmmo.ToString());
        _disguiseBar = GameObject.Find("PlayerDisguiseBar").GetComponent<DisguiseBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuScript.GamePaused) return; // TODO: change this?
        
        HandleInput();
        
        _animator.SetFloat("Move", Math.Abs(_hMove));

        
        if (_hMove < 0 && _previousMove >= 0)
        {
            TurnLeft();
        } 
        else if (_hMove > 0 && _previousMove <= 0)
        {
            TurnRight();
        }

        if (_hMove != 0)
            _previousMove = _hMove;
        
        
        if (_jump && _grounded)
        {
            _model.AddForce(new Vector2(_model.velocity.x, jumpSpeed));
            _grounded = false;
        }

        if (_disguiseTime > 0)
        {
            if ((_disguiseTime -= Time.deltaTime) < 0)
                RemoveDisguise(false);

            _disguiseBar.SetProgress(_disguiseTime);
        } 
        else if (_currentInvicible > 0)
        {
            _blinkTimer -= Time.deltaTime;
            if ((_currentInvicible -= Time.deltaTime) <= 0)
            {
                _spriteRenderer.enabled = true;
            }
            else if (_blinkTimer <= 0)
            {
                _blinkTimer = blinkTime;
                
                if (_spriteRenderer.enabled)
                {
                    _spriteRenderer.enabled = false;
                }
                else
                {
                    _spriteRenderer.enabled = true;
                }
            }
        }

        
        if (Input.GetButtonDown("Fire2"))
        {
            if (bananaAmmo > 0)
            {
                Vector3 mouseLocation = Input.mousePosition;
                GameObject bananaInstance = Instantiate(banana, transform.position, transform.rotation);
                Vector3 diff = transform.position - mouseLocation;
                double angle = Math.Atan(diff.y / diff.x);
                bananaInstance.GetComponent<BananaScript>().StartBanana(_previousMove < 0 ? -1 : 1, bananaThrowForce, angle);
                bananaAmmo--;
                _hotbarScript.UpdateBananaCount(bananaAmmo.ToString());
            }
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (pencilAmmo > 0)
            {
                int direction = _previousMove < 0 ? -1 : 1;
                GameObject pencilInstance = Instantiate(pencil, transform.position, transform.rotation);
                pencilInstance.GetComponent<ProjectileScript>().setEnemyProjectile(false);
                pencilInstance.GetComponent<ProjectileScript>().StartProjectile(10 * direction,2);
                pencilAmmo--;
                _hotbarScript.UpdatePencilAmmo(pencilAmmo.ToString());
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

    void HandleInput()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            _jump = true;
        else _jump = false;
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
        _disguiseBar.ActivateDisguise(duration);
    }

    public void RemoveDisguise(bool invincibility)
    {
        _disguiseTime = 0;
        _animator.SetBool("Disguised", false);

        if (invincibility)
            _currentInvicible = disguiseInvincibility;
        else
            _currentInvicible = 0;
        
        _disguiseBar.DeactivateDisguise();
    }

    public bool IsDisguised()
    {
        return _disguiseTime > 0;
    }

    public bool IsInvincible()
    {
        return _currentInvicible > 0;
    }
    
    private void TurnLeft()
    {
        _spriteRenderer.flipX = true;
    }

    private void TurnRight()
    {
        _spriteRenderer.flipX = false;
    }

    public void AddAmmo(AmmoScript.Ammo ammoType, int i)
    {
        switch (ammoType)
        {
            case AmmoScript.Ammo.Banana:
                bananaAmmo++;
                _hotbarScript.UpdateBananaCount(bananaAmmo.ToString());
                break;
            case AmmoScript.Ammo.Pencil:
                pencilAmmo++;
                _hotbarScript.UpdatePencilAmmo(pencilAmmo.ToString());
                break;
            default:
                break;
        }
    }

    public int GetAmmo(AmmoScript.Ammo ammoType)
    {
        switch (ammoType)
        {
            case AmmoScript.Ammo.Banana:
                return bananaAmmo;
            case AmmoScript.Ammo.Pencil:
                return pencilAmmo;
            default:
                return 0;
        }
    }
}
