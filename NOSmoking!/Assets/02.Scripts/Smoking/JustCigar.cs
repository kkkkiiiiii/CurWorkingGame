using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustCigar : MonoBehaviour
{
    public ParticleSystem smokeEffect; // ��� ���� ȿ��
    
    private void Start()
    {
        smokeEffect.Play();
    }   
}
