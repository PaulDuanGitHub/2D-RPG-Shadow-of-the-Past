using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject content;
    public GameObject scroller;
    public Toggle EN;
    public Toggle ZH;
    public Button Button;
    public TMP_FontAsset fontAsset;
    public float interval;
    private string zh =
//啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊
@"#（一）开场
-（菲尔斯利的城堡中；大卫走下楼梯，手中握着剑）
-大卫
啊，不幸的人们！
若大的房间中居然没有你们灵魂的藏身之处。
罪孽将你们带到这里来，
在这阴冷潮湿的房间。
愿主能垂怜你们灵魂，我的朋友。
#（二）进场歌
（歌队上）
-歌队
身负罪孽的人们，
是谁使你们变成这软弱模样？
古老的预言，将会显灵，将会显灵。
（歌队起跳）
请原谅我，请原谅我！
伟大的主！
（歌队下）";

    private string en =
//-------------------------------------------------
@"#1. Prologue
-Scene. Inside the castle of Fyrslim, Davidus comes down the stairs, holding a sword in his hand.
-Davidus
O, foul people,
There is even no hiding space for your soul in this spacious room.
Sin, sin has brought you here;
In this moist, cold place!
May the Lord have mercy on your souls, my friends.
#2. Parodos
[Chorus comes in]
-Chorus
Men of sin,
Who has made you adhesive?
Ancient prophecies, will be fulfilled, be fulfilled!
[Chorus jumps like slimes]
Forgive me, forgive me!
Great Lord!
[Chorus goes]";

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("isFrozen", 1);
    }

    public void Action()
    {
        EN.interactable = false;
        ZH.interactable = false;
        Button.interactable = false;

        string[] scripts = PlayerPrefs.GetString("lang").Equals("EN")? en.Split('\n') : zh.Split('\n');

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

        }
        PlayerPrefs.SetInt("isFrozen", 0);
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

    // Update is called once per frame
    void Update()
    {

    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
