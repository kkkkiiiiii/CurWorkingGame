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
        Debug.Log(tempIndex);
        objCurrentHat = Instantiate(HatsPrefab[tempIndex], SpawnPos);
    }

    public void SpawnHatInGame(int HatIndex)
    {
        Destroy(objCurrentHat);
        objCurrentHat = Instantiate(HatsPrefab[HatIndex], SpawnPos);
    }
}
