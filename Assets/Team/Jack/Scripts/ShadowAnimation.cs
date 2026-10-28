using UnityEngine;
using UnityEngine.UI;

public class ShadowAnimation : MonoBehaviour
{
    private float YposTimer = 0;
    private RectTransform rectTransform;
    private Image image;
    private float xConst;
    private int yConst;
    void Start()
    {
        xConst = (int) Random.Range(0, 2);
        xConst = (xConst*2) - 1;
        Debug.Log(xConst);

        xConst *= Random.Range(0.5f, 1);

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if(xConst > 0) rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        image.color = new Color(255, 255, 255, 0);

        yConst = Random.Range(-300, 300);
        rectTransform.transform.localRotation = rotation;

        rectTransform.transform.localPosition = new Vector3(1300 * xConst, yConst, 0);
    }

    void Update()
    {
        YposTimer += Time.deltaTime;

        image.color = new Color(255, 255, 255, Mathf.Lerp
        (
            image.color.a, 
            ((float) (Mathf.Sin(YposTimer*3) / 2 + 1.5)/4) + 0.5f, 
            0.01f
        ));

        rectTransform.localPosition = Vector3.Lerp
        (
            rectTransform.localPosition, 
            new Vector3(xConst * 900 + Mathf.Sin(YposTimer * 5)*50, Mathf.Sin(YposTimer)*200 + yConst, 0), 
            0.01f
        );
    }
}
