namespace RazorDemo.Dtos.UserDtos.RequestDto
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
