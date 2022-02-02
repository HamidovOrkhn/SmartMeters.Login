namespace SmartMeterControl.Access_MS.Models.User
{
    public class Department:BaseModel
    {
        public string Title { get; set; }
        public int ParentId { get; set; } = 0;
    }
}
