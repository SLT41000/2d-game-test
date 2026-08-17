using UnityEngine;

public class playAnimationEvent : MonoBehaviour
{
    private player Player;

    private void Awake()
    {
        Player = GetComponentInParent<player>();
    }

    private void disableMovementAndJump()
    {
        Player.setIsCanJump(false);
        Player.setIsCanMove(false);
    }
    
    private void enableMovementAndJump()
    {
        Player.setIsCanJump(true);
        Player.setIsCanMove(true);
    }

    private void DamageEnemies()=>Player.DamageEnemies();
    


}
