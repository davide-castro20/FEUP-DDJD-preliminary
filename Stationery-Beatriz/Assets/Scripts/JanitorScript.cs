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
    
    [SerializeField]
    private CleanAreaScript areaScript;
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

        _bananaInArea = false;
        
        if (direction < 0)
        {
            TurnLeft();
        }
        else
        {
            TurnRight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_bananaInArea)
        {
            Vector3 bananaPos = _banana.transform.position;
            bananaPos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, bananaPos, 
                speed * 1.25f * Time.deltaTime);
            if (transform.position.x == _banana.transform.position.x)
            {
                _bananaTime += Time.deltaTime;
                if (_bananaTime >= 2)
                {
                    _bananaInArea = false;
                    Destroy(_banana);
                    _bananaTime = 0;
                }
            }

            return;
        }
        
        if (transform.position == _rightPos)
        {
            TurnLeft();

        }
        else if (transform.position == _leftPos)
        {
            TurnRight();
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_ps.IsDisguised())
            {
                _ps.RemoveDisguise(true);
            }
            else
            {
                _ps.Kill();
            }
        }
    }

    private void TurnLeft()
    {
        _currentTarget = _leftPos;
        _spriteRenderer.flipX = true;
    }

    private void TurnRight()
    {
        _currentTarget = _rightPos;
        _spriteRenderer.flipX = false;
    }

    public void CleanBanana(GameObject banana)
    {
        _bananaInArea = true;
        _banana = banana;
    }

    public void LeftBanana(GameObject otherGameObject)
    {
        _bananaInArea = false;
        _banana = null;
    }
}
