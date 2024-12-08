
using UnityEngine;

public class Test : MonoBehaviour
{
    private int num = 100;
    int count = 0;
    private string str = "hello";

    private int iNum1, iNum2;
    private void Start()
    {
        iNum1 = 1;
        iNum2 = 2;
        char[] chars = str.ToCharArray();

        char temp = chars[iNum1];
        chars[iNum1] = chars[iNum2];
        chars[iNum2] = temp;
        
        str = new string(chars);
        Debug.Log(count);
        Debug.Log(str);
    }
}
