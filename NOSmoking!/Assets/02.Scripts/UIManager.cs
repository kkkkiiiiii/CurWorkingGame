using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    private static UIManager m_instance;
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UIManager>();
            return m_instance;
        }
    }

    [SerializeField] private TextMeshProUGUI stageNumTMP;
    [SerializeField] private TextMeshProUGUI finalScoreTMP;
    [SerializeField] private TextMeshProUGUI stageScoreTMP;
    [SerializeField] private TextMeshProUGUI comboScoreTMP;
    [SerializeField] private TextMeshProUGUI MoneyTMP;
    [SerializeField] private TextMeshProUGUI clearRewardTMP;
    public GameObject gameoverUI; // ���� ���� �� Ȱ��ȭ�� UI
    public GameObject mainUI;
    public GameObject gameClearUI;

    public Slider comboSlider;

    private float Duration = 3f;
    private float elapsedTime;
    private bool isCleared;
    private void OnEnable()
    {        
        // �޺� �����̴� Ȱ��ȭ
        comboSlider.gameObject.SetActive(true);
        comboSlider.maxValue = 100;
        comboSlider.value = 0;
    }
    
    private void Start()
    {
        isCleared = false;
        SetActiveMainUI(true);
        SetActiveGameoverUI(false);
        SetActiveGameClearUI(false);
        UpdateMoneySumText(DataManager.instanceData.moneySum);
    }
    private void Update()
    {
        if (isCleared)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete =elapsedTime / Duration;
            float reward = Mathf.Lerp(1f, GameManager.instance.getMoney, (Mathf.SmoothStep(0,1,percentageComplete)));
            clearRewardTMP.text = reward.ToString("000");            
        }
    }
    // �������� ����
    public void UpdateStageNum()
    {
        stageNumTMP.text = "Stage : " + DataManager.instanceData.curStage;
    }

    // �������� ���� ����
    public void UpdateStageScoreText(int stageScore)
    {
        stageScoreTMP.text = "Score : " + stageScore.ToString();
    }
    // �޺� ���� ���� 
    public void UpdateComboScoreText(int comboScore)
    {              
        comboScoreTMP.text = comboScore.ToString();
        comboSlider.value = comboScore;
    }
    public void UpdateFinalScoreText(int stageScore, int comboScore)
    {        
        finalScoreTMP.text = (stageScore + comboScore).ToString();
        isCleared = true;
    }
    public void UpdateMoneySumText(int MoneySum)
    {
        MoneyTMP.text = MoneySum.ToString();        
    }    
    // UI Ȱ��ȭ
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }
    public void SetActiveMainUI(bool active)
    {
        mainUI.SetActive(active);

        isCleared = false;
    }
    public void SetActiveGameClearUI(bool active)
    {
        gameClearUI.SetActive(active);
    }
}
