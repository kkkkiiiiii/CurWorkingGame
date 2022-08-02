using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 이 스크립트는 현재 스테이지, 착용한 모자 인덱스, 가진 돈에 대한 정보를 가지고 있는다. DontDestroyOnLoad
    // ShopScene에서는 ShopManager가 DataManager를 업데이트한다. 

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
