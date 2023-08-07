using System;

namespace PlayPal.ModelDTO
{
    public interface ICoreUserDTO
    {
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        string Email { get; set; }
        Guid Id { get; set; }
        bool IsActive { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}