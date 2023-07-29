using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private float _inputH;
    private float _inputV;

    public void SetInput(float h, float v)
    {
        _inputH = h;
        _inputV = v;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(_inputH, 0f, _inputV).normalized;
        
        transform.position += direction * (_speed * Time.deltaTime);
    }

    public void GetMoveInfo(out Vector3 position)
    {
        position = transform.position;
    }
}