using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;

public class RepairMiniGame : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _rightEndPivotPoint;
    [SerializeField] private Transform _leftEndPivotPoint;
    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _matchingTarget;

    [SerializeField] private float _smallerBarSpeed = 1.5f;
    [SerializeField] private float _biggerBarSpeed = 2f;
    [SerializeField] private float _toMatchThreshold = .1f;
    [SerializeField] private GameObject _image;
    
    private bool _isMoving;

    private void OnValidate()
    {
        _image.SetActive(false);
    }

    private void Start()
    {
        _movingTarget.transform.position = _leftEndPivotPoint.position;
        _matchingTarget.transform.position = _rightEndPivotPoint.position;
    }
    

    private IEnumerator MoveTarget()
    {
        while (_isMoving)
        {
            float distance = Vector2.Distance(_leftEndPivotPoint.position, _rightEndPivotPoint.position);
            float pingPongForSmallBar = Mathf.PingPong(Time.time * _smallerBarSpeed, distance);    
            float pingPongForBigBar = Mathf.PingPong(Time.time * _biggerBarSpeed, distance);    
            
            _movingTarget.transform.position = Vector3.Lerp(_leftEndPivotPoint.position, _rightEndPivotPoint.position, pingPongForSmallBar / distance);
            _matchingTarget.transform.position = Vector3.Lerp(_rightEndPivotPoint.position, _leftEndPivotPoint.position, pingPongForBigBar / distance);
            yield return null;
        }
        
    }

    private void WhenTargetMatched()
    {
        float distance = Vector3.Distance(_movingTarget.transform.position, _matchingTarget.transform.position);
        if (distance <= _toMatchThreshold)
        {
            _isMoving = false;
            Debug.Log("Spawn Weapon");
            StopCoroutine(MoveTarget());
            _image.SetActive(true);
            Invoke(nameof(DisableImage), 2f);
        }
        else
        {
            Debug.Log("Match failed");
        }
        
    }

    public void Interact(InputValue value)
    {
        if (!_isMoving)
        {
            _isMoving = true;
            StartCoroutine(MoveTarget());
        }
        else
        {
           WhenTargetMatched(); 
        }
    }

    private void DisableImage()
    {
        _image.SetActive(false);
    }
}
