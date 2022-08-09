using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    // policePrefab�� ���� ���� �ϳ� �ϳ��� ������ ��ũ��Ʈ 
    // smoker�� �浹�ϸ� �� ������Ʈ �����ؼ� ���ְ� ����Ʈ �߻�
    // �⺻ ���¿� �����ؼ� ������ ���� �ִϸ��̼�     
    private new CapsuleCollider collider;
    public Animator policmanAnimator;
    private GameObject smokerGameObject;    

    public ParticleSystem catchSmokerParticle;

    public float chasingSpeed = 1f;
    private float Duration = 3f;
    private float elapsedTime;
    public bool isChasing= false;

    private void Awake()
    {
        collider = this.gameObject.GetComponent<CapsuleCollider>();
        policmanAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isChasing)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / Duration;
            this.transform.position = Vector3.Lerp(this.transform.position, smokerGameObject.transform.position,chasingSpeed* percentageComplete);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attachable"))
        {
            // IAttachable ������Ʈ �������� �õ�
            IAttachable attachable = collision.gameObject.GetComponent<IAttachable>();

            //attachable != null;
            if (collision.gameObject.TryGetComponent(out Smoker smoker)) // �������� �����ߴٸ� �޼��� ����
            {
                smoker.gameObject.SetActive(false);
                catchSmokerParticle.Play();
                collider = null;
                policmanAnimator.SetTrigger($"Chasing{Random.Range(1, 3)}");
                GameManager.instance.UpdatecomboScore(-1);
            }
        }
    }
    
    public void ChaseSmoker(GameObject targetSmoker)
    {
        policmanAnimator.SetTrigger("Chase");
        smokerGameObject = targetSmoker;
        isChasing = true;
        Debug.Log(isChasing);
    }
}
