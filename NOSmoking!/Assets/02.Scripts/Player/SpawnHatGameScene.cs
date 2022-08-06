using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHatGameScene : MonoBehaviour
{
    // �ٸ��� ���� �ſ��� ������ �� ����� ���̴�. �ε����� DataManager�� ���� ������ ���̴�. 

    public GameObject[] HatsPrefab;
    public Transform SpawnPos;
    private GameObject objCurrentHat;

    private void Start()
    {
        int tempIndex = DataManager.instanceData.curHatIndex;
        Debug.Log($"tempIndex : {tempIndex}");
        if (tempIndex != -1)
            objCurrentHat = Instantiate(HatsPrefab[tempIndex], SpawnPos);
        else if (tempIndex == -1)
        {
            Debug.Log(objCurrentHat == null);
            Destroy(objCurrentHat);
        }
            
    }

    public void SpawnHatInGame(int HatIndex)
    {
        Destroy(objCurrentHat);
        objCurrentHat = Instantiate(HatsPrefab[HatIndex], SpawnPos);
    }    
}
