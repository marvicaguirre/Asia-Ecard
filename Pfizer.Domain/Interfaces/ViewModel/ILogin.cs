using System;


namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface ILogin
    {
        string UserName { get; set; }
        string Password { get; set; }

    }
}
