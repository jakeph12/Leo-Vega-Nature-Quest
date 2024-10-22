using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : Window_Interface
{
    [SerializeField]
    private Window_Interface m_wiWin,m_wiLose,m_wiReadStroy;

    public List<Slot> m_slaAllSlot = new List<Slot>();
    [SerializeField]
    private GameObject m_gmPanelSlot, m_gmSPanelSlot, m_gmPanelOfSlots, m_gmPrefabSlot;

    private int m_inMin = 0;
    private int _m_inSecond = 0;
    private int m_inSecond
    {
        get => _m_inSecond;
        set
        {
            _m_inSecond = value;
            if(_m_inSecond <= 0)
            {
                if (m_inMin <= 0) Lose();
                else
                {
                    m_inMin--;
                    _m_inSecond = 60;
                }
            }
            if (m_txTime)
            {
                m_txTime.text = $"{m_inMin}:";
                if(_m_inSecond >= 10)
                    m_txTime.text += $"{_m_inSecond}";
                else
                    m_txTime.text += $"0{_m_inSecond}";

            }
        }
    }

    private CancellationTokenSource m_tkSourc;

    [SerializeField]
    private Text m_txTime;
    [SerializeField]
    private AudioClip m_clWin, m_clLose;



    [SerializeField] private CanvasGroup m_cvGroop;

    public void Start()
    {
        SetListImg(Animal_Holder.m_spCurrentSprite);
    }

    public void Up(bool up)
    {
        if(up)
            m_cvGroop.alpha = 0.4f;
        else
            m_cvGroop.alpha = 1f;
    }

    public Texture2D GetTexture(Sprite texture)
    {
        var tr = texture.textureRect;
        var mt = texture.texture;

        var newT = mt.GetPixels((int)tr.x, (int)tr.y, (int)tr.width, (int)tr.height);
        var Tx = new Texture2D((int)tr.width, (int)tr.height);

        Tx.SetPixels(newT);
        Tx.Apply();

        return Tx;
    }

    public void SetListImg(Sprite spr)
    {
        Texture2D texture = GetTexture(spr);
        var Sx = (int)(texture.width / 6);
        var Sy = (int)(texture.height / 6);

        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                var c = texture.GetPixels(x * Sx, (5 - y) * Sy, (int)Sx, (int)Sy);
                Texture2D ne = new Texture2D(Sx, Sy);
                ne.SetPixels(c);
                ne.Apply();
                Rect rcN = new Rect(0, 0, Sx, Sy);
                Sprite sp = Sprite.Create(ne, rcN, Vector2.one / 2);

                m_gmPanelSlot.transform.GetChild(x + y * 6).GetComponent<Image>().sprite = sp;
                m_gmSPanelSlot.transform.GetChild(x + y * 6).GetComponent<Slot_Games_Main>().m_bEmty = true;

                m_slaAllSlot.Add(new Slot(x + y * 6, sp));
            }
        }
        Swan();

    }
    public void Swan()
    {
        m_inMin = 3;
        m_inSecond = 1;
        m_tkSourc = new CancellationTokenSource();
        StartTimer(m_tkSourc).Forget();
        List<Slot> sl = new List<Slot>(m_slaAllSlot);
        var r = sl.Count;
        for (int i = 0; i < r; i++)
        {
            var rr = Random.Range(0, sl.Count);
            if (sl[rr] == null) continue;
            var n = Instantiate(m_gmPrefabSlot, m_gmPanelOfSlots.transform);
            n.GetComponent<Slot_Game>().m_slCurrent = sl[rr];
            n.GetComponent<Image>().sprite = sl[rr].m_spMain;
            sl.RemoveAt(rr);
            n.GetComponent<Slot_Game>().m_acOnDestroy += () =>
            {
                if (m_gmPanelOfSlots.transform.childCount == 0) Win();
            };
        }
    }
    public void ReSpawn()
    {
        for (int i = 0;i < m_gmSPanelSlot.transform.childCount;i++)
            m_gmSPanelSlot.transform.GetChild(i).GetComponent<Slot_Games_Main>().m_bEmty = true;
        for (int i = 0; i < m_gmPanelOfSlots.transform.childCount; i++)
            Destroy(m_gmPanelOfSlots.transform.GetChild(0).gameObject);
        Swan();
    }

    public void Win()
    {
        FxsManager.PlayOneShot(m_clWin);
        m_tkSourc.Cancel();
        var t = Window_Controll_Main.OpenWindow(m_wiWin).GetComponent<Window_Game_Answer>();
        t.Inits(() =>
        {
            PlayerPrefs.SetInt("Animal" + Animal_Holder.m_inCurId, 1);
            Animal_Holder.m_inCurId++;
            DCC(t);

        }, () =>
        {
            PlayerPrefs.SetInt("Animal" + Animal_Holder.m_inCurId, 2);
            Animal_Holder.m_inCurId++;
            DCC(t);
        });

    }

    public void DCC(Window_Game_Answer t)
    {
        t.SetBlocK(true);
        var ts =  Window_Controll_Main.OpenWindow(m_wiLose).GetComponent<Window_Game_Lose>();
        ts.Inits(() =>
        {
            if (m_wiReadStroy)
            {
                ts.SetBlocK(true);
                var tss = Window_Controll_Main.OpenWindow(m_wiReadStroy,TypeWindow.NonClose).GetComponent<Window_Interface>();
                tss.m_acOnEndDell += () =>
                {
                    ts.SetBlocK(false);
                };

            }
        }, () =>
        {
            var c = Window_Controll_Main.m_sinThis.m_gmMainWi.GetComponent<Window_Interface>();
            c.SetBlocK(true);
            ts.Close();
            ts.m_acOnEndDell = () =>
            {
                c?.SetBlocK(false);
            };

        }, false);
    }
    public void Lose()
    {
        FxsManager.PlayOneShot(m_clLose);
        SetBlocK(true);
        m_tkSourc.Cancel();
        var t = Window_Controll_Main.OpenWindow(m_wiLose,TypeWindow.NonClose).GetComponent<Window_Game_Lose>();
        t.Inits(() =>
        {
            t.Close();
            ReSpawn();
            t.m_acOnEndDell += () =>
            {
                SetBlocK(false);
            };
        }, () =>
        {
            Close();
            t.Close();
        });
    }

    public async UniTask StartTimer(CancellationTokenSource tk)
    {
        while (m_inSecond >= -2)
        {
            await UniTask.Delay(1000,cancellationToken:tk.Token);
            if (tk == null || tk.Token.IsCancellationRequested)
            {
                if(tk != null)
                    tk.Dispose();
                return;
            }
            m_inSecond--;
        }
    }

    public override void Close(float time = 1)
    {
        m_tkSourc.Cancel();
        base.Close(time);
    }
}

public class Slot
{
    public int m_inId { get; set; }
    public Sprite m_spMain { get; set; }

    public Slot(int id,Sprite sp)
    {
        m_inId = id;
        m_spMain = sp;
    }
}
