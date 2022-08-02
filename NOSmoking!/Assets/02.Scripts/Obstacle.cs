using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int powerHitToPlayer=1;
    public int breakObstacleMinusCombo;
    public ParticleSystem wallExplosion;

    private Player player; // �ε��� �÷��̾��� ü�� 1�� ��� ����


    private void Start()
    {
        player = GetComponent<Player>();
    }

    public int GetDamageValue()
    {        
        wallExplosion.Play();
        this.gameObject.SetActive(false);
        return powerHitToPlayer;
    }

    public int MinusCombo()
    {
        wallExplosion.Play();
        this.gameObject.SetActive(false);
        return breakObstacleMinusCombo;
    }
}
