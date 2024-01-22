using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchLang : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text buttonText;
    public TMP_Text replayButtonText;
    public TMP_Text structures;
    public TMP_Text characters;
    public TMP_Text names;
    public TMP_Text roles;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("lang", "EN");
    }

    // true: change to ZH, false: change to EN
    public void ChangeLang(bool b)
    {
        //Debug.Log(b);

        if (b)
        {
            PlayerPrefs.SetString("lang", "ZH");
            title.text = "旧日之影";
            title.lineSpacing = 20;

            structures.text = 
@"（一）开场
（二）进场歌
（三）第一场
（四）第一合唱歌
（五）第二场
（六）第二合唱歌
（七）第三场
（八）第三合唱歌
（九）退场";
            structures.lineSpacing = 20;

            characters.text = "出场人物";
            characters.lineSpacing = 20;

            names.text =
@"大卫
乔治
梅斯杰林
歌队
史莱姆若干";
            names.lineSpacing = 20;

            roles.text =
@"骑士，乔治之子
菲尔斯利姆国王，大卫的父亲
菲尔斯利姆巫师
菲尔斯利姆民众组成
";
            roles.lineSpacing = 20;

            buttonText.text = "开场";
            replayButtonText.text = "重新开始";
        }
        else
        {
            PlayerPrefs.SetString("lang", "EN");
            title.text = "Shadow of the Past";
            title.lineSpacing = 64;

            structures.text =
@"1. Prologue
2. Parodos
3. First Episode
4. First Stasimon
5. Second Episode
6. Second Stasimon
7. Third Episode
8. Third Stasimon
9. Exodos";
            structures.lineSpacing = 64;

            characters.text = "Characters";
            characters.lineSpacing = 64;

            names.text =
@"Davidus
George
Mestierin
Chorus
Slimes";
            names.lineSpacing = 64;

            roles.text =
@"Knight, son of George
King of Fyrslim, father of Davidus
Wizard of Fyrslim
People of Fyrslim
";
            roles.lineSpacing = 64;

            buttonText.text = "Prologue";
            replayButtonText.text = "Replay";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetString("lang"));
    }
}
