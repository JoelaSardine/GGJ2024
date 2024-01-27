using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchController : SingletonMono<SearchController>
{
    public int maxSearchResult;
    public ArticlesAssets articles;

    [Header("TopBar")]
    public TMP_InputField searchField;

    [Header("Side bar")]
    public Button favoriteButton;

    [Header("Search Result")]
    public RectTransform searchResultParent;
    public List<SearchResultController> searchResult;
    public SearchResultController resultPrefab;
    public VerticalLayoutGroup layout;

    [Header("Opened article")]
    public TextMeshProUGUI articleTitle;
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

        StartSearch("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSearch(string fieldContent)
    {
        List<string> tags = new List<string>();

        fieldContent.Replace(' ', ',');
        string[] splited = fieldContent.Split(',');
        
        foreach(string s in splited)
        {
            if(s != "" && s != " " && !tags.Contains(s))
            {
                tags.Add(s);
            }
        }

        print(articles.GetArticlesByTags(tags).Count);

        DisplayResult(articles.GetArticlesByTags(tags));
    }
    
    public void DisplayResult(List<ArticleContent> articles)
    {
        
        for (int i =0; i < maxSearchResult; i++)
        {
            if( i < articles.Count)
            {
                searchResult[i].gameObject.SetActive(true);
                searchResult[i].title.text = articles[i].title;
                searchResult[i].description.text = articles[i].searchDescription;
                searchResult[i].linkedArticle = articles[i];
            }
            else
            {
                searchResult[i].gameObject.SetActive(false);
            }
            
        }

        layout.enabled = false;
        layout.enabled = true;
    }

    public void DisplayArticle(ArticleContent article)
    {
        articleTitle.text = article.title;
        if(article.description != "")
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
}
