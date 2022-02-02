namespace SmartMeterControl.Access_MS.Models.User
{
    public class User:BaseModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RToken { get; set; }
        public int DivisionId { get; set; }
        public int SubjectId { get; set; }
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
