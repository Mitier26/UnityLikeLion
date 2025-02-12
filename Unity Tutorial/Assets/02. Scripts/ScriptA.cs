using UnityEngine;

public class ScriptA : MonoBehaviour
{
    public int A = 1; // 멤버 변수 A
    public int B = 2; // 멤버 변수 B

    int AddMethod(int a, int b) // 매개 변수 a, b
    {
        return a + b;
    }

    void Update()
    {
        AddMethod(A, B);
    }
}