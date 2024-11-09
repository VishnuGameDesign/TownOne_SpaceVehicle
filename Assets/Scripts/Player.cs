using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: SerializeField] private Rigidbody2D Rigidbody { get; set; }
    [field: SerializeField] private BoxCollider2D BoxCollider { get; set; }
    [field: SerializeField] private PlayerInput PlayerInput { get; set; }

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashSpeed = 2f;
    [SerializeField] private float _dashCoolDown = 3f;
    [SerializeField] private float _interactableObjectRadius = 10f;
    
    private Vector2 _moveDirection;
    private bool _isDashing;
    private IInteractable _interactableObject;

    private void OnValidate()
    {
        if(Rigidbody == null) Rigidbody = GetComponent<Rigidbody2D>();
        if(BoxCollider == null) Rigidbody = GetComponent<Rigidbody2D>();
        if(PlayerInput == null) PlayerInput = GetComponent<PlayerInput>();
    }

    private void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        _moveDirection = new Vector2(input.x, input.y).normalized;

        if (_moveDirection.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(_moveDirection.x), 1f);
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
        yield return new WaitForSeconds(_dashCoolDown);
        _isDashing = false;
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;
        Move();
    }


    private void Move()
    {
        Rigidbody.velocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
    }

    private void OnInteract()
    {
        var nearestGameObject = GetNearestObject();
        var interactable = nearestGameObject?.GetComponent<IInteractable>();
        interactable?.Interact();
    }
    
    private GameObject GetNearestObject()
    {
        var nearestInteractable = Physics2D.OverlapCircle(transform.position, _interactableObjectRadius, LayerMask.GetMask("Interactable"));
        return nearestInteractable == null ? null : nearestInteractable.gameObject;
    }
}
