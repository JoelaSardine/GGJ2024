using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArticleContent
{
    public string title;
    public string author;
    [TextArea]
    public string searchDescription;
    [TextArea]
    public string description;
    [TextArea]
    public string culture;
    [TextArea]
    public string trivia;
    [TextArea]
    public string tags;
}

[CreateAssetMenu(fileName = "ArticlesAsset", menuName = "ScriptableObjects/ArticlesAsset", order = 1)]
public class ArticlesAssets : ScriptableObject
{
    [SerializeField]
    public List<ArticleContent> raceArticles;
    public List<ArticleContent> cultureArticles;

    public List<ArticleContent> GetArticlesByTags(List<string> tags)
    {
        List<ArticleContent> result = new List<ArticleContent>();

        foreach (ArticleContent a in raceArticles)
        {
            bool isOk = true;
            foreach (string t in tags)
            {
                if (a.description.Contains(t) || a.culture.Contains(t) || a.trivia.Contains(t))
                {
                    
                }
                else
                {
                    isOk = false;
                    break;
                }

                if (isOk == false) break;
            }

            if (isOk)
            {
                result.Add(a);

                if (result.Count >= 5)
                    break;
            }
        }

        foreach (ArticleContent a in cultureArticles)
        {
            bool isOk = true;
            foreach (string t in tags)
            {
                if (a.description.Contains(t) || a.culture.Contains(t) || a.trivia.Contains(t))
                {

                }
                else
                {
                    isOk = false;
                    break;
                }

                if (isOk == false) break;
            }

            if (isOk)
            {
                result.Add(a);

                if (result.Count >= 5)
                    break;
            }
        }

        return result;
    }
}
