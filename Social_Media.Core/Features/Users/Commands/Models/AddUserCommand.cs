using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string User_FirstName_In_English { get; set; }
        public string? User_LastName_In_English { get; set; }
        public string User_FirstName_In_Arabic { get; set; }
        public string? User_LastName_In_Arabic { get; set; }
        [EmailAddress]
        public string User_Email { get; set; }
        [Phone]
        public string? User_Mobile { get; set; }
        public GenderType User_Gender { get; set; }
        public DateTime User_BirthDate { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public IFormFile? User_Picture { get; set; }
        public AddUserCommand()
        {

        }
        public AddUserCommand(string User_FirstName_In_English, string? User_LastName_In_English, string User_FirstName_In_Arabic,
            string? User_LastName_In_Arabic, string User_Email, string? User_Mobile, GenderType User_Gender, DateTime User_BirthDate, string Password,
            string ConfirmPassword, IFormFile? User_Picture)
        {
            this.User_Picture = User_Picture;
            this.User_Gender = User_Gender;
            this.User_FirstName_In_Arabic = User_FirstName_In_Arabic;
            this.User_LastName_In_Arabic = User_LastName_In_Arabic;
            this.User_Email = User_Email;
            this.User_Mobile = User_Mobile;
            this.User_FirstName_In_English = User_FirstName_In_English;
            this.User_LastName_In_English = User_LastName_In_Arabic;
            this.Password = Password;
            this.ConfirmPassword = ConfirmPassword;
            this.User_BirthDate = User_BirthDate;
        }
    }
}
