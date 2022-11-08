using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
struct Player
{
    public string playerName;
    public string playerComment;
}
public class GameLogic : MonoBehaviour
{
    [SerializeField] List<FlagSO> FlagSOs;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] TextMeshProUGUI commentBox;
    public string DMJpath;
    FlagSO currentFlag;
    [SerializeField]Image flagImage;
    Timer timer;
    bool isChanging=false;
    ArrayList info;
    ArrayList comment;
    ArrayList currentPlayers;
    ArrayList rightPlayers;
    string commentContent;
    void Start()
    {
        rightPlayers=new ArrayList();
        currentPlayers=new ArrayList();
        comment=new ArrayList();
        timer=FindObjectOfType<Timer>();
        
    }
    void Update()
    {
        if(FlagSOs.Count<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            comment.Clear();

        }
        if(timer.isTesting)
        {
            info=loadDMJ();
            commentManager();
            showComment();
        }
        if(isChanging!=timer.isTesting)
        {
            if(timer.isTesting)
            {
                currentPlayers.Clear();
                getRandomTest();
            }
            else
            {
                showCorrectAnswer();
                selectRightPlayer();
                showRightPlayer();
            }
            isChanging=!isChanging;
        }
        
        
        
        
        
    }
    void selectRightPlayer()
    {
        foreach(Player player in currentPlayers)
        {
            string playername=player.playerName;
            if(player.playerComment==currentFlag.getNationName())
            {
                rightPlayers.Add(playername);
                Debug.Log(playername);
                
            }
        } 
    }
    void showRightPlayer()
    {
        string Content="答对的同学：\n";
        foreach(string s in rightPlayers)
        {
            Content=Content+s+"\n";
        }
        commentBox.text=Content;
        rightPlayers.Clear();
    }
    void getRandomTest()
    {
        int index=Random.Range(0,FlagSOs.Count);
        currentFlag=FlagSOs[index];
        if(FlagSOs.Contains(currentFlag))
        {
            FlagSOs.Remove(currentFlag);
        }
        textBox.text="猜猜这是哪个国家或地区？";
        flagImage.sprite=currentFlag.getNationFlag();
    }
    ArrayList loadDMJ()
    {
        ArrayList info= new ArrayList();;
        //读取弹幕姬路径
        try
        {
        //使用流的形式读取
		StreamReader sr = null;
		sr = File.OpenText(DMJpath);
		string line;
		while ((line = sr.ReadLine()) != null)
		{
			//将取出的数据放入数组
			info.Add(line);
		}
		//关闭流
		sr.Close();
		//销毁流
		sr.Dispose();
		//将数组返回
        }

        //如果出错则不执行后边的操作
        catch (IOException)
        {
            Debug.Log("弹幕姬路径读取异常");
        }
        return info;
    }
    void showCorrectAnswer()
    {
        textBox.text=currentFlag.getNationName();        
    }
    void showComment()
    {
        foreach(Player pl in currentPlayers)
        {
            commentContent+=pl.playerName+":"+pl.playerComment+"\n";
        }
        commentBox.text=commentContent;
        commentContent="";
    }
    void commentManager()
    {
        //从存储弹幕的数组中提取观众的名字
        int i = 0;
        int re2=0;
        foreach (string str in info)
        {
            i++;
            
            if (i > 8)
            {
                string[] strarr = str.Split(' ');
                string str2 = strarr[2];
                //读取弹幕内容
                if(str2.Substring(0, 2) =="收到")
                {
                    
                    string speakerName = str2.Substring(5);
                    string speakWhat = strarr[4];
                    //判断读取的弹幕是否重复，如果重复则跳出
                    
                    foreach (string s in comment)
                    {
                        if (s != speakerName+speakWhat)
                        {
                            re2 = 0;
                        }
                        else
                        {
                            re2 = 1;
                            break;
                        }
                    }
                    //如果弹幕不重复
                    if (re2 == 0)
                    {
                        comment.Add(speakerName+speakWhat);
                        Player player;
                        player.playerName=speakerName;
                        player.playerComment=speakWhat;
                        currentPlayers.Add(player);  
                    }                        
                }
            }
        }
    }   


}
