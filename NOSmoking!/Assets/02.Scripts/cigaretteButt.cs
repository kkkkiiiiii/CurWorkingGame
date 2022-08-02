using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cigaretteButt : MonoBehaviour, IAttachable
{   
    public ParticleSystem cigaretteParticle;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cigaretteParticle = rb.GetComponentInChildren<ParticleSystem>();
    }
    public void Drop(GameObject runner) // ��ֹ��� �ǰ� �� ���� ��� �Ѵ�. ��ֹ� ���� �ٸ� ���ݷ��� ���� �� �ִ�. 
    {
        // ����, ���� �׳� ����߸�. �� ���� �Ŵ����� ���� �� ����, �޺� ���ҡ� UI UpdateScore ó��
        Player player = runner.GetComponent<Player>();
        Debug.Log("cigar.Drop");
        if (player != null)
        {
            this.gameObject.SetActive(true);
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(1f, 2f);
            float randomZ = Random.Range(-1f, 3f);
            
            this.gameObject.transform.position = runner.transform.position+ new Vector3(2,1,0);
            rb.AddForce(new Vector3(randomX, randomY, randomZ), ForceMode.Impulse);
            //Instantiate ���� �׳� ��ġ�� �÷��̾� ��ó�� �ű���             
        }
    }
    public void OnAttach(GameObject runner) // �����߰��� ����, ���� �ٸ��� �����۸��� ���� �ٸ��� �ϱ� ���ؼ�
    {
        // ����, ���� �÷��̾� �ڿ� ���δ�. �� ���� �Ŵ����� ���� �� ���� ������ UI UpdateScore ó��
        // ���޹��� ���� ������Ʈ�κ��� Player ������Ʈ �������� �õ�
        Player player = runner.GetComponent<Player>();

        // Player ������Ʈ�� �ִٸ�
        if (player != null)
        {            
            GameManager.instance.UpdateStageScore(1);
            GameManager.instance.UpdatecomboScore(1);
        }       
        gameObject.SetActive(false);
        // �߰� : �÷��̾� �տ� �����ʸ� ���ϴ� �۾�, ����Ʈ�� �����ջ����ؼ� �÷��̾� ���� ��ġ�� ���δ�.
    }
    public void Detach(GameObject scoreZone)
    {
        ThrowZone throwZone = scoreZone.GetComponent<ThrowZone>();       
        if (throwZone != null)
        {
            GameManager.instance.UpdateStageScore(1);
            GameManager.instance.UpdatecomboScore(2);
        }


        // ����, ���� scoreZone�� ���д�. �� ���� �Ŵ����� ���� �� ����, �޺� ������ UI UpdateScore ó��

    }
}
