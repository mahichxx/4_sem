// DAL_Celebrity_MSSQL/Entities.cs
namespace DAL_Celebrity_MSSQL
{
    public class Celebrity  //  Знаменитость  
    {
        public Celebrity()
        {
            FullName = string.Empty;
            Nationality = string.Empty;
        }

        public int Id { get; set; }                        // Id Знаменитости        
        public string FullName { get; set; }               // полное имя   Знаменитости
        public string Nationality { get; set; }            // гражданство  (2 символа ISO)
        public string? ReqPhotoPath { get; set; }          // request path  Фотографии   

        public virtual bool Update(Celebrity celebrity)    // вспомогательный метод  
        {
            if (!string.IsNullOrEmpty(celebrity.FullName)) FullName = celebrity.FullName;
            if (!string.IsNullOrEmpty(celebrity.Nationality)) Nationality = celebrity.Nationality;
            if (!string.IsNullOrEmpty(celebrity.ReqPhotoPath)) ReqPhotoPath = celebrity.ReqPhotoPath;
            return true;
        }
    }

    public class Lifeevent  //  Событие в жизни знаменитости 
    {
        public Lifeevent()
        {
            Description = string.Empty;
        }

        public int Id { get; set; }              // Id События  
        public int CelebrityId { get; set; }     // Id Знаменитости
        public DateTime? Date { get; set; }      // дата События 
        public string Description { get; set; }  // описание События 
        public string? ReqPhotoPath { get; set; }// request path  Фотографии

        public virtual bool Update(Lifeevent lifeevent)    // вспомогательный метод
        {
            if (lifeevent.CelebrityId > 0) CelebrityId = lifeevent.CelebrityId;
            if (lifeevent.Date.HasValue && lifeevent.Date.Value != default) Date = lifeevent.Date;
            if (!string.IsNullOrEmpty(lifeevent.Description)) Description = lifeevent.Description;
            if (!string.IsNullOrEmpty(lifeevent.ReqPhotoPath)) ReqPhotoPath = lifeevent.ReqPhotoPath;
            return true;
        }
    }
}
