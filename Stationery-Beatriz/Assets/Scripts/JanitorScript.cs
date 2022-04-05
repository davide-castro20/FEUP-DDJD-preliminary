using System;
using UnityEngine;

public class JanitorScript : MonoBehaviour
{
    [SerializeField] private float leftOffset;
    [SerializeField] private float rightOffset;
    [SerializeField] private int direction; // -1 -> left first; 1 -> right first
    [SerializeField] private float speed;
    
    private Vector3 _initialPos;
    private Vector3 _leftPos;
    private Vector3 _rightPos;
    private Vector3 _currentTarget;
    private PlayerScript _ps;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Enemy _enemyScript;

    [SerializeField] 
    private float bananaPickupTime = 2f;
    
    [SerializeField]
    private CleanAreaScript areaScript;

    [SerializeField] private GameObject detectionWarning;
    private bool _bananaInArea;
    private GameObject _banana;
    private float _bananaTime;

    // Start is called before the first frame update
    void Start()
    {
        _initialPos = gameObject.transform.position;
        _leftPos = new Vector3(_initialPos.x - leftOffset, _initialPos.y, _initialPos.z);
        _rightPos = new Vector3(_initialPos.x + rightOffset, _initialPos.y, _initialPos.z);
        
        _ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _enemyScript = GetComponent<Enemy>();

        _bananaInArea = false;
        
        if (direction < 0)
        {
            _currentTarget = _leftPos;
            TurnLeft();
        }
        else
        {
            _currentTarget = _rightPos;
            TurnRight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemyScript.getDead())
        {
            if (_bananaInArea)
            {
                Vector3 bananaPos = _banana.transform.position;
                bananaPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, bananaPos,
                    speed * 1.25f * Time.deltaTime);

                if (bananaPos.x - transform.position.x > 0)
                {
                    TurnRight();
                }
                else
                {
                    TurnLeft();
                }

                if (transform.position.x == _banana.transform.position.x)
                {
                    _animator.SetBool("cleaning", true);
                    _bananaTime += Time.deltaTime;
                    if (_bananaTime >= bananaPickupTime)
                    {
                        _bananaInArea = false;
                        _bananaTime = 0;
                        _animator.SetBool("cleaning", false);
                        detectionWarning.SetActive(false);
                        Destroy(_banana);
                    }
                }

                return;
            }

            if (_currentTarget.x > transform.position.x)
            {
                TurnRight();
            }
            else
            {
                TurnLeft();
            }

            if (transform.position == _rightPos)
            {
                _currentTarget = _leftPos;
                TurnLeft();
            }
            else if (transform.position == _leftPos)
            {
                _currentTarget = _rightPos;
                TurnRight();
            }

            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_enemyScript.getDead() && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BottomCollider")))
        {
            if (_ps.IsDisguised())
            {
                _ps.RemoveDisguise(true);
            }
            else if (!_ps.IsInvincible())
            {
                _ps.Kill();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_enemyScript.getDead() && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BottomCollider")))
        {
            if (!_ps.IsInvincible() && !_ps.IsDisguised())
            {
                _ps.Kill();
            }
        }
    }

    private void TurnLeft()
    {
        _spriteRenderer.flipX = true;
    }

    private void TurnRight()
    {
        _spriteRenderer.flipX = false;
    }

    public void CleanBanana(GameObject banana)
    {
        _bananaInArea = true;
        _banana = banana;
        detectionWarning.SetActive(true);
    }

    public void LeftBanana()
    {
        _bananaInArea = false;
        _banana = null;
    }
}
