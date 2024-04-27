using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.TableCreation;

namespace AuthenticationAPI.Models
{
    public class AuthenticationResponse
    {
        public string? FullName { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
        public AuthenticationResponse(User userDetail, string token)
        {
            Token = token;
            FullName = userDetail.FirstName + ". " + userDetail.LastName;
            UserId = userDetail.UserId;
        }
        //Output Json sent to the Client {"FullName" :"xxx" ,"UserId": "99", "Token": "ENCTOKEN"}
    }
}
