using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: SerializeField] private Rigidbody2D Rigidbody { get; set; }
    [field: SerializeField] private BoxCollider2D BoxCollider { get; set; }
    [field: SerializeField] private PlayerInput PlayerInput { get; set; }
    [field: SerializeField] private Animator Animator { get; set; }
    [field: SerializeField] private AudioSource AudioSource { get; set; }

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashSpeed = 2f;
    [SerializeField] private float _dashCoolDown = 3f;
    [SerializeField] private float _interactableObjectRadius = 10f;

    
    [SerializeField] private SpaceVehicle _spaceVehicle;
    
    [SerializeField] private ToolInfo _currentTool;

    [SerializeField] private TrailRenderer tr;
    
    public AudioClip mainTheme;
    public AudioClip pauseTheme;

    public UnityEvent OnPaused;
    
    private Vector2 _moveDirection;
    private bool _isDashing;
    private IInteractable _interactableObject;
    
    
    
    
    private void OnValidate()
    {
        if(Rigidbody == null) Rigidbody = GetComponent<Rigidbody2D>();
        if(BoxCollider == null) Rigidbody = GetComponent<Rigidbody2D>();
        if(PlayerInput == null) PlayerInput = GetComponent<PlayerInput>();
        if(AudioSource == null) AudioSource = GetComponent<AudioSource>();
        if(Animator == null) Animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (AudioSource != null && mainTheme != null) {
            AudioSource.clip = mainTheme;
            AudioSource.Play();
        }
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _moveDirection = new Vector2(input.x, input.y).normalized;

        if (_moveDirection.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(-_moveDirection.x), 1f);
        }
    }

    private void OnDash(InputValue value)
    {
        _isDashing = value.isPressed;
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        _isDashing = true;
        Rigidbody.velocity = new Vector2(_moveDirection.x * _dashSpeed, _moveDirection.y * _dashSpeed);
        tr.emitting = true;
        yield return new WaitForSeconds(_dashCoolDown);
        tr.emitting = false;
        _isDashing = false;
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;
        Animator.SetBool ("IsWalking", _moveDirection.x != 0 || _moveDirection.y != 0);
        Move();
    }


    private void Move()
    {
        // isometric movement
        // Rigidbody.velocity = new Vector3(_moveDirection.x - _moveDirection.y, (_moveDirection.x + _moveDirection.y) / 2, 0) * (_moveSpeed * Time.fixedDeltaTime);
        
        // normal 
        Rigidbody.velocity = new Vector3(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed) * Time.fixedDeltaTime;
    }

    private void OnInteract(InputValue value)
    {
        var nearestGameObject = GetNearestInteractableObject();
        var interactable = nearestGameObject?.GetComponent<IInteractable>();
        interactable?.Interact(value);
        
    }

    private void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            PauseGame();
            if (AudioSource != null && pauseTheme != null) {
                AudioSource.clip = pauseTheme;
                AudioSource.Play();
            }
        }
    }

    private void OnRepair(InputValue value)
    {
        
            // Repair nearest vehicle if it's interactable
            var nearestRepairObject = GetNearestRepairObject();
            var nearestInteractableObject = GetNearestInteractableObject();
            if (nearestRepairObject != null && value.isPressed)
            {
                Debug.Log("repairing");
                SpaceVehicle spaceVehicle = nearestRepairObject.GetComponent<SpaceVehicle>();
                PickupItem pickupItem = nearestInteractableObject.GetComponent<PickupItem>();
                if (spaceVehicle && pickupItem!= null && !spaceVehicle.Fixed)
                {
                    spaceVehicle.ApplyTool(pickupItem.toolId); 
                    Debug.Log(pickupItem.toolId);
                }
            }
    }

    public void PauseGame()
    {
        OnPaused?.Invoke();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (AudioSource != null && mainTheme != null) {
            AudioSource.clip = mainTheme;
            AudioSource.Play();
        }
    }
    
    private GameObject GetNearestInteractableObject()
    {
        var nearestInteractable = Physics2D.OverlapCircle(transform.position, _interactableObjectRadius, LayerMask.GetMask("Interactable"));
        return nearestInteractable == null ? null : nearestInteractable.gameObject;
    }
    
    private GameObject GetNearestRepairObject()
    {
        var nearestRepairable = Physics2D.OverlapCircle(transform.position, _interactableObjectRadius, LayerMask.GetMask("Repairable"));
        return nearestRepairable == null ? null : nearestRepairable.gameObject;
    }
    
}
