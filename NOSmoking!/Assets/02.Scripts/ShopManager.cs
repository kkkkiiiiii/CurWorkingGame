using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public Item(string _Num, string _Name, string _Explain, bool _isAcquired ,bool _isUsing)
    {
        Num = _Num;
        Name = _Name; 
        Explain = _Explain;             
        isAcquired = _isAcquired;
        isUsing = _isUsing;
    }
    public string Num, Name, Explain;
    public bool isAcquired ,isUsing;
}
public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;
    public int currentPageIndex = 0;
    public GameObject[] Page;
    public Button buyButton;
    public Button wearingButton;
    private int Price = 0;

    

    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, isntAcquiredList;
    public GameObject[] UsingImage;
    public Image[] TabImage, ItemImage;
    public Material TabIdleMaterial, TabSelectMaterial;
    public PageSwiper pageSwiper;

    private int curSelectedNum=0;
    
    // Start is called before the first frame update
    void Start()
    {
        // ��ü ������ ����Ʈ �ҷ�����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE", row[4] == "TRUE"));
        }
        Load();

        DataManager.instanceData.AcquiredNumList.Sort();
        for (int i = 0; i < MyItemList.Count; i++)
        {
            for (int j = 0; j < DataManager.instanceData.AcquiredNumList.Count; j++)
            {
                if(DataManager.instanceData.AcquiredNumList[j] == i)
                    UsingImage[i].gameObject.SetActive(false);
            }
        }
        /*
        foreach (ShopHatInfo Hat in Hats)
        {
            Hat.isLocked = PlayerPrefs.GetInt(Hat.name,0) == 0 ? false : true;           
        }

        currentPlayerIndex = PlayerPrefs.GetInt("SelectedChar", 0);

        foreach (GameObject HatModule in HatsModules)
        {
            HatModule.SetActive(false);
        }
        HatsModules[currentPlayerIndex].SetActive(true);*/
    }
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        TabClick(curSelectedNum);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TabClick(curSelectedNum);
    }

    // �� Ŭ���ϸ� �� �������� ���õǾ��� �� material ���������� �ð������� �����ְ� ������ ��ǰ�̸� Buy��ư�� ����� �����ư�� �����ش�. 
    // �������� ���� ��ǰ�̸� Buy��ư�� �����ְ� �����ư�� �����.
    public void TabClick(int tabNum)
    {
        curSelectedNum = tabNum;        
        for (int i = 0; i < TabImage.Length; i++)
        {            
            TabImage[i].material = i == tabNum ? TabSelectMaterial : TabIdleMaterial;
        }
        DataManager.instanceData.curHatIndex = tabNum;
        Debug.Log("tabNum :" + tabNum);
        if (MyItemList[tabNum].isAcquired == true)
        {
            buyButton.gameObject.SetActive(false);
            wearingButton.gameObject.SetActive(true);
        }else if(MyItemList[tabNum].isAcquired == false)
        {
            buyButton.gameObject.SetActive(true);
            wearingButton.gameObject.SetActive(false);
        }
    }
    // ���� ������ ���� �˾ƿ��� �� ������ �ȿ� �ִ� isAcquired = false�� �� �߿��� �����ϰ� �ϳ��� ȹ���Ѵ�.
    // ������ 0 �̸� MyItemList�� 0 ~ 8�� �ε����� �������� ȹ�濩�θ� �����ϰ� i*pageNum 
    // �ش� �������� isAcquired == false�� �������� ����Ʈ�� �޴´�. 0�������� false ����Ʈ���� �����ؼ� �ϳ��� ��� ó���� �Ѵ�.
    public void BuyRandomItem()
    {
        Debug.Log("Bu");
        if (DataManager.instanceData.moneySum >= Price)
        {
            Debug.Log("Buy");
            Debug.Log($"PanelIndex :{pageSwiper.PanelIndex}");
            // isAcquired == false �޾ƿ���
            for (int i = 0 + 9* pageSwiper.PanelIndex; i < 9 + 9*pageSwiper.PanelIndex; i++) // �ش� �������� ������ ����Ʈ�� ����!
            {
                Debug.Log(MyItemList[i].isAcquired);
                if (MyItemList[i].isAcquired == false)
                {
                    isntAcquiredList.Add(MyItemList[i]);
                }                
            }
            int acquiredIndex = Random.Range(1, isntAcquiredList.Count); // �ش��������� ����Ʈ�� ������ �ִ�. �ִ� 9�� 
            int BuyNum =  int.Parse(isntAcquiredList[acquiredIndex].Num);
            isntAcquiredList.RemoveAt(acquiredIndex);            
            MyItemList[BuyNum].isAcquired = true;
            Debug.Log(MyItemList[BuyNum].isAcquired);
            UsingImage[BuyNum].gameObject.SetActive(false);
            DataManager.instanceData.AcquiredNumList.Add(BuyNum);
            // �����ϴ� MyItemList�� �ε����� �� �� �ֳ�? ���� ������ ���� �� �� �ִ�. ���°����?
        }
        Price += 500;
        //Save();
    }
    // ���� ������ �ε����� ������ ��ǰ���� Ȯ���ϰ� �����ϸ� DataManager�� ���� ������ ������ �ε����� �ѱ��. 
    public void WearingHat()
    {
        if (curSelectedNum != -1)
        {
            DataManager.instanceData.curHatIndex = curSelectedNum;
        }        
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void PlayBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}