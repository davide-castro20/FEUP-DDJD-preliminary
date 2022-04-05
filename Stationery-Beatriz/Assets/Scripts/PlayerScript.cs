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

    private PauseMenuScript _menuScript;
    
    private bool _grounded = true;
    private Rigidbody2D _rb;
    private Vector3 _spawnPoint;
    
    private float _hMove;
    private bool _jump = false;
    private float _previousMove;

    private Animator _animator;
    private float _disguiseTime;
    
    private SpriteRenderer _spriteRenderer;
    private AudioManager _audioManager;

    private HotbarScript _hotbarScript;

    [SerializeField]
    private int bananaAmmo = 0;
    [SerializeField]
    private int pencilAmmo = 0;
    [SerializeField] 
    private int gliderAmmo = 0;
    [SerializeField] 
    private int rulerAmmo = 0;
    
    private bool _gliding;
    [SerializeField] 
    private float glideVelocityX = 1f;
    [SerializeField] 
    private float glideVelocityYFactor = 2f;
    
    [FormerlySerializedAs("_ruler")] [SerializeField] 
    private GameObject ruler;
    private float _timeAttack = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hotbarScript = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<HotbarScript>();
        _hotbarScript.UpdatePencilAmmo(pencilAmmo.ToString());
        _disguiseBar = GameObject.Find("PlayerDisguiseBar").GetComponent<DisguiseBar>();
        _menuScript = GameObject.Find("HUD").GetComponent<PauseMenuScript>();
        _audioManager = FindObjectOfType<AudioManager>();
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

        
        
        
        if (_jump && _grounded && !IsAttacking())
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, jumpSpeed));
            _grounded = false;
            _audioManager.PlaySound("PlayerJump");
            _animator.SetBool("Jump", true);
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

        
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (bananaAmmo > 0)
            {
                GameObject bananaInstance = Instantiate(banana, transform.position, transform.rotation);
                bananaInstance.GetComponent<BananaScript>().StartBanana(_previousMove < 0 ? -1 : 1, bananaThrowForce);
                bananaAmmo--;
                GameData.BananaUses++;
                _hotbarScript.UpdateBananaCount(bananaAmmo.ToString());
                _audioManager.PlaySound("PlayerThrow");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (pencilAmmo > 0)
            {
                int direction = _previousMove < 0 ? -1 : 1;
                GameObject pencilInstance = Instantiate(pencil, transform.position, transform.rotation);
                pencilInstance.GetComponent<ProjectileScript>().setEnemyProjectile(false);
                pencilInstance.GetComponent<ProjectileScript>().StartProjectile(10 * direction,2);
                pencilAmmo--;
                _hotbarScript.UpdatePencilAmmo(pencilAmmo.ToString());
                _audioManager.PlaySound("PlayerThrow");
            }
        }

        if(_timeAttack >= 0)
            _timeAttack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.L) && !IsAttacking() && _grounded && rulerAmmo > 0)
        {
            this._animator.SetBool("Attack",true);
            _timeAttack = 0;
           ruler.SetActive(true); 
        }   

        if (_timeAttack >= 1)
        {
            this._animator.SetBool("Attack", false);
            ruler.SetActive(false);
            _timeAttack = -1;
        }

        if (Input.GetButtonDown("Fire3") && !IsAttacking()) // start gliding
        {
            if (!_gliding && !_grounded)
            {
                StartGliding();
            }
        } 
        else if (Input.GetButtonUp("Fire3")) // release button - stop gliding
        {
            if (_gliding)
            {
                StopGliding();
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    public bool IsAttacking()
    {
        return _timeAttack >= 0;
    }

    void FixedUpdate()
    {
        var xVel = _hMove * speed;
        var yVel = _rb.velocity.y;
        
        if (_gliding)
        {
            var moveGlide = _hMove != 0 ? _hMove : _previousMove;

            if (moveGlide > 0)
            {
                xVel = glideVelocityX;
            } 
            else if (moveGlide < 0)
            {
                xVel = -glideVelocityX;
            }
            else
            {
                xVel = _previousMove;
            }
            
            _rb.velocity = new Vector2(xVel, yVel / glideVelocityYFactor);
            return;
        }
        
        _rb.velocity = new Vector2(xVel, yVel);
    }

    void HandleInput()
    {
        if (_hMove != 0)
            _previousMove = _hMove;
        
        _hMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            _jump = true;
        else _jump = false;
    }

    public void ResetJump()
    {
        // Debug.Log("Reset Jump");
        _grounded = true;
        
        if (_gliding)
        {  
            StopGliding();
        }
        else
        {
            _animator.SetBool("Jump", false);
        }
    }

    public void StopJumpAnim()
    {
        _animator.SetBool("Jump", false);
    }

    public void SetAirborne()
    {
        // Debug.Log("Set Airborne");
        _grounded = false;
    }

    public void Kill()
    {
        _menuScript.DeathScreen();
        // transform.position = _spawnPoint;
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
        ruler.transform.localScale = new Vector3(-1,1,1);
    }

    private void TurnRight()
    {
        _spriteRenderer.flipX = false;
        ruler.transform.localScale = new Vector3(1,1,1);
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
            case AmmoScript.Ammo.Glider:
                gliderAmmo++;
                _hotbarScript.UpdateGlider(true);
                break;
            case AmmoScript.Ammo.Ruler:
                rulerAmmo = 2;
                _hotbarScript.UpdateRuler(rulerAmmo);
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
            case AmmoScript.Ammo.Glider:
                return gliderAmmo;
            case AmmoScript.Ammo.Ruler:
                return rulerAmmo;
            default:
                return 0;
        }
    }

    private void StartGliding()
    {
        if (gliderAmmo > 0)
        {
            _gliding = true;
            _animator.SetBool("Glide", true);
        }
    }

    private void StopGliding()
    {
        _gliding = false;
        _animator.SetBool("Glide", false);
    }

    public void RemoveRulerAmmo()
    {
        rulerAmmo--;
        _hotbarScript.UpdateRuler(rulerAmmo);
    }
}
