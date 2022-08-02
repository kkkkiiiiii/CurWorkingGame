using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Smoker : MonoBehaviour, IAttachable
{
    public float smokerRunningSpeed = 5f;
    public float smokerAnimaionSpeed;
    public GameObject cigarPivot;
    private Player targetPlayer;
    private ScoreZone targetScoreZone;
    private PoliceZone targetPoliceZone;
    private NavMeshAgent pathFinder;
    // private ScoreZone targetScoreZone;
    private SmokerHand smokerhand;

    public AudioClip beTakenSound; // �÷��̾����� ������ �� �Ҹ�
    public AudioClip cheerUpSound; // SmokingZone�� �������� �� �Ҹ�

    //private PlayerMovement playerMovement;
    private Animator smokerAnimator; 
    private Renderer smokerRenderer; // ������ -> �� ����
    // ���� ����� �����ϴ� �� �˷��ִ� ������Ƽ

    // && Vector3.Distance(target.transform.position,this.transform.position)<10
    
    public bool hasTarget 
    {
        get
        {
            if (targetPlayer == null)
                return false;
            else
                return true;            
        }        
    }
    public bool dead { get; private set; }
    private void OnEnable()
    {
        dead = false;
    }
    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        smokerAnimator = GetComponent<Animator>();
        smokerRenderer = GetComponentInChildren<Renderer>();
    }
    private void Start()
    {
        smokerhand = GetComponent<SmokerHand>();
        StartCoroutine(UpdatePath());        
    }
    private void Update()
    {
        // ���� ����� ���� ���ο� ���� �ٸ� �ִϸ��̼��� ���
        smokerAnimator.SetBool("HasTarget", hasTarget);
    }
    // �ֱ������� ������ ����� ��ġ�� ã�� ��� ����
    private IEnumerator UpdatePath()
    {
        //Debug.Log(hasTarget);       

        // �÷��̾ ���󰡴� �ڵ�
        while (!dead)
        {
            if (hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.speed = smokerRunningSpeed;
                if(targetPlayer != null)
                {
                    pathFinder.SetDestination(targetPlayer.transform.position);
                    smokerAnimator.SetFloat("Run", smokerAnimaionSpeed);
                }                 
            }
            else if(!hasTarget)
            {
                if (targetScoreZone != null)
                {
                    pathFinder.SetDestination(targetScoreZone.transform.position);
                    smokerAnimator.SetTrigger($"cheering{Random.Range(1, 3)}");
                }
                else if (targetPoliceZone != null)
                {
                    //pathFinder.SetDestination(targetPoliceZone.tr)
                }
                pathFinder.isStopped = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Drop(GameObject player)
    {
        // ����, ���� �׳� ����߸�. �� ���� �Ŵ����� ���� �� ����, �޺� ���ҡ� UI UpdateScore ó��
        targetPlayer = null;
    }
    public void OnAttach(GameObject runner)
    {
        targetScoreZone = null;
        targetPlayer = runner.GetComponent<Player>();              

        // ����, ���� �÷��̾� �ڿ� ���δ�. �� ���� �Ŵ����� ���� �� ���� ������ UI UpdateScore ó��
        Player player = runner.GetComponent<Player>();        

        if (player != null)
        {
            //Debug.Log("GetComponent<Player>");
            GameManager.instance.UpdateStageScore(1);
            GameManager.instance.UpdatecomboScore(1);
        }
    }
    public void Detach(GameObject Zone)
    {
        targetPlayer = null;
        if (Zone.TryGetComponent<ScoreZone>(out ScoreZone ScoreZone))
        {
            smokerhand.isArrive = true;
            targetScoreZone = ScoreZone.GetComponent<ScoreZone>();
            //ScoreZone scorezone1 = Zone.GetComponent<ScoreZone>();
            cigarPivot.gameObject.transform.position = new Vector3(0, 0, 0);
            if (targetScoreZone != null)
            {
                GameManager.instance.UpdateStageScore(1);
                GameManager.instance.UpdatecomboScore(2);
            }
        }
        if (Zone.TryGetComponent<PoliceZone>(out PoliceZone policeZone))
        {
            smokerhand.isArrive = true;
            targetPoliceZone = policeZone.GetComponent<PoliceZone>();
            cigarPivot.gameObject.transform.position = new Vector3(0, 0, 0);
            if (targetPoliceZone != null)
            {
                GameManager.instance.UpdateStageScore(-1);
            }

        }
        // ����, ���� scoreZone�� ���д�. �� ���� �Ŵ����� ���� �� ����, �޺� ������ UI UpdateScore ó��
        //hasTarget = true; // Target�� scoreZone���� �ٲ۴�.
        //targetScoreZone = scoreZone.GetComponent<ScoreZone>();
    }

}
