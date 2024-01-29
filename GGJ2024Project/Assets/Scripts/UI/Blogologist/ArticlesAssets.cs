using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Linq;

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
    [TextArea]
    public string features;
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
            bool isOk = false;
            int occurences = 0;
            foreach (string t in tags)
            {
                if (a.title.ToLowerInvariant().Contains(t))
                {
                    isOk = true;
                    occurences++;
                }
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
            bool isOk = false;
            foreach (string t in tags)
            {
                if (a.title.ToLowerInvariant().Contains(t))
                {
                    isOk = true;
                }
            }

            if (isOk)
            {
                result.Add(a);

                if (result.Count >= 5)
                    break;
            }
        }

        foreach (ArticleContent a in raceArticles)
        {
            bool isOk = true;
            foreach (string t in tags)
            {
                if (a.description.ToLowerInvariant().Contains(t) || a.culture.ToLowerInvariant().Contains(t) 
                    || a.trivia.ToLowerInvariant().Contains(t) || a.tags.ToLowerInvariant().Contains(t))
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
                if (a.description.ToLowerInvariant().Contains(t) || a.culture.ToLowerInvariant().Contains(t) 
                    || a.trivia.ToLowerInvariant().Contains(t) || a.tags.ToLowerInvariant().Contains(t))
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

    public List<KeyValuePair<ArticleContent, int>> GetArticlesRankedByTags(List<string> tags)
    {
        Dictionary<ArticleContent, int> result = new Dictionary<ArticleContent, int>();

        foreach (ArticleContent a in raceArticles)
        {
            int occurences = 0;
            foreach (string t in tags)
            {
                if (a.title.ToLowerInvariant().Contains(t) || a.description.ToLowerInvariant().Contains(t) || a.culture.ToLowerInvariant().Contains(t)
                    || a.trivia.ToLowerInvariant().Contains(t) || a.tags.ToLowerInvariant().Contains(t) || a.features.ToLowerInvariant().Contains(t))
                {
                    occurences++;
                }
            }

            if(occurences > 0)
            result.Add(a, occurences);
        }

        foreach (ArticleContent a in cultureArticles)
        {
            int occurences = 0;
            foreach (string t in tags)
            {
                if (a.title.ToLowerInvariant().Contains(t) || a.description.ToLowerInvariant().Contains(t) || a.culture.ToLowerInvariant().Contains(t)
                    || a.trivia.ToLowerInvariant().Contains(t) || a.tags.ToLowerInvariant().Contains(t) || a.features.ToLowerInvariant().Contains(t))
                {
                    occurences++;
                }
            }

            if (occurences > 0)
                result.Add(a, occurences);
        }

        List<KeyValuePair<ArticleContent, int>> resultSorted = result.ToList();
        resultSorted.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        if(resultSorted.Count > 5)
            resultSorted.RemoveRange(5, resultSorted.Count - 5);

        return resultSorted;
    }

    public void UpdateRacesArticles()
    {
        raceArticles.Clear();

        string xmlFilePath = "Assets/Settings/ArticleRacesData.xml";

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // Get the root element
        XmlElement root = xmlDoc.DocumentElement;

        // Iterate through child nodes or perform other operations
        foreach (XmlNode node in root.ChildNodes)
        {
            ArticleContent newArticle = new ArticleContent();
            newArticle.title = node.Attributes["Title"].Value;
            newArticle.features = node.Attributes["Features"].Value;
            newArticle.author = node.Attributes["Author"].Value;
            newArticle.searchDescription = node.Attributes["Summary"].Value;
            newArticle.description = node.Attributes["Description"].Value;
            newArticle.culture = node.Attributes["Culture"].Value;
            newArticle.trivia = node.Attributes["Trivia"].Value;
            newArticle.tags = node.Attributes["Tags"].Value;

            raceArticles.Add(newArticle);
        }
    }

    public void UpdateCultureArticles()
    {
        cultureArticles.Clear();

        string xmlFilePath = "Assets/Settings/ArticleCulturesData.xml";

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFilePath);

        // Get the root element
        XmlElement root = xmlDoc.DocumentElement;

        // Iterate through child nodes or perform other operations
        foreach (XmlNode node in root.ChildNodes)
        {
            ArticleContent newArticle = new ArticleContent();
            newArticle.title = node.Attributes["Title"].Value;
            newArticle.searchDescription = node.Attributes["Summary"].Value;
            newArticle.author = node.Attributes["Author"].Value;
            newArticle.description = node.Attributes["Description"].Value;
            newArticle.culture = node.Attributes["Culture"].Value;
            newArticle.trivia = node.Attributes["Trivia"].Value;
            newArticle.tags = node.Attributes["Tags"].Value;
            newArticle.features = node.Attributes["Features"].Value;

            cultureArticles.Add(newArticle);
        }
    }
}
