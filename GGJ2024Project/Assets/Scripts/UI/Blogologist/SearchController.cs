using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchController : SingletonMono<SearchController>
{
    public int maxSearchResult;
    public ArticlesAssets articles;
    public ArticleContent openedArticle;
    public ArticleContent favArticle;

    public Sprite[] favSprites;

    [Header("TopBar")]
    public TMP_InputField searchField;

    [Header("Side bar")]
    public Button favoriteButton;

    [Header("Search Result")]
    public RectTransform searchResultParent;
    public List<SearchResultController> searchResult;
    public Button addToFavButton;
    public SearchResultController resultPrefab;
    public VerticalLayoutGroup layout;

    [Header("Opened article")]
    public TextMeshProUGUI articleTitle;
    public TextMeshProUGUI author;
    public TextMeshProUGUI descriptionTitle;
    public TextMeshProUGUI descriptionBody;
    public TextMeshProUGUI cultureTitle;
    public TextMeshProUGUI cultureBody;
    public TextMeshProUGUI triviaTitle;
    public TextMeshProUGUI triviaBody;

    // Start is called before the first frame update
    void Start()
    {
        searchField.onSubmit.AddListener(val => { StartSearch(searchField.text);});

        for(int i = 0; i< maxSearchResult; i++)
        {
            SearchResultController newresult = Instantiate(resultPrefab, searchResultParent);
            searchResult.Add(newresult);
        }

        StartSearch("race");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSearch(string fieldContent)
    {
        List<string> tags = new List<string>();

        fieldContent.Replace(',', ' ');
        fieldContent.ToLowerInvariant();
        string[] splited = fieldContent.Split(' ');
        
        foreach(string s in splited)
        {
            if(s != "" && s != " " && !tags.Contains(s))
            {
                tags.Add(s.ToLowerInvariant());
            }
        }

        DisplayResult(articles.GetArticlesRankedByTags(tags));
    }
    
    public void DisplayResult(List<KeyValuePair<ArticleContent, int>> articles)
    {
        for (int i =0; i < maxSearchResult; i++)
        {
            if( i < articles.Count)
            {
                searchResult[i].gameObject.SetActive(true);
                searchResult[i].title.text = articles[i].Key.title;
                searchResult[i].description.text = articles[i].Key.searchDescription;
                searchResult[i].linkedArticle = articles[i].Key;
            }
            else
            {
                searchResult[i].gameObject.SetActive(false);
            }
            
        }

        //layout.enabled = false;
        DOVirtual.DelayedCall(0.05f, () =>
        {
            float totalSize = 100f;
            foreach (SearchResultController s in searchResult)
            {
                if(s.gameObject.activeSelf)
                    totalSize += s.GetComponent<RectTransform>().sizeDelta.y;
          
            }

            searchResultParent.sizeDelta = new Vector2(searchResultParent.sizeDelta.x, totalSize);
            searchResultParent.anchoredPosition = new Vector2(searchResultParent.anchoredPosition.x, -totalSize / 2f);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
        });
    }

    public void DisplayArticle(ArticleContent article)
    {
        openedArticle = article;

        articleTitle.text = article.title;
        author.text = "Author : " + ( article.author == "" ? "Unknown" : article.author);

        if(favArticle == article)
        {
            addToFavButton.image.sprite = favSprites[1];
        }
        else
        {
            addToFavButton.image.sprite = favSprites[0];
        }

        if (article.description != "")
        {
            descriptionTitle.gameObject.SetActive(true);
            descriptionBody.gameObject.SetActive(true);
            descriptionBody.text = article.description;
        }
        else
        {
            descriptionTitle.gameObject.SetActive(false);
            descriptionBody.gameObject.SetActive(false);
        }

        if (article.culture != "")
        {
            cultureTitle.gameObject.SetActive(true);
            cultureBody.gameObject.SetActive(true);
            cultureBody.text = article.culture;
        }
        else
        {
            cultureTitle.gameObject.SetActive(false);
            cultureBody.gameObject.SetActive(false);
        }

        if (article.trivia != "")
        {
            triviaTitle.gameObject.SetActive(true);
            triviaBody.gameObject.SetActive(true);
            triviaBody.text = article.trivia;
        }
        else
        {
            triviaTitle.gameObject.SetActive(false);
            triviaBody.gameObject.SetActive(false);
        }
    }

    public void FavoriteArticle()
    {
        if (openedArticle == null)
            return;
        favArticle = openedArticle;
        favoriteButton.image.sprite = favSprites[1];
        addToFavButton.image.sprite = favSprites[1];
    }

    public void OpenFavorite()
    {
        DisplayArticle(favArticle);
    }

    public void ClearFavorite()
    {
        favArticle = null;
        favoriteButton.image.sprite = favSprites[0];
    }
}
