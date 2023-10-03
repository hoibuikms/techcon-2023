using System;

namespace Common.Entites
{
    public interface ICreatedEntity
    {
        DateTime? CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}