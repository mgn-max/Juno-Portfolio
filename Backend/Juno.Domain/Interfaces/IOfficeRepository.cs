using Juno.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juno.Domain.Interfaces
{
    public interface IOfficeRepository
    {
        #region [Office Management]
        Task Add(Office office);
        Task SaveChangesAsync();
        #endregion

        #region [Getters]
        Task<Office?> GetById(Guid id);
        #endregion

        #region [Existence Checks]
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByDocumentNumber(string documentNumber);
        Task<bool> ExistsByDocumentNumberExceptId(string documentNumber, Guid id);
        Task<bool> ExistsByEmailExceptId(string email, Guid id);
        #endregion
    }
}
