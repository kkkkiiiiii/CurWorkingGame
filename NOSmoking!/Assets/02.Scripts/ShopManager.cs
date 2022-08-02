using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public Item(string _Name, string _Explain, string _Num, bool _isUsing)
    { 
        Name = _Name; 
        Explain = _Explain;     
        Num = _Num;
        isUsing = _isUsing;
    }
    public string Name, Explain, Num;
    public bool isUsing;
}
public class ShopManager : MonoBehaviour
{
    public int currentPlayerIndex;
    public int currentPageIndex = 0;
    public GameObject[] Page;
    public GameObject[] HatsModules;
    public ShopHatInfo[] Hats;
    public Button buyButton;
    public Button NextButton;
    public Button PrevButton;
    private int Price = 500;


    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, CurItemList;
    public GameObject[] UsingImage;
    public Image[] TabImage, ItemImage;
    public Material TabIdleMaterial, TabSelectMaterial;

    private int curSelectedNum=-1;
    // Start is called before the first frame update
    void Start()
    {
        // 전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3] == "TRUE"));
        }


        Load();

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
    public void SlotClick(int slotNum)
    {
        Item currentItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x=>x.isUsing == true);

        currentItem.isUsing = true;

        if (UsingItem != null)
        {
            UsingItem.isUsing = false;
            currentItem.isUsing = true;
        }
        Save();
    }
    public void TabClick(int tabNum)
    {
        curSelectedNum = tabNum;        
        for (int i = 0; i < TabImage.Length; i++)
        {            
            TabImage[i].material = i == tabNum ? TabSelectMaterial : TabIdleMaterial;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void NextPage()
    {
        Page[currentPageIndex].SetActive(false);
        currentPageIndex++;

        if (currentPageIndex == Page.Length -1)
        {
            currentPageIndex = Page.Length - 1;
            NextButton.gameObject.SetActive(false);
            PrevButton.gameObject.SetActive(true);
        }
        else
        {
            PrevButton.gameObject.SetActive(true);
        }
        Page[currentPageIndex].SetActive(true);

    }

    public void Previous()
    {
        Page[currentPageIndex].SetActive(false);
        currentPageIndex--;

        if (currentPageIndex == 0)
        {
            currentPageIndex = 0;
            PrevButton.gameObject.SetActive(false);
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(true);
        }
            
        Page[currentPageIndex].SetActive(true);


    }

    public void UnlockItem()
    {
        ShopHatInfo p = Hats[currentPlayerIndex];
        PlayerPrefs.SetInt(p.name, 1);
        PlayerPrefs.SetInt("SelectedChar", currentPlayerIndex);
        p.isLocked = true;
        //PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score", 0) - p.price);

        Price += 250;
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}