using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchResultController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public ArticleContent linkedArticle;

    public void Init(string titleContent, string descriptionContent, ArticleContent article)
    {
        title.text = titleContent;
        description.text = descriptionContent;
        linkedArticle = article;
    }

    public void OnButtonClicked()
    {
        SearchController.Instance.DisplayArticle(linkedArticle);
    }
}
