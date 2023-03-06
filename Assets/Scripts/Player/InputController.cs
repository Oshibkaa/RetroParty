using UnityEngine;

public class InputController : MonoBehaviour
{
    private IControlable _controlable;

    private void Awake()
    {
        _controlable = GetComponent<IControlable>();
    }

    private void Update()
    {
        ReadMove();
        ReadDash();
        ReadShoot();
        ReadSkill();
    }

    private void ReadMove()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var _direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _controlable.MovementLogic(_direction);
    }

    private void ReadDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _controlable.DashLogic();
        }
    }

    private void ReadShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _controlable.ShootLogic();
        }
    }

    private void ReadSkill()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _controlable.ActiveShield();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _controlable.ActiveUnlimited();
        }
    }
}
