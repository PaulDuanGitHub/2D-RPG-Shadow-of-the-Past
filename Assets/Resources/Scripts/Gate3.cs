using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gate3 : MonoBehaviour
{
    public GameObject G3;
    public GameObject content;
    public GameObject scroller;
    public Toggle EN;
    public Toggle ZH;
    public TMP_FontAsset fontAsset;
    public float interval;
    private string zh =
//啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊
@"#（七）第三场
（乔治上，梅斯杰林上，）
-乔治
啊，大卫，我的儿子，
你真的要将你手中的剑对准你的父亲吗？
这会使你的灵魂变得沉重，心灵变得麻木。
在你做出无法挽回的事情前，
你还有机会从这出去。
-大卫
我的父亲，使这个国家被诅咒的罪魁祸首。
使者带回来了来自宙斯的神谕，请你相信，
使人们不幸的是一位罪恶的国王。";

    private string en =
//-------------------------------------------------
@"#7. Third Episode
[George comes in, and Mestierin comes in]
-George
O, Davidus, my son.
Are you truly going to point your sword at your father?
It will weigh your soul down and numb your heart.
You still have a chance to escape before committing irreparable acts.
-Davidus
George, my father, the culprit who made this country cursed.
The messenger has brought back a prophecy from the Lord, please believe,
A king tainted with sins made this land cursed.";
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
            PlayerPrefs.SetInt("Level", 2);

            G3.GetComponent<TilemapRenderer>().enabled = true;
            G3.GetComponent<TilemapCollider2D>().enabled = true;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Action();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("KingStatus", 1);
        PlayerPrefs.SetInt("WizardStatus", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("KingStatus") == 0)
        {
            PlayerPrefs.SetInt("KingStatus", -1);
            string zh =
@"（大卫击败国王，国王退）";

            string en =
@"[Davidus defeats George, George goes]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }

        if (PlayerPrefs.GetInt("WizardStatus") == 0)
        {
            PlayerPrefs.SetInt("WizardStatus", -1);
            string zh =
@"（大卫击败梅斯杰林，梅斯杰林退）";

            string en =
@"[Davidus defeats Merstierin, Merstierin goes]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }

        if (PlayerPrefs.GetInt("KingStatus") == -1 && PlayerPrefs.GetInt("WizardStatus") == -1)
        {
            PlayerPrefs.SetInt("KingStatus", -2);
            PlayerPrefs.SetInt("WizardStatus", -2);
            string zh =
@"#（八）第三和唱歌
（歌队上）
-歌队
胜利！胜利女神在山顶呼喊，
这将是黑暗的终结，
可怕的预言终是个谎言，
黎明，属于我们的黎明！
（歌队退）
#（九）退场
-大卫
我的灵魂变得沉重，心灵变得麻木。
天啊，这强烈的罪恶感。
请主原谅我可怕的行为，
我最开始是高尚的不是吗？
（大卫在惨叫中退）";

            string en =
@"#8. Third Stasimon
[Chorus comes]
-Chorus
Victory! The goddess of victory cries out from the mountaintops.
This shall be the end of darkness,
The dreadful prophecy is but a lie.
Dawn, our dawn is here!
[Chorus leaves]
#9. Exodos
-Davidus
My soul has become heavy, my heart numb.
Oh heavens, this intense guilt.
My Lord, please forgive me for my terrible behavior.
Wasn’t I noble from the beginning? 
[Davidus goes in screaming]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }

        if (PlayerPrefs.GetInt("PlayerStatus") == 0 && PlayerPrefs.GetInt("Level") == 2)
        {
            PlayerPrefs.SetInt("PlayerStatus", -1);
            string zh =
@"（大卫被国王击败，大卫退）
#（八）第三和唱歌
（歌队上）
-歌队
啊，多么可怕的场景。
儿子刺杀父亲，父亲刺杀儿子。
这般不幸的事发生了，沉重的罪孽产生了！
这个国家永无安宁。
（歌队退）
#（九）退场
-乔治
大卫，我的儿子。
在这既定的命运中，你一无所获。
你定然不肯背负起这罪恶。
（乔治退，梅斯杰林退）";

            string en =
@"[Davidus is defeated by George, and Davidus goes]
# 8. Third Stasimon
[Chorus comes]
-Chorus
O, how dreadful the scene is.
Son killing father, father killing son.
Such tragedy has occurred, 
A heavy sin is born!
This country shall never know peace!
[Chorus leaves]
# 9. Exodos
-George
Davidus, my son.
In this predetermined destiny, you have gained nothing.
You are not yet willing to bear this sin.
[George leaves, Mestierin leaves]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }
    }
}
