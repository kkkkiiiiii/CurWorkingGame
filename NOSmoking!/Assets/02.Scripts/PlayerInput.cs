using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float touchXDelta { get; set; }

    void Update()
    {
        // ���ӿ��� ���¿����� ����� �Է��� �������� �ʴ´�. �÷��� ��ư�� �����߸� ����
        if(GameManager.instance != null && GameManager.instance.isGameOver)
        {
            touchXDelta = 0;
            return;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            touchXDelta = Input.GetTouch(0).deltaPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }
    }    
}
