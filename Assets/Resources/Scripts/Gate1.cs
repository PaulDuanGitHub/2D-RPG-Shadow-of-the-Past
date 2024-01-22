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
//����������������������������������������
@"#��������һ��
��ʷ��ķ���ϣ�
-����
�ҵ����ѣ���ԭ���ң�
���벻Ҫ������Ǻ��������ҡ�
Ҫ�־͹��ⱻ����ķ��䣬
�ұ���Ҫ������ȫ��������";

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
@"����������ʷ��ķ��ʷ��ķ���ˣ�
#���ģ���һ�ϳ���
������ϣ�
-���
����Ӣ�µ���ʿ������֮�ӣ�
�������������߳����ˣ�
�ô�����³���������
���������ܼ�����
��Ҫ���ˣ�������ɣ�
����������ʲ��٤�յĽŲ����ˣ�
����ǰ��ķ��䣡
������ˣ�";

            string en =
@"[Davidus defeats Slimes, and Slimes goes]
#4. First Stasimon
[Chorus comes in]
-Chorus
O, valiant knight, son of George.
Lead us out of this misfortune!
Let the earth be filled with vitality.
Let the people be free from starving.
It��s coming, come quickly,
I hear the footsteps of Ereshkigal,
It��s in the room ahead!
[Chorus goes]";

            string[] scripts = PlayerPrefs.GetString("lang").Equals("EN") ? en.Split('\n') : zh.Split('\n');

            StartCoroutine(TypeScript(scripts));
        }
        if (PlayerPrefs.GetInt("PlayerStatus") == 0 && PlayerPrefs.GetInt("Level") == 1)
        {
            PlayerPrefs.SetInt("PlayerStatus", -1);
            string zh =
@"����ʷ��ķɱ���������ˣ�
������ϣ�
-���
�¸ҵ���ʿ����
��Ϊ����������ճ�������֮�У�
Ը��������԰�Ϣ��
������ˣ�ʷ��ķ�ˣ�";

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
