using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class gameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]
    private Transform karelerPaneli; 

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Sprite[] kareSprites;

    private GameObject[] karelerDizisi = new GameObject[25];
    List<int> bolumDegerleriListesi = new List<int>();
    int bolenSayi, bolunenSayi;
    int kacinciSoru, butonDegeri, dogruSonuc;
    bool butonaBasildimi;
    int kalanHak;
    kalanHaklarManager kalanHaklarManager;
    puanManager puanManager;
    string zorlukDerecesi;
    GameObject gecerliKare;

    [SerializeField]
    private GameObject sonucPaneli;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    private GameObject settingPaneli;
    [SerializeField]
    private GameObject durdurbutton;

    public AudioClip butonSesi;
    private void Awake()
    {
        kalanHak = 3;
        audioSource = GetComponent<AudioSource>();
        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        settingPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanHaklarManager = Object.FindObjectOfType<kalanHaklarManager>();
        puanManager = Object.FindObjectOfType<puanManager>();
        kalanHaklarManager.kalanHaklarıKontrolEt(kalanHak);

    }

    void Start()
    {
        butonaBasildimi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kareleriOlustur();

    }
    public void kareleriOlustur()
    {
        for(int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0, kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => butonaBasildi());
            karelerDizisi[i] = kare;
        }
        bolumDegerleriTexteYazdır();
        StartCoroutine(DoFadeRoutine());
        Invoke("soruPaneliAc", 2f);

     }
    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.07f);
        }
    }
    
    void bolumDegerleriTexteYazdır()
    {
        foreach(var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerleriListesi.Add(rastgeleDeger);
            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();
        }
    }
    void soruPaneliAc()
    {
        soruyuSor();
        butonaBasildimi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    
    void soruyuSor()
    {
        bolenSayi = Random.Range(2, 11);
        kacinciSoru = Random.Range(0, bolumDegerleriListesi.Count);
        bolunenSayi = bolenSayi * bolumDegerleriListesi[kacinciSoru];
        dogruSonuc = bolumDegerleriListesi[kacinciSoru];
        soruText.text = bolunenSayi.ToString() + " : " + bolenSayi.ToString();
        if (bolunenSayi <= 40)
        {
            zorlukDerecesi = "kolay";
        }else if(bolunenSayi>40 && bolunenSayi <= 80)
        {
            zorlukDerecesi = "orta";
        }
        else
        {
            zorlukDerecesi = "zor";
        }

    }
    
    
    
    void butonaBasildi()
    {
        if (butonaBasildimi)
        {
            audioSource.PlayOneShot(butonSesi);
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            
        }
        if (dogruSonuc == butonDegeri)
        {
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<Text>().text = "";
            gecerliKare.transform.GetComponent<Button>().interactable = false;
            puanManager.PuanArtir(zorlukDerecesi);
            bolumDegerleriListesi.RemoveAt(kacinciSoru);
            if (bolumDegerleriListesi.Count > 0)
            {
                soruPaneliAc();
            }
            else
            {
                oyunBitti();

            }
        }
        else
        {
            kalanHak--;
            kalanHaklarManager.kalanHaklarıKontrolEt(kalanHak);
            if (kalanHak <= 0)
            {
                oyunBitti();
            }
        }
    }
    
    
    void oyunBitti()
    {
        butonaBasildimi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    
    
    
    public void oyunuDurdur()
    {
        settingPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    public void devamEt()
    {
        settingPaneli.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack);
    }
    void Update()
    {
        
    }
}
