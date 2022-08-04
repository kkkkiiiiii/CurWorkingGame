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
    public int prevUsingIndex;
    public int curUsingIndex;
    public GameObject[] Page;
    public Button buyButton;
    public Button wearingButton;
    public Button removingButton;
    private int Price = 0;


    private AudioSource ShopAudio;
    public AudioClip clickClip;
    public TextAsset ItemDatabase;
    public TextAsset CashDatabase;
    public List<Item> AllItemList, MyItemList, isntAcquiredList;
    public int Cash;
    public GameObject[] UsingImage;
    public Image[] TabImage, ItemImage;
    public Material TabIdleMaterial, TabSelectMaterial;
    public PageSwiper pageSwiper;

    private int curSelectedNum=0;

    void Start()
    {
        ShopAudio = GetComponent<AudioSource>();
        // ��ü ������ ����Ʈ �ҷ�����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE", row[4] == "TRUE"));
        }
        Load();
        Cash = DataManager.instanceData.moneySum;
        DataManager.instanceData.AcquiredNumList.Sort();
        for (int i = 0; i < MyItemList.Count; i++)
        {
            if (MyItemList[i].isAcquired == true)
            {
                Debug.Log(i);
                UsingImage[i].SetActive(false);
            }            
                        
            for (int j = 0; j < DataManager.instanceData.AcquiredNumList.Count; j++)
            {
                if(DataManager.instanceData.AcquiredNumList[j] == i)
                    UsingImage[i].SetActive(false);
                
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
        string cashJdata = JsonConvert.SerializeObject(Cash);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
        File.WriteAllText(Application.dataPath + "/Resources/CashDataText.txt", cashJdata);
        TabClick(curSelectedNum);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        //string cashJdata = File.ReadAllText(Application.dataPath + "/Resources/CashDataText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);
        //Cash = JsonConvert.DeserializeObject<int>(cashJdata);
        //DataManager.instanceData.moneySum = Cash;
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
        Debug.Log("tabNum :" + tabNum);
        if (MyItemList[tabNum].isAcquired == true)
        {
            buyButton.gameObject.SetActive(false);
            if (MyItemList[tabNum].isUsing == true)
            {
                wearingButton.gameObject.SetActive(false);
                removingButton.gameObject.SetActive(true);
            }
            else if (MyItemList[tabNum].isUsing == false)
            {
                wearingButton.gameObject.SetActive(true);
                removingButton.gameObject.SetActive(false);
            }
        }
        else if(MyItemList[tabNum].isAcquired == false)
        {
            buyButton.gameObject.SetActive(true);
            wearingButton.gameObject.SetActive(false);
        }

        
        ShopAudio.PlayOneShot(clickClip);
    }
    // ���� ������ ���� �˾ƿ��� �� ������ �ȿ� �ִ� isAcquired = false�� �� �߿��� �����ϰ� �ϳ��� ȹ���Ѵ�.
    // ������ 0 �̸� MyItemList�� 0 ~ 8�� �ε����� �������� ȹ�濩�θ� �����ϰ� i*pageNum 
    // �ش� �������� isAcquired == false�� �������� ����Ʈ�� �޴´�. 0�������� false ����Ʈ���� �����ؼ� �ϳ��� ��� ó���� �Ѵ�.
    public void BuyRandomItem()
    {
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
            UsingImage[BuyNum].gameObject.SetActive(false);
            DataManager.instanceData.AcquiredNumList.Add(BuyNum);
            ShopAudio.PlayOneShot(clickClip);

            Cash -= DataManager.instanceData.priceData;
            DataManager.instanceData.moneySum -= DataManager.instanceData.priceData;
            DataManager.instanceData.priceData += 500; // DataManager�� �� �� �ִ� ������ ����
        }
        Save();
    }
    // ���� ������ �ε����� ������ ��ǰ���� Ȯ���ϰ� �����ϸ� DataManager�� ���� ������ ������ �ε����� �ѱ��. 
    public void WearingHat()
    {
        // 1. �ݺ��������� isUsing == true �ΰ� ã�Ƽ� false�ιٲ�
        // 2. ���� ���� ������ ������ �ε����� ����ϰ� 
        // ������ ��ǰ�� �ε����� ���� �����ϰ� �ִ� ��ǰ�� �ε����� �ٸ��ٸ� ���� �����ϴ� ��ǰ�� isUsing�� false�� �ٲ���Ѵ�.

        if (DataManager.instanceData.curHatIndex != curSelectedNum)
            MyItemList[DataManager.instanceData.curHatIndex].isUsing = false;

        DataManager.instanceData.curHatIndex = curSelectedNum;
        MyItemList[curSelectedNum].isUsing = true;
        wearingButton.gameObject.SetActive(false);
        removingButton.gameObject.SetActive(true);
        ShopAudio.PlayOneShot(clickClip);
        Save();
    }
    public void RemovingHat()
    {
        DataManager.instanceData.curHatIndex = -1;
        MyItemList[curSelectedNum].isUsing = false;
        wearingButton.gameObject.SetActive(true);
        removingButton.gameObject.SetActive(false);
        ShopAudio.PlayOneShot(clickClip);
        Save();
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void PlayBtn()
    {
        ShopAudio.PlayOneShot(clickClip);
        SceneManager.LoadScene("GameScene");
    }
}