using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingZone : ScoreZone
{
    // smokers ����Ʈ�� �����ؼ� ��� �ο���ŭ Detach�Ѵ�.
    public override int GetScoreValue()
    {
        return Random.Range(5, 10);
    }
}
