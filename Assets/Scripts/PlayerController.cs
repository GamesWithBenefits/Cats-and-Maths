using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xLim;
    public float speed = 1;
    private Rigidbody2D _rb;
    private float _xInput;
    private Vector2 _newPos;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    { 
        MovePlayer();   
    }

    void MovePlayer()
    {
        _xInput = Input.acceleration.x;
        _newPos = transform.position;
        _newPos.x = Mathf.Clamp(_newPos.x + (_xInput * speed), -xLim, xLim);
        _rb.MovePosition(_newPos);
    }
}
