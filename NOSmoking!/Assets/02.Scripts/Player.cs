using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public int health { get; set; }
    public bool dead { get;  set; } // ��� ����
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

    public int clearBrokeObstacle=0;

    public ParticleSystem getEffect;

    public AudioClip throwZoneClip;
    public AudioClip smokingZoneClip;
    public AudioClip itemPickUpClip;
    public AudioClip smokerPickUpClip;
    public AudioClip policeZoneClip;
    public AudioClip collideObstacleClip;
    public AudioClip finishfanfareClip;
    public AudioClip deathClip;

    public LayerMask obstacleLayer;   
    public LayerMask attachableLayer;
    public LayerMask scoreZoneLayer;
    public LayerMask finishLineLayer;

    private PlayerMovement playerMovement;
    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;
    public BagScale bagScale;

    public List<Smoker> smokers;
    public List<cigaretteButt> cigaretteButts;
    public List<Obstacle> obstacles;


    // ScoroeZone scoreZone;
    
    void Start()
    {
        dead = true;
        smokers = new List<Smoker>();
        cigaretteButts = new List<cigaretteButt>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }
    public void StartRevive()
    {
        playerMovement.runningSpeed = 5f;
        dead = false;
        health = 2;
        playerAnimator.SetTrigger("StartGame");
    }
    public void Idle()
    {        
        playerAnimator.SetBool("Injured",false);
        playerAnimator.ResetTrigger("PickUpItem");
        playerAnimator.ResetTrigger("FallingBack");
        playerAnimator.SetTrigger("Idle");
        gameObject.transform.position = new Vector3(0, -3.4f, 3.6f);
        gameObject.transform.rotation = Quaternion.identity;
    }
    public void OnDamage(int damage)
    {
        //Debug.Log("health : "+health);

        // �������ŭ ü�� ����
        health -= damage;
        // ���� �������ŭ ���ڳ� ���� �Ͷ߸��� ó��
        //Debug.Log("health : " + health);
        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <=0 && !dead)
        {
            Die();
        }
        playerAnimator.SetBool("Injured", true);
    }
    public void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if(onDeath != null)
            onDeath();  // onDeath�̺�Ʈ�� GameManager GameOver() �޼��� ����

        dead = true;

        // �޺� �����̴� ��Ȱ��ȭ
         UIManager.instance.comboSlider.gameObject.SetActive(false);

        // ����� ��� 
        playerAudioPlayer.PlayOneShot(deathClip);
        // �ִϸ������� Die Ʈ���Ÿ� �ߵ����� ��� �ִϸ��̼� ��� 
        playerAnimator.SetTrigger($"Die{UnityEngine.Random.Range(1,3)}");        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾ ������� �ʾ����� �浹ó�� ����
        if (!dead)
        {

            if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) // IAttachable attach = other.GetComponent<IAttachable>();
            {
                Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    playerAudioPlayer.PlayOneShot(collideObstacleClip);
                    GameManager.instance.UpdatecomboScore(UnityEngine.Random.Range(-14, -7));
                    playerAnimator.SetTrigger("FallingBack");
                    playerMovement.runningSpeed = 2.5f;
                    playerMovement.animationSpeed = 0.1f;
                    int damage = obstacle.GetDamageValue();
                    OnDamage(damage);
                    if (smokers != null)
                    {
                        for (int i = smokers.Count-1; i >= 0; i--)
                        {
                            smokers[i].Drop(this.gameObject);
                        }
                    }
                    if (cigaretteButts != null)
                    {
                        for (int i = cigaretteButts.Count -1 ; i >=0; i--)
                        {
                            cigaretteButts[i].Drop(this.gameObject);
                            cigaretteButts.RemoveAt(i);
                        }
                    }
                }
                // ��ֹ��� ��, �Ѹ�, waterZone
                // �浹�� ������ ������� �Ϸ��� ���� Ÿ������ �˾ƾ� �Ѵ�. 
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Attachable"))
            {
                // IAttachable ������Ʈ �������� �õ�
                IAttachable attachable = collision.gameObject.GetComponent<IAttachable>();

                //attachable != null;
                if (collision.gameObject.TryGetComponent(out Smoker smoker)) // �������� �����ߴٸ� �޼��� ����
                {
                    attachable.OnAttach(gameObject);
                    playerAudioPlayer.PlayOneShot(smokerPickUpClip);

                    playerMovement.runningSpeed += 0.2f;
                    playerMovement.animationSpeed += 0.02f;

                    smokers.Add(collision.gameObject.GetComponent<Smoker>());
                    for (int i = 0; i < smokers.Count; i++)
                    {
                        smokers[i].smokerRunningSpeed = playerMovement.runningSpeed;
                        smokers[i].smokerAnimaionSpeed = playerMovement.animationSpeed;
                    }
                }
                else if (collision.gameObject.TryGetComponent(out cigaretteButt cigar))
                {
                    attachable.OnAttach(gameObject);
                    playerAudioPlayer.PlayOneShot(itemPickUpClip);
                    playerMovement.runningSpeed += 0.2f;
                    playerMovement.animationSpeed += 0.02f;
                    playerAnimator.SetTrigger("PickUpItem");


                    getEffect.Play();

                    cigaretteButts.Add(collision.gameObject.GetComponent<cigaretteButt>());
                    //Debug.Log($"cigarCount " + cigaretteButts.Count);                    
                    bagScale.GetSizeValue(cigaretteButts.Count);                    
                    for (int i = 0; i < smokers.Count; i++)
                    {
                        smokers[i].smokerRunningSpeed = playerMovement.runningSpeed + 0.05f;
                        smokers[i].smokerAnimaionSpeed = playerMovement.animationSpeed;
                    }
                }
            }
            else if (collision.gameObject.layer == scoreZoneLayer)
            {
                
            }//else if(other.gameObject.layer == finishLine){}
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (!dead)
        {
            // ScoreZone�� ����
            // 1. smokingZone ��밡���� �ο� ���� �޾ƿ��� �� ����ŭ�� ��ǥ���� smokingArea�� �ٲ�
            // 2. throwZone ���� ��� ���ʸ� ������. 
            if (other.gameObject.layer == LayerMask.NameToLayer("ScoreZone"))
            {
                ScoreZone scorezone = other.GetComponent<ScoreZone>();
                if (scorezone!=null)
                {
                    if (other.TryGetComponent(out SmokingZone smokingzone))
                    {
                        if (smokers.Count>0)
                        {
                            playerAudioPlayer.PlayOneShot(smokingZoneClip);
                            playerAnimator.SetTrigger("LookBack");
                        }
                        int allow;
                        int allowable = smokingzone.GetScoreValue();
                        if (allowable >= smokers.Count)
                            allow = smokers.Count - 1;
                        else allow = allowable - 1;
                        for (int i = allow; i >=0; i--)
                        {
                            smokers[i].Detach(smokingzone.gameObject);
                            smokers.RemoveAt(i);
                            // �÷��̾ ���� �� �ָ��� ���� limitX�� ������. NavMeshAgent�� �ٽ� �ϰ�
                        }                        
                    }
                    else if(other.TryGetComponent(out ThrowZone throwzone))
                    {
                        if (cigaretteButts.Count>0)
                        {
                            playerAudioPlayer.PlayOneShot(throwZoneClip);
                        }
                        for (int i = cigaretteButts.Count-1; i >= 0; i--)
                        {
                            cigaretteButts[i].Detach(throwzone.gameObject);
                            cigaretteButts.RemoveAt(i);
                        }
                        // ������ �ִ� ��� ���ʸ� ������ ������ ȹ���Ѵ�.
                        // ���� ������ ����Ʈ�� ���̰� 
                    }
                    return ;
                }
            }
            else if(other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) // ����� FinishLine ���� Obstacle
            {
                Debug.Log($"GameManager.instance.finalScore : {GameManager.instance.finalScore}");
                
                Obstacle obstacle = other.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    GameManager.instance.finalScore -= obstacle.MinusCombo();
                    playerAudioPlayer.PlayOneShot(collideObstacleClip);
                    Debug.Log($"WGameManager.instance.finalScore : {GameManager.instance.finalScore}");
                    clearBrokeObstacle += 1;
                    if (GameManager.instance.finalScore <= 0)
                    {
                        playerMovement.runningSpeed = 0;
                        Clear();
                    }
                }
                                                       
            }else if(other.gameObject.layer == LayerMask.NameToLayer("FinishLine"))
            {
                playerAudioPlayer.PlayOneShot(finishfanfareClip);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("PoliceZone"))
            {
                PoliceZone policeZone = other.GetComponent<PoliceZone>();
                if (policeZone != null&&policeZone.policeNum==0)
                {                    
                    for (int i = smokers.Count-1; i >= 0; i--)
                    {
                        smokers[i].Detach(policeZone.gameObject);
                    }
                    playerAudioPlayer.PlayOneShot(policeZoneClip);
                    playerAnimator.SetTrigger("LookBack");
                }
                if (policeZone != null && policeZone.policeNum > 0)
                {
                    for (int i = smokers.Count - 1; i >= 0; i--)
                    {
                        smokers[i].Drop(this.gameObject); //targetPlayer == null ó��
                        policeZone.TriggerAndChase(smokers[i].transform);
                        // smokers[i].transform.position; // smokers�� ��ġ�� ���� PoliceMan�� Chase�޼ҵ忡 �ѱ�
                    }
                    playerAudioPlayer.PlayOneShot(policeZoneClip);
                    playerAnimator.SetTrigger("LookBack");

                }
            }
        }
    }
    private void Clear()
    {
        this.transform.position = new Vector3(00, -1.45f, 395f);
        this.transform.rotation = new Quaternion(0,180,0,0);
        playerAnimator.SetTrigger($"Victory{UnityEngine.Random.Range(1, 4)}");    
        
        GameManager.instance.UpdateFinalScore();
        GameManager.instance.StageClear();

        GameManager.instance.UpdateMoneySum();
    }
}    
