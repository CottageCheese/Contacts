using System;
using System.Collections.Generic;
using System.Linq;
using StaffData;

namespace DataAccess.Staff
{
  public class StaffDal : IStaff
  {
    private static readonly List<StaffEntity> _staffTable = new List<StaffEntity>
    { 
      new StaffEntity { Id = 1, Firstname = "Faiz", Surname = "Manjra"},
      new StaffEntity { Id = 2, Firstname = "Neil", Surname = "Mawson"},
      new StaffEntity { Id = 3, Firstname = "David", Surname = "Furey"},
      new StaffEntity { Id = 4, Firstname = "Jonathan", Surname = "Smith"},
      new StaffEntity { Id = 5, Firstname = "John", Surname = "Bown"},
    };

    public bool Delete(int id)
    {
      var person = _staffTable.FirstOrDefault(p => p.Id == id);
      if (person != null)
      {
        lock (_staffTable)
          _staffTable.Remove(person);
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool Exists(int id)
    {
      var person = _staffTable.FirstOrDefault(p => p.Id == id);
      return person != null;
    }

    public StaffEntity Get(int id)
    {
      var person = _staffTable.FirstOrDefault(p => p.Id == id);
      if (person != null)
        return person;
      else
        throw new KeyNotFoundException($"Id {id}");
    }

    public List<StaffEntity> Get()
    {
      // return projection of entire list
      return _staffTable.Where(r => true).ToList();
    }

    public StaffEntity Insert(StaffEntity staff)
    {
      if (Exists(staff.Id))
        throw new InvalidOperationException($"Key exists {staff.Id}");
      lock (_staffTable)
      {
        int lastId = _staffTable.Max(m => m.Id);
        staff.Id = ++lastId;
        _staffTable.Add(staff);
      }
      return staff;
    }

    public StaffEntity Update(StaffEntity staff)
    {
      lock (_staffTable)
      {
        var old = Get(staff.Id);
        old.Firstname = staff.Firstname;
        old.Surname = staff.Surname;
        return old;
      }
    }
  }
}
