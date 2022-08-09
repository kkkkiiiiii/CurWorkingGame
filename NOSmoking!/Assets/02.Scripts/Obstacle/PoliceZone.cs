using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceZone : MonoBehaviour
{
    // PoliceZone�� ���� �����ϰ� �ִ� ������ ��ġ�� ������������ �ű��. Vector3.Lerp
    // �޺� ���� ��´�. 
    public int policeNum;

    private float xposition;
    private float yposition;
    private float zposition;
    private Vector3 position;
    
    public GameObject[] policePrefab;

    public List<PoliceMan> policeMen;
    private void Start()
    {

        if (policeNum>0)
        {
            CreatePoliceMan();
        }
    }

    private void CreatePoliceMan()
    {

        for (int i = 0; i < policeNum; i++)
        {
            int index = Random.Range(0, 2);
            float x = Random.Range(-0.8f, 0.8f);
            float z = Random.Range(-2f, 0.2f);
            position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
            GameObject policemanGameObject = Instantiate(policePrefab[index], position, Quaternion.AngleAxis(Random.Range(140f,220f),Vector3.up));
            PoliceMan policeman = policemanGameObject.GetComponent<PoliceMan>();
            GameManager.instance.exsitingPoliceMan.Add(policemanGameObject);
            policeman.policmanAnimator.SetTrigger($"Talking{Random.Range(1, 3)}");
            if (policeman!= null)
            {
                policeMen.Add(policeman);
            }                                   
        }        
    }

    public void TriggerAndChase(Transform smokerPosition)
    {
        if (policeMen.Count == 0)
            return;        
        policeMen[0].ChaseSmoker(smokerPosition.gameObject);
        policeMen.RemoveAt(0);
    }

    
    // policeNum�� 1�̻��� ���ڰ� �ִٸ� policeZone�� �� �� ��ŭ�� policePrefab���� ��ȯ�ϰ� Player�� smokers����Ʈ�� 
    // ������Ʈ�� �����ؼ� ��ġ�� �ٲ۴�. �޸��� �ִϸ��̼��� ���ϰ� Vector3.lerp�� �ڿ������� �浹�Ѵ�. 
    // �浹�ϸ� ������Ʈ ����� ��� ����Ʈ(����?) 

    // policeZone�� ������ ����ǥ ����Ʈ �߰�! 
}
