using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Serializatio<T>
{
    public Serializatio(List<T> _data) => data = _data;
    public List<T> data;
}
[System.Serializable]
public class data
{
    public data(string _Money, string _Stage, string _Price)
    {
        Money = _Money;
        Stage = _Stage;
        Price = _Price;
    }
    public string Money, Stage, Price;    
}
public class DataManager : MonoBehaviour
{
    // �� ��ũ��Ʈ�� ���� ��������, ������ ���� �ε���, ���� ���� ���� ������ ������ �ִ´�. DontDestroyOnLoad
    // ShopScene������ ShopManager�� DataManager�� ������Ʈ�Ѵ�. 
    public TextAsset CashDatabase;
    public List<data> dataList, myDataList;    

    public static DataManager instanceData;
    public List<int> AcquiredNumList;
    public int curStage;
    public int curHatIndex = -1;
    public int moneySum;
    public int priceData = 0;

    string filePath;
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
    /*string cashJdata = File.ReadAllText(Application.dataPath + "/Resources/CashDataText.txt");
            = JsonConvert.DeserializeObject<List<data>>(cashJdata);
        DataManager.instanceData.moneySum = 
    */
    private void Start()
    {
        filePath = Application.persistentDataPath + "/MyDataText.txt";
        Load();
        moneySum = int.Parse(myDataList[0].Money);
        curStage = int.Parse(myDataList[0].Stage);
        priceData = int.Parse(myDataList[0].Price);
        Debug.Log(moneySum);
        Debug.Log(curStage);
        Debug.Log(priceData);
        /*
        string cashJdata = File.ReadAllText(Application.dataPath + "/Resources/CashDataText.txt");
        Debug.Log($"moneySum : {moneySum}");        
        myDataList = JsonConvert.DeserializeObject<List<data>>(cashJdata);
        moneySum = int.Parse(myDataList[0].Money);
        curStage = int.Parse(myDataList[0].Stage);
        string[] line = CashDatabase.text.Substring(0, CashDatabase.text.Length).Split('\n');
        Debug.Log(line.Length);
        Debug.Log(line[0]);
        Debug.Log(line[1]);
        Debug.Log(line[2]);
        dataList.Add(new data(line[0], line[1], line[2]));
        */
    }
    public void Save()
    {
        
        myDataList[0].Money = moneySum.ToString();
        myDataList[0].Stage = curStage.ToString();
        myDataList[0].Price = priceData.ToString();        
        string jdata = JsonUtility.ToJson(new Serializatio<data>(myDataList));
        File.WriteAllText(filePath, jdata);               
    }
    void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("datafilePath is null");
            ResetData();
            return;
        }
        string jdata = File.ReadAllText(filePath);
        myDataList = JsonUtility.FromJson<Serializatio<data>>(jdata).data;
        //string jdata = File.ReadAllText(Application.dataPath + "/Resources/myCashDataText.txt");
        //myDataList = JsonConvert.DeserializeObject<List<data>>(jdata);
    }
    void ResetData()
    {
        string[] line = CashDatabase.text.Substring(0, CashDatabase.text.Length).Split('\n');
        Debug.Log(line[0]);
        Debug.Log(line[1]);
        Debug.Log(line[2]);
        myDataList.Add(new data(line[0], line[1], line[2]));

        print(myDataList);
        Save();
        Load();
    }
}
