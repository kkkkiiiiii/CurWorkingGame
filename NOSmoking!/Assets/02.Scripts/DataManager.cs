using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // �� ��ũ��Ʈ�� ���� ��������, ������ ���� �ε���, ���� ���� ���� ������ ������ �ִ´�. DontDestroyOnLoad
    // ShopScene������ ShopManager�� DataManager�� ������Ʈ�Ѵ�. 

    public static DataManager instanceData;
    public List<int> AcquiredNumList;
    public int curHatIndex;
    public int moneySum;
    private void Awake()
    {
        if (instanceData != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceData = this;

            DontDestroyOnLoad(gameObject);
        }
        
    }


}
