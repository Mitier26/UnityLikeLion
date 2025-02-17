using TMPro;
using UnityEngine;

public class LottoBall : MonoBehaviour
{
    public TMP_Text textNumber;

    private bool isScale;

    void Start()
    {
        textNumber = this.transform.GetChild(0).GetComponent<TMP_Text>();

        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isScale)
        {
            this.transform.localScale += Vector3.one * Time.deltaTime * 0.5f;

            if (this.transform.localScale.x >= 1f)
            {
                isScale = true;

                this.transform.localScale = Vector3.one;
            }
        }
    }

}