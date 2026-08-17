using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    private int redDuration =1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }
    public void TakeDamage()
    {
        sr.color = Color.red;
        Invoke(nameof(TurnWhite),redDuration);
    }
    
    private void TurnWhite()
    {
        sr.color = Color.white;
    }
}
