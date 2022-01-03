using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fetchData : MonoBehaviour
{
    public string urlFetchMainMenu;
    [SerializeField]
    public List<Category> allCategory = new List<Category>();
    public GameObject mainmenuPrefab;
    public GameObject mainMenuContent;
    public GameObject obatContent;
    public GameObject mainmenu;
    public GameObject obatmenu;
    bool dataFethced;
    bool dataAssign;

    private void Start()
    {
        StartCoroutine(loadingJsonValue(urlFetchMainMenu));
        
    }

    public void OnActiive(string menu)
    {
        if(menu == "obat")
        {
            mainmenu.SetActive(false);
            obatmenu.SetActive(true);
        }
        else if(menu == "main")
        {
            mainmenu.SetActive(true);
            obatmenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (dataFethced && !dataAssign)
        {
            dataAssign = true;
            foreach (var category in allCategory)
            {
                GameObject instantiate = Instantiate(mainmenuPrefab, obatContent.transform);
                if(category.show_at_home == "Y")
                {
                    GameObject instantiated = Instantiate(mainmenuPrefab, mainMenuContent.transform);
                    instantiated.GetComponent<CategoryManagement>().textDisplay.text = category.nama;
                    StartCoroutine(getTexture(instantiated, category.image));
                }
                instantiate.GetComponent<CategoryManagement>().textDisplay.text = category.nama;
                StartCoroutine(getTexture(instantiate, category.image));
            }
        }
    }


    IEnumerator getTexture(GameObject instantiate, string fetchUrl)
    {
        WWW www =  new WWW("https://adm.gotc.app/uploads/product_categories/" + fetchUrl);
        yield return www;
        if(www.error == null)
        {
            Texture2D spriteDownload = www.texture;
            Sprite spriteObject = Sprite.Create(spriteDownload, new Rect(0, 0, spriteDownload.width, spriteDownload.height), new Vector2(0f, 0f), 100f);
            instantiate.GetComponent<CategoryManagement>().fetchImage.sprite = spriteObject;

        }
        else
        {
            Debug.Log(www.error);
        }
        yield return null;
    }

    IEnumerator loadingJsonValue(string url)
    {
        WWW wWW = new WWW(url);
        yield return wWW;
        if(wWW.error == null)
        {
            LoadToModel(wWW.text);
        }
        else
        {
            Debug.Log(wWW.error);
        }
    }

    void LoadToModel(string res)
    {
       allCategory = JsonUtility.FromJson<CategoryList>(res).category;
        dataFethced = true;
    }
}
