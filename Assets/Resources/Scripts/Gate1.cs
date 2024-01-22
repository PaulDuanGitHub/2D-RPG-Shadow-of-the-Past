using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Gate1 : MonoBehaviour
{
    public GameObject G1;
    public GameObject G2;
    public GameObject content;
    public GameObject scroller;
    public Toggle EN;
    public Toggle ZH;
    public TMP_FontAsset fontAsset;
    public float interval;
    private string zh =
//啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊
@"#（三）第一场
（史莱姆们上）
-大卫
我的朋友，请原谅我，
还请不要在那硫磺海中诅咒我。
要怪就怪这被诅咒的房间，
我必须要将你们全部除掉。";

    private string en =
//-------------------------------------------------
@"#3. First Episode
[Slimes come in]
-Davidus
Please forgive me, my friends.
And do not curse me in the sulfurous sea.
Blame not me but this cursed room.
I must eliminate all of you.";

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("TotalSlimes", 10);
        PlayerPrefs.SetInt("PlayerStatus", 1);
        PlayerPrefs.SetInt("Level", 1);
    }

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
        if(collider.tag == "Player")
        {
            G1.GetComponent<TilemapRenderer>().enabled = true;
            G1.GetComponent<TilemapCollider2D>().enabled = true;

            G2.GetComponent<TilemapRenderer>().enabled = true;
            G2.GetComponent<TilemapCollider2D>().enabled = true;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Action();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("TotalSlimes") == 0)
        {
            PlayerPrefs.SetInt("TotalSlimes", -1);
            G1.GetComponent<TilemapRenderer>().enabled = false;
            G1.GetComponent<TilemapCollider2D>().enabled = false;

            G2.GetComponent<TilemapRenderer>().enabled = false;
            G2.GetComponent<TilemapCollider2D>().enabled = false;

            string zh =
@"（大卫击败史莱姆，史莱姆们退）
#（四）第一合唱歌
（歌队上）
-歌队
啊，英勇的骑士，乔治之子，
请您带领我们走出厄运！
让大地重新充满生机，
让人民免受饥饿。
就要来了，快点来吧，
我听到埃列什基伽勒的脚步声了，
就在前面的房间！
（歌队退）";

            string en =
@"[Davidus defeats Slimes, and Slimes goes]
#4. First Stasimon
[Chorus comes in]
-Chorus
O, valiant knight, son of George.
Lead us out of this misfortune!
Let the earth be filled with vitality.
Let the people be free from starving.
It’s coming, come quickly,
I hear the footsteps of Ereshkigal,
It’s in the room ahead!
[Chorus goes]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }
        if (PlayerPrefs.GetInt("PlayerStatus") == 0 && PlayerPrefs.GetInt("Level") == 1)
        {
            PlayerPrefs.SetInt("PlayerStatus", -1);
            string zh =
@"（被史莱姆杀死，大卫退）
（歌队上）
-歌队
勇敢的骑士啊，
你为何葬身于这粘稠的生物之中？
愿你的灵魂得以安息。
（歌队退，史莱姆退）";

            string en =
@"[Davidus is defeated by Slimes, and Davidus goes]
[Chorus comes on]
-Chorus
O brave knight.
Why art thou buried in these viscous creatures?
May your soul rest in peace.
[Chorus leaves, Slimes leaves]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }
    }
}
