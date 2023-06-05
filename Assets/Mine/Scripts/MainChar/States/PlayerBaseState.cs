using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(MainChar player);
    public abstract void Update(MainChar player);
    public abstract void OnCollisionEnter(MainChar player);

}
