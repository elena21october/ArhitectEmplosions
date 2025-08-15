using Newtonsoft.Json;

namespace ArhitectEmplosions.Models.Poke
{
    public class EmotionForm
    {
        public EmotionForm() 
        {
            Title = string.Empty;
            Description = string.Empty;
            ImageList = string.Empty;
            Positive = 0;
            Neutral = 0;
            Negative = 0;
            Type = string.Empty;
            Folder = string.Empty;
        }

        public EmotionForm(string title, string description, string imageList, string folder)
        {
            Title = title;
            Description = description;
            ImageList = imageList;
            Positive = 0;
            Neutral = 0;
            Negative = 0;
            Type = string.Empty;
            Folder = folder;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ImageList { get; set; }
        public string Folder { get; set; }
        public int Positive { get; set; }
        public int Neutral { get; set; }
        public int Negative { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class Zabeyka : EmotionForm
    {
        public Zabeyka(EmotionForm emotionForm, string authorName) 
        { 
            Id = emotionForm.Id;
            Title = emotionForm.Title;
            ImageList = emotionForm.ImageList;
            Description = emotionForm.Description;
            CreationDate = emotionForm.CreationDate;
            Positive = emotionForm.Positive;
            Negative = emotionForm.Negative;
            ImagePaths = JsonConvert.DeserializeObject<Dictionary<string, Picture>>(emotionForm.ImageList)!;
            AuthorName = authorName;
        }

        public void SetMainPic()
        {
            MainPicture = ImagePaths.FirstOrDefault().Value;
        }

        public Picture? MainPicture { get; set; }
        public Dictionary<string, Picture> ImagePaths { get; set; }
        public string AuthorName { get; set; }
    }
    public class EmotionMuralView : EmotionForm
    {
        public EmotionMuralView(EmotionForm emotionForm, string authorName) 
        { 
            Id = emotionForm.Id;
            Title = emotionForm.Title;
            ImageList = emotionForm.ImageList;
            Description = emotionForm.Description;
            CreationDate = emotionForm.CreationDate;
            Positive = emotionForm.Positive;
            Negative = emotionForm.Negative;
            ImagePaths = JsonConvert.DeserializeObject<List<Picture>>(emotionForm.ImageList)!;
            AuthorName = authorName;
        }
        public void SetMainPic()
        {
            MainPicture = ImagePaths.FirstOrDefault();
        }

        public Picture? MainPicture { get; set; }
        public List<Picture> ImagePaths { get; set; }
        public string AuthorName { get; set; }
    }
}
