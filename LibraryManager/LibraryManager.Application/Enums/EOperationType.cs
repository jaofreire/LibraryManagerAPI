using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Enums
{
    public enum EOperationType
    {
        Create = 0,
        CreateMany = 1,
        Get = 2,
        GetById = 3,
        GetByName = 4,
        GetByCategory = 5,
        GetByCategories = 6,
        GetByAuthor = 7,
        GetByAuthors = 8,
        Update = 9,
        Delete = 10,
        DeleteMany = 11,
        ValidateCredentials = 12
    }
}
