using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScale : MonoBehaviour
{
    private float XBigScale = 0.1f;
    private float YBigScale = 0.1f;
    private float ZBigScale = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
    }
        
    public void GetSizeValue(int buttsNum)
    {
        Debug.Log("Size");
        switch (buttsNum)
        {
            case 4:
                XBigScale = 0.2f;
                YBigScale = 0.1f;
                ZBigScale = 0.1f;
                break;
            case 8:
                XBigScale = 0.2f;
                YBigScale = 0.2f;
                ZBigScale = 0.1f;
                break ;
            case 12:
                XBigScale = 0.2f;
                YBigScale = 0.2f;
                ZBigScale = 0.2f;
                break;
            case 16:
                XBigScale = 0.3f;
                YBigScale = 0.2f;
                ZBigScale = 0.2f;
                break;
            case 20:
                XBigScale = 0.3f;
                YBigScale = 0.3f;
                ZBigScale = 0.2f;
                break;
            case 24:
                XBigScale = 0.3f;
                YBigScale = 0.3f;
                ZBigScale = 0.3f;
                break;
            case 28:
                XBigScale = 0.4f;
                YBigScale = 0.3f;
                ZBigScale = 0.3f;
                break;
            case 32:
                XBigScale = 0.4f;
                YBigScale = 0.4f;
                ZBigScale = 0.3f;
                break;
            case 36:
                XBigScale = 0.4f;
                YBigScale = 0.4f;
                ZBigScale = 0.4f;
                break;
            default:
                break;
        }

        ScaleUp();
    }
    void ScaleUp()
    {        
        this.gameObject.transform.localScale = new Vector3(XBigScale, YBigScale, ZBigScale);
    }
}
