using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
public class GameManager : MonoBehaviour
{
    
    public bool isGameOver { get; private set; }
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            // �̱��� ������ ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            { 
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>(); 
            }
            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }    
    public int finalScore = 0;
    private int stageScore;
    private int comboScore;

    public int Money=0;
    private int Diamond;

    public Player player;
    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ� �ڽ��� �ı�
        if(instance != this)
            Destroy(gameObject);
        isGameOver = true;                
    }
    void Start()
    {
        FindObjectOfType<Player>().onDeath += GameOver;
        UpdateMoneySum();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateStageScore();
        //UpdatecomboScore();
    }
    public void UpdateStageScore(int newScore)
    {
        if (!isGameOver)
        {
            // ���� �߰�
            stageScore += newScore;
            finalScore += newScore;
            // ���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateStageScoreText(stageScore);
        }
    }
    public void UpdatecomboScore(int newScore)
    {
        if (!isGameOver)
        {
            // ���� �߰�
            comboScore += newScore;
            finalScore += newScore;
            if(comboScore <= 0)
                comboScore = 0;
            //Debug.Log($"GameManager : " + comboScore);
            // �޺� ���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateComboScoreText(comboScore);
            
        }
    }    
    public void UpdateMoneySum()
    {
        Money = finalScore * player.clearBrokeObstacle;
        DataManager.instanceData.moneySum += Money;
        string cashJdata = JsonConvert.SerializeObject(DataManager.instanceData.moneySum);
        File.WriteAllText(Application.dataPath + "/Resources/CashDataText.txt", cashJdata);
        UIManager.instance.UpdateMoneySumText(DataManager.instanceData.moneySum);
    }
    public void UpdateFinalScore()
    {
        finalScore = stageScore + comboScore;
        UIManager.instance.UpdateFinalScoreText(stageScore, finalScore);
    }
    public void StageClear() 
    {
        isGameOver = true;
        UIManager.instance.SetActiveGameClearUI(true);

    }
    public void StartGame()
    {

        player.StartRevive();
        isGameOver = false;
        UIManager.instance.SetActiveMainUI(false);        
    }
    // ���� ���� ó��
    public void GameOver()
    {
        isGameOver = true;
        // �÷��̾ ��Ʈ���� ȿ�� �߰�

        // ���� ���� UI�� Ȱ��ȭ
        UIManager.instance.SetActiveGameoverUI(true);

    }
    public void GoMainUI()
    {
        stageScore = 0;
        comboScore = 0;
        UIManager.instance.UpdateStageScoreText(0);
        UIManager.instance.UpdateComboScoreText(0);
        UIManager.instance.SetActiveMainUI(true);
        UIManager.instance.SetActiveGameClearUI(false);
        UIManager.instance.SetActiveGameoverUI(false);
        player.Idle();
    }

}
