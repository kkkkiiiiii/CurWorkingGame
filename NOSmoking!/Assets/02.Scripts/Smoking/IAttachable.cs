using UnityEngine;
public interface IAttachable
{
    void Drop(GameObject player);
    // ����, ���� �׳� ����߸�. �� ���� �Ŵ����� ���� �� ����, �޺� ���ҡ� UI UpdateScore ó��
    void OnAttach(GameObject player);
    // ����, ���� �÷��̾� �ڿ� ���δ�. �� ���� �Ŵ����� ���� �� ���� ������ UI UpdateScore ó��
    void Detach(GameObject scoreZone);
    // ����, ���� scoreZone�� ���д�. �� ���� �Ŵ����� ���� �� ����, �޺� ������ UI UpdateScore ó��
}
