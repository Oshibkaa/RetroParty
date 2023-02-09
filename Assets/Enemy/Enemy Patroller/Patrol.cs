using System.Collections;
using UnityEngine;


public class Patrol : MonoBehaviour 
{
    private int _range = 1;
    private int _rangePlayer = 12;
    private bool _enemyFindPlayer = false;
    private Transform target;
    private Light _lightPlayerSpot;
    private int _randomValue;

    public float Speed = 2f;
    public Transform StartFindPosition;

    private void Start()
    {
        target = GameObjectManager.instance.allObjects[2].transform;
        _lightPlayerSpot = GameObjectManager.instance.allObjects[4].GetComponent<Light>();
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, 1f) * Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 12)
        {
            StartCoroutine(FindPlayer());
        }
        //DrawRayCast();
        if (_enemyFindPlayer == false)
        {
            RotateEnemy();
        }
    }

    public void StartBlinking()
    {
        _lightPlayerSpot.color = Color.red;
    }

    public void StopBlinking()
    {
        _lightPlayerSpot.color = Color.white;
    }

    IEnumerator FindPlayer()
    {
        Ray PlayerCenterRay = new Ray(StartFindPosition.position + transform.up * -0.5f, transform.forward);
        Ray PlayerLeftRay = new Ray(StartFindPosition.position + transform.up * -0.5f + transform.right * -0.4f, transform.forward);
        Ray PlayerRightRay = new Ray(StartFindPosition.position + transform.up * -0.5f + transform.right * 0.4f, transform.forward);

        LayerMask player = LayerMask.GetMask("Player");

        yield return new WaitForSeconds(1f);

        //Player
        if (Physics.Raycast(PlayerCenterRay, _rangePlayer, player) || Physics.Raycast(PlayerLeftRay, _rangePlayer, player) || Physics.Raycast(PlayerRightRay, _rangePlayer, player))
        {
            Speed = 4f;
            _enemyFindPlayer = true;
            StartBlinking();

            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f, transform.forward * _rangePlayer, Color.red);
            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f + transform.right * -0.4f, transform.forward * _rangePlayer, Color.red);
            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f + transform.right * 0.4f, transform.forward * _rangePlayer, Color.red);
        }
        else
        {
            Speed = 2f;
            _enemyFindPlayer = false;
            StopBlinking();
            StopCoroutine(FindPlayer());

            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f, transform.forward * _rangePlayer, Color.blue);
            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f + transform.right * -0.4f, transform.forward * _rangePlayer, Color.blue);
            Debug.DrawRay(StartFindPosition.position + transform.up * -0.5f + transform.right * 0.4f, transform.forward * _rangePlayer, Color.blue);
        }
    }

    public void RotateEnemy()
    {
        Ray leftRay = new Ray(transform.position + transform.up * -0.5f, -transform.right);
        Ray rightRay = new Ray(transform.position + transform.up * -0.5f, transform.right);
        Ray forwardRay = new Ray(transform.position + transform.up * -0.5f, transform.forward);
        Ray backRay = new Ray(transform.position + transform.up * -0.5f, -transform.forward);

        LayerMask triple = LayerMask.GetMask("TripleRotate");
        LayerMask turn = LayerMask.GetMask("TurnRotate");
        LayerMask wall = LayerMask.GetMask("Wall");

        //Corner
        if (Physics.Raycast(leftRay, _range, wall) && Physics.Raycast(forwardRay, _range, wall) && !Physics.Raycast(rightRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            //Debug.Log("Право");
            transform.Rotate(0f, 90f, 0f, Space.Self);
        }

        //Corner
        if (Physics.Raycast(rightRay, _range, wall) && Physics.Raycast(forwardRay, _range, wall) && !Physics.Raycast(leftRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            //Debug.Log("Лево");
            transform.Rotate(0f, -90f, 0f, Space.Self);
        }

        //turn
        if (Physics.Raycast(forwardRay, _range, turn) || Physics.Raycast(forwardRay, _range, wall) && Physics.Raycast(leftRay, _range, wall) && Physics.Raycast(rightRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            //Debug.Log("Разворот");
            transform.Rotate(0f, 180f, 0f, Space.Self);
        }

        //Triple
        if (Physics.Raycast(forwardRay, _range, wall) && !Physics.Raycast(leftRay, _range, wall) && !Physics.Raycast(rightRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            Randomaizer();

            if (_randomValue < 5)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
                //Debug.Log("ПТ Лево");
                _range = 0;
            }
            if (_randomValue > 6)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                //Debug.Log("ПТ Право");
                _range = 0;
            }
        }
        _range = 1;

        //Triple
        if (Physics.Raycast(rightRay, _range, triple) && !Physics.Raycast(forwardRay, _range, wall) && !Physics.Raycast(leftRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            Randomaizer();

            if (_randomValue < 4)
            {
                //Debug.Log("Перекрёсток на лево");
                transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            if (_randomValue > 4)
            {
                //Debug.Log("Перекрёсток прямо");
            }
        }

        //Triple
        if (Physics.Raycast(leftRay, _range, triple) && !Physics.Raycast(forwardRay, _range, wall) && !Physics.Raycast(rightRay, _range, wall) && !Physics.Raycast(backRay, _range, wall))
        {
            Randomaizer();

            if (_randomValue < 4)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                //Debug.Log("Перекрёсток на право");
            }
            if (_randomValue > 4)
            {
                //Debug.Log("Перекрёсток прямо");
            }
        }
    }

    public void DrawRayCast()
    {
        Debug.DrawRay(transform.position, -transform.forward * _range, Color.red);
        Debug.DrawRay(transform.position, transform.forward * _range, Color.red);
        Debug.DrawRay(transform.position, -transform.right * _range, Color.red);
        Debug.DrawRay(transform.position, transform.right * _range, Color.red);

        Debug.DrawRay(transform.position + transform.up * -0.5f, transform.forward * _rangePlayer, Color.blue);
        Debug.DrawRay(transform.position + transform.up * -0.5f + transform.right * -0.4f, transform.forward * _rangePlayer, Color.blue);
        Debug.DrawRay(transform.position + transform.up * -0.5f + transform.right * 0.4f, transform.forward * _rangePlayer, Color.blue);
    }

    public void Randomaizer()
    {
        _randomValue = Random.Range(1, 10);
    }

}
