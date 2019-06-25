using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.DAL.Contracts
{
    public interface IBaseRepositoy<TEntity>
        where TEntity:class

    {
        void Create(TEntity obj);
        void Update(TEntity obj);
        void Remove(int id);

        List<TEntity> GetAll();
        TEntity GetById(int id);


    }
}
