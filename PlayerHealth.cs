using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int curHealth;
    [SerializeField] int regenerateAmount = 1;
    [SerializeField] float regenerationRate = 2f;

    private void Start()
    {
        curHealth = maxHealth;
        //regenerate health
        InvokeRepeating("Regenerate", regenerationRate, regenerationRate);
    }
    //regenerates the players health
    void Regenerate()
    {
        //Debug.Log("Regeneration: " + curHealth);

        if (curHealth < maxHealth)
            curHealth += regenerateAmount;

        if (curHealth > maxHealth)
            curHealth = maxHealth;
    }
    //used for taking damage, you probably dont need it jac
    public void TakeDamage(int dmg = 1)
    {
        curHealth -= dmg;
        if (curHealth < 0)
            curHealth = 0;

        EventManager.TakeDamage(curHealth/(float)maxHealth);
        if (curHealth < 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Explosions temp = transform.GetComponent<Explosions>();
            EventManager.PlayerDeath();
            temp.CollidedWithEnemy();
            FindObjectOfType<AudioManager>().Stop("Engine");

        }
    }
    public int GetMaxHP()
    {
        return maxHealth;
    }
}
