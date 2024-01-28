using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
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
            newArticle.title = node.Name;
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
            newArticle.title = node.Name;
            newArticle.author = node.Attributes["Author"].Value;
            newArticle.description = node.Attributes["Description"].Value;
            newArticle.culture = node.Attributes["Culture"].Value;
            newArticle.trivia = node.Attributes["Trivia"].Value;
            newArticle.tags = node.Attributes["Tags"].Value;

            cultureArticles.Add(newArticle);
        }
    }
}
