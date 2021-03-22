using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Public Fields

    public PlayerInput playerInput;

    public float moveSpeed;

    public float jumpForce;

    public float raySize;
    
    #endregion

    #region Serializable Private Fields

    [SerializeField] private bool isGrounded;

    #endregion

    #region Private Fields

    private GameControls _gameControls;

    private Vector2 _inputVector;

    private Rigidbody2D _rigidbody2D;

    private int _groundMask = 1 << 8;

    private Animator _animator;

    private float _batteryLevel;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameControls = new GameControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _batteryLevel = 100f;
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        if(GameManager.Instance.GameStarted)
        {
            _batteryLevel -= Time.deltaTime * 10f;

            RaycastHit2D result = Physics2D.Raycast(transform.position,
                Vector2.down, raySize, _groundMask);
            if (result.collider != null)
            {
                isGrounded = true;
                _animator.SetTrigger("isGround");
            }
            else
            {
                isGrounded = false;
                _animator.SetBool("Jump", false);
            }

            SetSlider(_batteryLevel / 100f);

            if (_batteryLevel <= 0) GameManager.Instance.IsGameOver = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += (Vector3)_inputVector * Time.deltaTime * moveSpeed;
        _rigidbody2D.AddForce(_inputVector * Time.fixedDeltaTime * moveSpeed);
        _animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        _animator.SetFloat("VertSpeed", Mathf.Abs(_rigidbody2D.velocity.y));
    }
    

    #endregion

    #region Private Methods

    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == _gameControls.Gameplay.Move.name)
        {
            _inputVector = obj.ReadValue<Vector2>();
        }

        if (obj.action.name == _gameControls.Gameplay.Jump.name)
        {
            GameManager.Instance.ResetGame();
            
            if (obj.performed && isGrounded)
            {
                DoJump();
            }
        }
    }

    #endregion

    #region Public Methods

    public void DoJump()
    {
        _rigidbody2D.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        _animator.SetBool("Jump", true);
    }

    private void SetSlider(float slide)
    {
        Observer.OnSetSlider(slide);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Battery"))
        {
            //Destroy(other.gameObject);
            BatteryPoolManager.Instance.DeactivateObject(other.gameObject);
            _batteryLevel += 5f;
        }
    }
    
    #endregion

    #region Debug Methods

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,Vector3.down*raySize,Color.red);
    }

    #endregion
}
