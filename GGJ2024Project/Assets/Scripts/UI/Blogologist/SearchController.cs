using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchController : MonoBehaviour
{
    public TMP_InputField searchField;

    public ArticlesAssets articles;

    // Start is called before the first frame update
    void Start()
    {
        List<string> test = new List<string>();
        test.Add("test");
        test.Add("player");
        searchField.onSubmit.AddListener(val => { print(articles.GetArticlesByTags(test).Count); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSearch()
    {

    }
}
