using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //Gives you an field to interact in the inspector of the game object
    [SerializeField] float _launchForce = 800;
    [SerializeField] float _maxDragDistance = 5;
    public Rigidbody2D _rigidbody;
    private Vector2 _startPosition;
    SpriteRenderer _spr;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _startPosition = _rigidbody.position;
        _rigidbody.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spr.color = new Color(0, 0, 0);
    }

    private void OnMouseUp()
    {
        var currentPosition = _rigidbody.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _launchForce);
        _spr = GetComponent<SpriteRenderer>();
        _spr.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance >_maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody.position = _startPosition;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector2.zero;
    }
}
