using UnityEngine;

public interface IControlable
{
    void MovementLogic(Vector3 direction);
    void DashLogic();
    void RotationLogic();
    void ShootLogic();
    void ActiveShield();
    void ActiveUnlimited();

}
