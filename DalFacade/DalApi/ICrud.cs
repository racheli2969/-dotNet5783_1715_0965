using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

public interface ICrud<T>
{
    public int Add(T item);
    public void Delete(int id);
    public void Update(T item);
    public T GetById(int id);
    public IEnumerable<T> GetAll();

}

