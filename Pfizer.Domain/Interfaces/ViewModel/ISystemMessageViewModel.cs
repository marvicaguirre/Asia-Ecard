using System;

namespace Pfizer.Domain.Interfaces.ViewModel
{
    public interface ISystemMessageViewModel
    {
        Guid SystemMessageId { get; set; }
        string Code { get; set; }
        string Message { get; set; }
    }
}
