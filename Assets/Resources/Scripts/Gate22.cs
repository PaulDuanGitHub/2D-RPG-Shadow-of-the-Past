using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gate22 : MonoBehaviour
{
    public GameObject content;
    public GameObject scroller;
    public Toggle EN;
    public Toggle ZH;
    public TMP_FontAsset fontAsset;
    public float interval;
    private string zh =
//啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊
@"（大卫走出迷宫）
#（六）第二合唱歌
（歌队上）
-歌队
黑暗！黑暗使人软弱。
无惧的骑士啊，
穿过这可怖的迷宫。
这更加证明你有资格成为菲尔斯利的新王。
请无视那个不详的神谕，
让罪恶从这片土地上消失吧！
（歌队退）";

    private string en =
//-------------------------------------------------
@"[Davidus comes out form the labyrinth]
#6. Second Stasimon
[Chorus comes in]
-Chorus
Darkness! Darkness makes people weak.
Fearless Davidus,
You have crossed this dreadful labyrinth.
This further proves your qualification to be the new king.
Please ignore that ominous prophecy,
Let evil vanish from this land!
[Chorus leaves]";
    public void Action()
    {
        string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');
        StartCoroutine(TypeScript(scripts));

    }

    IEnumerator TypeScript(string[] scripts)
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            string text = scripts[i];
            bool alignCenter = (text.StartsWith('#') || text.StartsWith('-')) ? true : false;
            int fontSize = (text.StartsWith('#')) ? 25 : 20;
            string lang = PlayerPrefs.GetString("lang");

            text = (text.StartsWith('#') || text.StartsWith('-')) ? text.Substring(1) : text;

            GameObject textObject = new GameObject("Scripts");
            TextMeshProUGUI textMeshPro = textObject.AddComponent<TextMeshProUGUI>();

            textMeshPro.fontSize = fontSize;
            textMeshPro.alignment = alignCenter ? TextAlignmentOptions.Center : TextAlignmentOptions.Left;
            textMeshPro.font = fontAsset;

            if (lang == "ZH")
            {
                textMeshPro.lineSpacing = 20;
            }
            else
            {
                textMeshPro.lineSpacing = 64;
            }

            // get RectTransform
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();

            // set width sizeDelta.x
            rectTransform.sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, rectTransform.sizeDelta.y);

            textObject.transform.SetParent(content.transform, false);

            yield return StartCoroutine(TypeNewLine(text, textMeshPro, interval));

            if(i == scripts.Length - 1) 
            {
                PlayerPrefs.SetInt("isFrozen", 0);
            }

        }
    }

    IEnumerator TypeNewLine(string text, TextMeshProUGUI textMeshPro, float interval)
    {


        for (int i = 0; i <= text.Length; i++)
        {
            textMeshPro.text = text.Substring(0, i);
            scroller.GetComponent<Scrollbar>().value = 0;
            yield return new WaitForSeconds(interval);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerPrefs.SetInt("isFrozen", 1);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Action();

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
