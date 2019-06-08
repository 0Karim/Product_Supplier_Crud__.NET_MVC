using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Supp_DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);

        bool Update(TEntity entity);

        bool DeleteByID(int ID);

        TEntity SelectByID(int ID);

        List<TEntity> SelectAll();

    }
}
