//Задача 4 - Журнал + статьи + массив журналов
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;


List<Article> list = new List<Article>(3);
list.Add(new Article("Surjery", "scientic article", 5));
list.Add(new Article("Flue and illness", "researching of flue cure", 10));
list.Add(new Article("Stomac ache", "Benefits of fruits in the diet", 12));


List<Journal> library = new List<Journal>(2);
library.Add( new Journal("The Lancet", "ElSeiver publishing", 1823, 120, list));
library.Add( new Journal("Electrotechnic and Computer Systems","ONPU", 1965, 100, new List<Article>()));
library[1].ArticleList.Add(new Article("Power networks", "Researching of reliability of power networks", 7));
library[1].ArticleList.Add(new Article("Transformers and autotransformers", "Development transes with reduced losses", 12));

Console.WriteLine("Initial array of Journals");
foreach (Journal j in library)
    Console.Write(j);

using (FileStream file = new FileStream("journals.json", FileMode.OpenOrCreate))
{
    DataContractJsonSerializer journalSaver = new DataContractJsonSerializer(typeof(List<Journal>));
    journalSaver.WriteObject(file, library);
}

List<Journal> ? LoadedJournalList;
using (FileStream file = new FileStream("journals.json", FileMode.Open))
{
    DataContractJsonSerializer journalSaver = new DataContractJsonSerializer(typeof(List<Journal>));
    LoadedJournalList = (List<Journal>?)journalSaver.ReadObject(file);
    Console.WriteLine("\nDeserialized journals");
   
    foreach (Journal j in LoadedJournalList)
        Console.Write(j);
}


[Serializable]
[DataContract]
public class Journal
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Publisher { get; set; }

    [DataMember]
    public uint Date { get; set; }
    [DataMember]
    public uint Pages { get; set; }
    [DataMember]
    public List<Article> ArticleList { get; set; }

    public Journal(string name, string publisher, uint date, uint pages, List<Article> article)
    {
        Name = name;
        Publisher = publisher;
        Date = date;
        Pages = pages;
        ArticleList = article;
    }
    public override string ToString()
    {
        StringBuilder temp = new StringBuilder(255);
        temp.Append($"\"{Name ?? "NoData"}\".{Publisher ?? "NoData"} - {Date} - {Pages}p\n");
        temp.Append("Articles of the Journal:");
        foreach (Article a in ArticleList)
            temp.Append(a.ToString() + '\n');
        return temp.ToString() + '\n';
    }
}

public class Article
{
    public string Title { get; set; }
    public string Description { get; set; }
    public uint Pages { get; set; }

    public Article()
    {
        Title = "";
        Description = "";
        Pages = 0;
    }
    public Article(string title, string description, uint pages)
    {
        Title = title;
        Description = description;
        Pages = pages;
    }
    public override string ToString()
    {
        return $"\"{Title}\" ({Description}), {Pages}p.";
    }
}